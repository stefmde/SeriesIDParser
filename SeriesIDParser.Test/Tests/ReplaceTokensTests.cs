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


using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[SuppressMessage( "ReSharper", "StringLiteralTypo" )]
[ExcludeFromCodeCoverage]
[TestFixture]
public class ReplaceTokensTests
{
	[Test]
	public void ReplaceTokensTestDefault()
	{
		var ps = new ParserSettings();

		// Regular Test
		ps.ReplaceRegexWithoutListTokens.Add( new KeyValuePair<string, string>( "EXTENDED", "." ) );
		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestDots()
	{
		var ps = new ParserSettings();

		ps.ReplaceRegexWithoutListTokens.Clear();
		ps.ReplaceRegexWithoutListTokens.Add( new KeyValuePair<string, string>( ".EXTENDED.", "." ) );
		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestCase()
	{
		var ps = new ParserSettings();

		ps.ReplaceRegexWithoutListTokens.Clear();
		ps.ReplaceRegexWithoutListTokens.Add( new KeyValuePair<string, string>( "extended", "." ) );
		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestNothing()
	{
		var ps = new ParserSettings();

		var input = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
		var expected = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestEmptyList()
	{
		var ps = new ParserSettings { ReplaceRegexWithoutListTokens = [] };

		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestNullList()
	{
		var ps = new ParserSettings { ReplaceRegexWithoutListTokens = null };

		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}

	[Test]
	public void ReplaceTokensTestRegexTest()
	{
		var ps = new ParserSettings { ReplaceRegexWithoutListTokens = [] };

		ps.ReplaceRegexWithoutListTokens.Clear();
		ps.ReplaceRegexWithoutListTokens.Add( new KeyValuePair<string, string>( "e.tended", "." ) );

		var input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
		var expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
		List<string> expectedOutputList = [];

		var actualRemovedTokens = HelperWorker.ReplaceTokens( ref input, ".", ps.ReplaceRegexWithoutListTokens, false );
		Assert.AreEqual( expected, input, " String compare " );
		CollectionAssert.AreEqual( expectedOutputList, actualRemovedTokens, " List compare " );
	}
}