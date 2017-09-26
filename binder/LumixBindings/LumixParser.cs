using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LumixBindings
{
    public class LumixParser
    {
        NamespaceCollector nsc_;
        public LumixParser(NamespaceCollector _nsc)
        {
            nsc_ = _nsc;
        }
        public void Parse()
        {
            List<string> cpps_ = new List<string>();
            List<ProperterRegister> knownRegisters_ = new List<ProperterRegister>();
            List<FunctionRegister> knownFunctions = new List<FunctionRegister>();
            cpps_.AddRange(System.IO.Directory.GetFiles(Bindings.RootPath, "*.cpp", System.IO.SearchOption.AllDirectories));

            //iterate all found cpp files
            foreach (var file in cpps_)
            {
                int index = 0;
                using (StreamReader sr = new StreamReader(file))
                {
                    index++;
                    string sLine = "";
                    bool readProperty = false;
                    ProperterRegister current = null;
                    string rawContent = "";

                    while ((sLine = sr.ReadLine()) != null)
                    {
                        var t = sLine.Trim();
                        if (readProperty)
                        {
                            if (t.EndsWith(";"))
                            {
                                rawContent += t;
                                current = new ProperterRegister(rawContent, nsc_);
                                knownRegisters_.Add(current);
                                rawContent = "";
                                readProperty = false;
                            }
                            else
                            {
                                rawContent += t;
                            }
                        }

                        if (t.ToLower().StartsWith("propertyregister::add("))
                        {
                            rawContent = t;
                            if (t.EndsWith(";"))//catch one liner
                            {
                                current = new ProperterRegister(rawContent, nsc_);
                                knownRegisters_.Add(current);
                                rawContent = "";
                            }
                            else//multiline
                            {
                                readProperty = true;
                            }
                        }

                    }
                    sr.Close();
                }
            }
            if (!File.Exists(Bindings.CppSharpPath))
            {
                Console.WriteLine("Not able to find csharp.cppp");
                return;
            }

            using (StreamReader sr = new StreamReader(Bindings.CppSharpPath))
            {
                string rawFunction = "";
                string sLine = "";
                bool readFunction = false;
                while ((sLine = sr.ReadLine()) != null)
                {
                    var t = sLine.Trim();
                    if (readFunction)
                    {
                        rawFunction += t;
                        if (t.Contains(";"))
                        {
                            knownFunctions.Add(new FunctionRegister(rawFunction));
                            readFunction = false;
                            rawFunction = "";
                            continue;
                        }
                    }
                    if (t.ToLower().StartsWith("csharp_function("))
                    {
                        rawFunction += t;
                        readFunction = true;
                        if (t.Contains(";"))
                        {
                            knownFunctions.Add(new FunctionRegister(rawFunction));
                            readFunction = false;
                            rawFunction = "";
                            continue;
                        }

                    }
                }
                sr.Close();
            }
            foreach (var kfucn in knownFunctions)
            {
                var test = kfucn.Components;
            }
            //dump to disk, for debuging!
            using (StreamWriter sw = new StreamWriter("functions.h"))
            {
                foreach (var func in knownFunctions)

                {
                    if (!func.ToString().Contains("INVALID"))
                        sw.WriteLine(func.ToString());
                }
                sw.Flush();
            }


            using (StreamWriter tmpWriter = new StreamWriter(Bindings.ApiPath))
            {
                var natives = knownRegisters_.SortByNativeClass();
                //write down the template calls for every known property
                foreach (var native in natives)
                {
                    foreach (var func in native.Value)
                    {
                        if (func.IsInvalid)
                            continue;
                        tmpWriter.WriteLine("{");
                        tmpWriter.WriteLine(func.ToString());
                        tmpWriter.WriteLine("}");
                    }
                }
                /*
                 * extern "C" void physics_scene_put_to_sleep(PhysicsScene* _ptr) {
		                _ptr->putToSleep();
	                }

            		auto f = &CSharpMethodProxy<decltype(&PhysicsScene::putToSleep)>::call<&PhysicsScene::putToSleep>;
		            mono_add_internal_call("Lumix.Animable::getSource", csharp_Animable_getSource);
                 */
                var classes = knownFunctions.FindComponents(natives, nsc_);
                //write down all known functions as a mono call via template calls
                foreach (var func in knownFunctions)
                {
                    if (func.IsInvalid)
                        continue;

                    string cast = "";
                    if (func.IsClass)
                    {
                       
                     
                        var meth = nsc_.GetMethodFromClass(func.NativeClass, func.Name);
                        bool needCast = meth.Length > 1;
                        
                        for (int k = 0; k < meth.Length; k++)
                        {
                            tmpWriter.WriteLine("\t{");
                            string methodType = meth[k].ToFunctionTypedef(func.NativeClass.Replace("Impl", ""));
                            //contstruct template call
                            if (needCast)
                            {
                                cast = meth[k].CastToFunctionPointer(func.NativeClass.Replace("Impl", ""));
                                tmpWriter.WriteLine("\t\t" + methodType);
                                // tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<{2}>::call<({2})&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                //construct mono call
                                tmpWriter.Write(string.Format("\t\tmono_add_internal_call(\"Lumix.{0}::{1}{2}\", f);\n", func.ManagedClass, func.Name,meth[k].CastToManagedArgs()));
                            }
                            else
                            {
                                tmpWriter.WriteLine("\t\t" + methodType);
                                //tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<decltype({2}&{0}::{1})>::call<{2}&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                //construct mono call
                                tmpWriter.Write(string.Format("\t\tmono_add_internal_call(\"Lumix.{0}::{1}\", f);\n", func.ManagedClass, func.Name));
                            }

                            tmpWriter.WriteLine("\t}");
                        }
                    }
                    else
                    {
                        foreach (var component in func.Components)
                        {
                            var meth = nsc_.GetMethodFromClass(func.NativeClass, func.Name);
                       
                            bool needCast = meth.Length > 1;
                            for (int k = 0; k < meth.Length; k++)
                            {
                                string methodType = meth[k].ToFunctionTypedef(func.NativeClass.Replace("Impl", ""));
                                tmpWriter.WriteLine("\t{");
                                //contstruct template call
                                if (needCast)
                                {
                                    cast = meth[k].CastToFunctionPointer(func.NativeClass.Replace("Impl", ""));
                                    tmpWriter.WriteLine("\t\t" + methodType);
                                    // tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<{2}>::call<({2})&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                    tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                    //construct mono call
                                    tmpWriter.Write(string.Format("\t\tmono_add_internal_call(\"Lumix.{0}::{1}{2}\", f);\n", component, func.Name,meth[k].CastToManagedArgs()));
                                }
                                else
                                {
                                    tmpWriter.WriteLine("\t\t" + methodType);
                                    //tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<decltype({2}&{0}::{1})>::call<{2}&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                    tmpWriter.Write(string.Format("\t\tauto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&{0}::{1}>;\n", func.NativeClass.Replace("Impl", ""), func.Name, cast));
                                    //construct mono call
                                    tmpWriter.Write(string.Format("\t\tmono_add_internal_call(\"Lumix.{0}::{1}\", f);\n", component, func.Name));
                                }
                               
                                tmpWriter.WriteLine("\t}");
                            }
                        }
                    }
                }
                //write down all known functions as a c function
                //foreach (var func in knownFunctions)
                //{
                //    var meth = nsc_.GetMethodFromClass(func.NativeClass.Replace("Impl", ""), func.Name);
                //    if (meth != null)
                //    {
                //        tmpWriter.Write("\textern \"C\" " + func.NativeClass.ToSeperateLower() +"_"+ func.Name.ToSeperateLower() + "( " + func.NativeClass.Replace("Impl", "") + "* _ptr");
                //        int idx = 0;
                //        if (meth.Values.Length > 0)
                //            tmpWriter.Write(", ");
                //        foreach (var arg in meth.Values)
                //        {
                //            bool ptr = arg.TypeMap.IsPointer;
                //            tmpWriter.Write(arg.NativeType.Replace("struct Lumix::", "").Replace("class Lumix::", "") + " " + arg.Name);
                //            if (++idx < meth.Values.Length)
                //                tmpWriter.Write(", ");
                //        }
                //        tmpWriter.Write(") {\n");
                //        tmpWriter.Write("\t\t_ptr->" + func.Name + "(");
                //        idx = 0;
                //        foreach(var arg in meth.Values)
                //        {
                //            tmpWriter.Write(arg.Name);
                //            if (++idx < meth.Values.Length)
                //                tmpWriter.Write(", ");
                //        }
                //        tmpWriter.Write(")\n");
                //        tmpWriter.WriteLine("\t}");
                //    }
                //}
            }


            var klasses = knownRegisters_.SortByClass();
            var project = new Project("Lumix");
            //write down components

            foreach (var klass in klasses)
            {
                project.AddClass(klass.Key);
                using (StreamWriter tmpWriter = new StreamWriter(Path.Combine(Bindings.CSRootPath, klass.Key + ".cs")))
                {
                    //write down the using directives
                    tmpWriter.WriteLine("using System;");
                    tmpWriter.WriteLine("using System.Runtime.InteropServices;");
                    tmpWriter.WriteLine("using System.Runtime.CompilerServices;\n");
                    tmpWriter.WriteLine("namespace Lumix"); ;
                    tmpWriter.WriteLine("{"); ;
                    //class def
                    tmpWriter.WriteLine("\tpublic class " + klass.Key + " : NativeComponent");
                    tmpWriter.WriteLine("\t{");
                    //local fields
                    //tmpWriter.WriteLine("\t\tint componentId_;");
                    //tmpWriter.WriteLine("\t\tIntPtr scene_;\n");
                    //platform calls
                    foreach (var func in klass.Value)
                    {
                        if (func.IsInvalid)
                        {
                            Console.WriteLine("Failed to parse: {0}...", func.RawContent);
                            continue;
                        }
                        //getter
                        tmpWriter.WriteLine("\t\t[MethodImplAttribute(MethodImplOptions.InternalCall)]");
                        tmpWriter.WriteLine(string.Format("\t\textern static {0} {1}(IntPtr scene, int cmp);\n", func.Type, func.GetterFunction));
                        //setter
                        tmpWriter.WriteLine("\t\t[MethodImplAttribute(MethodImplOptions.InternalCall)]");
                        tmpWriter.WriteLine(string.Format("\t\textern static void {0}(IntPtr scene, int cmp, {1} value);\n", func.SetterFunction, func.Type));
                    }

                    //write down decl functions
                    foreach (var func in knownFunctions)
                    {
                        //write only functions who belong to this component!
                        if (!func.IsInClass(klass.Key))
                            continue;

                        WriteCsharpMonoDecl(tmpWriter, func, false);
                    }
                    tmpWriter.WriteLine("");
                    //static helper function
                    tmpWriter.WriteLine("\t\tpublic static string GetCmpType{ get { return \"" + klass.Key.Replace("ModelInstance", "renderable").ToSeperateLower() + "\"; } }\n\n");
                    //default ctor1
                    //tmpWriter.WriteLine("\t\tpublic " + klass.Key + "(Entity _entity, int _componenId)");
                    //tmpWriter.WriteLine("\t\t{");
                    //tmpWriter.WriteLine("\t\t\tentity_ = _entity;");
                    //tmpWriter.WriteLine("\t\t\tcomponentId_ = _componenId;");
                    //tmpWriter.WriteLine("\t\t\tscene_ = getScene(entity_.instance_, \"" + klass.Key.Replace("ModelInstance", "renderable").ToSeperateLower() + "\");");
                    //tmpWriter.WriteLine("\t\t}\n");
                    //default ctor2
                    //tmpWriter.WriteLine("\t\tpublic " + klass.Key + "(Entity _entity)");
                    //tmpWriter.WriteLine("\t\t{");
                    //tmpWriter.WriteLine("\t\t\tentity_ = _entity;");
                    //tmpWriter.WriteLine("\t\t\tcomponentId_ = create(entity_.instance_, entity_.entity_Id_, \"" + klass.Key.Replace("ModelInstance", "renderable").ToSeperateLower() + "\");");
                    //tmpWriter.WriteLine("\t\t\tif (componentId_ < 0) throw new Exception(\"Failed to create component\");");
                    //tmpWriter.WriteLine("\t\t\tscene_ = getScene(entity_.instance_, \"" + klass.Key.Replace("ModelInstance", "renderable").ToSeperateLower() + "\");");
                    //tmpWriter.WriteLine("\t\t}\n");

                    //write down all known properties
                    foreach (var func in klass.Value)
                    {
                        if (func.IsInvalid)
                        {
                            Console.WriteLine("Failed to parse: {0}...", func.RawContent);
                            continue;
                        }

                        //n00bish comment
                        tmpWriter.WriteLine("\t\t/// <summary>");
                        tmpWriter.WriteLine("\t\t/// Gets or sets the " + func.PropertyName);
                        tmpWriter.WriteLine("\t\t/// </summary>");
                        //actual c# property
                        tmpWriter.WriteLine("\t\tpublic " + func.Type + " " + (func.Type == "bool" ? "Is" : "") + func.PropertyName.SharpyFy());
                        tmpWriter.WriteLine("\t\t{");
                        tmpWriter.WriteLine("\t\t\tget { return " + func.GetterFunction + "(scene_, componentId_); }");
                        tmpWriter.WriteLine("\t\t\tset { " + func.SetterFunction + "(scene_, componentId_, value); }");
                        tmpWriter.WriteLine("\t\t}\n");

                    }

                    //default component ctor
                    tmpWriter.WriteLine("\t\tpublic {0}(Entity _entity, int _cmpId)", klass.Key);
                    tmpWriter.WriteLine("\t\t\t: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }\n");

                    //write down all known funtions for that component
                    foreach (var func in knownFunctions)
                    {
                        if (!func.IsInClass(klass.Key))
                            continue;

                        //if (func.HasSceneGetter)
                        //{
                        //    tmpWriter.WriteLine("\t\tpublic " + func.NativeClass.Replace("Impl", "") + " Scene");
                        //    tmpWriter.WriteLine("\t\t{");
                        //    tmpWriter.WriteLine("\t\t\tget { return new " + func.NativeClass.Replace("Impl", "") + "( scene_ ); }");
                        //    tmpWriter.WriteLine("\t\t}\n");
                        //}

                        WriteCsharpFunction(tmpWriter, func, false);
                    }

                    tmpWriter.WriteLine("\t}//end class\n");
                    tmpWriter.WriteLine("}//end namespace");
                    tmpWriter.Flush();
                    tmpWriter.Close();
                }

                var staticClasses = knownFunctions.GetClasses();
                //write down classes
                foreach (var kvp in staticClasses)
                {
                    project.AddClass(kvp.Value[0].ManagedClass);
                    using (var tmpWriter = new StreamWriter(Path.Combine(Bindings.CSRootPath, kvp.Value[0].ManagedClass + ".cs")))
                    {
                        //write down the using directives
                        tmpWriter.WriteLine("using System;");
                        tmpWriter.WriteLine("using System.Runtime.InteropServices;");
                        tmpWriter.WriteLine("using System.Runtime.CompilerServices;\n");
                        tmpWriter.WriteLine("namespace Lumix"); ;
                        tmpWriter.WriteLine("{");
                        WriteCsharpClass(tmpWriter, kvp.Value, kvp.Key, true);
                        tmpWriter.WriteLine("}");
                    }
                }

                var normalClasses = knownFunctions.GetClasses(false);
                //write down classes
                foreach (var kvp in normalClasses)
                {
                    project.AddClass(kvp.Key);
                    using (var tmpWriter = new StreamWriter(Path.Combine(Bindings.CSRootPath, kvp.Key + ".cs")))
                    {
                        //write down the using directives
                        tmpWriter.WriteLine("using System;");
                        tmpWriter.WriteLine("using System.Runtime.InteropServices;");
                        tmpWriter.WriteLine("using System.Runtime.CompilerServices;\n");
                        tmpWriter.WriteLine("namespace Lumix"); ;
                        tmpWriter.WriteLine("{");
                        WriteCsharpClass(tmpWriter, kvp.Value, kvp.Key, false);
                        tmpWriter.WriteLine("}");
                    }
                }

                var partial = knownFunctions.GetPartialClass();
                //write down classes
                foreach (var kvp in partial)
                {
                    
                    project.AddClass(kvp.Value[0].ManagedClass+".Automatic");
                    using (var tmpWriter = new StreamWriter(Path.Combine(Bindings.CSRootPath, kvp.Value[0].ManagedClass + ".Automatic.cs")))
                    {
                        //write down the using directives
                        tmpWriter.WriteLine("using System;");
                        tmpWriter.WriteLine("using System.Runtime.InteropServices;");
                        tmpWriter.WriteLine("using System.Runtime.CompilerServices;\n");
                        tmpWriter.WriteLine("namespace Lumix"); ;
                        tmpWriter.WriteLine("{");
                        WriteCsharpClass(tmpWriter, kvp.Value, kvp.Value[0].ManagedClass, false, true);
                        tmpWriter.WriteLine("}");
                    }
                }

            }

            project.Export(Bindings.CSRootPath);
        }

        void WriteCsharpClass(StreamWriter _writer,List<FunctionRegister> _methods, string _name, bool _isStatic, bool _isPartial = false)
        {
            //class def
            _writer.WriteLine("\tpublic " + (_isPartial ? "partial " : "") + (_isStatic ? "static class " : "class ") + _name);
            _writer.WriteLine("\t{");
            if (!_isStatic && !_isPartial)
                _writer.WriteLine("\t\tIntPtr instance_;\n");
            //write down all mono decls
            foreach (var func in _methods)
            {
                WriteCsharpMonoDecl(_writer, func, _isStatic);
            }
            if(!_isStatic)
            {
                WriteCsharpDefaultCtor(_writer, _name);
            }
            //convert funcs in properties where possible
            var props = new List<KeyValuePair<FunctionRegister, FunctionRegister>>(); 
            if (_isPartial)//propertie extraction only for partials atm
            {
                props = _methods.GetProperties(nsc_);
                foreach (var prop in props)
                {
                    var func = prop.Key.Method;
                    string getter = func.Name.StartsWith("get") ? func.Name : prop.Value.Name;
                    string setter = func.Name.StartsWith("set") ? func.Name : prop.Value.Name;
                    string retType = func.Name.StartsWith("get") ? func.ReturnTypemap.ToCsharp() : prop.Value.Method.ReturnTypemap.ToCsharp();
                    //actual c# property
                    _writer.WriteLine("\t\tpublic " + retType + " " + getter.Replace("get", "").SharpyFy());
                    _writer.WriteLine("\t\t{");
                    if (func.ReturnTypemap.NativeCPP == "Lumix::Entity")
                        _writer.WriteLine(string.Format("\t\t\tget {{ return new Entity(instance_, " + getter + "(instance_, {0})); }}", prop.Key.ManagedClass.ToLower() + "_Id_"));
                    else
                        _writer.WriteLine(string.Format("\t\t\tget {{ return " + getter + "(instance_, {0}); }}", prop.Key.ManagedClass.ToLower() + "_Id_"));
                    _writer.WriteLine(string.Format("\t\t\tset {{ " + setter + "(instance_, {0}, value); }}", prop.Key.ManagedClass.ToLower() + "_Id_"));

                    _writer.WriteLine("\t\t}\n");
                }
            }
            //write down all funtions
            foreach (var func in _methods)
            {
                if (_isPartial)
                {
                    var val = props.Find(x => x.Key == func || x.Value == func);
                    if (val.Key != null)
                        continue;
                }
                WriteCsharpFunction(_writer, func, _isStatic, _isPartial, _name);
            }

            _writer.WriteLine("\t}");
            _writer.WriteLine("");
        }

        void WriteCsharpDefaultCtor(StreamWriter _writer,string _name)
        {
            _writer.WriteLine("\t\tinternal " + _name + "(IntPtr _instance)");
            _writer.WriteLine("\t\t{");
            _writer.WriteLine("\t\t\tinstance_ = _instance;");
            _writer.WriteLine("\t\t}\n");
        }

        void WriteCsharpMonoDecl(StreamWriter _writer, FunctionRegister _func, bool _isStatic)
        {

            if (_func.IsInvalid)
                return;
            var meth = nsc_.GetMethodFromClass(_func.NativeClass.Replace("Impl", ""), _func.Name);

            if (meth != null)
            {
                for (int k = 0; k < meth.Length; k++)
                {
                    //mono decl
                    _writer.WriteLine("\t\t[MethodImplAttribute(MethodImplOptions.InternalCall)]");
                    string args = "";
                    if (!_isStatic)
                    {
                        args = "IntPtr instance";
                        if (meth[k].Values.Length > 0)
                            args += ", ";
                    }
                    int idx = 0;
                    foreach (var argument in meth[k].Values)
                    {
                        args += argument.TypeMap.ToCsharp(true) + " " + argument.Name;
                        if (++idx < meth[k].Values.Length)
                            args += ", ";
                    }
                    _writer.Write(string.Format("\t\textern static {0} {1}({2});\n", (meth[k].IsReturnSomething ? meth[k].ReturnTypemap.ToCsharp(true) : "void"), _func.Name, args));
                }
            }
            else
            {
                Console.WriteLine("No clang function named {0} found...", _func.Name);
            }

            _writer.WriteLine("\n");
        }

        void WriteCsharpFunction(StreamWriter _writer, FunctionRegister _func, bool _isStatic,bool _isPartial = false, string _klassName = "")
        {
            var meth = nsc_.GetMethodFromClass(_func.NativeClass.Replace("Impl", ""), _func.Name);

            if (meth != null)
            {
                for (int i = 0; i < meth.Length; i++)
                {
                    //func def
                    _writer.Write("\t\tpublic " + (_isStatic ? "static " : "") + (meth[i].IsReturnSomething ? meth[i].ReturnTypemap.ToCsharp() : "void"));
                    _writer.Write(" " + meth[i].Name.Capitalize());

                    //arguments
                    _writer.Write("(");
                    for (int k = 0; k < meth[i].Values.Length; k++)
                    {
                        var arg = meth[i].Values[k];
                        if (arg.TypeMap.NativeCPP == "Lumix::ComponentHandle" && _func.IsComponent)
                            continue;
                        if (arg.TypeMap.NativeCPP == "Lumix::"+_klassName && _isPartial)
                            continue;
                        _writer.Write(arg.TypeMap.ToCsharp() + " ");
                        _writer.Write(arg.Name);
                        if (k + 1 < meth[i].Values.Length)
                            _writer.Write(", ");
                    }
                    _writer.Write(")\n");

                    //func body start
                    _writer.WriteLine("\t\t{");
                    bool isEntReturn = false;
                    
                    if (meth[i].IsReturnSomething)
                    {
                        _writer.Write("\t\t\treturn ");
                        if(meth[i].ReturnTypemap.NativeCPP == "Lumix::Entity")
                        {
                            _writer.Write("new Entity(instance_, ");
                            isEntReturn = true;
                        }
                    }
                    else
                    {
                        _writer.Write("\t\t\t");
                    }
                    
                    string args = "";
                    if (!_isStatic)
                    {
                        args = _func.IsComponent ? "scene_" : "instance_";
                        if (meth[i].Values.Length > 0)
                            args += ", ";
                    }
                    int idx = 0;
                    foreach (var argument in meth[i].Values)
                    {
                        if (argument.TypeMap.NativeCPP == "Lumix::ComponentHandle" && _func.IsComponent)
                            args += "componentId_";
                        else if (argument.TypeMap.NativeCPP == "Lumix::" + _klassName && _isPartial)
                            args += _klassName.ToLower() + "_Id_";
                        else
                            args += argument.Name;

                        if (++idx < meth[i].Values.Length)
                            args += ", ";

                    }
                    //call native func
                    _writer.Write(string.Format("{0}({1}{2});\n", _func.Name, args, (isEntReturn ? ")" : "")));
                    //func body end
                    _writer.WriteLine("\t\t}\n");
                }
            }
        }
    }
}
