using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClangSharp;

namespace LumixBindings
{
    public abstract class Collector<T> : ICursorVisitor where T : IABObject
    {
        protected CXTranslationUnit translationUnit_;

        public abstract string Name
        { get; }

        public CXTranslationUnit TU
        {
            get { return translationUnit_; }
            set { translationUnit_ = value; }
        }
        //protected Dictionary<string, T> knownObjects_ = new Dictionary<string, T>();
        protected List<T> knownObjects_ = new List<T>();

        public T[] Values
        {
            get { return knownObjects_.ToArray(); }
        }

        public Collector(CXTranslationUnit unit)
        {
            translationUnit_ = unit;
        }
        public abstract CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data);

        public virtual void Add(T obj)
        {
          //  if (!knownObjects_.ContainsKey(obj.Name))
                knownObjects_.Add(obj);
        }
        public void Remove(T obj)
        {
            knownObjects_.Remove(obj);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
