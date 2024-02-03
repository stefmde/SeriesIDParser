// MIT License
// 
// Copyright(c) 2016 - 2024
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

// using System;
// using System.Diagnostics.CodeAnalysis;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using SeriesIDParser.Models;
//
// namespace SeriesIDParser.Test.Tests;
//
// /// <summary>
// ///     Runs some benchmarktests against the library. Results in TestExplorer -> Summary/Details
// /// </summary>
// [ExcludeFromCodeCoverage]
// [TestClass]
// public class BenchmarkTests
// {
// 	private static readonly int _benchmarkLoopCount = 10000;
//
// 	// ### Series
// 	// ##################################################
// 	/// <summary>
// 	///     Benchmarks a series string without cache
// 	/// </summary>
// 	[TestMethod]
// 	public void BenchmarkSeriesNoCache()
// 	{
// 			DateTime start = DateTime.Now;
//
// 			ParserSettings parserSettings = new ParserSettings( true ) {CacheMode = CacheMode.None};
//
// 			SeriesID seriesId = new SeriesID( parserSettings );
//
// 			for (int i = 0; i < _benchmarkLoopCount; i++)
// 			{
// 				seriesId.Parse( Constants.SeriesFile );
// 			}
//
// 			TimeSpan totalDuration = DateTime.Now - start;
// 			TimeSpan singleDuration = new TimeSpan( totalDuration.Ticks / _benchmarkLoopCount );
// 			Console.WriteLine( "BenchmarkSeriesNoCache total duration: " + totalDuration.Seconds + "s " + totalDuration.Milliseconds + "ms " +
// 								totalDuration.Ticks + "ticks" );
// 			Console.WriteLine( "BenchmarkSeriesNoCache single duration: " + singleDuration.Seconds + "s " + singleDuration.Milliseconds + "ms " +
// 								singleDuration.Ticks + "ticks" );
// 		}
//
// 	/// <summary>
// 	///     Benchmarks a series string with cache
// 	/// </summary>
// 	[TestMethod]
// 	public void BenchmarkSeriesCache()
// 	{
// 			DateTime start = DateTime.Now;
// 			SeriesID seriesId = new SeriesID();
//
// 			for (int i = 0; i < _benchmarkLoopCount; i++)
// 			{
// 				seriesId.Parse( Constants.SeriesFile );
// 			}
//
// 			TimeSpan totalDuration = DateTime.Now - start;
// 			TimeSpan singleDuration = new TimeSpan( totalDuration.Ticks / _benchmarkLoopCount );
// 			Console.WriteLine( "BenchmarkSeriesCache total duration: " + totalDuration.Seconds + "s " + totalDuration.Milliseconds + "ms " +
// 								totalDuration.Ticks + "ticks" );
// 			Console.WriteLine( "BenchmarkSeriesCache single duration: " + singleDuration.Seconds + "s " + singleDuration.Milliseconds + "ms " +
// 								singleDuration.Ticks + "ticks" );
// 		}
//
// 	// ### Movie
// 	// ##################################################
// 	/// <summary>
// 	///     Benchmarks a movie string without cache
// 	/// </summary>
// 	[TestMethod]
// 	public void BenchmarkMovieNoCache()
// 	{
// 			DateTime start = DateTime.Now;
//
// 			ParserSettings parserSettings = new ParserSettings( true ) {CacheMode = CacheMode.None};
//
// 			SeriesID seriesId = new SeriesID( parserSettings );
//
// 			for (int i = 0; i < _benchmarkLoopCount; i++)
// 			{
// 				seriesId.Parse( Constants.MovieFile );
// 			}
//
// 			TimeSpan totalDuration = DateTime.Now - start;
// 			TimeSpan singleDuration = new TimeSpan( totalDuration.Ticks / _benchmarkLoopCount );
// 			Console.WriteLine( "BenchmarkMovieNoCache total duration: " + totalDuration.Seconds + "s " + totalDuration.Milliseconds + "ms " +
// 								totalDuration.Ticks + "ticks" );
// 			Console.WriteLine( "BenchmarkMovieNoCache single duration: " + singleDuration.Seconds + "s " + singleDuration.Milliseconds + "ms " +
// 								singleDuration.Ticks + "ticks" );
// 		}
//
// 	/// <summary>
// 	///     Benchmarks a movie string with cache
// 	/// </summary>
// 	[TestMethod]
// 	public void BenchmarkMovieCache()
// 	{
// 			DateTime start = DateTime.Now;
// 			SeriesID seriesId = new SeriesID();
//
// 			for (int i = 0; i < _benchmarkLoopCount; i++)
// 			{
// 				seriesId.Parse( Constants.MovieFile );
// 			}
//
// 			TimeSpan totalDuration = DateTime.Now - start;
// 			TimeSpan singleDuration = new TimeSpan( totalDuration.Ticks / _benchmarkLoopCount );
// 			Console.WriteLine( "BenchmarkMovieCache total duration: " + totalDuration.Seconds + "s " + totalDuration.Milliseconds + "ms " +
// 								totalDuration.Ticks + "ticks" );
// 			Console.WriteLine( "BenchmarkMovieCache single duration: " + singleDuration.Seconds + "s " + singleDuration.Milliseconds + "ms " +
// 								singleDuration.Ticks + "ticks" );
// 		}
// }