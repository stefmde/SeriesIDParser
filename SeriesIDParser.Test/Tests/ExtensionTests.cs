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
using System.IO;
using System.Linq;
using NUnit.Framework;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ExtensionTests
{
	private const string Title = "Der.Regenmacher";

	// ### Single File - String
	// ##################################################
	[Test]
	public void ParseSeriesIDStringDefault()
	{
		var parserResult = Constants.SeriesFile.ParseSeriesID();
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[Test]
	public void ParseSeriesIDStringNullSettings()
	{
		var parserResult = Constants.SeriesFile.ParseSeriesID( null );
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[Test]
	public void ParseSeriesIDStringEmptyInput()
	{
		var parserResult = string.Empty.ParseSeriesID();
		Assert.IsTrue( parserResult.State == State.Error );
		Assert.IsTrue( parserResult.Exception == null );
	}

	// ### Single File - FileInfo
	// ##################################################
	[Test]
	public void ParseSeriesIDFileInfoDefault()
	{
		var parserResult = new FileInfo( Constants.SeriesFilePath ).ParseSeriesID();
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[Test]
	public void ParseSeriesIDFileInfoNullInput()
	{
		var parserSettings = new ParserSettings();
		var parserResult = ((FileInfo)null).ParseSeriesID( parserSettings );
		Assert.IsTrue( parserResult.State == State.Error );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[Test]
	public void ParseSeriesIDFileInfoNullSetting()
	{
		var parserResult = new FileInfo( Constants.SeriesFilePath ).ParseSeriesID( null );
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	// ### Multi - String
	// ##################################################
	[Test]
	public void ParseSeriesIDPathStringDefault()
	{
		var parserResults = Constants.TestDataDirectoryCleanRoot.ParseSeriesIDPath();
		Assert.IsTrue( parserResults.Count() == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries )?.Season == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries )?.Title == Title );
	}

	[Test]
	public void ParseSeriesIDPathStringCustomSettings()
	{
		var parserResults = Constants.TestDataDirectoryCleanRoot.ParseSeriesIDPath( new ParserSettings() );
		Assert.IsTrue( parserResults.Count() == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries )?.Season == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries )?.Title == Title );
	}

	[Test]
	public void ParseSeriesIDPathNullString()
	{
		var parserSettings = new ParserSettings();
		var parserResults = ((string)null).ParseSeriesIDPath( parserSettings );
		Assert.IsTrue( !parserResults.Any() );
	}

	[Test]
	public void ParseSeriesIDPathNullSettings()
	{
		var directoryInfo = Constants.TestDataDirectoryCleanRoot;
		var parserResults = directoryInfo.ParseSeriesIDPath( null );
		Assert.IsTrue( parserResults.Count() == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries )?.Season == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries )?.Title == Title );
	}

	// ### Multi - DirectoryInfo
	// ##################################################
	[Test]
	public void ParseSeriesIDPathDirectoryInfoDefault()
	{
		var directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryCleanRoot );
		var parserResults = directoryInfo.ParseSeriesIDPath();
		Assert.IsTrue( parserResults.Count() == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries )?.Season == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries )?.Title == Title );
	}

	[Test]
	public void ParseSeriesIDPathDirectoryInfoNull()
	{
		var parserResults = ((DirectoryInfo)null).ParseSeriesIDPath();
		Assert.IsTrue( !parserResults.Any() );
	}

	[Test]
	public void ParseSeriesIDPathDirectoryInfoNullSetting()
	{
		var directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryCleanRoot );
		var parserResults = directoryInfo.ParseSeriesIDPath( null );
		Assert.IsTrue( parserResults.Count() == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries )?.Season == 2 );
		Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries )?.Title == Title );
	}
}