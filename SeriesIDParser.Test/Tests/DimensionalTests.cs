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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SeriesIDParser.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SeriesIDParser.Test.Tests;

/// <summary>
///     Runs some Dimensional 2D/3D against the library. Results in TestExplorer -> Summary/Details
/// </summary>
[ExcludeFromCodeCoverage]
[TestFixture]
public class DimensionalTests
{
	[Test]
	public void DimensionalTestDefaultHou()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.3D.HOU.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.3D.HOU.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_3DHOU, parserResult.DimensionalType, "-DimensionalType " );
	}

	[Test]
	public void DimensionalTestDefaultHsbs()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.3D.HSBS.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.3D.HSBS.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_3DHSBS, parserResult.DimensionalType, "-DimensionalType " );
	}

	[Test]
	public void DimensionalTestDefaultSingle()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.3D.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.3D.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_3DAny, parserResult.DimensionalType, "-DimensionalType " );
	}

	[Test]
	public void DimensionalTestNoThirdDimension()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
	}

	[Test]
	public void DimensionalTestMissingSpacerHOU()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.3DHOU.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.3D.HOU.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_3DHOU, parserResult.DimensionalType, "-DimensionalType " );
	}

	[Test]
	public void DimensionalTest2DToken()
	{
		var parserSettings = new ParserSettings { DimensionalString2DAny = "2D", CacheMode = CacheMode.None };
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.2D.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D " );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
	}
}