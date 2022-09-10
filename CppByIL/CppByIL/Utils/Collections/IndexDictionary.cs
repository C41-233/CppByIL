using System.Reflection.Metadata;

namespace CppByIL.Utils.Collections;

internal sealed class IndexDictionary<TKey, TValue> where TKey : notnull
{

    private readonly Dictionary<TKey, TValue> dict = new();
    private readonly List<TKey> list = new();

    public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var item in list)
            {
                yield return dict[item];
            }
        }
    }

    public void Add(TKey key, TValue value)
    {
        dict.Add(key, value);
        list.Add(key);
    }

    public TValue? GetValueOrDefault(TKey handle)
    {
        return dict.GetValueOrDefault(handle);
    }
}
