
SeriesIDParser
===================
 
Links
-----
**Download it from [NuGet](https://www.nuget.org/packages/SeriesIDParser/)!**
If you want, you can tell me, your thoughts about this project, on my [GitHub Profile](https://github.com/stefmde)

About
-----
This project is designed to receive a movie series string like that:
`Knight.Rider.S01E07.Die.grosse.Duerre.3D.HOU.1982.German.DVDRip.XviD-c0nFuSed.mkv`

The output would be a `ParserResult` object like that (Demo-App):
| Property | Value |
|--|--|
| OriginalString (string) | Knight.Rider.S01E07.Die.grosse.Duerre.3D.HOU.1982.German.DVDRip.XviD-c0nFuSed.mkv |
| ParsedString (string) | Knight.Rider.S01E07.Die.grosse.Duerre.1982.SD.3D.HOU.German.XviD.mkv |
| Title (string) | Knight.Rider |
| EpisodeTitle (string) | Die.grosse.Duerre |
| FullTitle (string) | Knight.Rider.S01E07.Die.grosse.Duerre |
| IsSeries (bool) | True |
| IsMultiEpisode (bool) | False |
| Season (int) | 1 |
| Episodes (int list) | 7 |
| IDString (string) | S01E07 |
| Resolutions (enum list Resolutions) | SD_Any |
| Year (int) | 1982 |
| FileExtension (string) | .mkv |
| RemovedTokens (string list) | German, XviD |
| State (enum State) | Notice |
| DetectedOldSpacingChar (char) | . |
| ~~ProcessingDuration (TimeSpan)~~ | ~~63842568894266,29 ms~~ |
| ReleaseGroup (string) | c0nFuSed |
| AudioCodec (string) |  |
| VideoCodec (string) | XviD |
| Is3D (string) | True |
| DimensionalType (string) | Dimension_3DHOU |
