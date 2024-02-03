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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class CacheTests
{
	[TestMethod]
	public void CacheDictionaryInit()
	{
		CacheDictionary cacheDictionary = new( 10 );
		Assert.AreEqual( 10, cacheDictionary.Limit, " -LimitCount" );
		Assert.AreEqual( 0, cacheDictionary.Count, " -ItemCount" );
		Assert.AreEqual( false, cacheDictionary.IsFull, " -IsFull" );
		Assert.AreEqual( 0, cacheDictionary.LimitDropCount, " -LimitDropCount" );
		Assert.AreNotEqual( null, cacheDictionary._dictionary, " -RootDictionary" );
		Assert.AreNotEqual( null, cacheDictionary._keys, " -RootKeys" );
	}

	[TestMethod]
	public void CacheDictionaryDrop()
	{
		CacheDictionary cacheDictionary = new( 10 );

		for (var i = 1; i <= 11; i++)
		{
			cacheDictionary.Add( "Key_" + i, new MediaData() {Title = "Title_" + i} );
		}

		var a = cacheDictionary._dictionary.Count;
		var b = cacheDictionary._keys.Count;

		Assert.IsFalse( cacheDictionary.Contains( "Key_1" ), " -DroppedKey" );

		for (var i = 2; i < 11; i++)
		{
			KeyValuePair<string, MediaData> keyValuePair = new( "Key_" + i, new MediaData() {Title = "Title_" + i} );

			Assert.IsTrue( cacheDictionary.Contains( keyValuePair.Key ), " -Key" );
			Assert.IsTrue( cacheDictionary.Contains( keyValuePair ), " -KeyValuePair" );
		}

		Assert.AreEqual( 10, cacheDictionary.Count, " -ItemCount" );
		Assert.AreEqual( true, cacheDictionary.IsFull, " -IsFull" );
		Assert.AreEqual( 1, cacheDictionary.LimitDropCount, " -LimitDropCount" );
		Assert.AreEqual( 10, cacheDictionary._dictionary.Count, " -RootDictionaryCount" );
		Assert.AreEqual( 10, cacheDictionary._keys.Count, " -RootKeysCount" );
	}
}