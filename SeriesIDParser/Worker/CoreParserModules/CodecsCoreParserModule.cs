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
using System.Collections.Generic;
using System.Linq;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker.CoreParserModules;

public class CodecsCoreParserModule : ICoreParser
{
	/// <summary>
	///     Defines the priority in which order the Parser have to execute. Higher values came first. -1 is last/no prio needed
	/// </summary>
	public int Priority { get; } = 9400;

	/// <summary>
	///     The readable Name of the CoreParser. Used for error messages and things like that
	/// </summary>
	public string Name { get; } = "CodecsCoreParser";

	/// <summary>
	///     The description what that CoreParser did
	/// </summary>
	public string Description { get; } = "Parses and removes the Codecs";

	private State _state = State.Unknown;

	private string _errorOrWarningMessage = String.Empty;

	/// <summary>
	///     The main parse methode
	/// </summary>
	/// <param name="inputResult">The result that get hopped from each parser to the next. The info get to the next parser</param>
	/// <returns></returns>
	public CoreParserResult Parse( CoreParserResult inputResult )
	{
			CoreParserResult outputResult = inputResult;
			string modifiedString = inputResult.ModifiedString;

			outputResult.MediaData.AudioCodec = HelperWorker.FindTokens( ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(),
																		inputResult.ParserSettings.AudioCodecs, true ).LastOrDefault();
			if (outputResult.MediaData.AudioCodec != null)
			{
				outputResult.MediaData.RemovedTokens.Add( outputResult.MediaData.AudioCodec );
			}

			outputResult.MediaData.VideoCodec = HelperWorker.FindTokens( ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(),
																		inputResult.ParserSettings.VideoCodecs, true ).LastOrDefault();
			if (outputResult.MediaData.VideoCodec != null)
			{
				outputResult.MediaData.RemovedTokens.Add( outputResult.MediaData.VideoCodec );
			}

			outputResult.ModifiedString = modifiedString;
			_state = State.Success;

			outputResult.MediaData.ModuleStates.Add( new CoreParserModuleStateResult( Name,
																					new List<CoreParserModuleSubState>() {new( _state, _errorOrWarningMessage )} ) );

			return outputResult;
		}

	/// <summary>Returns a string that represents the current object.</summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString()
	{
			return "Name: " + Name + " Priority: " + Priority + " State: " + _state;
		}
}