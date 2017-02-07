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
	internal class DimensionalTypeCoreParserModule : ICoreParser
	{
		/// <inheritdoc />
		public int Priority { get; } = 9600;

		/// <inheritdoc />
		public string Name { get; } = "DimensionalTypeCoreParser";

		/// <inheritdoc />
		public string Description { get; } = "Parses and removes the DimensionalType";

		/// <inheritdoc />
		public State State { get; internal set; } = State.Unknown;

		/// <inheritdoc />
		public string ErrorOrWarningMessage { get; internal set; }

		/// <inheritdoc />
		public CoreParserResult Parse(CoreParserResult inputResult)
		{
			CoreParserResult outputResult = inputResult;
			string modifiedString = inputResult.ModifiedString;

			List<DimensionalType> tempDimensionalTypes = new List<DimensionalType>();
			DimensionalType foundDimensionalType;

			// Try get 3D HSBS
			tempDimensionalTypes.AddRange(HelperWorker.GetDimensionalTypeByResMap(inputResult.ParserSettings.DetectHsbs3DTokens, DimensionalType.Dimension_3DHSBS, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get 3D HOU
			tempDimensionalTypes.AddRange(HelperWorker.GetDimensionalTypeByResMap(inputResult.ParserSettings.DetectHou3DTokens, DimensionalType.Dimension_3DHOU, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));

			// Try get 3D Any
			if (tempDimensionalTypes.Count == 0)
			{
				tempDimensionalTypes.AddRange(HelperWorker.GetDimensionalTypeByResMap(inputResult.ParserSettings.DetectAny3DTokens, DimensionalType.Dimension_3DAny, inputResult.MediaData.DetectedOldSpacingChar, ref modifiedString));
			}

			if (tempDimensionalTypes.Count == 0)
			{
				foundDimensionalType = DimensionalType.Dimension_2DAny;
			}
			else if (tempDimensionalTypes.Count == 1)
			{
				foundDimensionalType = tempDimensionalTypes.LastOrDefault();
			}
			else
			{
				foundDimensionalType = DimensionalType.Dimension_3DAny;
			}

			if (foundDimensionalType == DimensionalType.Unknown)
			{
				State = State.WarnUnknownWarning;
				ErrorOrWarningMessage = "No DimensionalType found";
			}
			else
			{
				outputResult.ModifiedString = modifiedString;
				outputResult.MediaData.DimensionalType = foundDimensionalType;
				State = State.OkSuccess;
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