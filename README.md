About this project
===================

This project is designed to receivs a series string like

    Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD-c0nFuSed.mkv

The output would be a object like that:

 - SeriesID (object)
  - |-> FullTitle       -> Knight.Rider.S01E07.Die.grosse.Duerre (string)
  - |-> SeriesTitle   -> Knight.Rider (string)
  - |-> EpisodeTitle-> Die.grosse.Duerre (string)
  - |-> State            -> OK_SUCCESS (Flagable Enum)
  - |-> IsSeries        -> true (bool)
  - |-> OriginalString-> Knight.Rider.S01E07.Die.grosse.Duerre.German.DVDRip.XviD-c0nFuSed.mkv (string)
  - |-> ParsedString -> Knight.Rider.S01E07.Die.grosse.Duerre.DVDRip.German.mkv (string)
  - |-> RemovedTokens -> {German, DVDRip, XviD, -C0nFuSed} (string-list)
  - |-> Season        -> 1 (int)
  - |-> Episode       -> 7 (int)
  - |-> IDString      -> S01E07 (string)
  - |-> Resolution  -> SD_Any (enum)
  - |-> Year             -> 1982 (int)

**Now on [NuGet](https://www.nuget.org/packages/SeriesIDParser/)!**