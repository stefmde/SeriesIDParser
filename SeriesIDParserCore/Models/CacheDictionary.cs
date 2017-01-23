// 
// MIT License
// 
// Copyright(c) 2016 - 2017
// Stefan Müller, Stefm, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo( "SeriesIDParserCore.Test" )]

namespace SeriesIDParserCore.Models
{
	internal class CacheDictionary //: IDictionary<string, MediaData>
	{
		public int Count => _keys.Count;
		public int Limit { get; }
		public int LimitDropCount { get; internal set; }
		public bool IsFull => Count >= Limit;
		internal List<string> _keys;
		internal Dictionary<string, MediaData> _dictionary;

		/// <summary>
		///     Initalizes the dictionary with the limit size. If the elements count exceeds the limit, the oldest entrie would be
		///     dropped. Default: 10.000
		/// </summary>
		/// <param name="limit">Drop limit</param>
		public CacheDictionary( int limit = 10000 )
		{
			Limit = limit;
			_keys = new List<string>( limit );
			_dictionary = new Dictionary<string, MediaData>( limit );
		}

		public void Add( KeyValuePair<string, MediaData> item )
		{
			Add( item.Key, item.Value );
		}

		public void Add( string key, MediaData value )
		{
			if (_dictionary.Count >= Limit)
			{
				string oldestKey = _keys.First();
				_dictionary.Remove( oldestKey );
				_keys.Remove( oldestKey );
				LimitDropCount++;
			}

			_dictionary.Add( key, value );
			_keys.Add( key );
		}

		public MediaData this[ string key ]
		{
			get { return _dictionary[key]; }
			set { _dictionary[key] = value; }
		}

		public bool Contains( string key )
		{
			return _keys.Contains( key );
		}

		public bool Contains( KeyValuePair<string, MediaData> item )
		{
			return Contains( item.Key );
		}

		public bool Remove( string key )
		{
			if (Contains( key ))
			{
				_keys.Remove( key );
				_dictionary.Remove( key );
				return true;
			}

			return false;
		}

		public bool Remove( KeyValuePair<string, MediaData> item )
		{
			return Remove( item.Key );
		}

		public bool TryGetValue( string key, out MediaData value )
		{
			if (Contains( key ))
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
			_keys = new List<string>( Limit );
			_dictionary = new Dictionary<string, MediaData>( Limit );
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