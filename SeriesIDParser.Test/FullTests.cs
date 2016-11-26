using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FullTests
	{
		[TestMethod]
		public void FullTestDefault()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSmallSeriesAndEpisodeChar()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestThreeSeriesAndEpisodeChar()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSingleSeriesAndEpisodeChar()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S1E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S1E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestDifferentSeriesAndEpisodeChar()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestTokenDubble()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai.Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestCustomSpacer()
		{
			ParserSettings parserSettings = new ParserSettings(true) { NewSpacingChar = '-' };
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");

			Assert.AreEqual("Dubai-Airport-S01E05-Teil5", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Dubai-Airport-S01E05-Teil5-720p-DOKU-German-x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Teil5", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Dubai-Airport", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			CollectionAssert.AreEqual(new List<int>() { 5 }, parserResult.Episodes.ToList(), "-Episodes ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DOKU", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestReleaseGroup()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv");

			Assert.AreEqual("Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual(String.Empty, parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(String.Empty, parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Better.Call.Saul", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("h264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual("iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 10 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DUBBED", "h264", "WebHD" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestReleaseGroupWithSpaces()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv");

			Assert.AreEqual("Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual(String.Empty, parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(String.Empty, parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Better.Call.Saul", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("h264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual("iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 10 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DUBBED", "h264", "WebHD" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestReleaseGroupWithSpacesAndExtension()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi");

			Assert.AreEqual("Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD.avi", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual(String.Empty, parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".avi", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Better.Call.Saul", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("h264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual("iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 10 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DUBBED", "h264", "WebHD" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestMovieWithExtension()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv");

			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.1080p.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual(String.Empty, parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(false, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(-1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede", parserResult.Title, "-Title ");
			Assert.AreEqual(2013, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>(), parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264", "EXTENDED", "BluRay" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestMovieDefault()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264");

			Assert.AreEqual("A.Chinese.Ghost.Story.3", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("A.Chinese.Ghost.Story.3.1991.1080p.DTS.German.x264", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual(String.Empty, parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(String.Empty, parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(false, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(-1, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("A.Chinese.Ghost.Story.3", parserResult.Title, "-Title ");
			Assert.AreEqual(1991, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual("DTS", parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>(), parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "DTS", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSeriesMultiResolutions()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv");

			Assert.AreEqual("Narcos.S02E01.Endlich.frei", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Narcos.S02E01.Endlich.frei.720p.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Narcos", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 1 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSeriesDownSampled()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv");

			Assert.AreEqual("Narcos.S02E01.Endlich.frei", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Narcos.S02E01.Endlich.frei.1080p.AAC.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(false, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Narcos", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual("AAC", parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 1 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264", "AAC" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSeriesMultiEpisodes()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Narcos.S02E01E02.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv");

			Assert.AreEqual("Narcos.S02E01E02.Endlich.frei", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Narcos.S02E01E02.Endlich.frei.720p.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(true, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Narcos.S02E01E02.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Narcos", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 1, 2 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSeriesMultiEpisodesThree()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Narcos.S02E01E02E03.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv");

			Assert.AreEqual("Narcos.S02E01E02E03.Endlich.frei", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Narcos.S02E01E02E03.Endlich.frei.720p.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(true, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Narcos.S02E01E02E03.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Narcos", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 1, 2, 3 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}


		[TestMethod]
		public void FullTestSeriesMultiEpisodesWithMissing()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			ParserResult parserResult = seriesIDParser.Parse("Narcos.S02E01E02E04.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv");

			Assert.AreEqual("Narcos.S02E01E02E04.Endlich.frei", parserResult.FullTitle, "-FullTitle ");
			Assert.AreEqual("Narcos.S02E01E02E04.Endlich.frei.720p.German.x264.mkv", parserResult.ParsedString, "-ParsedString ");
			Assert.AreEqual(true, parserResult.IsMultiEpisode, "-IsMultiEpisode ");
			Assert.AreEqual("Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle ");
			Assert.AreEqual(".mkv", parserResult.FileExtension, "-FileExtension ");
			Assert.AreEqual(true, parserResult.IsSeries, "-IsSeries ");
			Assert.AreEqual("Narcos.S02E01E02E04.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated ");
			Assert.AreEqual(2, parserResult.Season, "-Season ");
			Assert.AreEqual(State.OkSuccess, parserResult.State, "-State ");
			Assert.AreEqual("Narcos", parserResult.Title, "-Title ");
			Assert.AreEqual(-1, parserResult.Year, "-Year ");
			Assert.AreEqual('.', parserResult.DetectedOldSpacingChar, "-DetectedOldSpacingChar ");
			Assert.AreEqual(String.Empty, parserResult.AudioCodec, "-AudioCodec ");
			Assert.AreEqual("x264", parserResult.VideoCodec, "-VideoCodec ");
			Assert.AreEqual(String.Empty, parserResult.ReleaseGroup, "-ReleaseGroup ");
			CollectionAssert.AreEqual(new List<int>() { 1, 2, 4 }, parserResult.Episodes.ToList(), "-Episodes ");
			CollectionAssert.AreEqual(new List<string>() { "German", "x264" }.OrderBy(x => x).ToList(), parserResult.RemovedTokens.OrderBy(x => x).ToList(), "-RemovedTokens ");
			CollectionAssert.AreEqual(new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy(x => x).ToList(), parserResult.Resolutions.OrderBy(x => x).ToList(), "-Resolution ");
		}
	}
}
