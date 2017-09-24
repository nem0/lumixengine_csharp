using System;
using ClangSharp;
using System.Linq;
namespace LumixBindings
{
    public class Method : Collector<Argument>, IABObject
    {
        CXCursor returnCursor_;
        TypeMap returnType_;
        public CXCursor Cursor
        {
            get;
            private set;
        }
        public bool IsVoid
        {
            get { return ReturnValue == "void"; }
        }

        public bool IsStatic
        {
            get { return clang.CXXMethod_isStatic(Cursor) != 0; }
        }

        public bool IsPublic
        {
            get { return clang.getCXXAccessSpecifier(Cursor) == CX_CXXAccessSpecifier.CX_CXXPublic; }
        }

        public override string Name
        {
            get { return clang.getCursorSpelling(Cursor).ToString(); }
        }

        public TypeMap ReturnTypemap
        {
            get
            {
               // if (!IsVoid)
                {
                    var type = clang.getCursorType(Cursor);
                    return new TypeMap(type,clang.getCanonicalType(clang.getCursorType(Cursor)));
                }
              //  return new TypeMap(null, null);
            }
        }
        public bool IsTemplate
        {
            get { return clang.Cursor_getNumTemplateArguments(Cursor) > 0; }
        }
        public bool IsReturnSomething
        {
            get { return IsConstructor || !IsVoid; }
        }
        public bool IsOverloaded
        {
            get { return clang.getNumOverloadedDecls(Cursor) > 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string XMLComment
        {
            get
            {
                
                var comment = clang.Cursor_getRawCommentText(Cursor).ToString();
                if(comment == null)
                {
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
                    for(int k =0; k < numToken;k++)
                    {
                        CXToken token = (CXToken)System.Runtime.InteropServices.Marshal.PtrToStructure(current, typeof(CXToken));
                        CXTokenKind kind = clang.getTokenKind(token);
                        if (kind == CXTokenKind.CXToken_Comment)
                        {
                            comment = clang.getTokenSpelling(TU, token).ToString();
                            break;
                        }
                        current = new System.IntPtr(tokePtr.ToInt64() + ((k+1)*TokenSize));
                    }
                    if(comment == null)
                    return "\t\t/// <summary>\n" +
                     "\t\t///\n" +
                     "\t\t/// </summary>\n";
                }
                string[] lines = comment.Split('\n');
                comment = "\t\t/// <summary>\n";
                foreach(var line in lines)
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
        public string ReturnValue
        {
            //get { return returnCursor_.data1 == IntPtr.Zero ? "void" : clang.getCursorSpelling(returnCursor_).ToString().Replace("class ", ""); }
            get
            {
                var type = clang.getCursorType(Cursor);
                var rType = clang.getResultType(type);
                return rType.ToString();
            }
        }

        public bool HasParameter
        {
            get { return Values.Length > 0; }
        }
        public bool IsConstructor
        {
            get { return Cursor.kind == CXCursorKind.CXCursor_Constructor; }
        }

        public bool IsAbstract
        {
            get { return clang.CXXMethod_isPureVirtual(Cursor) != 0; }
        }
        public Method(CXCursor cursor,CXTranslationUnit unit) : base(unit)
        {
            Cursor = cursor;
            if(Name == "SetNextTimeStep")
            {

            }
        }
        public string CSharpPITemplateLumix
        {
            get
            {
                return "\t\t [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]\n" +
                         "\t\tinternal static extern {1} {0}({2});";
            }
        }

        public string CSharpPITemplate
        {
            get
            {
                return "\t\t[global::System.Runtime.InteropServices.DllImport(\"{0}\", EntryPoint = \"{1}\")]\n" +
                         "\t\tinternal static extern {2} {1}({3});";
            }
        }
        public string CSharpFuncDecl
        {
            get
            {
                return "{4}" +
                        "\t\tpublic {5} {0} {1}({2})\n" +
                       "\t\t{{" +
                       "\t\t\t{3}\n" +
                       "\t\t}}";

            }
        }
        public string[] GetPropertyParts
        {
            get
            {
                var split = string.Concat(Name.Select(c => char.IsUpper(c) ? " " + c.ToString() : c.ToString())).TrimStart().Split(' ');
                if (split.Length > 3)
                {
                    var ret = new string[3];
                    ret[0] = split[0];
                    ret[1] = split[1];
                    for (int k = 2; k < split.Length; k++)
                    {
                        ret[2] += split[k];
                    }
                    return ret;
                }

                return split;

            }
        }
        public string CSharpFuncDeclBase
        {
            get
            {
                return "{4}" +
                        "\t\tpublic {0} {1}({2})\n" +
                        "\t\t\t:base({3})\n"+
                       "\t\t{{\n" +
                       "\t\t}}";

            }
        }
        bool lastDLLImport;
        CXCursor lastParam;
        public override CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
        {
            if(Name == "setControllerInput")
            {

            }
            string spelling = cursor.ToSharpString();
            var parentS = parent.ToSharpString();
            var t = clang.getCursorType(cursor);
            var canonicalStr = clang.getTypeSpelling(clang.getCanonicalType(t)).ToString();
            if (cursor.IsInSystemHeader())
                return CXChildVisitResult.CXChildVisit_Continue;

            if (cursor.kind == CXCursorKind.CXCursor_DLLImport)
            {
                lastDLLImport = true;
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            else if(cursor.kind == CXCursorKind.CXCursor_TypeRef && lastDLLImport)
            {
                returnCursor_ = cursor;
                lastDLLImport = false;
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            else if(cursor.kind == CXCursorKind.CXCursor_ParmDecl)
            {
                lastParam = cursor;
                lastDLLImport = false;
                if(t.IsBasicType() || canonicalStr == "const char *")
                {
                    Argument arg = new Argument(lastParam, cursor, TU);
                    Add(arg);
                }
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            else if(cursor.kind == CXCursorKind.CXCursor_TypeRef && lastParam.kind == CXCursorKind.CXCursor_ParmDecl)
            {
                Argument arg = new Argument(lastParam, cursor, TU);
                Add(arg);
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            //Console.Write(Name + ": " + Enum.GetName(typeof(CXCursorKind), cursor.kind) + " ");
            //Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
            return CXChildVisitResult.CXChildVisit_Continue;
        }
        public override void Add(Argument obj)
        {
            var item = knownObjects_.Find(x => x.Name == obj.Name);
            if (item == null)
                base.Add(obj);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
