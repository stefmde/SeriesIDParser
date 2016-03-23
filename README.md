About this project
===================

This project is designed to receivs a series string like

    Knight.Rider.S01E07.Die.grosse.Duerre.German.DVDRip.XviD-c0nFuSed

The output would be a object like that:

 - SeriesID (object)
  - |-> FullTitle       -> Knight.Rider.S01E07.Die.grosse.Duerre (string)
  - |-> SeriesTitle   -> Knight.Rider (string)
  - |-> EpisodeTitle-> Die.grosse.Duerre (string)
  - |-> State            -> OK_SUCCESS (Enum)
  - |-> Season        -> 1 (int)
  - |-> Episode       -> 7 (int)
  - |-> IDString      -> S01E07 (string)
  - |-> Resolution  -> SD_Any (enum)