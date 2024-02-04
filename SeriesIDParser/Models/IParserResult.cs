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
using System.IO;

namespace SeriesIDParser.Models;

public interface IParserResult
{
	/// <summary>
	///     Use only for unit tests. Do NOT set to public
	/// </summary>
	/// <summary>
	///     Returns the full series string for Series, title for movies. Property is cached. string.Empty on error
	/// </summary>
	string FullTitle { get; }

	/// <summary>
	///     Contains the string that was computed by the parser. Property is cached. string.Empty on error
	/// </summary>
	string ParsedString { get; }

	/// <summary>
	///     Contains the ID-String of a series e.g. S01E05. Property is cached. string.Empty on error
	/// </summary>
	string IDString { get; }

	/// <summary>
	///     Shows if a Episode is a MultiEpisode with more than one Episode in one file. Default: false
	/// </summary>
	bool IsMultiEpisode { get; }

	/// <summary>
	///     Contains the ParserSettings object used to generate this result object
	/// </summary>
	IParserSettings ParserSettingsUsed { get; }

	/// <summary>
	///     Contains the state information provided by each module
	/// </summary>
	List<CoreParserModuleStateResult> ModuleStates { get; }

	/// <summary>
	///     Contains tokens which are removed by the parser as string list
	/// </summary>
	List<string> RemovedTokens { get; }

	/// <summary>
	///     Contains the FileInfo that is given to the parser
	/// </summary>
	FileInfo FileInfo { get; }

	/// <summary>
	///     Contains the file-extension or string.Empty
	/// </summary>
	string FileExtension { get; }

	/// <summary>
	///     Contains the string that is given to the parser
	/// </summary>
	string OriginalString { get; }

	/// <summary>
	///     Contains the char who are detected as the old spacing char
	/// </summary>
	char OldSpacingChar { get; }

	/// <summary>
	///     Contains the series title or the movie name, depends on IsSeries
	/// </summary>
	string Title { get; }

	/// <summary>
	///     Contains the state of the object e.g. OK_SUCCESS
	/// </summary>
	State State { get; }

	/// <summary>
	///     Contains the dimensionalType of the object e.g. Dimension_3DHOU
	/// </summary>
	DimensionalType DimensionalType { get; }

	/// <summary>
	///     Contains the Exception if any occurs. Default: null
	/// </summary>
	Exception Exception { get; }

	/// <summary>
	///     Specifies if the object contains a series or a movie. Default: false
	/// </summary>
	bool IsSeries { get; }

	/// <summary>
	///     Specifies if the movie is a 3D series or a movie. Default: false
	/// </summary>
	bool Is3D { get; }

	/// <summary>
	///     Contains the AudioCodec if one is found. string.Empty on error
	/// </summary>
	string AudioCodec { get; }

	/// <summary>
	///     Contains the VideoCodec if one is found. string.Empty on error
	/// </summary>
	string VideoCodec { get; }

	/// <summary>
	///     Contains the episode title if object state is series. string.Empty on error
	/// </summary>
	string EpisodeTitle { get; }

	/// <summary>
	///     Contains the season id if object state is series. -1 on error
	/// </summary>
	int Season { get; }

	/// <summary>
	///     Contains the episode id if object state is series.
	/// </summary>
	List<int> Episodes { get; }

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	int Year { get; }

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	TimeSpan ProcessingDuration { get; }

	/// <summary>
	///     Returns the resolution as enum. UNKNOWN on error
	/// </summary>
	List<ResolutionsMap> Resolutions { get; }

	/// <summary>
	///     Contains the release group string if contained in the source. string.Empty on error
	/// </summary>
	string ReleaseGroup { get; }

	/// <summary>
	/// </summary>
	/// <returns>FullTitle and resolution. string.Empty on error</returns>
	string ToString();
}