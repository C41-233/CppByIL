using System.Xml;
using System.Xml.Serialization;

namespace CppByIL.Cpp.Project;

public class Project
{

    [XmlAttribute]
    public string DefaultTargets = "Build";

    [XmlArray]
    public List<ProjectDefinitionGroup> Groups = new();

}

public abstract class ProjectDefinitionGroup
{
}

public class ItemGroup : ProjectDefinitionGroup
{

    [XmlAttribute]
    public string? Label;

    [XmlArray]
    public List<ProjectConfiguration> ProjectConfigurations = new();

    [XmlArray]
    public List<ClInclude> ClIncludes = new();

    [XmlArray]
    public List<ClCompile> ClCompiles = new();
}

[XmlType(nameof(ProjectConfiguration))]
public class ProjectConfiguration
{

    [XmlAttribute]
    public string? Include;

    public string? Configuration;

    public string? Platform;

}

public class PropertyGroup : ProjectDefinitionGroup
{

    [XmlAttribute]
    public string? Condition;

    [XmlAttribute]
    public string? Label;

    public string? ConfigurationType;

    public string? PlatformToolset;

    public bool UseDebugLibraries;

    public string? CharacterSet;

    public bool LinkIncremental;

    public string? OutDir;

    public string? IntDir;
}

public class ItemDefinitionGroup : ProjectDefinitionGroup
{

    [XmlAttribute]
    public string? Condition;

    public ClCompile? ClCompile;

    public Link? Link;

}

public class ClCompile
{
    [XmlAttribute]
    public string? Include;

    public string? WarningLevel;
    public bool? FunctionLevelLinking;
    public bool? IntrinsicFunctions;
    public string? PreprocessorDefinitions;
    public string? AdditionalIncludeDirectories;
}

public class ClInclude
{

    [XmlAttribute]
    public string? Include;

}

public class Import : ProjectDefinitionGroup
{
    [XmlAttribute]
    public string? Project;
}

public class Link
{
    public bool? GenerateDebugInformation;
    public bool? OptimizeReferences;
}