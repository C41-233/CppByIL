using CppByIL.Cpp.Project;
using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;
using CppByIL.ILMeta;
using System.Text;

public class Program
{
    
    private const string OutputBase = @"D:\Workspace\CppByIL\ILLib\StaticLib";
    private static string OutputHeaderBase = Path.Combine(OutputBase, "include");
    private static string OutputVcxproj = Path.Combine(OutputBase, "StaticLib.vcxproj");

    public static void Main(string[] args)
    {
        PrepareBuild();

        var ass = ILAssemblyDefinition.Load("../../TestDLL/bin/Debug/TestDLL.dll");
        VCProject vc = new VCProject();

        var group = new ItemGroup();
        vc.Project.Groups.Add(group);
        vc.Project.Groups.Add(new ItemGroup
        {
            ClCompiles = {
                new ClCompile
                {
                    Include = @"src\TestDLL\Test.cpp",
                } 
            },
        });

        GenerateHeaders(ass, group);
        vc.WriteTo(OutputVcxproj);
    }

    private static void GenerateHeaders(ILAssemblyDefinition ass, ItemGroup vc)
    {
        foreach (var type in ass.TopLevelTypes)
        {
            var tree = GenerateHeaderTree(type);

            var fullname = SyntaxUtils.ILFullNameToCppFullName(type.FullName);
            var filename = fullname.Replace("::", Path.DirectorySeparatorChar.ToString()) + ".h";
            var output = Path.Combine(OutputHeaderBase, filename);
            Directory.CreateDirectory(Path.GetDirectoryName(output)!);
            File.WriteAllText(output, tree.ToString());
            vc.ClIncludes.Add(new ClInclude
            {
                Include = Path.Combine("include", filename),
            });
        }
    }

    private static SyntaxTree GenerateHeaderTree(ILTypeDefinition type)
    {
        var tree = new SyntaxTree();
        tree.AppendChild(new Pragma("once"));
        tree.AppendChild(new Include("il.h"));

        SyntaxNode rootDeclaration;
        if (type.Namespace == null)
        {
            rootDeclaration = new SyntaxTree();
        }
        else
        {
            var ns = SyntaxUtils.ILFullNameToCppFullName(type.Namespace);
            rootDeclaration = new NamespaceDeclaration(ns);
        }

        tree.AppendChild(rootDeclaration);

        var name = SyntaxUtils.ILFullNameToCppFullName(type.Name);
        var classDeclaration = new ClassDeclaration(name);
        rootDeclaration.AppendChild(classDeclaration);

        foreach (var method in type.Methods)
        {
            if (method.IsConstructor)
            {
                continue;
            }
            var methodDeclaration = new MethodDeclaration(method.Name)
            {
                ReturnType = TypeReference.Get(method.ReturnType),
                IsStatic = method.IsStatic,
            };
            classDeclaration.AppendChild(methodDeclaration);

            foreach (var parameter in method.Parameters)
            {
                var parameterDeclaration = new MethodParameterDeclaration(
                    parameter.Name, 
                    TypeReference.Get(parameter.ParameterType)
                );
                methodDeclaration.ParameterDeclarations.Add(parameterDeclaration);
            }
        }


        return tree;
    }

    private static void PrepareBuild()
    {
        if (Directory.Exists(OutputHeaderBase))
        {
            Directory.Delete(OutputHeaderBase, true);
        }
        Directory.CreateDirectory(OutputHeaderBase);
    }

}