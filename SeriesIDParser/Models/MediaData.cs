﻿// MIT License
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
using System.Linq;
using System.Text;

namespace SeriesIDParser.Models;

internal class MediaData
{
	/// <summary>
	///     Contains tokens which are removed by the parser as string list
	/// </summary>
	internal List<string> RemovedTokens { get; set; } = [];

	/// <summary>
	///     Contains the FileInfo that is given to the parser
	/// </summary>
	internal FileInfo FileInfo { get; set; }

	/// <summary>
	///     Contains the file-extension or string.Empty
	/// </summary>
	internal string FileExtension { get; set; } = string.Empty;

	/// <summary>
	///     Contains the string that is given to the parser
	/// </summary>
	internal string OriginalString { get; set; } = string.Empty;

	/// <summary>
	///     Contains the char who are detected as the old spacing char
	/// </summary>
	internal char DetectedOldSpacingChar { get; set; }

	/// <summary>
	///     Contains the series title or the movie name, depends on IsSeries
	/// </summary>
	internal string Title { get; set; } = string.Empty;

	/// <summary>
	///     Contains the state of the object e.g. OK_SUCCESS
	/// </summary>
	internal State State { get; set; } = State.Unknown;

	/// <summary>
	///     Contains the state information provided by each module
	/// </summary>
	public List<CoreParserModuleStateResult> ModuleStates { get; internal set; } = [];

	/// <summary>
	///     Contains the Exception if any occurs. Default: null
	/// </summary>
	internal Exception Exception { get; set; }

	/// <summary>
	///     Specifies if the object contains a series or a movie. Default: false
	/// </summary>
	internal bool IsSeries { get; set; }

	/// <summary>
	///     Contains the AudioCodec if one is found. string.Empty on error
	/// </summary>
	internal string AudioCodec { get; set; } = string.Empty;

	/// <summary>
	///     Contains the VideoCodec if one is found. string.Empty on error
	/// </summary>
	internal string VideoCodec { get; set; } = string.Empty;

	/// <summary>
	///     Contains the episode title if object state is series. string.Empty on error
	/// </summary>
	internal string EpisodeTitle { get; set; } = string.Empty;

	/// <summary>
	///     Contains the season id if object state is series. -1 on error
	/// </summary>
	internal int Season { get; set; } = -1;

	/// <summary>
	///     Contains the episode id if object state is series.
	/// </summary>
	internal List<int> Episodes { get; set; } = [];

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	internal int Year { get; set; } = -1;

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	internal TimeSpan ProcessingDuration { get; set; }

	/// <summary>
	///     Returns the resolution as enum. UNKNOWN on error
	/// </summary>
	internal List<ResolutionsMap> Resolutions { get; set; } = [];

	/// <summary>
	///     Contains the release group string if contained in the source. string.Empty on error
	/// </summary>
	internal string ReleaseGroup { get; set; } = string.Empty;

	/// <summary>
	///     Contains the dimensionalType of the object e.g. Dimension_3DHou
	/// </summary>
	public DimensionalType DimensionalType { get; set; } = DimensionalType.Unknown;
}