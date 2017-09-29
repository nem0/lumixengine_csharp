using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumixBindings
{
    public class ResourceRegister
    {
        string content_;
        string[] data_;

        public string Class
        {
            get { return data_[0]; }
        }

        public string ResourceType
        {
            get { return data_[1]; }
        }


        public ResourceRegister(string _raw)
        {
            content_ = _raw;
            data_ = content_.Replace("CSHARP_RESOURCE(", "").Replace(");", "").Split(',').Select(x => x.Trim()).ToArray();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Class, ResourceType);
        }
    }
}
