using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class ExtensionTests
	{
		[TestMethod]
		public void ParseSeriesIDStringDefault()
		{
			ParserResult parserResult = Constants.SeriesFile.ParseSeriesID();
			Assert.IsTrue(parserResult.IsSeries);
			Assert.IsTrue(parserResult.Season == 2);
			Assert.IsTrue(parserResult.FileExtension == ".mkv");
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDStringNullSettings()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			parserSettings = null;
			ParserResult parserResult = Constants.SeriesFile.ParseSeriesID(parserSettings);
			Assert.IsTrue(parserResult.IsSeries);
			Assert.IsTrue(parserResult.Season == 2);
			Assert.IsTrue(parserResult.FileExtension == ".mkv");
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDStringEmptyInput()
		{
			ParserResult parserResult = String.Empty.ParseSeriesID();
			Assert.IsTrue(parserResult.State == State.ErrEmptyOrToShortArgument);
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDFileInfoDefault()
		{
			ParserResult parserResult = new FileInfo(Constants.SeriesFilePath).ParseSeriesID();
			Assert.IsTrue(parserResult.IsSeries);
			Assert.IsTrue(parserResult.Season == 2);
			Assert.IsTrue(parserResult.FileExtension == ".mkv");
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDFileInfoNullInput()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			FileInfo file = new FileInfo(Constants.SeriesFilePath);
			file = null;
			ParserResult parserResult = file.ParseSeriesID(parserSettings);
			Assert.IsTrue(parserResult.State == State.ErrEmptyOrToShortArgument);
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDFileInfoNullSetting()
		{
			ParserResult parserResult = new FileInfo(Constants.SeriesFilePath).ParseSeriesID(null);
			Assert.IsTrue(parserResult.IsSeries);
			Assert.IsTrue(parserResult.Season == 2);
			Assert.IsTrue(parserResult.FileExtension == ".mkv");
			Assert.IsTrue(parserResult.Exception == null);
		}


		[TestMethod]
		public void ParseSeriesIDPathDirectoryInfoDefault()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.Path);
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath();
			Assert.IsTrue(parserResults.Count() == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => x.IsSeries).Season == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => !x.IsSeries).Title == "Der.Regenmacher");
		}


		[TestMethod]
		public void ParseSeriesIDPathDirectoryInfoNull()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = null;
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath();
			Assert.IsTrue(!parserResults.Any());
		}


		[TestMethod]
		public void ParseSeriesIDPathDirectoryInfoNullSetting()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.Path);
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath(null);
			Assert.IsTrue(parserResults.Count() == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => x.IsSeries).Season == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => !x.IsSeries).Title == "Der.Regenmacher");
		}


		[TestMethod]
		public void ParseSeriesIDPathStringDefault()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			string directoryInfo = Constants.Path;
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath();
			Assert.IsTrue(parserResults.Count() == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => x.IsSeries).Season == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => !x.IsSeries).Title == "Der.Regenmacher");
		}


		[TestMethod]
		public void ParseSeriesIDPathNullString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			string directoryInfo = null;
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath(parserSettings);
			Assert.IsTrue(!parserResults.Any());
		}


		[TestMethod]
		public void ParseSeriesIDPathNullSettings()
		{
			string directoryInfo = Constants.Path;
			IEnumerable<ParserResult> parserResults = directoryInfo.ParseSeriesIDPath(null);
			Assert.IsTrue(parserResults.Count() == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => x.IsSeries).Season == 2);
			Assert.IsTrue(parserResults.LastOrDefault(x => !x.IsSeries).Title == "Der.Regenmacher");
		}
	}
}
