namespace CppByIL.Cpp.Syntax.IL;

public class ILBlock : ILStatement
{

    public ILBlock(string name)
    {
        Name = name;
    }

    public string Name { get; }

}
