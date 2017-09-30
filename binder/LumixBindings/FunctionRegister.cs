using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace LumixBindings
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionRegister
    {
        string[] data_;
        string content_;

        public string RawContent
        {
            get { return content_; }
        }

        public string NativeClass
        {
            get
            {
                return data_[0];
            }
        }

        public string ManagedClass
        {
            get
            {
                //if (!IsClass)
                //    return "INVALID";
                //else
                    return data_[3];
            }
        }

        public bool IsInvalid
        {
            get { return Components.Contains("INVALID"); }
        }

        public string Name
        {
            get
            {
                return data_[1];
            }
        }

        public bool IsStatic
        {
            get { return data_[2] == "static"; }
        }

        /// <summary>
        /// Returns only true if the native class is a *Scene, like AudioScene
        /// </summary>
        public bool IsScene
        {
            get { return data_[0].ToLower().EndsWith("scene"); }
        }

        public bool HasSceneGetter
        {
            get
            {
                return IsScene && IsComponent;
            }
        }

        public bool IsComponent
        {
            get { return data_[4] == "component"; }
        }

        public bool IsClass
        {
            get { return data_[4] == "class"; }
        }

        public bool IsPartial
        {
            get { return data_[4] == "partial"; }
        }

        public bool IsGetterOnly
        {
            get { return data_.Length == 6; }
        }

        public string PropertyName
        {
            get
            {
                if (!IsGetterOnly)
                    return Name;

                return data_[5];
            }
        }
        public string[] Components
        {
            get
            {
                ///CSHARP_FUNCTION(AudioScene, setEcho, nostatic, AudioScene, class);
                if (data_.Length == 5)
                {
                    if (IsClass)
                        return new string[] { };
                    else
                    {
                        return new string[] { data_[3] };
                    }
                }
                else
                {
                    ///CSHARP_FUNCTION(PhysicsScene, getActorComponent, nostatic, (BoxRigidActor, SphereRigidActor, CapsuleRigidActor, MeshRigidActor), component);
                    if (IsComponent && !IsGetterOnly)
                    {
                        string[] ret = new string[data_.Length - 4];
                        Array.Copy(data_, 3, ret, 0, ret.Length);
                        return ret;
                    }
                    else if(IsGetterOnly)
                    {
                        string[] ret = new string[data_.Length - 5];
                        Array.Copy(data_, 3, ret, 0, ret.Length);
                        return ret;
                    }
                }
                return new string[] { "INVALID" };
            }
        }
        public Method Method
        {
            get;
            set;
        }
        public FunctionRegister(string _content)
        {
            content_ = _content;
            var tmp = content_.Replace("CSHARP_FUNCTION(", "").Replace("(", "").Replace(")", "").Replace(";", "");
            data_ = tmp.Split(',').ToTrimmedArray();
        }

        public override string ToString()
        {
            return NativeClass + "::" + Name;
        }

        public bool IsInClass(string _class)
        {
            return !string.IsNullOrEmpty(Components.FirstOrDefault(x => x == _class));
        }
    }
}
