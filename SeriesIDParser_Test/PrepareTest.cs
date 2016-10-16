
// MIT License

// Copyright(c) 2016
// Stefan Müller, Stefm, https://gitlab.com/u/Stefm

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using SeriesIDParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser_Test
{
	public struct TestData
	{
		public string Actual;
		public string FullTitle;
		public string ParsedString;
		public SeriesID Expected;
		public ParserSettings Settings;
		public string Comment;
	}

	public class PrepareTest
	{
		public List<TestData> GetTestData()
		{
			List<TestData> testData = new List<TestData>();
			
			// Usual - Test001
		    SeriesID id = new SeriesID
		    {
		        Episodes = new List<int>() {5},
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test001"
			});



			// Small s-e - Test002
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test002"
			});



			// three s-e - Test003
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test003"
			});



			// single s-e - Test004
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test004"
			});



			// different s-e count - Test005
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test005"
			});



			// token doubble - Test006
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.720p.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai.Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test006"
			});



			// Usual with custom spacer - Test007
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 5 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Teil5",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "DOKU", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p},
		        Season = 1,
		        State = State.OK_SUCCESS,
		        Title = "Dubai-Airport",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai-Airport-S01E05-Teil5",
				ParsedString = "Dubai-Airport-S01E05-Teil5-720p-DOKU-German-x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true) { NewSpacingChar = '-' },
				Comment = "Test007"
			});



			//Remove Hoster-String - Test008
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 10 },
                IsMultiEpisode = false,
		        EpisodeTitle = string.Empty,
		        FileExtension = null,
		        IsSeries = true,
		        OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv",
		        RemovedTokens = new List<string>() {"German", "DUBBED", "h264", "WebHD"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.FullHD_1080p},
		        Season = 2,
		        State = State.OK_SUCCESS,
		        Title = "Better.Call.Saul",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "h264",
                ReleaseGroup = "iNFOTv"
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test008"
			});



			//Remove Hoster-String with spaces - Test009
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 10 },
                IsMultiEpisode = false,
		        EpisodeTitle = string.Empty,
		        FileExtension = null,
		        IsSeries = true,
		        OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv",
		        RemovedTokens = new List<string>() {"German", "DUBBED", "h264", "WebHD"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.FullHD_1080p},
		        Season = 2,
		        State = State.OK_SUCCESS,
		        Title = "Better.Call.Saul",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "h264",
                ReleaseGroup = "iNFOTv"
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test009"
			});



			//Remove Hoster-String with spaces and extension - Test010
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 10 },
                IsMultiEpisode = false,
		        EpisodeTitle = string.Empty,
		        FileExtension = "avi",
		        IsSeries = true,
		        OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi",
		        RemovedTokens = new List<string>() {"German", "DUBBED", "h264", "WebHD"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.FullHD_1080p},
		        Season = 2,
		        State = State.OK_SUCCESS,
		        Title = "Better.Call.Saul",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "h264",
                ReleaseGroup = "iNFOTv"
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD.avi",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test010"
			});



			// Movie with extension - Test011
		    id = new SeriesID
		    {
                Episodes = new List<int>(),
                IsMultiEpisode = false,
		        EpisodeTitle = string.Empty,
		        FileExtension = "mkv",
		        IsSeries = false,
		        OriginalString = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "x264", "EXTENDED", "BluRay"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.FullHD_1080p},
		        Season = -1,
		        State = State.OK_SUCCESS,
		        Title = "Der.Hobbit.Smaugs.Einoede",
		        Year = 2013,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Der.Hobbit.Smaugs.Einoede",
				ParsedString = "Der.Hobbit.Smaugs.Einoede.2013.1080p.BluRay.EXTENDED.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test011"
			});



			// Movie usual - Test012
		    id = new SeriesID
		    {
                Episodes = new List<int>(),
                IsMultiEpisode = false,
		        EpisodeTitle = string.Empty,
		        FileExtension = null,
		        IsSeries = false,
		        OriginalString = "A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264",
		        RemovedTokens = new List<string>() {"German", "DTS", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.FullHD_1080p},
		        Season = -1,
		        State = State.OK_SUCCESS,
		        Title = "A.Chinese.Ghost.Story.3",
		        Year = 1991,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "DTS",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "A.Chinese.Ghost.Story.3",
				ParsedString = "A.Chinese.Ghost.Story.3.1991.1080p.DTS.German.x264",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test012"
			});



			// Series multi Resolutions - Test013
		    id = new SeriesID
		    {
                Episodes = new List<int>() { 1 },
                IsMultiEpisode = false,
		        EpisodeTitle = "Endlich.frei",
		        FileExtension = "mkv",
		        IsSeries = true,
		        OriginalString = "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.720p.x264.mkv",
		        RemovedTokens = new List<string>() {"German", "x264"},
		        Resolutions = new List<ResolutionsMap> {ResolutionsMap.HD_720p, ResolutionsMap.FullHD_1080p},
		        Season = 2,
		        State = State.OK_SUCCESS,
		        Title = "Narcos",
		        Year = -1,
		        DetectedOldSpacingChar = '.',
		        AudioCodec = "",
		        VideoCodec = "x264",
                ReleaseGroup = string.Empty
		    };
		    testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Narcos.S02E01.Endlich.frei",
				ParsedString = "Narcos.S02E01.Endlich.frei.720p.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(true),
				Comment = "Test013"
            });



            // Series down sampled (Original String) - Test014
            id = new SeriesID
            {
                Episodes = new List<int>() { 1 },
                IsMultiEpisode = false,
                EpisodeTitle = "Endlich.frei",
                FileExtension = "mkv",
                IsSeries = true,
                OriginalString = "Narcos.S02E01.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv",
                RemovedTokens = new List<string>() { "AAC", "German", "x264" },
                Resolutions = new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p },
                Season = 2,
                State = State.OK_SUCCESS,
                Title = "Narcos",
                Year = -1,
                DetectedOldSpacingChar = '.',
                AudioCodec = "AAC",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
            };
            testData.Add(new TestData()
            {
                Actual = id.OriginalString,
                FullTitle = "Narcos.S02E01.Endlich.frei",
                ParsedString = "Narcos.S02E01.Endlich.frei.1080p.AAC.German.x264.mkv",
                Expected = id,
                Settings = new ParserSettings(true),
                Comment = "Test014"
            });



            // Multi Episode - Test015
            id = new SeriesID
            {
                Episodes = new List<int>() { 1, 2 },
                IsMultiEpisode = true,
                EpisodeTitle = "Endlich.frei",
                FileExtension = "mkv",
                IsSeries = true,
                OriginalString = "Narcos.S02E01E02.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv",
                RemovedTokens = new List<string>() { "AAC", "German", "x264" },
                Resolutions = new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p },
                Season = 2,
                State = State.OK_SUCCESS,
                Title = "Narcos",
                Year = -1,
                DetectedOldSpacingChar = '.',
                AudioCodec = "AAC",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
            };
            testData.Add(new TestData()
            {
                Actual = id.OriginalString,
                FullTitle = "Narcos.S02E01E02.Endlich.frei",
                ParsedString = "Narcos.S02E01E02.Endlich.frei.1080p.AAC.German.x264.mkv",
                Expected = id,
                Settings = new ParserSettings(true),
                Comment = "Test015"
            });



            // Multi Episode - Test016
            id = new SeriesID
            {
                Episodes = new List<int>() { 1, 2, 3 },
                IsMultiEpisode = true,
                EpisodeTitle = "Endlich.frei",
                FileExtension = "mkv",
                IsSeries = true,
                OriginalString = "Narcos.S02E01E02E03.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv",
                RemovedTokens = new List<string>() { "AAC", "German", "x264" },
                Resolutions = new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p },
                Season = 2,
                State = State.OK_SUCCESS,
                Title = "Narcos",
                Year = -1,
                DetectedOldSpacingChar = '.',
                AudioCodec = "AAC",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
            };
            testData.Add(new TestData()
            {
                Actual = id.OriginalString,
                FullTitle = "Narcos.S02E01E02E03.Endlich.frei",
                ParsedString = "Narcos.S02E01E02E03.Endlich.frei.1080p.AAC.German.x264.mkv",
                Expected = id,
                Settings = new ParserSettings(true),
                Comment = "Test016"
            });



            // Multi Episode - Test017
            id = new SeriesID
            {
                Episodes = new List<int>() { 1, 2, 4 },
                IsMultiEpisode = true,
                EpisodeTitle = "Endlich.frei",
                FileExtension = "mkv",
                IsSeries = true,
                OriginalString = "Narcos.S02E01E02E04.Endlich.frei.German.DD51.DL.1080p.NetflixUHD.AAC.x264.mkv",
                RemovedTokens = new List<string>() { "AAC", "German", "x264" },
                Resolutions = new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p },
                Season = 2,
                State = State.OK_SUCCESS,
                Title = "Narcos",
                Year = -1,
                DetectedOldSpacingChar = '.',
                AudioCodec = "AAC",
                VideoCodec = "x264",
                ReleaseGroup = string.Empty
            };
            testData.Add(new TestData()
            {
                Actual = id.OriginalString,
                FullTitle = "Narcos.S02E01E02E04.Endlich.frei",
                ParsedString = "Narcos.S02E01E02E04.Endlich.frei.1080p.AAC.German.x264.mkv",
                Expected = id,
                Settings = new ParserSettings(true),
                Comment = "Test017"
            });


			//_data.Add(new TestData() { actual = "The.Big.Bang.Theory.S09E12.Der.romantische.Asteroid.GERMAN.DL.DUBBED.1080p.WebHD.x264", expected = "x" });     // Usual
			//_data.Add(new TestData() { actual = "House.of.Cards.S04E04.Akt.der.Verzweiflung.German.DD51.Synced.DL.2160p.NetflixUHD.x264", expected = "" });     // Usual
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                // Usual
			//_data.Add(new TestData() { actual = "NCIS.S01E01.Air.Force.One.German.DD20.Dubbed.DL.720p.iTunesHD.AVC-TVS", expected = "" });                      // Resolution and hoster
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.2160p.WebHD.x264", expected = "" });                                // Find 2160p?
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.mkv", expected = "" });                            // Extension
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.MKV", expected = "" });                            // Extension large
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.avi", expected = "" });                            // Extension
			//_data.Add(new TestData() { actual = "Arrow.s04e03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                // Find of small s-e
			//_data.Add(new TestData() { actual = "ARROW.S04E03.AUFERSTEHUNG.GERMAN.DUBBED.DL.2160P.WEBHD.X264", expected = "" });                                // Large chars converted to Default
			//_data.Add(new TestData() { actual = "Arrow.s04e3102.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                              // Find s-e with four digets
			//_data.Add(new TestData() { actual = "S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                      // No title
			//_data.Add(new TestData() { actual = "059.-.Peter.und.das.Sofamobil", expected = "" });                                                             // Only episode on the beginning; Replace big spacer
			//																																				   // Movies
			//_data.Add(new TestData() { actual = "Black.Mass.2015.German.AC3D.DL.1080p.WEB-DL.h264.mkv", expected = "" });                                       // Usual
			//_data.Add(new TestData() { actual = "Sie nannten ihn Knochenbrecher", expected = "" });

			return testData;
		}
	}
}
