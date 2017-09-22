using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClangSharp;

namespace LumixBindings
{
    public class EnumValue : IABObject, ICursorVisitor
    {
        TypeMap typeMap_;
        CXType type_;
        CXCursor parent_;
        Dictionary<string, int> values_ = new Dictionary<string, int>();
        public Dictionary<string, int> Values
        {
            get { return values_; }
        }
        public string Name
        {
            get { return Cursor.ToSharpString(); }
        }

        public string NativeType
        {
            get
            {
                return clang.getTypeSpelling(type_).ToString();
            }
        }
        public bool IsStruct
        {
            get { return NativeType.Contains("struct"); }
        }
        public string FullyQualyfiedType
        {
            get
            {
                if (typeMap_.CanonicalSTR == "const char *")
                    return "const char";
                var typeStr = "Lumix::" + NativeType.Replace("class", "").Replace("Lumix::", "").Replace("struct", "").Replace("enum", "").Trim();
                if (TypeMap.IsVector)
                {
                    return string.Format(TypeMap.CanonicalSTR.Contains("POD") ? "Atomic::PODVector<{0}>" : "Atomic::Vector<{0}>", typeStr);
                }
                else
                {
                    return typeStr;
                }
            }
        }


        public CXCursor Cursor
        {
            get;
            private set;
        }

        public TypeMap TypeMap
        {
            get { return typeMap_; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string XMLComment
        {
            get
            {

                var comment = clang.Cursor_getRawCommentText(Cursor).ToString();
                if (comment == null)
                {
                    var TU = Cursor.GetTranslationUnit();
                    CXSourceLocation location = clang.getCursorLocation(Cursor);
                    CXFile file;
                    uint line, col, offset;
                    clang.getSpellingLocation(location, out file, out line, out col, out offset);
                    var aboveStart = clang.getLocation(TU, file, line - 1, 1);
                    var aboveEnd = clang.getLocation(TU, file, line, 1);
                    IntPtr tokePtr = IntPtr.Zero;
                    uint numToken = 0;
                    var range = clang.getRange(aboveStart, aboveEnd);
                    clang.tokenize(TU, range, out tokePtr, out numToken);
                    int TokenSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CXToken));
                    IntPtr current = tokePtr;
                    for (int k = 0; k < numToken; k++)
                    {
                        CXToken token = (CXToken)System.Runtime.InteropServices.Marshal.PtrToStructure(current, typeof(CXToken));
                        CXTokenKind kind = clang.getTokenKind(token);
                        if (kind == CXTokenKind.CXToken_Comment)
                        {
                            comment = clang.getTokenSpelling(TU, token).ToString();
                            break;
                        }
                        current = new System.IntPtr(tokePtr.ToInt64() + ((k + 1) * TokenSize));
                    }
                    if (comment == null)
                        return "\t\t/// <summary>\n" +
                         "\t\t///\n" +
                         "\t\t/// </summary>\n";
                }
                string[] lines = comment.Split('\n');
                comment = "\t\t/// <summary>\n";
                foreach (var line in lines)
                {
                    comment += "\t\t///" + line.Replace("/", "");
                    comment += "\n";
                }
                if (comment == "\t\t/// <summary>\n")
                    comment += "\t\t///\n";
                comment += "\t\t/// </summary>\n";
                return comment;
            }
        }

        public EnumValue(CXCursor cursor, CXTranslationUnit tu)
        {
            type_ = clang.getCursorType(cursor);
            var canonical = clang.getCanonicalType(type_);
            Cursor = cursor;
         
            var str = Name;

            typeMap_ = new TypeMap(type_, clang.getCanonicalType(clang.getCursorType(cursor)));
        }

        public string ToCSharp()
        {
            return "";
        }

        CXCursor lastEnumName;
        public CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
        {
            if(cursor.kind == CXCursorKind.CXCursor_EnumConstantDecl)
            {
                lastEnumName = cursor;
                values_.Add(clang.getCursorSpelling(lastEnumName).ToString(), -1);
            }
            else if(cursor.kind == CXCursorKind.CXCursor_IntegerLiteral)
            {
                var tokens = cursor.Tokenize();
                int value = 0;
                var TU = cursor.GetTranslationUnit();
                foreach(var token in tokens)
                {
                    CXTokenKind kind = clang.getTokenKind(token);
                    if (kind == CXTokenKind.CXToken_Literal)
                    {
                        var spelling = clang.getTokenSpelling(TU, token).ToString();
                        //value = int.Parse(spelling);
                        int.TryParse(spelling.Replace("0x",""), out value);
                        break;
                    }
                }
                values_[clang.getCursorSpelling(lastEnumName).ToString()] = value;
            }
            //var type = clang.getCursorType(cursor);
            //Console.WriteLine("enum " + Enum.GetName(typeof(CXCursorKind), cursor.kind) + " : " + clang.getCursorSpelling(cursor));
            return CXChildVisitResult.CXChildVisit_Recurse;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj as EnumValue == null)
                return false;
            var right = obj as EnumValue;
            if (Name != right.Name)
                return false;
            foreach(var value in values_)
            {
                if (!right.values_.ContainsKey(value.Key))
                    return false;
            }
            return true;
        }
    }
}
