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


namespace SeriesIDParser.Models;

/// <summary>
///     Interface who must be implemented for all parser modules that have to be executed
/// </summary>
public interface ICoreParser
{
	/// <summary>
	///     Defines the priority in which order the Parser have to execute. Higher values came first. -1 is last/no prio needed
	/// </summary>
	int Priority { get; }

	/// <summary>
	///     The readable Name of the CoreParser. Used for error messages and things like that
	/// </summary>
	string Name { get; }

	/// <summary>
	///     The description what that CoreParser did
	/// </summary>
	string Description { get; }

	/// <summary>
	///     The main parse methode
	/// </summary>
	/// <param name="inputResult">The result that get hopped from each parser to the next. The info get to the next parser</param>
	/// <returns></returns>
	CoreParserResult Parse( CoreParserResult inputResult );
}