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


using System.Collections.Generic;
using System.Linq;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker.CoreParserModules
{
	internal class ResolutionsCoreParserModule : ICoreParser
	{
		/// <inheritdoc />
		public int Priority { get; } = 9800;

		/// <inheritdoc />
		public string Name { get; } = "ResolutionsCoreParser";

		/// <inheritdoc />
		public string Description { get; } = "Parses and removes the Resolutions";

		/// <inheritdoc />
		public State State { get; internal set; } = State.Unknown;

		/// <inheritdoc />
		public string ErrorOrWarningMessage { get; internal set; }

		/// <inheritdoc />
		public CoreParserResult Parse(CoreParserResult inputResult)
		{
			CoreParserResult outputResult = inputResult;
			string modifiedString = inputResult.ModifiedString;

			List<ResolutionsMap> tempFoundResolutions = new List<ResolutionsMap>();
			List<ResolutionsMap> foundResolutions = new List<ResolutionsMap>();

			// Try get 8K
			tempFoundResolutions.AddRange(HelperWorker.GetResolutionByResMap(inputResult.ParserSettings.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get 4K
			tempFoundResolutions.AddRange(HelperWorker.GetResolutionByResMap(inputResult.ParserSettings.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get FullHD
			tempFoundResolutions.AddRange(HelperWorker.GetResolutionByResMap(inputResult.ParserSettings.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get HD
			tempFoundResolutions.AddRange(HelperWorker.GetResolutionByResMap(inputResult.ParserSettings.DetectHDTokens, ResolutionsMap.HD_720p, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get SD
			tempFoundResolutions.AddRange(HelperWorker.GetResolutionByResMap(inputResult.ParserSettings.DetectSDTokens, ResolutionsMap.SD_Any, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			foreach (ResolutionsMap res in tempFoundResolutions)
			{
				if (!foundResolutions.Contains(res))
				{
					foundResolutions.Add(res);
				}
			}

			if (foundResolutions.Any())
			{
				outputResult.MediaData.Resolutions = foundResolutions;
				outputResult.ModifiedString = modifiedString;
				State = State.OkSuccess;
			}
			else
			{
				State = State.WarnUnknownWarning;
				ErrorOrWarningMessage = "No Resolutions found";
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