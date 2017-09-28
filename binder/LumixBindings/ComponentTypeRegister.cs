using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumixBindings
{
    /// <summary>
    /// universe.registerComponentType(ANIMABLE_TYPE, this, &AnimationSceneImpl::serializeAnimable, &AnimationSceneImpl::deserializeAnimable);
    /// </summary>
    public class ComponentTypeRegister
    {
        string[] data_;
        string content_;
        string file_;

        public string File
        {
            get { return file_; }
        }
        public string ComponentType
        {
            get { return data_[0]; }
        }

        public string Scene
        {
            get { return data_[2]; }
        }
        public ComponentTypeRegister(string _content, string _file)
        {
            file_ = _file;
            content_ = _content;
            var replace = content_.Substring(0, content_.IndexOf("(")+1);
            data_ = content_.Replace(replace,"").Replace(");", "").Split(',').Select(x => x.Trim()).ToArray();
            data_[2] = data_[2].Substring(0, data_[2].IndexOf(":")).Replace("&", "").Replace("Impl", "").Trim();
        }

        public override string ToString()
        {
            return ComponentType + "_" + Scene;
        }
    }
}
