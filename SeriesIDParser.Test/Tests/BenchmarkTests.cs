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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestFixture (Description = "Runs some BenchmarkTests against the library. Results in TestExplorer -> Summary/Details")]
public class BenchmarkTests
{
	private const int BenchmarkLoopCount = 10000;

	// ### Series
	// ##################################################
	[Test(Description = "Series - Benchmarks a series string without cache")]
	public void BenchmarkSeriesNoCache()
	{
		var start = DateTime.Now;
		var seriesId = new SeriesIDParser();

		for (var i = 0; i < BenchmarkLoopCount; i++)
		{
			seriesId.Parse( Constants.SeriesFile );
		}

		var totalDuration = DateTime.Now - start;
		PrintTimeSpans( nameof(BenchmarkSeriesNoCache), totalDuration );
		totalDuration.TotalMilliseconds.Should().BeLessThan( 200 );
	}

	/// <summary>
	///     Benchmarks a series string with cache
	/// </summary>
	[Test(Description = "Series - Benchmarks a series string with cache")]
	public void BenchmarkSeriesCache()
	{
		var start = DateTime.Now;
		var seriesIdParser = new SeriesIDParser();

		for (var i = 0; i < BenchmarkLoopCount; i++)
		{
			seriesIdParser.Parse( Constants.SeriesFile );
		}

		var totalDuration = DateTime.Now - start;
		PrintTimeSpans( nameof(BenchmarkSeriesCache), totalDuration );
		totalDuration.TotalMilliseconds.Should().BeLessThan( 200 );
	}

	// ### Movie
	// ##################################################
	[Test(Description = "Movie - Benchmarks a movie string without cache")]
	public void BenchmarkMovieNoCache()
	{
		var start = DateTime.Now;
		var seriesIdParser = new SeriesIDParser();

		for (var i = 0; i < BenchmarkLoopCount; i++)
		{
			seriesIdParser.Parse( Constants.MovieFile );
		}

		var totalDuration = DateTime.Now - start;
		PrintTimeSpans( nameof(BenchmarkMovieNoCache), totalDuration );
		totalDuration.TotalMilliseconds.Should().BeLessThan( 200 );
	}

	/// <summary>
	///     Benchmarks a movie string with cache
	/// </summary>
	[Test(Description = "Movie - Benchmarks a movie string with cache")]
	public void BenchmarkMovieCache()
	{
		var start = DateTime.Now;
		var seriesIdParser = new SeriesIDParser();

		for (var i = 0; i < BenchmarkLoopCount; i++)
		{
			seriesIdParser.Parse( Constants.MovieFile );
		}

		var totalDuration = DateTime.Now - start;
		PrintTimeSpans( nameof(BenchmarkMovieCache), totalDuration );
		totalDuration.TotalMilliseconds.Should().BeLessThan( 200 );
	}

	private static void PrintTimeSpans( string functionName, TimeSpan totalTs)
	{
		var singleTs = totalTs / BenchmarkLoopCount;
		Console.WriteLine($"{functionName} single duration: {singleTs.Seconds} s {singleTs.Milliseconds} ms {singleTs.Ticks} ticks");
		Console.WriteLine($"{functionName} total duration: {totalTs.Seconds} s {totalTs.Milliseconds} ms {totalTs.Ticks} ticks");
	}
}