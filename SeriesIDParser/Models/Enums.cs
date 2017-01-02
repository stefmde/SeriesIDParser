
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

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SeriesIDParser.Test")]
namespace SeriesIDParser.Models
{
	#region ParserResult
	/// <summary>
	/// Representing the series or movie resolution
	/// </summary>
	public enum ResolutionsMap
	{
		Unknown = 0,
		SD_Any = 1,
		HD_720p = 2,
		FullHD_1080p = 3,
		UltraHD_2160p = 4,
		UltraHD8K_4320p = 5
	}

	/// <summary>
	/// Representing the object success state
	/// </summary>
	[Flags]
	public enum State
	{
		Unknown = 0,
		OkSuccess = 1,
		WarnErrorOrWarningOccurred = 2,
		WarnNoTitleFound = 4,
		ErrEmptyOrToShortArgument = 8,
		ErrIDNotFound = 16,
		ErrUnknownError = 32
	}
	#endregion

	#region ParserSettings
	/// <summary>
	/// The properties for the ResolutionOutputBehavior
	/// </summary>
	public enum ResolutionOutputBehavior
	{
		/// <summary>
		/// Value is not set or unknown. May cause an exception
		/// </summary>
		Unknown,
		AllFoundResolutions,
		HighestResolution,
		LowestResolution
	}

	/// <summary>
	/// The properties for the ResolutionOutputBehavior
	/// </summary>
	public enum CacheMode
	{
		/// <summary>
		/// Value is not set or unknown. May cause an exception
		/// </summary>
		Unknown,

		/// <summary>
		/// Cache is running, used and is looking for exact names. Slower but way more exact. Default
		/// </summary>
		Advanced,

		/// <summary>
		/// Cache is running, used and but it is looking for lower and invariant names. Faster but less exact. May cause not 1:1 matching OriginalString's
		/// </summary>
		Simple,

		/// <summary>
		/// Cache is disabled. Every string is fully parsed
		/// </summary>
		None
	}
	#endregion
}
