using System;
using System.Collections.Generic;
using ClangSharp;
using System.Linq;
namespace LumixBindings
{
    public static class Bindings
    {
        public static readonly string NativeLib = "LumixSharpNative";
        public static readonly string Version = "0.0.2";
        public static readonly string RootPath = @"../../../../../../LumixEngine/Src";
        public static readonly string CppSharpPath = @"../../../../../src/csharp.cpp";
        public static readonly string EditorCsPath = @"../../../../../../lumixengine_data/cs";
        public static readonly string ApiPath =      @"../../../../../src/api.h";
        public static readonly string CSRootPath = @"../../../../../cs/";
        public static readonly bool DebugToken = false;
        public static readonly bool IgnoreOperator = true;

        public static List<string> StaticClasses = new List<string>()
        {
            "Engine", "App", "GUISystem", "StudioApp","ImportAssetDialog", "Universe"
        };
        public static List<KeyValuePair<string,string>> ClassRenames = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Universe","Engine"),
        };
        public static List<string> IgnoredClasses = new List<string>()
        {
            "PipelineImpl"
        };
        public static List<string> Classes = new List<string>()
        {
            //Lumix
            "PhysicsScene"
        };
        public static List<string> Headers = new List<string>()
        {
            //lumix
            "physics_scene.h"
        };
        public static List<string> Abstracts = new List<string>()
        {
        };

        public static List<string> IgnoredFunctions = new List<string>()
        {

        };
        public static List<Class> KnownClasses = new List<Class>();
        public static List<string> WrappedClasses = new List<string>();
        public static List<string> Enums = new List<string>();
        public static bool IsInSystemHeader(this CXCursor cursor)
        {
            return clang.Location_isInSystemHeader(clang.getCursorLocation(cursor)) != 0;
        }

        public static string ToSharpString(this CXCursor cursor)
        {
            return clang.getCursorSpelling(cursor).ToString();
        }

        public static bool IsString(this CXType type)
        {
            var str = clang.getTypeSpelling(type);
            var cleanStr = str.ToString();
            switch (cleanStr)
            {
                case "const char *":
                    break;
            }
            return false;
        }
        public static bool IsBasicType(this CXType type)
        {
            var rType = clang.getResultType(type).ToString().Replace("const ", "").Trim();
            if (string.IsNullOrEmpty(rType) && type.kind == CXTypeKind.CXType_Record)
            {
                rType = clang.getTypeSpelling(type).ToString();
            }
            switch (rType)
            {
                case "Lumix::Vec2":
                case "Lumix::Vec3":
                case "Lumix::Vec4":
                case "Lumix::ComponentHandle":
                case "bool":
                case "unsigned int":
                case "unsigned long long":
                case "float":
                case "double":
                case "long":
                case "int":
                case "long long":
                    return true;
            }
            switch (type.kind)
            {
                case CXTypeKind.CXType_Float:
                case CXTypeKind.CXType_Int:
                case CXTypeKind.CXType_Bool:
                case CXTypeKind.CXType_Double:
                case CXTypeKind.CXType_Long:
                case CXTypeKind.CXType_LongLong:
                case CXTypeKind.CXType_Short:
                case CXTypeKind.CXType_UShort:
                case CXTypeKind.CXType_ULong:
                case CXTypeKind.CXType_UInt:
                    return true;
            }
            return false;
        }

        public static List<CXToken> Tokenize(this CXCursor cursor)
        {
            List<CXToken> ret = new List<CXToken>();
            var extend = clang.getCursorExtent(cursor);
            IntPtr tokenPtr;
            CXToken token;
            uint numToken;
            CXTranslationUnit TU = cursor.GetTranslationUnit();
            clang.tokenize(TU, extend, out tokenPtr, out numToken);
            int TokenSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CXToken));
            IntPtr current = tokenPtr;
            for (int k = 0; k < numToken; k++)
            {
                token = (CXToken)System.Runtime.InteropServices.Marshal.PtrToStructure(current, typeof(CXToken));
                ret.Add(token);
                current = new System.IntPtr(tokenPtr.ToInt64() + ((k + 1) * TokenSize));
            }
            return ret;
        }

        public static CXTranslationUnit GetTranslationUnit(this CXCursor cursor)
        {
            return clang.Cursor_getTranslationUnit(cursor);
        }

        public static string Capitalize(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            char first = value[0].ToString().ToUpper().ToCharArray()[0];
            value = first.ToString() + value.Substring(1);
            return value;
        }

        public static string Capitalize(this string value, char seperator = ' ')
        {
            var split = value.Split(seperator);
            for (int k = 0; k < split.Length; k++)
                split[k] = split[k].First().ToString().ToUpper() + String.Join("", split[k].Skip(1));
            return string.Concat(split);
        }

        public static Dictionary<string, List<ProperterRegister>> SortByNativeClass(this List<ProperterRegister> value)
        {
            Dictionary<string, List<ProperterRegister>> ret = new Dictionary<string, List<ProperterRegister>>();
            foreach (var pr in value)
            {
                if (!ret.ContainsKey(pr.NativeClassName))
                {
                    ret.Add(pr.NativeClassName, new List<ProperterRegister>());
                }
                ret[pr.NativeClassName].Add(pr);
            }
            return ret;
        }
        public static Dictionary<string, List<ProperterRegister>> SortByClass(this List<ProperterRegister> value)
        {
            Dictionary<string, List<ProperterRegister>> ret = new Dictionary<string, List<ProperterRegister>>();
            foreach (var pr in value)
            {
                if (!ret.ContainsKey(pr.ClassName))
                {
                    ret.Add(pr.ClassName, new List<ProperterRegister>());
                }
                ret[pr.ClassName].Add(pr);
            }
            return ret;
        }
        public static string ToSeperateLower(this string _this, string _sep = "_")
        {
            var data = _this.ToArray();
            if (data.Length < 1)
                return _this.ToLower();
            string ret = "";
            ret += data[0];
            for (int k = 1; k < data.Length; k++)
            {
                if (char.IsUpper(data[k]))
                    ret += "_";
                ret += data[k];
            }
            return ret.ToLower();
        }
        public static string SharpyFy(this string value)
        {

            string ret = "";
            //  var c = value[0];
            foreach (var c in value)
            {
                if (char.IsNumber(c))
                {
                    //switch (c.ToString())
                    //{
                    //    case "0":
                    //        ret += "Zero";
                    //        break;
                    //    case "1":
                    //        ret += "One";
                    //        break;
                    //    case "2":
                    //        ret += "Two";
                    //        break;
                    //    case "3":
                    //        ret += "Three";
                    //        break;
                    //    case "4":
                    //        ret += "Four";
                    //        break;
                    //    case "5":
                    //        ret += "Five";
                    //        break;
                    //    case "6":
                    //        ret += "Six";
                    //        break;
                    //    case "7":
                    //        ret += "Seven";
                    //        break;
                    //    case "8":
                    //        ret += "Eight";
                    //        break;
                    //    case "9":
                    //        ret += "Nine";
                    //        break;

                    //}
                    //ret += "Is";
                }
                else if (char.IsSymbol(c))
                    continue;
                else if (c == '(' || c == ')')
                    continue;
                ret += c;
            }
            return ret;
        }

        /// <summary>
        /// Finds the components.
        /// </summary>
        /// <param name="_this">The this.</param>
        /// <param name="_byClass">The by class.</param>
        /// <param name="_nsc">The NSC.</param>
        /// <returns></returns>
        public static Dictionary<string, List<FunctionRegister>> FindComponents(this List<FunctionRegister> _this, Dictionary<string, List<ProperterRegister>> _byClass, NamespaceCollector _nsc)
        {
            var ret = new Dictionary<string, List<FunctionRegister>>();
            foreach (var klass in _byClass)
            {
                foreach (var func in _this)
                {
                    if (!func.IsInClass(klass.Key))
                        continue;
                    if (!ret.ContainsKey(klass.Key))
                    {
                        ret.Add(klass.Key, new List<FunctionRegister>());
                    }
                    ret[klass.Key].Add(func);
                }
            }
            return ret;
        }
        public static Dictionary<string, List<FunctionRegister>> GetPartialClass(this List<FunctionRegister> _this)
        {
            Dictionary<string, List<FunctionRegister>> ret = new Dictionary<string, List<FunctionRegister>>();
            foreach (var func in _this)
            {
                if (!func.IsPartial)
                    continue;
                if (!ret.ContainsKey(func.ManagedClass))
                {
                    ret.Add(func.ManagedClass, new List<FunctionRegister>());
                }
                ret[func.ManagedClass].Add(func);
            }
            return ret;
        }
        public static Dictionary<string, List<FunctionRegister>> GetClasses(this List<FunctionRegister> _this, bool _staticOnly = true)
        {
            Dictionary<string, List<FunctionRegister>> ret = new Dictionary<string, List<FunctionRegister>>();
            foreach (var func in _this)
            {
                if (_staticOnly)
                {
                    if (!func.IsStatic || !func.IsClass)
                        continue;
                }
                else
                {
                    if (func.IsStatic || !func.IsClass)
                        continue;
                }

                if (!ret.ContainsKey(func.NativeClass))
                {
                    ret.Add(func.NativeClass, new List<FunctionRegister>());
                }
                ret[func.NativeClass].Add(func);
            }
            return ret;
        }

        public static string[] ToTrimmedArray(this string[] _this)
        {
            return _this.Select(x => x.Trim()).ToArray();
        }

        public static List<KeyValuePair<FunctionRegister, FunctionRegister>> GetProperties(this List<FunctionRegister> _this, NamespaceCollector _nsc)
        {
            List<KeyValuePair<FunctionRegister, FunctionRegister>> properties = new List<KeyValuePair<FunctionRegister, FunctionRegister>>();
            Dictionary<string, FunctionRegister> funcs = new Dictionary<string, FunctionRegister>();
            foreach(var f in _this)
            {
                var meth = _nsc.GetMethodFromClass(f.NativeClass, f.Name);
                if (meth == null)
                {
                    continue;
                }
                if (meth.Length != 1)
                    continue;
               
                bool getter = meth[0].Name.StartsWith("get");
                bool setter = meth[0].Name.StartsWith("set");
                if (!getter && !setter)
                    continue;
                var fname = meth[0].Name.Replace("get", "").Replace("set", "");
                f.Method = meth[0];
                if (funcs.ContainsKey(fname))
                {
                    
                    properties.Add(new KeyValuePair<FunctionRegister, FunctionRegister>(funcs[fname], f));
                }
                else
                {
                    funcs.Add(fname, f);
                }
            }

            return properties;
        }
    }
}
