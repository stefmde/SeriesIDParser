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
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class GetYearTests
{
	[TestMethod]
	public void GetYearTestDefault()
	{
		ParserSettings ps = new ParserSettings( true );
		Assert.AreEqual( 2013, HelperWorker.GetYear( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex ),
						"Should give a year" );
	}

	[TestMethod]
	public void GetYearTestToOld()
	{
		ParserSettings ps = new ParserSettings( true );
		Assert.AreEqual( -1, HelperWorker.GetYear( "Der.Hobbit.Smaugs.Einoede.1899.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex ),
						"Should give no year" );
	}

	[TestMethod]
	public void GetYearTestOldest()
	{
		ParserSettings ps = new ParserSettings( true );
		Assert.AreEqual( 1900, HelperWorker.GetYear( "Der.Hobbit.Smaugs.Einoede.1900.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex ),
						"Should give a year" );
	}

	[TestMethod]
	public void GetYearTestFuture()
	{
		ParserSettings ps = new ParserSettings( true );
		Assert.AreEqual( -1,
						HelperWorker.GetYear( "Der.Hobbit.Smaugs.Einoede." + DateTime.Now.AddYears( 1 ) + ".EXTENDED.German.DL.1080p.BluRay.x264.mkv",
											ps.YearParseRegex ), "Should give no year" );
	}

	[TestMethod]
	public void GetYearTestSpecialCharNoise()
	{
		ParserSettings ps = new ParserSettings( true );
		Assert.AreEqual( 2013, HelperWorker.GetYear( "Der,Hobbit-Smaugs;Einoedex2013?EXTENDED(German)DL/1080p*BluRay+x264#mkv", ps.YearParseRegex ),
						"Should give a year" );
	}
}