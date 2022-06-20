using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsTrie
{
    class Trie<T>
    {
        string symbol;
        T data;
        bool isTerminal;
        List<Trie<T>> descendants;

        Trie(string symbol)
        {
            this.symbol = symbol;
            isTerminal = false;
            descendants = new List<Trie<T>>();
        }

        Trie<T> FindDescendant(string symbolToFind)
        {
            Trie<T> result = null;
            foreach (Trie<T> descendant in descendants)
            {
                if (descendant.symbol == symbolToFind)
                    result = descendant;
            }
            return result;
        }
        bool TryGetValue(string key, string currentKey, out T value, int i)
        {
            if (currentKey == key)
            {
                if (isTerminal == true)
                {
                    value = data;
                    return true;
                }
                else
                {
                    value = default(T);
                    return false;
                }
            }
            if (i > key.Length)
            {
                value = default(T);
                return false;
            }
            Trie<T> nextHead = FindDescendant(key[i].ToString());
            if (nextHead == null)
            {
                value = default(T);
                return false;
            }
            return nextHead.TryGetValue(key, currentKey + key[i], out value, i + 1);
        }
        void Insert(string key, string currentKey, T value, int i)
        {
            if (currentKey == key)
            {
                data = value;
                isTerminal = true;
                return;
            }
            Trie<T> nextHead = FindDescendant(key[i].ToString());
            if (nextHead == null)
            {
                nextHead = new Trie<T>(key[i].ToString());
                descendants.Add(nextHead);
            }
            nextHead.Insert(key, currentKey + key[i], value, i + 1);
        }
        void Remove(string key, string currentKey, int i)
        {
            if (currentKey == key)
            {
                isTerminal = false;
                return;
            }
            Trie<T> nextHead = FindDescendant(key[i].ToString());
            if (nextHead == null)
            {
                nextHead = new Trie<T>(key[i].ToString());
                descendants.Add(nextHead);
            }
            nextHead.Remove(key, currentKey + key[i], i + 1);
        }
        public Trie()
        {
            symbol = "";
            isTerminal = false;
            descendants = new List<Trie<T>>();
        }
        ///<summary>Returns false if element not found, otherwise returns true and found value. </summary>
        public bool TryGetValue(string key, out T value)
        {
            return TryGetValue(key, "", out value, 0);
        }
        ///<summary>Inserts value for given key. </summary>
        public void Insert(string key, T value)
        {
            Insert(key, "", value, 0);
        }
        ///<summary>Removes key. </summary>
        public void Remove(string key)
        {
            Remove(key, "", 0);
        }
        ///<summary>Returns List of <Key, Value> Pairs. </summary>
        public List<Tuple<string, T>> Each()
        {
            var result = new List<Tuple<string, T>>();
            Each("", result, "");
            return result;
        }
        // Padding is used to show key's last symbol depth
        void Each(string currentKey, List<Tuple<string, T>> list, string padding)
        {
            if (isTerminal == true)
            {
                list.Add(Tuple.Create(padding + currentKey + symbol, data));
            }
            foreach (var des in descendants)
                des.Each(currentKey + symbol, list, padding + ".");
        }
    }
}
