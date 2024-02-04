// MIT License
// 
// Copyright(c) 2016 - 2024
// Stefan (StefmDE) Müller, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
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
using System.Linq;
using System.Text;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;

namespace SeriesIDParser.Caching;

/// <summary>
///     The Cache
/// </summary>
internal class MediaDataCache
{
	#region FieldsAndProperties
	private static CacheDictionary _cacheDictionary;
	public static IParserSettings ParserSettings { get; private set; }

	private static MediaDataCache _instance;

	public static MediaDataCache Instance
	{
		get
		{
			if (_instance == null)
			{
				throw new InvalidOperationException( "Instance doesn't exist." );
			}

			return _instance;
		}
	}

	public static bool InstanceExists => _instance != null;
	#endregion FieldsAndProperties

	#region InstanceMaintenance
	// ReSharper disable once MemberCanBePrivate.Global
	public static void Create( IParserSettings parserSettings )
	{
		if (_instance != null)
		{
			throw new InvalidOperationException( "Instance is already created. Overriding is NOT allowed!" );
		}

		ParserSettings = parserSettings;
		_instance = new MediaDataCache();
		_cacheDictionary = new CacheDictionary( parserSettings.CacheItemCountLimit );
	}
	#endregion InstanceMaintenance

	#region CachingFunctions
	// ReSharper disable once MemberCanBePrivate.Global
	public void Add( string key, MediaData data )
	{
		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			_cacheDictionary.Add( key, data );
		}
		else if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			_cacheDictionary.Add( key.ToLowerInvariant(), data );
		}
		else
		{
			throw new ArgumentException( "Cache is used but is disabled" );
		}
	}

	// ReSharper disable once MemberCanBePrivate.Global
	public bool Contains( string fileName )
	{
		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return _cacheDictionary.Contains( fileName );
		}

		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return _cacheDictionary.Contains( fileName.ToLowerInvariant() );
		}

		throw new ArgumentException( "Cache is used but is disabled" );
	}

	// ReSharper disable once MemberCanBePrivate.Global
	public MediaData Get( string fileName )
	{
		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return Contains( fileName ) ? _cacheDictionary[fileName] : null;
		}

		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return Contains( fileName ) ? _cacheDictionary[fileName.ToLowerInvariant()] : null;
		}

		throw new ArgumentException( "Cache is used but is disabled" );
	}

	/// <summary>
	/// Gets a result from the cache
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public IParserResult GetAsParserResult( string fileName )
	{
		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return Contains( fileName ) ? _cacheDictionary[fileName].ToParserResult( ParserSettings ) : null;
		}

		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			return Contains( fileName ) ? _cacheDictionary[fileName.ToLowerInvariant()].ToParserResult( ParserSettings ) : null;
		}

		throw new ArgumentException( "Cache is used but is disabled" );
	}

	public bool TryGet( string fileName, out MediaData mediaData )
	{
		if (Contains( fileName ))
		{
			mediaData = Get( fileName );
			return true;
		}

		mediaData = null;
		return false;
	}

	// ReSharper disable once MemberCanBePrivate.Global
	public bool TryGetAsParserResult( string fileName, out ParserResult parserResult )
	{
		if (TryGet( fileName, out var innerMediaData ))
		{
			parserResult = innerMediaData.ToParserResult( ParserSettings );
			return true;
		}

		parserResult = null;
		return false;
	}

	/// <summary>
	/// Replaces a cache item
	/// </summary>
	/// <param name="fileName"></param>
	/// <param name="data"></param>
	/// <exception cref="ArgumentException"></exception>
	public void Replace( string fileName, MediaData data )
	{
		if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			_cacheDictionary[fileName] = data;
		}
		else if (ParserSettings.CacheMode == CacheMode.Advanced)
		{
			_cacheDictionary[fileName.ToLowerInvariant()] = data;
		}
		else
		{
			throw new ArgumentException( "Cache is used but is disabled" );
		}
	}
	#endregion
}