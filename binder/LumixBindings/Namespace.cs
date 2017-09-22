using System;
using ClangSharp;
using System.Collections.Generic;

namespace LumixBindings
{
    public class Namespace : Collector<Class>, IABObject
    {
        List<EnumValue> enums_ = new List<EnumValue>();
        public override string Name
        {
            get { return clang.getCursorSpelling(Cursor).ToString(); }
        }
        public CXCursor Cursor
        {
            get;
            private set;
        }

        public List<EnumValue> Enums
        {
            get { return enums_; }
            set { enums_ = value; }
        }
        public bool IsEmpty
        {
            get
            {
                bool ret = true;
                foreach(var cl in Values)
                {
                    foreach(var m in cl.Values)
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
        }
        public Namespace(CXCursor cursor, CXTranslationUnit unit)
            : base(unit)
        {
            Cursor = cursor;
        }

        public override CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
        {
            // Console.WriteLine(cursor.ToSharpString() + " " + Enum.GetName(typeof(CXCursorKind), cursor.kind));
            //if(cursor.kind == CXCursorKind.CXCursor_CXXMethod)
            //{
            //    var tp = clang.getCursorType(cursor);
            //    var result = clang.getCursorResultType(cursor);
            //}
            //return CXChildVisitResult.CXChildVisit_Recurse;
            if (cursor.IsInSystemHeader())
                return CXChildVisitResult.CXChildVisit_Continue;
            if (Bindings.DebugToken)
            {
                var type = clang.getCursorType(cursor);
                Console.WriteLine("namespace " + Enum.GetName(typeof(CXCursorKind), cursor.kind) + " : " + clang.getCursorSpelling(cursor));
            }
            if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
            {
                Class cl = new Class(cursor, TU, this);
                if(cl.Name == "Animation")
                {

                }
                Bindings.KnownClasses.Add(cl);
                if (Bindings.DebugToken)
                    Console.WriteLine("klass " + cl.Name);
                if (Bindings.Classes.Contains(cl.Name) || true)
                    Add(cl);
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            else if (cursor.kind == CXCursorKind.CXCursor_EnumDecl)
            {
                EnumValue ev = new EnumValue(cursor, TU);
                //var t = clang.getCursorType(cursor);
                //var tStr = clang.getTypeSpelling(t).ToString();
                //if(tStr == "Atomic::MouseMode")
                //{

                //}
                //Console.WriteLine(tStr);
                string value = clang.getCursorSpelling(cursor).ToString();
                clang.visitChildren(cursor, ev.Visit, new CXClientData(IntPtr.Zero));
                enums_.Add(ev);
            }
            return CXChildVisitResult.CXChildVisit_Recurse;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Namespace))
                return false;
            Namespace other = obj as Namespace;
            if(other!= null)
            {
                if (other.Name != Name)
                    return false;
                if (other.knownObjects_.Count != knownObjects_.Count)
                    return false;
                foreach(var kobj in other.knownObjects_)
                {
                    if (!knownObjects_.Contains(kobj))
                        return false;
                }
            }
            return true;
        }
    }
}
