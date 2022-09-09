using CppByIL.Utils.XML;
using System.Xml;

namespace CppByIL.Cpp.Project;

public class VCProject
{

    public readonly Project Project = new Project();

    public VCProject()
    {
        Project.Groups.Add(new Import
        {
            Project = @"$(VCTargetsPath)\Microsoft.Cpp.Default.props",
        });

        Project.Groups.Add(new ItemGroup
        {
            Label = "ProjectConfigurations",
            ProjectConfigurations =
            {
                new ProjectConfiguration
                {
                    Include = "Debug|x64",
                    Configuration = Configuration.Debug,
                    Platform = Platform.x64,
                },
                new ProjectConfiguration
                {
                    Include = "Release|x64",
                    Configuration = Configuration.Release,
                    Platform = Platform.x64,
                }
            },
        });
        Project.Groups.Add(new PropertyGroup
        {
            Label = "Configuration",
            ConfigurationType = ConfigurationType.StaticLibrary,
            CharacterSet = "Unicode",
            PlatformToolset = "v143",
            OutDir = @"..\bin\$(Configuration)\$(Platform)\",
            IntDir = @"obj\$(Configuration)\$(Platform)\",
        });
        Project.Groups.Add(new PropertyGroup
        {
            Condition = CreateCondition(Configuration.Debug, Platform.x64),
            Label = "Configuration",
            UseDebugLibraries = true,
            LinkIncremental = true,
        });
        Project.Groups.Add(new PropertyGroup
        {
            Condition = CreateCondition(Configuration.Release, Platform.x64),
            Label = "Configuration",
            UseDebugLibraries = false,
            LinkIncremental = false,
        });

        Project.Groups.Add(new Import
        {
            Project = @"$(VCTargetsPath)\Microsoft.Cpp.props",
        });

        Project.Groups.Add(new ItemDefinitionGroup
        {
            Condition = CreateCondition(Configuration.Debug, Platform.x64),
            ClCompile = new ClCompile
            {
                WarningLevel = "Level3",
                PreprocessorDefinitions = "_DEBUG;_LIB;%(PreprocessorDefinitions)",
                AdditionalIncludeDirectories = @"$(ProjectDir)..\ILCore\include;$(ProjectDir)include;%(AdditionalIncludeDirectories)",
            },
            Link = new Link
            {
                GenerateDebugInformation = true,
            },
        });
        Project.Groups.Add(new ItemDefinitionGroup
        {
            Condition = CreateCondition(Configuration.Release, Platform.x64),
            ClCompile = new ClCompile {
                WarningLevel = "Level3",
                PreprocessorDefinitions = "NDEBUG;_LIB;%(PreprocessorDefinitions)",
                FunctionLevelLinking = true,
                IntrinsicFunctions = true,
            },
            Link = new Link
            {
                OptimizeReferences = true,
                GenerateDebugInformation = true,
            },
        });

        Project.Groups.Add(new Import
        {
            Project = @"$(VCTargetsPath)\Microsoft.Cpp.targets",
        });
    }

    public void WriteTo(string path)
    {
        var doc = XmlFactory.Create(Project, "http://schemas.microsoft.com/developer/msbuild/2003");
        using (var os = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            var writer = XmlWriter.Create(os, new XmlWriterSettings
            {
                Indent = true,
            });
            doc.Save(writer);
        }
    }

    public static string CreateCondition(string configuration, string platform)
    {
        return $"'$(Configuration)|$(Platform)'=='{configuration}|{platform}'";
    }

}

