using System;
using ClangSharp;

namespace LumixBindings
{
    public class TypeMap
    {
        CXType type_;
        CXType canonical_;
        System.Text.RegularExpressions.Regex vectorPattern_ = new System.Text.RegularExpressions.Regex("(const[\\s]*)?(Atomic\\D\\D)?(Vector)?(PODVector)?<(Atomic\\D\\D)?([A-Za-z\\s\\*?]*)>(\\s*?&)?");
        public string NativeCPP
        {
            get
            {
                var str = clang.getResultType(type_).ToString();
                if (str == "")
                    return CanonicalSTR;
                return str;
            }
        }
        public string TypeSTR
        {
            get
            {

                //var pt = clang.getPointeeType(type_);
                // return pt.ToString();
                var rType = clang.getResultType(type_);
                return rType.ToString();
            }
        }
        public string NativeC
        {
            get
            {
                return GetCType(NativeCPP);
            }
        }
        public bool IsBasicType
        {
            get
            {
                return type_.IsBasicType(); }
        }

        public bool IsVectorWithPoiner
        {
            get
            {
                var vectorType = NativeCPP.Replace("Atomic::PODVector", "").Replace("Atomic::Vector", "").Replace("<", "").Replace(">", "").Replace("&", "");
                return vectorType.Contains("*");
            }
        }

        public string VectorType
        {
            get
            {
                return NativeCPP.Replace("Atomic::","").Replace("PODVector", "").Replace("Vector", "").Replace("<", "").Replace(">", "").Replace("&", "").Replace("const","");
            }
        }

        public string VectorTypeFQ
        {
            get
            {
                string ret = VectorType;
                if (!VectorType.StartsWith("Atomic::"))
                {
                    ret = "Atomic::" + ret;
                }
                return ret;
            }
        }
        public string VectorDecl
        {
            get
            {
               
                var match = vectorPattern_.Match(NativeCPP);
                if(match.Success)
                {
                    int k = 1;
                    string vec = "";
                    while(k < match.Groups.Count && !vec.ToLower().Contains("vector"))
                    {
                        vec = match.Groups[k++].ToString();
                        if (vec == NativeCPP)
                            vec = "";
                    }
                    return "Atomic::" + vec;
                   // var tp = match.Groups[6];
                }
                return "";
            }
        }

        public bool VectorIsConstRef
        {
            get
            {
                var vecType = VectorType;
                var decl = NativeCPP.Replace(VectorType, "");
                return decl.Contains("const") && decl.Contains("&") && IsVector;
            }
        }
        public string BasicTypeSTR
        {
            get
            {
                switch (type_.kind)
                {
                    case CXTypeKind.CXType_Float:
                        return "float";
                    case CXTypeKind.CXType_Int:
                        return "int";
                    case CXTypeKind.CXType_Bool:
                        return "bool";
                    case CXTypeKind.CXType_Double:
                        return "double";
                    case CXTypeKind.CXType_Long:
                        return "long";
                    case CXTypeKind.CXType_LongLong:
                        return "long long";
                    case CXTypeKind.CXType_Short:
                        return "short";
                    case CXTypeKind.CXType_UShort:
                        return "unsigned short";
                    case CXTypeKind.CXType_ULong:
                        return "unsigned long";
                    case CXTypeKind.CXType_UInt:
                        return "unsigned int";
                    case CXTypeKind.CXType_Record:
                        {
                            var rType = clang.getTypeSpelling(type_).ToString();
                            switch (rType)
                            {
                                case "Lumix::Vec2":
                                case "Lumix::Vec3":
                                case "Lumix::Vec4":
                                    return rType.Replace("Lumix::","");
                                case "Lumix::ComponentHandle":
                                    return "int";
                            }
                        }
                        throw new Exception("Not a basic type :(");
                    default:
                        throw new Exception("Not a basic type :(");
                }
            }
        }
        public bool IsPointer
        {
            get
            {
                return canonical_.kind == CXTypeKind.CXType_Pointer || NativeCPP.Replace("Atomic::", "") == "ClassID" || NativeCPP.Contains("*");
            }
        }

        public string CanonicalSTR
        {
            get { return clang.getTypeSpelling(canonical_).ToString(); }
        }
        public bool IsVector
        {
            get
            {
                var match = vectorPattern_.Match(NativeCPP);
                return match.Success && !NativeCPP.Contains("HashMap");//clang.getTypeSpelling(canonical_).ToString().ToLower().Contains("vector<"); }

            }
        }
        public bool IsConstPointer
        {
            get
            {
                return IsConst && NativeCPP.Contains("*");
            }
        }
        public bool IsConst
        {
            get { return NativeCPP.Contains("const"); }
        }

        public bool IsReference
        {
            get { return clang.Type_getCXXRefQualifier(type_) != CXRefQualifierKind.CXRefQualifier_None || NativeCPP.Contains("&"); }
        }

        public bool IsAbstract
        {
            get
            {
                var cl = NativeCPP.Replace("const", "").Replace("*", "").Replace("Atomic::", "").Replace("&", "").Trim();
                var known = Bindings.KnownClasses.Find(x => x.Name == cl);
                if (known != null)
                    return known.IsAbstract;
                return false;
            }
        }
        public TypeMap(CXType type, CXType canonical)
        {
            type_ = type;
            canonical_ = canonical;
        }

        public bool IsEnum
        {
            get { return type_.kind == CXTypeKind.CXType_Enum; }
        }
        public bool NeedCCast
        {
            get
            {
                var val = NativeCPP.Replace("&", "").Replace("const", "").Replace("*", "").Trim();
                switch (val)
                {
                    case "bool":
                        return false;
                    case "float":
                        return false;
                    case "int":
                        return false;
                    case "unsigned int":
                        return false;
                    default:
                        return true;
                }
            }
        }
        private string GetCType(string native)
        {
            var val = native.Replace("&", "").Replace("const", "").Replace("*", "").Trim();
            if (val.Contains("HashMap") || val.Contains("Vector") || native.Contains("*"))
                return "void*";
            switch(val)
            {
                case "bool":
                    return "bool";
                case "float":
                    return "float";
                case "int":
                case "Lumix::ComponentHandle":
                    return "int";
                case "unsigned int":
                    return "unsigned int";
                case "Atomic::StringHash":
                case "Atomic::String":
                case "Atomic::TypeInfo":
                case "VariantMap":
                case "Atomic::Context":
                case "Atomic::Variant":
                case "Atomic::Object":
                case "Atomic::EventHandler":
                case "ClassID":
                case "Atomic::ClassID":
                case "Atomic::Console":
                case "Atomic::DebugHud":
                case "SharedPtr<Atomic::Object>":
                case "Atomic::AttributeInfo":
                case "Atomic::Quaternion":
                case "Atomic::Matrix3x4":
                case "Atomic::Matrix4":
                case "Atomic::Matrix3":
                case "Atomic::Vector2":
                case "Atomic::Vector3":
                case "Atomic::Vector4":
                case "Atomic::IntRect":
                case "Atomic::Ray":
                case "Atomic::FrameInfo":
                case "Atomic::Rect":
                case "SharedPtr<Atomic::File>":
                case "SharedPtr<Atomic::Resource>":
                case "char":
                case "Iterator":
                case "Atomic::Color":
                case "Atomic::ResourceRef":
                case "Atomic::ResourceRefList":
                    return "void*";
                case "Atomic::MouseMode":
                case "Atomic::LoadMode":
                case "Atomic::TextureFilterMode":
                case "Atomic::ShadowQuality":
                case "Atomic::VariantType":
                    return "int";
                case "unsigned long long":
                    return "unsigned long long";
                case "long long":
                    return "long long";
                case "double":
                    return "double";
                default:
                   throw new Exception("Unknown type :(");
            }
        }
        public string FromNative()
        {
            switch(NativeCPP.Trim())
            {
                case "void*":
                case "void *":
                    return "System.IntPtr";
                case "const float":
                    return "float";
                case "unsigned int":
                    return "uint";
                case "unsigned long long":
                case "long long":
                    return "ulong";
            }
            return NativeCPP;
        }
        public string ToCsharp(bool _nativeDecl = false)
        {
            if (IsEnum)
            {
                return "int";
            }
            switch(NativeCPP)
            {
                case "const Lumix::Vec2 &":
                case "Lumix::Vec2":
                    return "Vec2";
                case "const Lumix::Vec3 &":
                case "Lumix::Vec3":
                    return "Vec3";
                case "const Lumix::Vec4 &":
                case "Lumix::Vec4":
                    return "Vec4";
                case "const Lumix::Quat &":
                case "Lumix::Quat":
                    return "Quat";
                case "Lumix::Entity":
                    return _nativeDecl ? "int" : "Entity";
                case "bool":
                    return "bool";
                case "float":
                    return "float";
                case "int":
                case "Lumix::ComponentType":
                case "Lumix::ComponentHandle":
                    return "int";
                case "unsigned int":
                    return "uint";
                case "const char *":
                    return "string";
              
            }
            var type = NativeCPP.Replace("const", "").Replace("*", "").Replace("Lumix::", "").Replace("&","").Trim();
            if (!Bindings.Classes.Contains(type) && type != "void" && !IsBasicType)
                type = "System.IntPtr";
           
            switch(type)
            {
                case "u32":
                    return "uint";
                case "Vec2":
                case "Vec3":
                case "Vec4":
                    return type;
                case "ComponentHandle":
                    return "int";
            }
            return type;
        }
    }
}
