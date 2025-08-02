using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoogleEngine.Utils
{
    public class SimpleDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
    {
        private Dictionary<TKey, TValue> dictBase;

        public SimpleDictionary()
        {
            dictBase = [];
        }
        [JsonConstructor]
        public SimpleDictionary(Dictionary<TKey, TValue> dictBase)
        {
            this.dictBase = dictBase;
        }

        private TValue Get(TKey key)
        {
            if (dictBase.TryGetValue(key,out TValue value))
            {
                return value;
            }
            return default;

        }
        private void Set(TKey key, TValue value)
        {
            if (!dictBase.TryAdd(key, value))
            {
                dictBase[key] = value;
            }
        }
        public TValue this[TKey key] { 
            get => Get(key); set => Set(key,value);
        }

        public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>)dictBase).Keys;

        public ICollection<TValue> Values => ((IDictionary<TKey, TValue>)dictBase).Values;

        public int Count => ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>)dictBase).Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>)dictBase).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)dictBase).GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>)dictBase).Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)dictBase).Remove(item);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return ((IDictionary<TKey, TValue>)dictBase).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)dictBase).GetEnumerator();
        }
    }
}
