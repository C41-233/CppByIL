using CppByIL.Cpp.Project;
using CppByIL.Cpp.Syntax;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;
using CppByIL.Decompile;
using CppByIL.Decompile.Transformer;
using CppByIL.ILMeta;

public class Program
{
    
    private const string OutputBase = @"D:\Workspace\CppByIL\ILLib\StaticLib";
    private static string OutputHeaderBase = Path.Combine(OutputBase, "include");
    private static string OutputSourceBase = Path.Combine(OutputBase, "src");
    private static string OutputVcxproj = Path.Combine(OutputBase, "StaticLib.vcxproj");

    public static void Main(string[] args)
    {
        PrepareBuild();

        var ass = ILAssemblyDefinition.Load("../../TestDLL/bin/Debug/TestDLL.dll");
        VCProject vc = new VCProject();

        var group = new ItemGroup();
        vc.Project.Groups.Add(group);

        GenerateAssembly(ass, group);
        vc.WriteTo(OutputVcxproj);
    }

    private static void GenerateAssembly(ILAssemblyDefinition ass, ItemGroup vc)
    {
        foreach (var type in ass.TopLevelTypes)
        {
            var headerTree = GenerateHeader(type);
            var sourceTree = GenerateSource(type);

            var fullname = SyntaxUtils.ILFullNameToCppPathName(type.FullName);
            var headerName = fullname + ".h";
            var sourceName = fullname + ".cpp";

            var headerFile = Path.Combine(OutputHeaderBase, headerName);
            var sourceFile = Path.Combine(OutputSourceBase, sourceName);

            File.WriteAllText(headerFile, headerTree.ToString());
            vc.ClIncludes.Add(new ClInclude
            {
                Include = Path.Combine("include", headerName),
            });

            File.WriteAllText(sourceFile, sourceTree.ToString());
            vc.ClCompiles.Add(new ClCompile
            {
                Include = Path.Combine("src", sourceName),
            });
        }
    }

    private static SyntaxTree GenerateHeader(ILTypeDefinition type)
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
        var headerClassDeclaration = new ClassDeclaration(name);
        rootDeclaration.AppendChild(headerClassDeclaration);

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
            headerClassDeclaration.AppendChild(methodDeclaration);

            foreach (var parameter in method.Parameters)
            {
                var parameterDeclaration = new MethodParameter(
                    parameter.Name, 
                    TypeReference.Get(parameter.ParameterType)
                );
                methodDeclaration.ParameterList.Add(parameterDeclaration);
            }
        }


        return tree;
    }

    private static SyntaxTree GenerateSource(ILTypeDefinition type)
    {
        var tree = new SyntaxTree();

        tree.AppendChild(new Include("il.h"));
        tree.AppendChild(new Include(SyntaxUtils.ILFullNameToCppPathName(type.FullName) + ".h"));

        foreach (var method in type.Methods)
        {
            if (method.IsConstructor)
            {
                continue;
            }
            tree.AppendChild(DecodeMethod(method));
        }

        return tree;
    }

    private static SyntaxNode DecodeMethod(ILMethodDefinition method)
    {
        var node = new MethodDefinition(method.Name)
        {
            ReturnType = TypeReference.Get(method.ReturnType),
            DeclaringType = TypeReference.Get(method.DeclaringType),
        };

        foreach (var parameter in method.Parameters) 
        {
            node.ParameterList.Add(new MethodParameter(
                parameter.Name,
                TypeReference.Get(parameter.ParameterType)
            ));
        }

        var decompiler = new ILMethodBodyDecompiler(method);
        var body = decompiler.Decompile();
        MethodBodyTransform(body);
        node.MethodBody = body;
        return node;
    }

    private static readonly ITransformer[] transformers = new ITransformer[]
    {
        new RemoveNopTransformer(),
        new VariableSplitTransformer(),
    };

    private static void MethodBodyTransform(BlockStatement body)
    {
        var ctx = new TransformContext();
        foreach (var transformer in transformers)
        {
            transformer.Run(body, ctx);
        }
    }

    private static void PrepareBuild()
    {
        if (Directory.Exists(OutputHeaderBase))
        {
            Directory.Delete(OutputHeaderBase, true);
        }
        Directory.CreateDirectory(OutputHeaderBase);

        if (Directory.Exists(OutputSourceBase))
        {
            Directory.Delete(OutputSourceBase, true);
        }
        Directory.CreateDirectory(OutputSourceBase);
    }

}