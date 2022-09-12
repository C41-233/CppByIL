using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Statements;
using System.Collections;

namespace CppByIL.Utils.Collections
{
    internal class ListDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, IReadOnlyList<TValue>>> where TKey : notnull
    {

        private readonly Dictionary<TKey, List<TValue>> dict = new ();

        public void Add(TKey key, TValue value)
        {
            if (!dict.TryGetValue(key, out var list))
            {
                list = new List<TValue> ();
                dict.Add(key, list);
            }
            list.Add(value);
        }

        public IEnumerator<KeyValuePair<TKey, IReadOnlyList<TValue>>> GetEnumerator()
        {
            foreach (var (key, list) in  dict)
            {
                yield return new KeyValuePair<TKey, IReadOnlyList<TValue>>(key, list);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
