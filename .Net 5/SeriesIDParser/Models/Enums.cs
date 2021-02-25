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
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Models
{
	#region ParserResult
	/// <summary>
	///     Representing the series or movie resolution
	/// </summary>
	public enum ResolutionsMap
	{
		/// <summary>
		///     Value is not set or unknown. May cause or caused by an exception
		/// </summary>
		Unknown = 0,

		/// <summary>
		///     Resolution is SD or lower
		/// </summary>
		SD_Any = 1,

		/// <summary>
		///     Resolution is 720p HD
		/// </summary>
		HD_720p = 2,

		/// <summary>
		///     Resolution is 1080p FullHD
		/// </summary>
		FullHD_1080p = 4,

		/// <summary>
		///     Resolution is 2160p UltraHD4k
		/// </summary>
		UltraHD_2160p = 8,

		/// <summary>
		///     Resolution is 4320p UltraHD8k
		/// </summary>
		UltraHD8K_4320p = 16
	}

	/// <summary>
	///     Representing the object success state
	/// </summary>
	public enum State
	{
		/// <summary>
		///     Value is not set or unknown. Probably a marker for an exception
		/// </summary>
		Unknown = 0,

		/// <summary>
		///     Everything looks fine. Result should be consistant and valid but there are some notices
		/// </summary>
		Notice = 1,

		/// <summary>
		///     Everything looks fine. Result should be consistant and valid
		/// </summary>
		Success = 2,

		/// <summary>
		///     One or more warning occoured. Result could be constistant but don't have to be
		/// </summary>
		Warning = 4,

		/// <summary>
		///     One or more errors occoured. Result is probably invalid and shouldn't be used
		/// </summary>
		Error = 8
	}

	/// <summary>
	///     Defindes if the given movie or series is 2D, 3D and which type of it
	/// </summary>
	public enum DimensionalType
	{
		/// <summary>
		///     Value is not set or unknown. May cause or caused by an exception
		/// </summary>
		Unknown = 0,

		/// <summary>
		///     Movie is any type of 2D
		/// </summary>
		Dimension_2DAny = 1,

		/// <summary>
		///     Movie is any type of 3D
		/// </summary>
		Dimension_3DAny = 2,

		/// <summary>
		///     Movie is type of HSBS 3D
		/// </summary>
		Dimension_3DHSBS = 4,

		/// <summary>
		///     Movie is type of HOU 3D
		/// </summary>
		Dimension_3DHOU = 8
	}
	#endregion

	#region ParserSettings
	/// <summary>
	///     The properties for the ResolutionOutputBehavior
	/// </summary>
	public enum ResolutionOutputBehavior
	{
		/// <summary>
		///     Value is not set or unknown. May cause or caused by an exception
		/// </summary>
		Unknown,

		/// <summary>
		///     The preformated output strings contains all found resolution tokens
		/// </summary>
		AllFoundResolutions,

		/// <summary>
		///     The preformated output strings contains only the highest resolution token
		/// </summary>
		HighestResolution,

		/// <summary>
		///     The preformated output strings contains only the lowest resolution token
		/// </summary>
		LowestResolution
	}

	/// <summary>
	///     The properties for the ResolutionOutputBehavior
	/// </summary>
	public enum CacheMode
	{
		/// <summary>
		///     Value is not set or unknown. May cause or caused by an exception
		/// </summary>
		Unknown,

		/// <summary>
		///     Cache is running, used and is looking for exact names. Slower but way more exact. Default
		/// </summary>
		Advanced,

		/// <summary>
		///     Cache is running, used and but it is looking for lower and invariant names. Faster but less exact. May cause not
		///     1:1 matching OriginalString's
		/// </summary>
		Simple,

		/// <summary>
		///     Cache is disabled. Every string is fully parsed
		/// </summary>
		None
	}
	#endregion
}