using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser.Models
{
	internal class CacheDictionary //: IDictionary<string, MediaData>
	{
		public int Count => _keys.Count;
		public int Limit { get; }
		public int LimitDropCount { get; internal set; }
		public bool IsFull => Count >= Limit;
		private List<string> _keys;
		private Dictionary<string, MediaData> _dictionary;

		/// <summary>
		/// Initalizes the dictionary with the limit size. If the elements count exceeds the limit, the oldest entrie would be dropped. Default: 10.000
		/// </summary>
		/// <param name="limit">Drop limit</param>
		public CacheDictionary(int limit = 10000)
		{
			Limit = limit;
			_keys = new List<string>(limit);
			_dictionary = new Dictionary<string, MediaData>(limit);
		}

		public void Add(KeyValuePair<string, MediaData> item)
		{
			Add(item.Key, item.Value);
		}

		public void Add(string key, MediaData value)
		{
			if (_dictionary.Count >= Limit)
			{
				string oldestKey = _keys.First();
				_dictionary.Remove(oldestKey);
				_keys.Remove(key);
				LimitDropCount++;
			}

			_dictionary.Add(key, value);
			_keys.Add(key);
		}

		public MediaData this[string key]
		{
			get { return _dictionary[key]; }
			set { _dictionary[key] = value; }
		}

		public bool Contains(string key)
		{
			return _keys.Contains(key);
		}

		public bool Contains(KeyValuePair<string, MediaData> item)
		{
			return Contains(item.Key);
		}

		public bool Remove(string key)
		{
			if (Contains(key))
			{
				_keys.Remove(key);
				_dictionary.Remove(key);
				return true;
			}

			return false;
		}

		public bool Remove(KeyValuePair<string, MediaData> item)
		{
			return Remove(item.Key);
		}

		public bool TryGetValue(string key, out MediaData value)
		{
			if (Contains(key))
			{
				value = _dictionary[key];
				return true;
			}
			else
			{
				value = new MediaData();
				return false;
			}
		}

		public void Clear()
		{
			_keys = new List<string>(Limit);
			_dictionary = new Dictionary<string, MediaData>(Limit);
		}

		// ### Unused
		// ##################################################

		//public void CopyTo(KeyValuePair<string, MediaData>[] array, int arrayIndex)
		//{
		//    throw new NotImplementedException();
		//}

		//public IEnumerator<KeyValuePair<string, MediaData>> GetEnumerator()
		//{
		//    throw new NotImplementedException();
		//}

		//IEnumerator IEnumerable.GetEnumerator()
		//{
		//    return GetEnumerator();
		//}
	}
}
