using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace LumixBindings
{
    public class ProperterRegister
    {
        const string propertyPattern_ = "\"[^\"]*\"";
        const string getterSetterPattern_ = "\\&[A-Za-z:\\d]*";
        const string descriptorPattern_ = "([A-Za-z]*Descriptor)";
        const string simplePropertyTypePattern_ = "(<[A-Za-z0-9]*)";
        Regex matcher_;
        string content_;
        NamespaceCollector nsc_;
        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName
        {
            get
            {
                var tmp = content_.Replace("PropertyRegister::add(\"", "");
                var raw = tmp.Substring(0, tmp.IndexOf(",")-1).Trim();
                raw = raw.Replace("renderable","ModelInstance");
                if (raw.Contains("_"))
                {
                    return raw.Capitalize('_');
                }
                else
                {
                    return raw.Capitalize();
                }
            }
        }

        /// <summary>
        /// Gets the name of the native class.
        /// </summary>
        /// <value>
        /// The name of the native class.
        /// </value>
        public string NativeClassName
        {
            get
            {
                matcher_ = new Regex(getterSetterPattern_);
                var tmp = content_.Replace("&(", "");
                if (matcher_.IsMatch(tmp))
                {
                    var getter = matcher_.Matches(tmp)[0];
                    return getter.Value.Substring(1, getter.Value.IndexOf(":") - 1);
                }
                return "INVALID";
            }
        }
        public bool IsNativeScene
        {
            get { return NativeClassName.ToLower().EndsWith("scene"); }
        }
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get
            {
                var tmp = content_.Split(',');
                if (tmp.Length < 3)
                    return "INVALID";
                matcher_ = new Regex(propertyPattern_);
                if (matcher_.IsMatch(tmp[2]))
                    return matcher_.Match(tmp[2]).Value.Replace("\"","").Capitalize(' ');
                else if(matcher_.IsMatch(tmp[3]))
                    return matcher_.Match(tmp[3]).Value.Replace("\"", "").Capitalize(' ');
                return "INVALID";
            }
        }

        /// <summary>
        /// Gets the getter function.
        /// </summary>
        /// <value>
        /// The getter function.
        /// </value>
        public string GetterFunction
        {
            get
            {
                matcher_ = new Regex(getterSetterPattern_);
                if(matcher_.IsMatch(content_))
                {
                    var getter = matcher_.Matches(content_)[0];
                    return getter.Value.Replace("&", "").Replace(NativeClassName, "").Replace("::","").Trim();
                }
                return "INVALID";
            }
        }

        /// <summary>
        /// Gets the setter function.
        /// </summary>
        /// <value>
        /// The setter function.
        /// </value>
        public string SetterFunction
        {
            get
            {
                matcher_ = new Regex(getterSetterPattern_);
                if (matcher_.IsMatch(content_))
                {
                    var matches = matcher_.Matches(content_);
                    if (matches.Count < 2)
                        return "INVALID";
                    var getter = matches[1];
                    return getter.Value.Replace("&", "").Replace(NativeClassName, "").Replace("::", "").Trim();
                }
                return "INVALID";
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get
            {
                matcher_ = new Regex(descriptorPattern_);
                if (matcher_.IsMatch(content_))
                {
                    return GetCType(matcher_.Match(content_).Value,false);
                    //return GetCType(tmp[2].Trim().Substring(0, tmp[2].Trim().IndexOf("<")));
                }
                else
                {
                    return "INVALID";
                }
            }
        }
        public string NativeType
        {
            get
            {
                matcher_ = new Regex(descriptorPattern_);
                if (matcher_.IsMatch(content_))
                {
                    return GetCType(matcher_.Match(content_).Value);
                    //return GetCType(tmp[2].Trim().Substring(0, tmp[2].Trim().IndexOf("<")));
                }
                else
                {
                    return "INVALID";
                }
            }
        }

        public bool IsInvalid
        {
            get {
                return Type == "INVALID" || NativeType == "INVALID" || ClassName == "INVALID"
                  || NativeClassName == "INVALID" || SetterFunction == "INVALID" || GetterFunction == "INVALID" ||
                  String.IsNullOrEmpty(SetterFunction) || String.IsNullOrEmpty(GetterFunction);
            }
        }
        public string DescriptorType
        {
            get
            {
                matcher_ = new Regex(descriptorPattern_);
                if (matcher_.IsMatch(content_))
                {
                    return matcher_.Match(content_).Value.Replace("PropertyDescriptor","");
                    //return GetCType(tmp[2].Trim().Substring(0, tmp[2].Trim().IndexOf("<")));
                }
                else
                {
                    return "INVALID";
                }
            }
        }
        public string RawContent
        {
            get { return content_; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProperterRegister"/> class.
        /// </summary>
        /// <param name="_raw">The raw.</param>
        public ProperterRegister(string _raw, NamespaceCollector _nsc)
        {
            content_ = _raw;
            nsc_ = _nsc;
        }

        /// <summary>
        /// Gets the type of the c.
        /// </summary>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GetCType(string _value, bool native = true)
        {
            if(GetterFunction == "getD6JointXMotion")
            {

            }
            //"PropertyRegister::add(\"d6_joint\",LUMIX_NEW(allocator, D6MotionDescriptor)(\"X motion\", &PhysicsScene::getD6JointXMotion, &PhysicsScene::setD6JointXMotion));"
            switch (_value)
            {
                case "ColorPropertyDescriptor":
                    return "Vec3";
                case "DynamicTypePropertyDescriptor":
                case "MotionDescriptor":
                    var meth = nsc_.GetMethodFromClass(NativeClassName, GetterFunction);
                    if(meth == null)
                    {
                        return "INVALID";
                    }
                    return native ? meth[0].ReturnValue : "int";
                case "ResourcePropertyDescriptor":
                    return native ? "Path" : "string";
                
                case "DynamicEnumPropertyDescriptor":
                case "PhysicsLayerPropertyDescriptor":
                    return "int";
                case "SampledFunctionDescriptor":
                case "BlobPropertyDescriptor":
                    return "INVALID";
                case "DecimalPropertyDescriptor":
                    return "float";
                case "EntityPropertyDescriptor":
                    return "Entity";
                case "StringPropertyDescriptor":
                    return native ? "const char*" : "string";
                case "BoolPropertyDescriptor":
                    return "bool";
                case "SimplePropertyDescriptor":
                    {

                        matcher_ = new Regex(simplePropertyTypePattern_);
                        if(matcher_.IsMatch(content_))
                        {
                            var ret = matcher_.Match(content_).Value.Substring(1);
                            return ret;
                        }
                        return _value;
                    }
                        
                default:
                    return native ? _value : "IntPtr";
                    //throw new NotImplementedException("");
            }
        }

        public override string ToString()
        {
            /*
             * auto f = csharp_getSubobjectCount<Scene, &Scene::get##Subclass##Count>; \
            mono_add_internal_call("Lumix." ## #Class ## "::get" ## #Subclass ##  "Count", f); \

            f = &csharp_getProperty<Type, Scene, &Scene::get##Class##Property>;
             * */
            string fn = string.Format("auto f{1}_{2} = &csharp_getProperty<{0}, {1}, &{1}::{2}>;\n", NativeType, NativeClassName.Replace("Impl", ""), GetterFunction);
            string mono = string.Format("mono_add_internal_call(\"Lumix.{0}::{1}\", f{2}_{1});\n", ClassName, GetterFunction, NativeClassName.Replace("Impl", ""));
            string fnSet = string.Format("auto f{1}_{2} = &csharp_setProperty<{0}, {1}, &{1}::{2}>;\n", NativeType, NativeClassName.Replace("Impl", ""), SetterFunction);
            string monoSet = string.Format("mono_add_internal_call(\"Lumix.{0}::{1}\", f{2}_{1});\n", ClassName, SetterFunction, NativeClassName.Replace("Impl", ""));
            return fn + mono + fnSet + monoSet; //string.Format("CSHARP_PROPERTY({0}, {1}, {2}, {3})", NativeType, NativeClassName.Replace("Impl",""), ClassName, PropertyName);
        }
    }
}
