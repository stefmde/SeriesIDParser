﻿// 
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker.CoreParserModules
{
	internal class FileExtensionCoreParserModule : ICoreParser
	{
		/// <inheritdoc />
		public int Priority { get; } = 9900;

		/// <inheritdoc />
		public string Name { get; } = "FileExtensionCoreParser";

		/// <inheritdoc />
		public string Description { get; } = "Parses and removes the FileExtension";

		/// <inheritdoc />
		public State State { get; internal set; } = State.Unknown;

		/// <inheritdoc />
		public string ErrorOrWarningMessage { get; internal set; }

		/// <inheritdoc />
		public CoreParserResult Parse(CoreParserResult inputResult)
		{
			CoreParserResult outputResult = inputResult;
			string tempExtension = Path.GetExtension(inputResult.OriginalString);

			if (inputResult.ParserSettings.FileExtensions.Any(x => x.Equals(tempExtension, StringComparison.OrdinalIgnoreCase)))
			{
				outputResult.MediaData.FileExtension = tempExtension.ToLower();

				Regex removeRegex = new Regex(outputResult.MediaData.FileExtension, RegexOptions.IgnoreCase);
				outputResult.ModifiedString = removeRegex.Replace(outputResult.ModifiedString, "");
				State = State.OkSuccess;
			}
			else
			{
				State = State.WarnUnknownWarning;
				ErrorOrWarningMessage = "No FileExtension found";
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