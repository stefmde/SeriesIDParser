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
using System.Linq;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker.CoreParserModules;

public class ReleaseGroupCoreParserModule : ICoreParser
{
	/// <inheritdoc />
	public int Priority { get; } = 9700;

	/// <inheritdoc />
	public string Name { get; } = "ReleaseGroupCoreParser";

	/// <inheritdoc />
	public string Description { get; } = "Parses and removes the FileExtensionReleaseGroup";

	private State _state = State.Unknown;

	private string _errorOrWarningMessage = String.Empty;

	/// <inheritdoc />
	public CoreParserResult Parse( CoreParserResult inputResult )
	{
		var outputResult = inputResult;

		if (inputResult.ModifiedString.Length >= 30)
		{
			var tmpTitle = inputResult.ModifiedString.Substring( inputResult.ModifiedString.Length - 20, 20 );

			if (tmpTitle.Any( x => x == inputResult.ParserSettings.ReleaseGroupSeparator ))
			{
				var separatorIndex = inputResult.ModifiedString.LastIndexOf( inputResult.ParserSettings.ReleaseGroupSeparator );
				outputResult.MediaData.ReleaseGroup = string.IsNullOrEmpty( inputResult.MediaData.FileExtension )
														? inputResult.ModifiedString.Substring( separatorIndex + 1 ).Trim()
														: inputResult.ModifiedString.Substring( separatorIndex + 1 ).Replace( inputResult.MediaData.FileExtension, "" ).Trim();
				outputResult.ModifiedString = outputResult.ModifiedString.Remove( separatorIndex ).Trim();
				_state = State.Success;
			}
			else
			{
				_state = State.Notice;
				_errorOrWarningMessage = "No ReleaseGroup found";
			}
		}
		else
		{
			_state = State.Notice;
			_errorOrWarningMessage = "String to short to parse for ReleaseGroup";
		}

		outputResult.MediaData.ModuleStates.Add( new CoreParserModuleStateResult( Name, new List<CoreParserModuleSubState>() { new(_state, _errorOrWarningMessage) } ) );

		return outputResult;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return "Name: " + Name + " Priority: " + Priority + " State: " + _state;
	}
}