//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ClangSharp;

//namespace LumixBindings
//{
//    public class EnumCollector : Collector<EnumValue>
//    {

//        public EnumCollector(CXTranslationUnit unit) : base(unit)
//        {
//        }

//        public override string Name => "EnumCollector";

//        public void Collect(List<CXTranslationUnit> translationUnits)
//        {
//            EnumCollector nsc = this;
//            foreach (var tu in translationUnits)
//            {
//                var tuName = clang.getTranslationUnitSpelling(tu).ToString();
//                bool use = false;
//                foreach (var header in Bindings.Headers)
//                {
//                    if (tuName.Contains(header))
//                    {
//                        use = true;
//                        break;
//                    }
//                }
//                if (use)
//                {
//                    nsc.TU = tu;
//                    clang.visitChildren(clang.getTranslationUnitCursor(tu), nsc.Visit, new CXClientData(IntPtr.Zero));
//                }
//            }

//        }

//        public override CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data)
//        {
//            if (cursor.kind == CXCursorKind.CXCursor_EnumDecl)
//            {
//                EnumValue ev = new EnumValue(cursor, TU);
//                Add(ev);
//            }
//            return CXChildVisitResult.CXChildVisit_Recurse;
//        }
//    }
//}
