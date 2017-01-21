![Project icon](https://raw.githubusercontent.com/stefmde/SeriesIDParser/master/Stuff/LibraryIcon/gray/icon_gray_256px.png)
# SeriesIDParser Page
This page should give you a short overview in how to use this project

***

# Description
## What you should know before you start
1. If you dont pass a ParserSettings object to the ctor of SeriesID it will use the default settings
1. You can create a ParserSettings object with empty lists with the empty ctor
1. You can create a ParserSettings object with default values in the lists with a `true` in the ctor
1. Before you use the ParserResult object you should check for a `NULL` in the `Exception` property and a `OKSuccess` in the `State` Property
1. If it is possible, properties are always `String.Empty` is they don't have a value. Otherweise they are `NULL`. Check the property comments to see it exactly.
1. Some properties are only available with series and some only with movies
1. Computed properties are automatically cached if the are queried the first time
1. Properties are only filled if the are found in the input string
1. The ParserSettings provides methodes to serialize the object to a file or deserialize them from a file

## Result Object

ParserResult.* | Type | Available Version | Comment
------------ | ------------- | ------------- | -------------
OriginalString | string | 3.0 | Contains the input string without modifications
ParsedString | string | 3.0 | Similar to the input string but according to guideline (ParserSettings). Can be used to rename files
Title | string | 2.0 | Contains the plain title
EpisodeTitle | string | 1.0 | Contains the episode/subtitle if available
FullTitle | string | 1.0 | Contains the title + IDString + EpisodeTitle
IsSeries | bool | 2.0 | Is `True` if the input string is recognized as series
IsMultiEpisode | bool | 8.0 | Is `True` if the input string contains something like that `S01E01E02`
Season | int | 1.0 | -
Episodes | int list | 6.1 | Contains the Episode ID's if available. Could be more than one, depends on `IsMultiEpisode`
IDString | string | 1.0 | Contains the Season ID and the Episode ID's combined as string
Exception | Exception | 1.0 | Contains the Exception if it is occoured or NULL if not 
Resolutions | enum list Resolutions | 6.0 | One string can contain more than one Resolution
Year | int | 2.0 | -
FileExtension | string | 3.0 | -
RemovedTokens | string list | 3.0 | Contains the tokens who are removed from the string and are not contained in the output
State | enum State | 1.0 | The object state. Should be checked before using the result
DetectedOldSpacingChar | char | 6.0 | -
ProcessingDuration | TimeSpan | 6.0 | -
ReleaseGroup | string | 6.1 | -
AudioCodec | string | 6.1 | -
VideoCodec | string | 6.1 | -
FileInfo| FileInfo | 8.0 | Is contained if the extension method with the FileInfo is used
ParserSettingsUsed | ParserSettings | 8.0 | Contains the given parser settings who are used to generate the result object


## Sample Result Series

Sample Input: `Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD-c0nFuSed.mkv`

ParserResult.* | Sample 
------------ | ------------- 
OriginalString | Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD-c0nFuSed.mkv
ParsedString | Knight.Rider.S01E07.Die.grosse.Duerre.1982.SD.German.XviD.mkv
Title | Knight.Rider 
EpisodeTitle | Die.grosse.Duerre 
FullTitle | Knight.Rider.S01E07.Die.grosse.Duerre 
IsSeries | True 
IsMultiEpisode | False 
Season | 1 
Episodes | 7 
IDString | S01E07 
Exception | NULL
Resolutions | SD_Any 
Year | 1982 
FileExtension | .mkv 
RemovedTokens | German, XviD 
State | OkSuccess
DetectedOldSpacingChar | . 
ProcessingDuration | 0,5018 ms 
ReleaseGroup | c0nFuSed 
AudioCodec | - 
VideoCodec | XviD 
FileInfo| - 
ParserSettingsUsed | ParserSettings


## Sample Result Movie
Sample Input: `Manhattan.Project.Der.atomare.Alptraum.1986.German.DL.1080p.HDTV.x264.mkv`

ParserResult.* | Sample 
------------ | ------------- 
OriginalString | Manhattan.Project.Der.atomare.Alptraum.1986.German.DL.1080p.HDTV.x264.mkv
ParsedString | Manhattan.Project.Der.atomare.Alptraum.1986.720p.German.x264.mkv
Title | Manhattan.Project.Der.atomare.Alptraum
EpisodeTitle | -
FullTitle | Manhattan.Project.Der.atomare.Alptraum
IsSeries | False
IsMultiEpisode | False
Season | -1 
Episodes | - 
IDString | -
Exception | NULL
Resolutions | FullHD_1080p, HD_720p 
Year | 1986 
FileExtension | .mkv 
RemovedTokens | German, x264 
State | OkSuccess 
DetectedOldSpacingChar | . 
ProcessingDuration | 0,5282 ms 
ReleaseGroup | c0nFuSed 
AudioCodec | - 
VideoCodec | x264 
FileInfo| - 
ParserSettingsUsed | ParserSettings

***

# Code Samples
## The easy way
Here you dont need some configuration or anything like that. If no settings is passed to it, it will use the default settings.
```csharp
// The string source who should be parsed
string parseMe = "Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD.mkv";

// Creating the parser object with default settings(empty ctor)
SeriesID sid = new SeriesID();

// Getting the result
ParserResult parserResult = sid.Parse(parseMe);
```
Now you can access all properties in the `ParserResult` object


## Custom ParserSettings
If you want to change how the parser handles something, you can edit the parser settings and pass it to the ctor
```csharp
// The string source who should be parsed
string parseMe = "Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD.mkv";

// Generate the ParserSettings object
ParserSettings ps = new ParserSettings();

// Modify a settings property
ps.NewSpacingChar = '-';

// Creating the parser object with custom settings
SeriesID sid = new SeriesID(ps);

// Getting the result
ParserResult parserResult = sid.Parse(parseMe);
```
Now you can access all properties in the `ParserResult` object

## Extension Methodes
### Single File
There are extension methodes for the following types available
* String -> String.ParseSeriesID
* FileInfo -> FileInfo.ParseSeriesID
Those extensions can be used for a single file and return a ParserResult object

#### Examples
```csharp
// The string source who should be parsed
string parseMe = "Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD.mkv";

// Parse the string
ParserResult result = parseMe.ParseSeriesID();
```

### Path
There are extension methodes for the following types available
* String -> String.ParseSeriesIDPath
* DirectoryInfo -> DirectoryInfo.ParseSeriesIDPath
Those extensions can be used for a path and return a IEnumerable<ParserResult> object-list

#### Examples
```csharp
// The string source who should be parsed
string parseDir = @"C:\MyMovieFiles";

// Parse the string
IEnumerable<ParserResult> result = parseDir.ParseSeriesIDPath();
```
