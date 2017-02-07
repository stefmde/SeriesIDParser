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


using System.Linq;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker.CoreParserModules
{
	internal class ReleaseGroupCoreParserModule : ICoreParser
	{
		/// <inheritdoc />
		public int Priority { get; } = 9700;

		/// <inheritdoc />
		public string Name { get; } = "ReleaseGroupCoreParser";

		/// <inheritdoc />
		public string Description { get; } = "Parses and removes the FileExtensionReleaseGroup";

		/// <inheritdoc />
		public CoreParserModuleStateResult CoreParserModuleStateResult { get; internal set; }

		private State _state = State.Unknown;

		private string _errorOrWarningMessage;

		/// <inheritdoc />
		public CoreParserResult Parse(CoreParserResult inputResult)
		{
			CoreParserResult outputResult = inputResult;

			if (inputResult.ModifiedString.Length >= 30)
			{
				string tmpTitle = inputResult.ModifiedString.Substring(inputResult.ModifiedString.Length - 20, 20);

				if (tmpTitle.Any(x => x == inputResult.ParserSettings.ReleaseGroupSeperator))
				{
					int seperatorIndex = inputResult.ModifiedString.LastIndexOf(inputResult.ParserSettings.ReleaseGroupSeperator);
					outputResult.MediaData.ReleaseGroup = string.IsNullOrEmpty(inputResult.MediaData.FileExtension)
								? inputResult.ModifiedString.Substring(seperatorIndex + 1).Trim()
								: inputResult.ModifiedString.Substring(seperatorIndex + 1).Replace(inputResult.MediaData.FileExtension, "").Trim();
					outputResult.ModifiedString = outputResult.ModifiedString.Remove(seperatorIndex).Trim();
					_state = State.OkSuccess;
				}
				else
				{
					_state = State.WarnUnknownWarning;
					_errorOrWarningMessage = "No ReleaseGroup found";
				}
			}
			else
			{
				_state = State.WarnUnknownWarning;
				_errorOrWarningMessage = "String to short to parse for ReleaseGroup";
			}

			CoreParserModuleStateResult = new CoreParserModuleStateResult(Name, _state, _errorOrWarningMessage, null);

			return outputResult;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return "Name: " + Name + " Priority: " + Priority + " State: " + _state;
		}
	}
}