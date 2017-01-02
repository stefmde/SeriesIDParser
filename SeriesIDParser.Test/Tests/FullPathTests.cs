using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;

namespace SeriesIDParser.Test.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FullPathTests
	{
		// ### Clean
		// ##################################################
		[TestMethod]
		public void FullPathTestCleanAsString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Constants.TestDataDirectoryCleanRoot);
			Assert.IsTrue(parserResults.Count() == 2);
		}


		[TestMethod]
		public void FullPathTestCleanAsDirectoryInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.TestDataDirectoryCleanRoot);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
			Assert.IsTrue(parserResults.Count() == 2);
		}


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
		[TestMethod]
		public void FullPathTestDirtyAsString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Constants.TestDataDirectoryDirtyRoot);
			Assert.IsTrue(parserResults.Count() == 1);
		}


		[TestMethod]
		public void FullPathTestDirtyAsDirectoryInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.TestDataDirectoryDirtyRoot);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
			Assert.IsTrue(parserResults.Count() == 1);
		}



		// ### Empty
		// ##################################################
		[TestMethod]
		public void FullPathTestEmptyAsString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Constants.TestDataDirectoryEmptyRoot);
			Assert.IsTrue(!parserResults.Any());
		}


		[TestMethod]
		public void FullPathTestEmptyAsDirectoryInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.TestDataDirectoryEmptyRoot);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
			Assert.IsTrue(!parserResults.Any());
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
}
