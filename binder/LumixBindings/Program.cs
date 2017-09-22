using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClangSharp;
using System.IO;
namespace LumixBindings
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var createIndex = clang.createIndex(0, 0);
            string[] arr = { "-x", "c++" };/// "-D","LUMIX_LIBRARY_IMPORT", "-D","LUMIX_FINAL" };
            var includeDirs = new List<string>();
            includeDirs.Add(Bindings.RootPath);

            arr = arr.Concat(includeDirs.Select(x => "-I" + x)).ToArray();

            List<CXTranslationUnit> translationUnits = new List<CXTranslationUnit>();

            var headers = new List<string>();
            headers.AddRange(Directory.GetFiles(Bindings.RootPath, "*.h", SearchOption.AllDirectories));
            //List<string> headers = new List<string>()
            //{
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Core\Object.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Engine\Engine.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Core\Context.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Metrics\Metrics.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Input\Input.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Scene\Node.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Scene\Scene.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Scene\Component.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Container\Ptr.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Graphics\Viewport.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Graphics\Renderer.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Resource\ResourceCache.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Math\StringHash.h",
            //   //@"I:\swigwin-3.0.12\swig\AtomicGameEngine\Source\Atomic\Core\Variant.h"
            //   //@"I:\dev\cpp\AtomicGameEngine\Source\Atomic\IO\Serializer.h"
            //   @"D:\dev\LumixEngine\src\physics\physics_scene.h"
            //  //  @"C:\tmp\Test.h",
            //};

            foreach (var file in headers)
            {
                CXTranslationUnit translationUnit;
                CXUnsavedFile unsavedFile = new CXUnsavedFile();
                var translationUnitError = clang.parseTranslationUnit2(createIndex, file, arr, 3,out unsavedFile, 0, 0, out translationUnit);

                if (translationUnitError != CXErrorCode.CXError_Success)
                {
                    Console.WriteLine("Error: " + translationUnitError);
                    var numDiagnostics = clang.getNumDiagnostics(translationUnit);

                    for (uint i = 0; i < numDiagnostics; ++i)
                    {
                        var diagnostic = clang.getDiagnostic(translationUnit, i);
                        Console.WriteLine(clang.getDiagnosticSpelling(diagnostic).ToString());
                        clang.disposeDiagnostic(diagnostic);
                    }
                }

                translationUnits.Add(translationUnit);
            }

            NamespaceCollector nsc = new NamespaceCollector(new CXTranslationUnit(IntPtr.Zero));
            nsc.Collect(translationUnits);
            FileCreator fc = new FileCreator(headers);
            /////iterate through all found ns
            foreach(var ns in nsc.Values)
            {
                ///search for classes within each ns
                clang.visitChildren(clang.getTranslationUnitCursor(ns.TU), ns.Visit, new CXClientData(IntPtr.Zero));
                ///iterate through all found classes in each ns
                foreach(var @class in ns.Values)
                {
                    if(@class.Name == "OutputBlob")
                    {
                    }
                    //var pos = clang.getCursorSpelling(@class.Cursor);
                    ///search for methods within each class
                    clang.visitChildren(@class.Cursor, @class.Visit, new CXClientData(IntPtr.Zero));
                    ///iterate through all found method in each class
                    foreach (var meth in @class.Values)
                    {
                        if(meth.Name == "applyForceToActor")
                        {

                        }
                         var parent = clang.getCursorSemanticParent(meth.Cursor);
                        clang.visitChildren(meth.Cursor, meth.Visit, new CXClientData(IntPtr.Zero));
                    }
                }
                
            }
            

            List<Class> uniqueClasses = new List<Class>();
            List<string> done = new List<string>();
            using (StreamWriter sw = new StreamWriter("macros"))
            {
                foreach(var ns in nsc.Values)
                {
                    foreach(var klass in ns.Values)
                    {
                        foreach(var meth in klass.Values)
                        {
                            string id = klass.Name + "_" + meth.Name;
                            if (!done.Contains(id))
                            {
                                if (!uniqueClasses.Contains(klass))
                                    uniqueClasses.Add(klass);
                                sw.WriteLine("CSHARP_FUNCTION(" + klass.Name.Replace("Impl", "") + "," + meth.Name + ",static)");
                                done.Add(id);
                            }
                        }
                    }
                }
                sw.Flush();
            }
            nsc.Cleanup();
            var lumixParser = new LumixParser(nsc);
            lumixParser.Parse();

            fc.Start();
            foreach(var ns in nsc.Values)
            {
                //fc.Save(ns);
                fc.WriteLumix(ns);
            }
            fc.Finish();

            fc.StartCSharp();
             foreach (var ns in nsc.Values)
            //var nss = nsc.Values[0];
            {
                if (ns.IsEmpty)
                    continue;
                // fc.GenerateCSharp(ns);
                fc.GenerateLumixCsharp(ns);
            }
            fc.FinishCSharp();
            Console.Write("Done");
            Console.ReadKey();
        }
    }
}
