using System;
using ClangSharp;
using System.IO;
using System.Collections.Generic;
namespace LumixBindings
{
    public class Class : Collector<Method>, IABObject
    {

        List<KeyValuePair<string, CXCursor>> baseClasses_ = new List<KeyValuePair<string, CXCursor>>();
        List<EnumValue> enums_ = new List<EnumValue>();
        public override string Name
        {
            get { return clang.getCursorSpelling(Cursor).ToString(); }
        }
        public string Header
        {
            get
            {
                return clang.getTranslationUnitSpelling(TU).ToString();
            }
        }
        public Namespace Namespace
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the fully qualified.
        /// </summary>
        /// <value>
        /// The name of the fully qualified.
        /// </value>
        public string FullyQualifiedName
        {
            get { return Namespace.Name + "::" + Name; }
        }
        public bool HasBaseClass
        {
            get { return baseClasses_.Count > 0; }
        }
        public bool IsAbstract
        {
            get
            {
                foreach (var meth in Values)
                    if (meth.IsAbstract)
                        return true;
                var abst = Bindings.Abstracts.Find(x => x == Name);
                return abst != null;
            }
        }

        public bool IsInterface
        {
            get;
            set;
        }

        public string CSharpClassDecl
        {
            get
            {
                string ret = ""; 
                if (IsAbstract && !IsInterface)
                    //ret += "abstract partial class";
#warning todo: make clases abstract when necessary
                    ret += "partial class";//should be abstract, we will fix that later
                else if (IsInterface)
                    ret += "interface";
                else if (!IsAbstract)
                    ret += "partial class";

                return ret;  
            }
        }

        public string BaseClass
        {
            get
            {
                string ret = "";
                if (baseClasses_.Count > 0)
                {
                    ret = baseClasses_[0].Key.Replace("struct","").Trim();
                }
                return ret;
            }
        }
        public CXCursor Cursor
        {
            get;
            private set;
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
                    return "\t/// <summary>\n" +
                     "\t///\n" +
                     "\t/// </summary>\n";
                }
                string[] lines = comment.Split('\n');
                comment = "\t/// <summary>\n";
                foreach (var line in lines)
                {
                    comment += "\t///" + line.Replace("/", "");
                    comment += "\n";
                }
                if (comment == "\t/// <summary>\n")
                    comment += "\t///\n";
                comment += "\t/// </summary>\n";
                return comment.Replace("Urho3D", "Atomic");
            }
        }
        public Class( CXCursor cursor, CXTranslationUnit unit, Namespace ns)
            : base(unit)
        {
            Cursor = cursor;
            Namespace = ns;
        }

        public override CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
        {
            if (cursor.IsInSystemHeader())
                return CXChildVisitResult.CXChildVisit_Continue;
            if(Name == "PrefabResource" && Header.Contains("prefab.h"))
            {
                var name = clang.getCursorSpelling(cursor);
            }
            if(Bindings.DebugToken)
            {
                var type = clang.getCursorType(cursor);
                Console.WriteLine("class " + Enum.GetName(typeof(CXCursorKind),cursor.kind) + " : " + clang.getCursorSpelling(cursor));
            }
            if (cursor.kind == CXCursorKind.CXCursor_CXXMethod || cursor.kind == CXCursorKind.CXCursor_FunctionDecl || cursor.kind == CXCursorKind.CXCursor_Constructor)
            {
                Method m = new Method(cursor,TU);
              
                Add(m);
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            else if (cursor.kind == CXCursorKind.CXCursor_CXXBaseSpecifier)
            {
                string bsClass = clang.getCursorSpelling(cursor).ToString().Replace("Lumix::", "").Replace("class", "").Trim();
                if (Bindings.Classes.Contains(bsClass) || true)
                    baseClasses_.Add(new KeyValuePair<string,CXCursor>(bsClass,cursor));
            }
            else if (cursor.kind == CXCursorKind.CXCursor_EnumDecl)
            {
                EnumValue ev = new EnumValue(cursor, TU);
                enums_.Add(ev);
            }
            return CXChildVisitResult.CXChildVisit_Recurse;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
