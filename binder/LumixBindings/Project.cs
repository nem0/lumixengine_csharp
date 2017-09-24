using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LumixBindings
{
    public class Project
    {
        protected string name_;
        protected List<string> classes_ = new List<string>();
        public string Name
        {
            get { return name_; }
        }

        public Project(string _name)
        {
            name_ = _name;
        }

        public void AddClass(string _class)
        {
            if (!classes_.Contains(_class))
                classes_.Add(_class);
        }

        public void Export(string _to)
        {
            StringBuilder template = new StringBuilder();
            template.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            template.AppendLine("<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
            template.AppendLine("  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />");
            template.AppendLine("  <PropertyGroup>");
            template.AppendLine("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
            template.AppendLine("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>");
            template.AppendLine("    <ProjectGuid>0667610d-468e-4d6d-98e7-9f12ecf3dd2e</ProjectGuid>");
            template.AppendLine("    <OutputType>Library</OutputType>");
            template.AppendLine("    <AppDesignerFolder>Properties</AppDesignerFolder>");
            template.AppendLine("    <RootNamespace>Lumix</RootNamespace>");
            template.AppendLine("    <AssemblyName>Lumix</AssemblyName>");
            template.AppendLine("    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
            template.AppendLine("    <FileAlignment>512</FileAlignment>");
            template.AppendLine("  </PropertyGroup>");
            template.AppendLine("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">");
            template.AppendLine("    <DebugSymbols>true</DebugSymbols>");
            template.AppendLine("    <DebugType>full</DebugType>");
            template.AppendLine("    <Optimize>false</Optimize>");
            template.AppendLine("    <OutputPath>bin\\Debug\\</OutputPath>");
            template.AppendLine("    <DefineConstants>DEBUG;TRACE</DefineConstants>");
            template.AppendLine("    <ErrorReport>prompt</ErrorReport>");
            template.AppendLine("    <WarningLevel>4</WarningLevel>");
            template.AppendLine("    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>");
            template.AppendLine("  </PropertyGroup>");
            template.AppendLine("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
            template.AppendLine("    <DebugType>pdbonly</DebugType>");
            template.AppendLine("    <Optimize>true</Optimize>");
            template.AppendLine("    <OutputPath>bin\\Release\\</OutputPath>");
            template.AppendLine("    <DefineConstants>TRACE</DefineConstants>");
            template.AppendLine("    <ErrorReport>prompt</ErrorReport>");
            template.AppendLine("    <WarningLevel>4</WarningLevel>");
            template.AppendLine("    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>");
            template.AppendLine("  </PropertyGroup>");
            template.AppendLine("  <ItemGroup>");
            template.AppendLine("    <Reference Include=\"System\"/>");
            template.AppendLine("    <Reference Include=\"System.Core\"/>");
            template.AppendLine("    <Reference Include=\"System.Xml.Linq\"/>");
            template.AppendLine("    <Reference Include=\"System.Data.DataSetExtensions\"/>");
            template.AppendLine("    <Reference Include=\"Microsoft.CSharp\"/>");
            template.AppendLine("    <Reference Include=\"System.Data\"/>");
            template.AppendLine("    <Reference Include=\"System.Net.Http\"/>");
            template.AppendLine("    <Reference Include=\"System.Xml\"/>");
            template.AppendLine("  </ItemGroup>");
            template.AppendLine("  <ItemGroup>");
            template.AppendLine(string.Format("    <Compile Include=\"Manual\\{0}.cs\" />", "Imgui"));
            template.AppendLine(string.Format("    <Compile Include=\"Manual\\{0}.cs\" />", "Component"));
            template.AppendLine(string.Format("    <Compile Include=\"Manual\\{0}.cs\" />", "Vec"));
            template.AppendLine(string.Format("    <Compile Include=\"Manual\\{0}.cs\" />", "Quat"));
            template.AppendLine(string.Format("    <Compile Include=\"Manual\\{0}.cs\" />", "Entity"));
            foreach (var klass in classes_)
            {
                template.AppendLine(string.Format("    <Compile Include=\"{0}.cs\" />", klass));
            }
            template.AppendLine("  </ItemGroup>");
            template.AppendLine("  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
            template.AppendLine(" </Project>");

            File.WriteAllText(Path.Combine(_to, name_ + ".csproj"), template.ToString());
        }
    }
}
