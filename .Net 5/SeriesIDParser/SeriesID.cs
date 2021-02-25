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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SeriesIDParser.Caching;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;
using SeriesIDParser.Worker.CoreParserModules;

namespace SeriesIDParser
{
	/// <summary>
	///     The result object representing the series or movie string
	/// </summary>
	public class SeriesID
	{
		#region Fields
		private CoreParserResult _coreParserResult;
		private readonly ParserSettings _parserSettings = new ParserSettings( true );
		private readonly DateTime _parseStartTime = new DateTime();
		private FileInfo _fileInfo;
		private readonly bool _cacheEnabled;
		#endregion Fields

		/// <summary>
		///     ctor with optional settings. Null settings are overriden with default settings
		/// </summary>
		/// <param name="settings"></param>
		public SeriesID( ParserSettings settings = null )
		{
			if (settings != null)
			{
				_parserSettings = settings;
			}

			if (_parserSettings.CacheMode == CacheMode.Unknown)
			{
				throw new ArgumentException( "ParserSettings.CacheMode.Unknown is not a valid argument" );
			}

			if (_parserSettings.CacheMode == CacheMode.Advanced || _parserSettings.CacheMode == CacheMode.Simple)
			{
				_cacheEnabled = true;
			}

			if (_cacheEnabled && !MediaDataCache.InstanceExists)
			{
				MediaDataCache.Create( _parserSettings );
			}
		}

		// ############################################################
		// ### Wrapper Functions
		// ############################################################

		#region WrapperFunctions
		/// <summary>
		///     The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie string who get parsed. Must be atleast five chars</param>
		/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
		public ParserResult Parse( string input )
		{
			_fileInfo = null;
			if (_cacheEnabled)
			{
				ParserResult parserResult;
				if (MediaDataCache.Instance.TryGetAsParserResult( input, out parserResult ))
				{
					return parserResult;
				}
			}

			MediaData mediaData = CoreParser( input );

			if (_cacheEnabled)
			{
				MediaDataCache.Instance.Add( input, mediaData );
			}

			return mediaData.ToParserResult( _parserSettings );
		}

		/// <summary>
		///     The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie FileInfo who get parsed.</param>
		/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
		public ParserResult Parse( FileInfo input )
		{
			_fileInfo = input;
			return CoreParser( input?.Name ?? String.Empty ).ToParserResult( _parserSettings );
		}

		/// <summary>
		///     Get all files in a path parsed
		/// </summary>
		/// <param name="path"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public IEnumerable<ParserResult> ParsePath(DirectoryInfo path, SearchOption searchOption = SearchOption.AllDirectories)
		{
			return ParsePath(path?.FullName ?? String.Empty, searchOption);
		}

		/// <summary>
		///     Get all files in a path parsed
		/// </summary>
		/// <param name="path"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public IEnumerable<ParserResult> ParsePath( string path, SearchOption searchOption = SearchOption.AllDirectories )
		{
			List<ParserResult> results = new List<ParserResult>();

			if (string.IsNullOrEmpty( path ) || !Directory.Exists( path ))
			{
				return results;
			}

			List<FileInfo> files = HelperWorker.GetSeriesAndMovieFileInfos( path, _parserSettings, searchOption ).ToList();

			foreach (FileInfo file in files)
			{
				_fileInfo = file;

				if (_cacheEnabled)
				{
					MediaData mediaData;
					if (MediaDataCache.Instance.TryGet(file.Name, out mediaData ))
					{
						results.Add( mediaData.ToParserResult( _parserSettings ) );
						continue;
					}
				}

				results.Add( CoreParser(file.Name).ToParserResult( _parserSettings ) );
			}

			return results;
		}
		#endregion WrapperFunctions

		// ############################################################
		// ### Core Function
		// ############################################################
		private MediaData CoreParser( string input )
		{
			try
			{
				_coreParserResult = new CoreParserResult( input, _parserSettings );
				_coreParserResult.MediaData.FileInfo = _fileInfo;
				
				if (input.Length < 5)
				{
					// ERROR
					_coreParserResult.MediaData.State = State.Error;
					_coreParserResult.MediaData.Resolutions = HelperWorker.MaintainUnknownResolution( _coreParserResult.MediaData.Resolutions.ToList() );
					return _coreParserResult.MediaData;
				}

				// Get all corparsers activated with filter of disabled ones
				IEnumerable<ICoreParser> coreParserModules = HelperWorker.GetAllCoreParsers( _parserSettings.DisabledCoreParserModules );

				foreach (ICoreParser coreParserModule in coreParserModules)
				{
					try
					{
						_coreParserResult = coreParserModule.Parse( _coreParserResult );
					}
					catch (Exception ex)
					{
						_coreParserResult.MediaData.ModuleStates.Add( new CoreParserModuleStateResult( coreParserModule.Name,
																										new List<CoreParserModuleSubState>()
																										{
																											new CoreParserModuleSubState( State.Error,
																																		"Exception on executing module occoured. See exception for more details." )
																										}, ex ) );

						// Throw exception if the flag is set
						if (_parserSettings.ThrowExceptionInsteadOfErrorFlag)
						{
							throw;
						}
					}
				}

				_coreParserResult.MediaData.RemovedTokens = _coreParserResult.MediaData.RemovedTokens.OrderBy( x => x ).ToList();
				_coreParserResult.MediaData.Resolutions = HelperWorker.MaintainUnknownResolution( _coreParserResult.MediaData.Resolutions.ToList() );

				int errors = 0;
				int warnings = 0;
				int notice = 0;

				foreach (CoreParserModuleStateResult coreParserModuleStateResult in _coreParserResult.MediaData.ModuleStates)
				{
					errors += coreParserModuleStateResult.CoreParserModuleSubState.Count( x => x.State == State.Error );
					warnings += coreParserModuleStateResult.CoreParserModuleSubState.Count( x => x.State == State.Warning || x.State == State.Unknown );
					notice += coreParserModuleStateResult.CoreParserModuleSubState.Count( x => x.State == State.Notice );
				}

				if (errors > 0)
				{
					_coreParserResult.MediaData.State = State.Error;
				}
				else if (warnings > 0)
				{
					_coreParserResult.MediaData.State = State.Warning;
				}
				else if (notice > 0)
				{
					_coreParserResult.MediaData.State = State.Notice;
				}
				else
				{
					_coreParserResult.MediaData.State = State.Success;
				}

				_coreParserResult.MediaData.ProcessingDuration = DateTime.Now - _parseStartTime;

				return _coreParserResult.MediaData;
			}
			catch (Exception ex)
			{
				_coreParserResult.MediaData.Exception = ex;

				// Throw exception if the flag is set
				if (_parserSettings.ThrowExceptionInsteadOfErrorFlag)
				{
					throw;
				}

				// ERROR
				_coreParserResult.MediaData.State = State.Error;
				return _coreParserResult.MediaData;
			}
		}

		// ############################################################
		// ### Local Helper Functions
		// ############################################################

		#region HelperFunctions

		#endregion HelperFunctions
	}
}