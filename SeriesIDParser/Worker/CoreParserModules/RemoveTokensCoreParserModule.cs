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

namespace SeriesIDParser.Worker.CoreParserModules
{
	internal class RemoveTokensCoreParserModule : ICoreParser
	{
		/// <inheritdoc />
		public int Priority { get; } = 9500;

		/// <inheritdoc />
		public string Name { get; } = "RemoveTokensCoreParser";

		/// <inheritdoc />
		public string Description { get; } = "Parses and removes the Tokens";

		/// <inheritdoc />
		public State State { get; internal set; } = State.Unknown;

		/// <inheritdoc />
		public string ErrorOrWarningMessage { get; internal set; }

		/// <inheritdoc />
		public CoreParserResult Parse(CoreParserResult inputResult)
		{
			CoreParserResult outputResult = inputResult;
			string modifiedString = inputResult.ModifiedString;

			List<string> tempFoundTokens = new List<string>();
			List<string> foundTokens = new List<string>();

			tempFoundTokens.AddRange(HelperWorker.FindTokens(ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(), inputResult.ParserSettings.RemoveAndListTokens, true));
			tempFoundTokens.AddRange(HelperWorker.FindTokens(ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(), inputResult.ParserSettings.RemoveWithoutListTokens, false));

			tempFoundTokens.AddRange(HelperWorker.ReplaceTokens(ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(), inputResult.ParserSettings.ReplaceRegexAndListTokens, true));
			tempFoundTokens.AddRange(HelperWorker.ReplaceTokens(ref modifiedString, inputResult.MediaData.DetectedOldSpacingChar.ToString(), inputResult.ParserSettings.ReplaceRegexWithoutListTokens, false));

			foreach (string item in tempFoundTokens)
			{
				if (!foundTokens.Any(x => x.Equals(item, StringComparison.OrdinalIgnoreCase)))
				{
					foundTokens.Add(item);
				}
			}

			if (foundTokens.Any())
			{
				outputResult.ModifiedString = modifiedString;
				outputResult.MediaData.RemovedTokens = foundTokens;
				State = State.OkSuccess;
			}
			else
			{
				State = State.WarnUnknownWarning;
				ErrorOrWarningMessage = "No Tokens found";
			}

			return outputResult;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return "Name: " + Name + " Priority: " + Priority + " State: " + State;
		}
	}
}