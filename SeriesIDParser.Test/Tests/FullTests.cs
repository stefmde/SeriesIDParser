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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SeriesIDParser.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
[SuppressMessage( "ReSharper", "StringLiteralTypo" )]
public class FullTests
{
	[Test]
	public void FullTestDefault()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestSmallSeriesAndEpisodeChar()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestThreeSeriesAndEpisodeChar()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestSingleSeriesAndEpisodeChar()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S1E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S1E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestDifferentSeriesAndEpisodeChar()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestTokenDouble()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai.Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestCustomSpacer()
	{
		var parserSettings = new ParserSettings { NewSpacingChar = '-', CacheMode = CacheMode.None };
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );

		Assert.AreEqual( "Dubai-Airport-S01E05-Teil5", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Dubai-Airport-S01E05-Teil5-720p-DOKU-German-x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Teil5", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Dubai-Airport", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		CollectionAssert.AreEqual( new List<int> { 5 }, parserResult.Episodes.ToList(), "-Episodes " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<string> { "German", "DOKU", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestReleaseGroup()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv" );

		Assert.AreEqual( "Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Better.Call.Saul", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "h264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( "iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 10 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DUBBED", "h264", "WebHD" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestReleaseGroupWithSpaces()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv" );

		Assert.AreEqual( "Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Better.Call.Saul", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "h264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( "iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 10 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DUBBED", "h264", "WebHD" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestReleaseGroupWithSpacesAndExtension()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi" );

		Assert.AreEqual( "Better.Call.Saul.S02E10", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD.avi", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".avi", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Better.Call.Saul", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "h264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( "iNFOTv", parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 10 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DUBBED", "h264", "WebHD" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestMovieWithExtension()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv" );

		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.1080p.BluRay.EXTENDED.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( false, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( -1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Der.Hobbit.Smaugs.Einoede", parserResult.Title, "-Title " );
		Assert.AreEqual( 2013, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int>(), parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "EXTENDED", "BluRay" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestMovieDefault()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264" );

		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.1991.1080p.DTS.German.x264", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( false, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( -1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.Title, "-Title " );
		Assert.AreEqual( 1991, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( "DTS", parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int>(), parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DTS", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestSeriesMultiResolutions()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv" );

		Assert.AreEqual( "Narcos.S02E01.Endlich.frei", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Narcos.S02E01.Endlich.frei.720p.DD51.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Narcos", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 1 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "DD51" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(),
									"-Resolution " );
	}

	[Test]
	public void FullTestSeriesDownSampled()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv" );

		Assert.AreEqual( "Narcos.S02E01.Endlich.frei", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Narcos.S02E01.Endlich.frei.1080p.AAC.DD51.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Narcos", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( "AAC", parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 1 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "AAC", "DD51" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }.OrderBy( x => x ).ToList(),
									parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTestSeriesMultiEpisodes()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Narcos.S02E01E02.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv" );

		Assert.AreEqual( "Narcos.S02E01E02.Endlich.frei", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Narcos.S02E01E02.Endlich.frei.720p.DD51.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Narcos.S02E01E02.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Narcos", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 1, 2 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "DD51" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(),
									"-Resolution " );
	}

	[Test]
	public void FullTestSeriesMultiEpisodesThree()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Narcos.S02E01E02E03.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv" );

		Assert.AreEqual( "Narcos.S02E01E02E03.Endlich.frei", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Narcos.S02E01E02E03.Endlich.frei.720p.DD51.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Narcos.S02E01E02E03.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Narcos", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 1, 2, 3 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "DD51" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(),
									"-Resolution " );
	}

	[Test]
	public void FullTestSeriesMultiEpisodesWithMissing()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "Narcos.S02E01E02E04.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv" );

		Assert.AreEqual( "Narcos.S02E01E02E04.Endlich.frei", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "Narcos.S02E01E02E04.Endlich.frei.720p.DD51.German.x264.mkv", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( true, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( "Endlich.frei", parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( ".mkv", parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( true, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "Narcos.S02E01E02E04.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( 2, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "Narcos", parserResult.Title, "-Title " );
		Assert.AreEqual( -1, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( string.Empty, parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( false, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_2DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int> { 1, 2, 4 }, parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "x264", "DD51" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(),
									"-Resolution " );
	}

	[Test]
	public void FullTest3DDefault()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "A.Chinese.Ghost.Story.3.3D.1991.German.DTS.1080p.BD9.x264" );

		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.1991.1080p.3D.DTS.German.x264", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( false, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.3D.1991.German.DTS.1080p.BD9.x264", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( -1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.Title, "-Title " );
		Assert.AreEqual( 1991, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( "DTS", parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_3DAny, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int>(), parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DTS", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTest3DHouDefault()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "A.Chinese.Ghost.Story.3.3D.HOU.1991.German.DTS.1080p.BD9.x264" );

		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.1991.1080p.3D.HOU.DTS.German.x264", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( false, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.3D.HOU.1991.German.DTS.1080p.BD9.x264", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( -1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.Title, "-Title " );
		Assert.AreEqual( 1991, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( "DTS", parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_3DHOU, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int>(), parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DTS", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}

	[Test]
	public void FullTest3DHsbsDefault()
	{
		var parserSettings = new ParserSettings();
		var seriesIDParser = new SeriesIDParser( parserSettings );
		var parserResult = seriesIDParser.Parse( "A.Chinese.Ghost.Story.3.3D.HSBS.1991.German.DTS.1080p.BD9.x264" );

		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.FullTitle, "-FullTitle " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.1991.1080p.3D.HSBS.DTS.German.x264", parserResult.ParsedString, "-ParsedString " );
		Assert.AreEqual( false, parserResult.IsMultiEpisode, "-IsMultiEpisode " );
		Assert.AreEqual( string.Empty, parserResult.EpisodeTitle, "-EpisodeTitle " );
		Assert.AreEqual( string.Empty, parserResult.FileExtension, "-FileExtension " );
		Assert.AreEqual( false, parserResult.IsSeries, "-IsSeries " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3.3D.HSBS.1991.German.DTS.1080p.BD9.x264", parserResult.OriginalString, "-OriginalString manipulated " );
		Assert.AreEqual( -1, parserResult.Season, "-Season " );
		Assert.AreEqual( State.Notice, parserResult.State, "-State " );
		Assert.AreEqual( "A.Chinese.Ghost.Story.3", parserResult.Title, "-Title " );
		Assert.AreEqual( 1991, parserResult.Year, "-Year " );
		Assert.AreEqual( '.', parserResult.OldSpacingChar, "-DetectedOldSpacingChar " );
		Assert.AreEqual( "DTS", parserResult.AudioCodec, "-AudioCodec " );
		Assert.AreEqual( "x264", parserResult.VideoCodec, "-VideoCodec " );
		Assert.AreEqual( true, parserResult.Is3D, "-Is3D" );
		Assert.AreEqual( DimensionalType.Dimension_3DHSBS, parserResult.DimensionalType, "-DimensionalType " );
		Assert.AreEqual( string.Empty, parserResult.ReleaseGroup, "-ReleaseGroup " );
		CollectionAssert.AreEqual( new List<int>(), parserResult.Episodes.ToList(), "-Episodes " );
		CollectionAssert.AreEqual( new List<string> { "German", "DTS", "x264" }.OrderBy( x => x ).ToList(), parserResult.RemovedTokens.OrderBy( x => x ).ToList(), "-RemovedTokens " );
		CollectionAssert.AreEqual( new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }.OrderBy( x => x ).ToList(), parserResult.Resolutions.OrderBy( x => x ).ToList(), "-Resolution " );
	}
}