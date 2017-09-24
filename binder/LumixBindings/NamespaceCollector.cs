using System;
using ClangSharp;
using System.Collections.Generic;

namespace LumixBindings
{
    public class NamespaceCollector : Collector<Namespace>
    {
        Dictionary<string, List<Class>> nsclasses_ = new Dictionary<string, List<Class>>();
        public override string Name
        {
            get { return "Collector"; }
        }
        public NamespaceCollector(CXTranslationUnit unit)
            : base(unit) { }
        
        public override CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
        {
            if (cursor.IsInSystemHeader())
                return CXChildVisitResult.CXChildVisit_Continue;

          //  Console.WriteLine("NSCOLLECT : " + Enum.GetName(typeof(CXCursorKind), cursor.kind));
            if(cursor.kind == CXCursorKind.CXCursor_Namespace)
            {
                Namespace ns = new Namespace(cursor, TU);
                Add(ns);
                return CXChildVisitResult.CXChildVisit_Recurse;
            }
            if(cursor.kind == CXCursorKind.CXCursor_EnumDecl)
            {

            }
            return CXChildVisitResult.CXChildVisit_Recurse;
        }

        public void Collect(List<CXTranslationUnit> translationUnits)
        {
            NamespaceCollector nsc = this;
            foreach (var tu in translationUnits)
            {
                var tuName = clang.getTranslationUnitSpelling(tu).ToString();
                bool use = false;
                foreach (var header in Bindings.Headers)
                {
                    if (tuName.Contains(header))
                    {
                        use = true;
                        break;
                    }
                }
                if (use || true)
                {
                    nsc.TU = tu;
                    clang.visitChildren(clang.getTranslationUnitCursor(tu), nsc.Visit, new CXClientData(IntPtr.Zero));
                }
            }

        }
        /// <summary>
        /// delete all empty namespacecs
        /// </summary>
        public void Cleanup()
        {

            nsclasses_.Clear();
            List<Class> uniqueClasses = new List<Class>();
            List<string> done = new List<string>();
            NamespaceCollector nsc = this;
            foreach (var ns in nsc.Values)
            {
                foreach (var klass in ns.Values)
                {
                    foreach (var meth in klass.Values)
                    {
                        string id = ns.Name+"_"+klass.Name + "_" + meth.Name;
                        if (!done.Contains(id))
                        {
                            if (!nsclasses_.ContainsKey(ns.Name))
                            {
                                nsclasses_[ns.Name] = new List<Class>();
                            }

                            if (!nsclasses_[ns.Name].Contains(klass))
                            {
                                nsclasses_[ns.Name].Add(klass);
                            }
                            done.Add(id);
                        }
                    }
                }
            }
              
            
            List<EnumValue> enums = new List<EnumValue>();
            
            for (int k = 0; k < nsc.Values.Length; k++)
            {
                foreach(var en in nsc.Values[k].Enums)
                {
                    if (!enums.Contains(en))
                        enums.Add(en);
                }
                for (int c = 0; c < nsc.Values[k].Values.Length; c++)
                {
                    if (nsc.Values[k].Values[c].Values.Length == 0)
                    {
                        nsc.Values[k].Remove(nsc.Values[k].Values[c]);
                        c--;
                    }
                }

                if (nsc.Values[k].Values.Length == 0)
                {
                    nsc.Remove(nsc.Values[k]);
                    k--;
                }
            }
            List<string> classExists = new List<string>();
            for (int k = 0; k < nsc.Values.Length; k++)
            {
                for (int c = 0; c < nsc.Values[k].Values.Length; c++)
                {
                    if (classExists.Contains(nsc.Values[k].Values[c].Name) && nsc.Values[k].Values[c].Values.Length == 0)
                    {
                        nsc.Values[k].Remove(nsc.Values[k].Values[c]);
                        c--;
                    }
                    else
                        classExists.Add(nsc.Values[k].Values[c].Name);
                }
                if (nsc.Values[k].IsEmpty)
                {
                    nsc.Remove(nsc.Values[k]);
                    k--;
                }
            }

            ////merge
            List<Class> klasses = new List<Class>();
            Namespace nss = Values[0];
            foreach (var n in Values)
            {
                if (n.Equals(nss))
                    continue;
                nss.Add(n.Values[0]);
            }
            knownObjects_.Clear();
            nss.Enums.Clear();
            nss.Enums.AddRange(enums);
            Add(nss);
        }

        public Method[] GetMethodFromClass(string _klass, string _method,string _ns = "Lumix")
        {
            List<Method> found = new List<Method>();
            if (!nsclasses_.ContainsKey(_ns))
                return null;

           
            foreach(var kl in nsclasses_[_ns])
            {
                if(kl.Name == _klass)
                {
                    foreach (var m in kl.Values)
                    {
                        if (m.Name == _method)
                        {
                            found.Add(m);
                        }
                    }
                    return found.ToArray();
                }
            }
            return found.ToArray();
            //foreach(var ns in knownObjects_)
            //{
            //    if (ns.Name != _ns)
            //        continue;
            //    foreach(var klass in ns.Values)
            //    {
            //        if(klass.Name == _klass)
            //        {
            //            foreach(var meth in klass.Values)
            //            {
            //                if (meth.Name == _method)
            //                    return meth;
            //            }
            //        }
            //    }
            //}
        }
    }
}
