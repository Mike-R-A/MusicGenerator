using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Model
{
    public static class Music
    {
        public static Note NextNote(this Note start, List<Note> key, int distance)
        {
            var startIndex = key.IndexOf(start);
            var allNotes = Key.Chromatic();
            while (startIndex == -1)
            {
                var nextChromaticNote = allNotes[allNotes.IndexOf(start)].NextNote(allNotes, 1);
                startIndex = key.IndexOf(nextChromaticNote);
            }
            var lastIndex = key.Count - 1;
            var newIndex = startIndex + distance;
            if (newIndex > lastIndex)
            {
                newIndex = newIndex - key.Count;
            }
            var nextNote = key[newIndex];
            return nextNote;
        }

        public static List<Note> Chord(this List<Note> key, int rootNumber, int noOfNotes)
        {
            var chord = new List<Note>();
            var rootIndex = rootNumber - 1;
            var root = key[rootIndex];
            chord.Add(root);
            Note previous = root;
            for (var i = 0; i < noOfNotes - 1; i++)
            {
                var next = previous.NextNote(key, 2);
                chord.Add(next);
                previous = next;
            }

            return chord;
        }

        public static Motif Motif(int length, int maxSize, int stasisInhibitor = 5, double restChance = 0.01)
        {
            var motif = new Motif();
            var randomIntGenerator = new Random();
            var addRest = randomIntGenerator.Next(0, (int)(1 / restChance));
            var randomPitch = addRest == 1 ? -1 : randomIntGenerator.Next(0, maxSize);
            var previousDirection = randomIntGenerator.Next(-1, 2);
            var nextIndex = randomPitch;
            int lastIndex = randomPitch;
            motif.Pitches.Add(nextIndex);
            motif.Rhythm.Add(RandomNoteLength());
            for (var i = 0; i < length; i++)
            {
                if (nextIndex != -1)
                {
                    lastIndex = nextIndex;
                    var direction = randomIntGenerator.Next(-1, 2);
                    for (var j = 0; j < stasisInhibitor; j++)
                    {
                        if (direction == 0 || direction != previousDirection)
                        {
                            direction = randomIntGenerator.Next(-1, 2);
                        }
                    }
                    var potentialNextIndex = lastIndex + direction;
                    previousDirection = direction;
                    while (potentialNextIndex < 0 || potentialNextIndex > maxSize)
                    {
                        var newDirection = randomIntGenerator.Next(-1, 2);
                        potentialNextIndex = nextIndex + newDirection;
                    }
                    addRest = randomIntGenerator.Next(0, (int)(1 / restChance));
                    nextIndex = restChance == 1 ? -1 : potentialNextIndex;
                }
                
                motif.Pitches.Add(nextIndex);
                motif.Rhythm.Add(RandomNoteLength());
            }

            return motif;
        }

        public static NoteLength RandomNoteLength()
        {
            const int noOfNoteLengths = 8;
            var randomIntGenerator = new Random();
            var randomNoteLengthSelector = randomIntGenerator.Next(0, noOfNoteLengths);
            switch (randomNoteLengthSelector)
            {
                case 0:
                {
                    return NoteLength.Semibreve;
                }
                case 1:
                {
                    return NoteLength.DottedMinim;
                }
                case 2:
                {
                    return NoteLength.Minim;
                }
                case 3:
                {
                    return NoteLength.DottedCrotchet;
                }
                case 4:
                {
                    return NoteLength.Crotchet;
                }
                case 5:
                {
                    return NoteLength.DottedQuaver;
                }
                case 6:
                {
                    return NoteLength.Quaver;
                }
                case 7:
                {
                    return NoteLength.SemiQuaver;
                }
                default:
                {
                    return NoteLength.Crotchet;
                }
            }
        }

        public static Motif MakeChordal(this Motif motif)
        {
            var chordalMotif = new Motif();
            chordalMotif.Pitches = motif.Pitches.Select(i => i == -1 ? i : i * 2).ToList();
            chordalMotif.Rhythm = motif.Rhythm.Select(i => i).ToList();

            return chordalMotif;
        }

        public static List<int> Transpose(this List<int> motif, int amount)
        {
            return motif.Select(i => i == -1 ? i : i + amount).ToList();
        }

        public static Motif Concatenate(this Motif motif1, Motif motif2)
        {
            var newMotif = new Motif();
            newMotif.Pitches.AddRange(motif1.Pitches);
            newMotif.Rhythm.AddRange(motif1.Rhythm);
            newMotif.Pitches.AddRange(motif2.Pitches);
            newMotif.Rhythm.AddRange(motif2.Rhythm);
            return newMotif;
        }

        public static Motif ModifyMotif(this Motif motif, List<Motif> motifPool = null)
        {
            var noOfTypesOfDevelopment = motifPool == null ? 4 : 5;
            var developedMotif = new Motif();
            var developedMotifPitches = motif.Pitches.Select(t => t).ToList();
            var developedMotifRhythm = motif.Rhythm.Select(t => t).ToList();
            var randomIntGenerator = new Random();
            var randomInt = randomIntGenerator.Next(1, noOfTypesOfDevelopment);
            var displacement = randomIntGenerator.Next(-1, 2);
            switch (randomInt)
            {
                case 1:
                {
                    developedMotifPitches.Reverse();
                    break;
                }
                case 2:
                {
                    developedMotifPitches.AddRange(developedMotifPitches.Transpose(displacement));
                    developedMotifRhythm.AddRange(developedMotifRhythm);
                    break;
                }
                case 3:
                {
                    var copyPitches = developedMotifPitches.Select(t => t).ToList();
                    var copyRhythm = developedMotifRhythm.Select(nl => nl).ToList();
                    copyPitches.Reverse();
                    copyRhythm.Reverse();
                    developedMotifPitches.AddRange(copyPitches.Transpose(displacement));
                    developedMotifRhythm.AddRange(copyRhythm);
                    break;
                }
                case 4:
                {
                    var copyPitches = developedMotifPitches.Select(t => t).ToList();
                    var copyRhythm = developedMotifRhythm.Select(nl => nl).ToList();
                    developedMotifPitches.Reverse();
                    copyRhythm.Reverse();
                    developedMotifPitches.AddRange(copyPitches.Transpose(displacement));
                    developedMotifRhythm.AddRange(copyRhythm);
                    break;
                }
                case 5:
                {
                    if (motifPool != null)
                    {
                        int poolSelection = randomIntGenerator.Next(1, motifPool.Count);
                        developedMotif = motif.Concatenate(motifPool[poolSelection]);
                    }  
                    break;
                }
            }

            developedMotif.Pitches = developedMotifPitches;
            developedMotif.Rhythm = developedMotifRhythm;
            return developedMotif;
        }

        public static List<Tone> ApplyMotif(this List<Note> key, Motif motif, int? startIndex = null, int startOctave = 4)
        {
            int start = startIndex ?? motif.Pitches[0];
            var translatedMotif = new Motif();
            var translationAmount = start + startOctave * key.Count - motif.Pitches[0];
            translatedMotif.Pitches = motif.Pitches.Select(i => i == -1 || i + translationAmount < 0 ? i : i + translationAmount).ToList();
            var appliedMotif = new List<Tone>();
            var octaves = 100;
            var keyRange = key.KeyRange(octaves);
            for (int i = 0; i < translatedMotif.Pitches.Count; i++)
            {
                var tone = new Tone();
                if (translatedMotif.Pitches[i] == -1)
                {
                    tone.Note = Note.Rest;
                    tone.Octave = null;
                }
                else
                {
                    var octave = keyRange[translatedMotif.Pitches[i]].Octave;
                    var octaveToUse = octave <= Sound.MaxOctave ? octave : octave - 1;
                    tone.Note = keyRange[translatedMotif.Pitches[i]].Note;
                    tone.Octave = octave;
                }
                tone.Length = motif.Rhythm[i];
                appliedMotif.Add(tone);
            }

            return appliedMotif;
        }

        public static List<Tone> DevelopMotif(this List<Note> key, Motif motif, List<int> startIndexes, TimeSignature timeSignature, int maxBars = 8, int startOctave = 4, double alterChance = 0.5)
        {
            var phrase = new List<Tone>();
            var randomIntGenerator = new Random();
            foreach (var startIndex in startIndexes)
            {
                var max = (int)Math.Round((decimal)(1 / alterChance));
                var randomAlterChance = randomIntGenerator.Next(1, max);
                var altered = new Motif();
                if (randomAlterChance == 1)
                {
                    altered = motif.ModifyMotif();
                }
                else
                {
                    altered = motif;
                }

                var addition = key.ApplyMotif(altered, startIndex, startOctave);
                var timeWithAddition = phrase.TotalTime() + addition.TotalTime();
                var maxTime = maxBars * timeSignature.BarTime;
                if (timeWithAddition < maxTime)
                {
                    phrase.AddRange(addition);
                }
            }

            for (int i = 0; i < phrase.Count; i++)
            {
                var timeUpToThisTone = phrase.Take(i).ToList().TotalTime();
                var singleBarTime = timeSignature.Beats * (double)timeSignature.BeatType;
                if (timeUpToThisTone % singleBarTime == 0)
                {
                    phrase[i].Volume = 0.3;
                }
                else
                {
                    phrase[i].Volume = 0.2;
                }
            }
            phrase.PadWithRests(timeSignature);

            return phrase;
        }

        private static void PadWithRests(this List<Tone> phrase, TimeSignature timeSignature)
        {
            var singleBarTime = timeSignature.Beats * (double)timeSignature.BeatType;
            var remainder = phrase.TotalTime() % singleBarTime;
            var endRestTime = singleBarTime - remainder;
            while (endRestTime > (double)NoteLength.Semibreve)
            {
                phrase.Add(new Tone
                {
                    Note = Note.Rest,
                    Length = NoteLength.Semibreve,
                    Octave = null
                });
                endRestTime = endRestTime - (double)NoteLength.Semibreve;
            }
            while (endRestTime > (double)NoteLength.Minim)
            {
                phrase.Add(new Tone
                {
                    Note = Note.Rest,
                    Length = NoteLength.Minim,
                    Octave = null
                });
                endRestTime = endRestTime - (double)NoteLength.Minim;
            }
            while (endRestTime > (double)NoteLength.Crotchet)
            {
                phrase.Add(new Tone
                {
                    Note = Note.Rest,
                    Length = NoteLength.Crotchet,
                    Octave = null
                });
                endRestTime = endRestTime - (double)NoteLength.Crotchet;
            }
            while (endRestTime > (double)NoteLength.Quaver)
            {
                phrase.Add(new Tone
                {
                    Note = Note.Rest,
                    Length = NoteLength.Quaver,
                    Octave = null
                });
                endRestTime = endRestTime - (double)NoteLength.Quaver;
            }
            while (endRestTime > (double)NoteLength.SemiQuaver)
            {
                phrase.Add(new Tone
                {
                    Note = Note.Rest,
                    Length = NoteLength.SemiQuaver,
                    Octave = null
                });
                endRestTime = endRestTime - (double)NoteLength.SemiQuaver;
            }
        }

        public static double TotalTime(this List<Tone> phrase)
        {
            return phrase.Sum(t => (double)t.Length);
        }
    }
}
