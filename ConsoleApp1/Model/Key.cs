using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.CoreAudioApi;

namespace ConsoleApp1.Model
{
    public static class Key
    {
        public static List<Note> Chromatic()
        {
            return new List<Note>
            {
                Note.A,
                Note.AsharpBflat,
                Note.B,
                Note.C,
                Note.CsharpDflat,
                Note.D,
                Note.DsharpEflat,
                Note.E,
                Note.F,
                Note.FsharpGflat,
                Note.G,
                Note.GsharpAflat
            };
        }

        public static List<Note> Major(Note tonic)
        {
            var allNotes = Chromatic();
            var majorKey = new List<Note>();

            majorKey.Add(tonic);
            var second = tonic.NextNote(allNotes, 2);
            majorKey.Add(second);
            var third = second.NextNote(allNotes, 2);
            majorKey.Add(third);
            var fourth = third.NextNote(allNotes, 1);
            majorKey.Add(fourth);
            var fifth = fourth.NextNote(allNotes, 2);
            majorKey.Add(fifth);
            var sixth = fifth.NextNote(allNotes, 2);
            majorKey.Add(sixth);
            var seventh = sixth.NextNote(allNotes, 2);
            majorKey.Add(seventh);

            return majorKey;
        }

        public static List<Note> MinorHarmonic(Note tonic)
        {
            var allNotes = Chromatic();
            var minorHarmonicKey = new List<Note>();

            minorHarmonicKey.Add(tonic);
            var second = tonic.NextNote(allNotes, 2);
            minorHarmonicKey.Add(second);
            var third = second.NextNote(allNotes, 1);
            minorHarmonicKey.Add(third);
            var fourth = third.NextNote(allNotes, 2);
            minorHarmonicKey.Add(fourth);
            var fifth = fourth.NextNote(allNotes, 2);
            minorHarmonicKey.Add(fifth);
            var sixth = fifth.NextNote(allNotes, 1);
            minorHarmonicKey.Add(sixth);
            var seventh = sixth.NextNote(allNotes, 3);
            minorHarmonicKey.Add(seventh);

            return minorHarmonicKey;
        }

        public static List<Note> Mode(this List<Note> key, int modeNumber)
        {
            var mode = new List<Note>();
            var modeIndex = modeNumber - 1;
            var modeIndexUp = key.GetRange(modeIndex, key.Count - modeIndex);
            mode.AddRange(modeIndexUp);
            var tonicToModeIndex = key.GetRange(0, modeIndex);
            mode.AddRange(tonicToModeIndex);

            return mode;
        }

        public static List<Tone> KeyRange(this List<Note> key, int noOfRepeats)
        {
            var keyRange = new List<Tone>();
            var octave = 0;
            for (var i = 0; i < noOfRepeats; i++)
            {
                foreach (var note in key)
                {
                    if (note == Note.C)
                    {
                        octave++;
                    }
                    keyRange.Add(new Tone
                    {
                        Note = note,
                        Octave = octave
                    });
                }
            }

            return keyRange;
        }
    }
}
