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
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class ExtensionTests
{
	// ### Single File - String
	// ##################################################
	[TestMethod]
	public void ParseSeriesIDStringDefault()
	{
		var parserResult = Constants.SeriesFile.ParseSeriesID();
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[TestMethod]
	public void ParseSeriesIDStringNullSettings()
	{
		ParserSettings parserSettings = new( true );
		parserSettings = null;
		var parserResult = Constants.SeriesFile.ParseSeriesID( parserSettings );
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[TestMethod]
	public void ParseSeriesIDStringEmptyInput()
	{
		var parserResult = String.Empty.ParseSeriesID();
		Assert.IsTrue( parserResult.State == State.Error );
		Assert.IsTrue( parserResult.Exception == null );
	}

	// ### Single File - FileInfo
	// ##################################################
	[TestMethod]
	public void ParseSeriesIDFileInfoDefault()
	{
		var parserResult = new FileInfo( Constants.SeriesFilePath ).ParseSeriesID();
		Assert.IsTrue( parserResult.IsSeries );
		Assert.IsTrue( parserResult.Season == 2 );
		Assert.IsTrue( parserResult.FileExtension == ".mkv" );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[TestMethod]
	public void ParseSeriesIDFileInfoNullInput()
	{
		ParserSettings parserSettings = new( true );
		FileInfo file = new( Constants.SeriesFilePath );
		file = null;
		var parserResult = file.ParseSeriesID( parserSettings );
		Assert.IsTrue( parserResult.State == State.Error );
		Assert.IsTrue( parserResult.Exception == null );
	}

	[TestMethod]
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
	// [TestMethod]
	// public void ParseSeriesIDPathStringDefault()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	string directoryInfo = Constants.TestDataDirectoryCleanRoot;
	// 	IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath();
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries ).Season == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries ).Title == "Der.Regenmacher" );
	// }

	// [TestMethod]
	// public void ParseSeriesIDPathNullString()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	string directoryInfo = null;
	// 	IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath( parserSettings );
	// 	Assert.IsTrue( !parserResults.Any() );
	// }

	// [TestMethod]
	// public void ParseSeriesIDPathNullSettings()
	// {
	// 	string directoryInfo = Constants.TestDataDirectoryCleanRoot;
	// 	IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath( null );
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries ).Season == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries ).Title == "Der.Regenmacher" );
	// }

	// ### Multi - DirectoryInfo
	// ##################################################
	// [TestMethod]
	// public void ParseSeriesIDPathDirectoryInfoDefault()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	DirectoryInfo directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryCleanRoot );
	// 	IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath();
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries ).Season == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries ).Title == "Der.Regenmacher" );
	// }

	[TestMethod]
	public void ParseSeriesIDPathDirectoryInfoNull()
	{
		ParserSettings parserSettings = new( true );
		SeriesIdParser seriesIDParser = new( parserSettings );
		DirectoryInfo directoryInfo = null;
		var parserResults = directoryInfo.ParseSeriesIDPath();
		Assert.IsTrue( !parserResults.Any() );
	}

	// [TestMethod]
	// public void ParseSeriesIDPathDirectoryInfoNullSetting()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	DirectoryInfo directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryCleanRoot );
	// 	IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath( null );
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => x.IsSeries ).Season == 2 );
	// 	Assert.IsTrue( parserResults.LastOrDefault( x => !x.IsSeries ).Title == "Der.Regenmacher" );
	// }
}