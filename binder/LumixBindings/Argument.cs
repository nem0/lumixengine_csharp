using System;
using ClangSharp;

namespace LumixBindings
{
    public class Argument : IABObject
    {
        TypeMap typeMap_;
        CXCursor type_;
        public string Name
        {
            get { return Cursor.ToSharpString(); }
        }

        public string NativeType
        {
            get
            {
                return type_.ToSharpString();
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
                var typeStr = "Lumix::" + NativeType.Replace("class", "").Replace("Lumix::", "").Replace("struct","").Replace("enum","").Trim();
                if (TypeMap.IsVector)
                {
                    return string.Format(TypeMap.CanonicalSTR.Contains("POD") ? "Lumix::PODVector<{0}>" : "Lumix::Vector<{0}>", typeStr);
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
        public Argument(CXCursor cursor,CXCursor type, CXTranslationUnit tu)
        {
            var canonical = clang.getCanonicalType(clang.getCursorType(cursor));
            Cursor = cursor;
            type_ = type;
            var str = Name;

            typeMap_ = new TypeMap(clang.getCursorType(type_), clang.getCanonicalType(clang.getCursorType(cursor)));
        }

        public string ToCSharp()
        {
            return "";
        }

    }
}
