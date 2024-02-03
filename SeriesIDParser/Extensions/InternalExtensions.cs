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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SeriesIDParser.Models;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Extensions;

internal static class InternalExtensions
{
	internal static ParserResult ToParserResult( this MediaData mediaData, ParserSettings parserSettings )
	{
		ParserResult result = new ParserResult( mediaData.OriginalString, parserSettings, mediaData.AudioCodec, mediaData.VideoCodec,
												mediaData.ProcessingDuration, mediaData.Resolutions, mediaData.Season, mediaData.Episodes, mediaData.Year,
												mediaData.DetectedOldSpacingChar, mediaData.Exception, mediaData.IsSeries, mediaData.RemovedTokens, mediaData.State,
												mediaData.ModuleStates, mediaData.FileExtension, mediaData.FileInfo, mediaData.Title, mediaData.EpisodeTitle, mediaData.ReleaseGroup,
												mediaData.DimensionalType );

		return result;
	}
}