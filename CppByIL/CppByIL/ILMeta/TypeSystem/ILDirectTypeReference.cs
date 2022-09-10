namespace CppByIL.ILMeta.TypeSystem
{
    public class ILDirectTypeReference : ILTypeReference
    {

        internal ILDirectTypeReference(string? ns, string name)
        {
            if (ns == null)
            {
                FullName = name;
            }
            else
            {
                FullName = $"{ns}.{name}";
            }
        }

        public override string FullName { get; }
    }
}
