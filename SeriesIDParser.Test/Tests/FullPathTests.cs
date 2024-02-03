// MIT License
// 
// Copyright(c) 2016 - 2024
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


using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class FullPathTests
{
	// ### Clean
	// ##################################################
	// [TestMethod]
	// public void FullPathTestCleanAsString()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	SeriesID seriesIDParser = new SeriesID( parserSettings );
	// 	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath( Constants.TestDataDirectoryCleanRoot );
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// }

	// [TestMethod]
	// public void FullPathTestCleanAsDirectoryInfo()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	SeriesID seriesIDParser = new SeriesID( parserSettings );
	// 	DirectoryInfo directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryCleanRoot );
	// 	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath( directoryInfo );
	// 	Assert.IsTrue( parserResults.Count() == 2 );
	// }

	//[TestMethod]
	//public void FullPathTestAsFileInfo()
	//{
	//	ParserSettings parserSettings = new ParserSettings(true);
	//	SeriesID seriesIDParser = new SeriesID(parserSettings);
	//	FileInfo fileInfo = new FileInfo(Constants.MovieFilePath);
	//	ParserResult parserResult = seriesIDParser.Parse(fileInfo);
	//	Assert.IsTrue(parserResult != null);
	//}

	// ### Dirty
	// ##################################################
	// [TestMethod]
	// public void FullPathTestDirtyAsString()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	SeriesID seriesIDParser = new SeriesID( parserSettings );
	// 	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath( Constants.TestDataDirectoryDirtyRoot );
	// 	Assert.IsTrue( parserResults.Count() == 1 );
	// }

	// [TestMethod]
	// public void FullPathTestDirtyAsDirectoryInfo()
	// {
	// 	ParserSettings parserSettings = new ParserSettings( true );
	// 	SeriesID seriesIDParser = new SeriesID( parserSettings );
	// 	DirectoryInfo directoryInfo = new DirectoryInfo( Constants.TestDataDirectoryDirtyRoot );
	// 	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath( directoryInfo );
	// 	Assert.IsTrue( parserResults.Count() == 1 );
	// }

	// ### Empty
	// ##################################################
	[TestMethod]
	public void FullPathTestEmptyAsString()
	{
		ParserSettings parserSettings = new(true);
		SeriesIDParser seriesIDParser = new(parserSettings);
		var parserResults = seriesIDParser.ParsePath( Constants.TestDataDirectoryEmptyRoot );
		Assert.IsTrue( !parserResults.Any() );
	}

	[TestMethod]
	public void FullPathTestEmptyAsDirectoryInfo()
	{
		ParserSettings parserSettings = new(true);
		SeriesIDParser seriesIDParser = new(parserSettings);
		DirectoryInfo directoryInfo = new(Constants.TestDataDirectoryEmptyRoot);
		var parserResults = seriesIDParser.ParsePath( directoryInfo );
		Assert.IsTrue( !parserResults.Any() );
	}

	// ### Removed
	// ##################################################
	//[TestMethod]
	//public void FullPathTestRemovedAsString()
	//{
	//	ParserSettings parserSettings = new ParserSettings(true);
	//	SeriesID seriesIDParser = new SeriesID(parserSettings);
	//	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Constants.TestDataDirectoryRemovedRoot);
	//	Assert.IsTrue(!parserResults.Any());
	//}

	//[TestMethod]
	//public void FullPathTestRemovedAsDirectoryInfo()
	//{
	//	ParserSettings parserSettings = new ParserSettings(true);
	//	SeriesID seriesIDParser = new SeriesID(parserSettings);
	//	DirectoryInfo directoryInfo = new DirectoryInfo(Constants.TestDataDirectoryRemovedRoot);
	//	IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
	//	Assert.IsTrue(!parserResults.Any());
	//}
}