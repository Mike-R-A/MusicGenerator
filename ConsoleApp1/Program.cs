using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Xsl;
using ConsoleApp1.Model;

namespace MusicGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var motifs = new List<Motif>();

            var randomIntGenerator = new Random();

            for (var i = 0; i < 4; i++)
            {
                var randomLength = randomIntGenerator.Next(1, 10);
                Console.WriteLine("Motif Length, " + randomLength);
                var randomMaxSize = randomIntGenerator.Next(1, 12);
                Console.WriteLine("Motif Max Size, " + randomMaxSize);
                var randomStasisInhibitor = randomIntGenerator.Next(0, 5);
                Console.WriteLine("Motif Stasis Inhibitor, " + randomStasisInhibitor);
                var motif = Music.Motif(randomLength, randomMaxSize, randomStasisInhibitor);

                var alteredMotif1 = motif.ModifyMotif(motifs);
                var alteredMotif2 = motif.ModifyMotif(motifs);
                var chordalMotif1 = motif.MakeChordal();
                var chordalMotif2 = motif.MakeChordal();
                var chordalMotif3 = motif.MakeChordal();
                motifs.Add(motif);
                motifs.Add(alteredMotif1);
                motifs.Add(alteredMotif2);
                motifs.Add(chordalMotif1);
                motifs.Add(chordalMotif2);
                motifs.Add(chordalMotif3);
            }

            var keys = new List<List<Note>>();
            keys.Add(Key.Major(Note.DsharpEflat));
            keys.Add(Key.MinorHarmonic(Note.C));
            keys.Add(Key.Major(Note.GsharpAflat));
            keys.Add(Key.MinorHarmonic(Note.F));
            keys.Add(Key.Major(Note.CsharpDflat));
            keys.Add(Key.MinorHarmonic(Note.AsharpBflat));
            keys.Add(Key.Major(Note.E));
            keys.Add(Key.MinorHarmonic(Note.DsharpEflat));
            string command = "";
            while (command != "q")
            {
                var allPhrases = new List<List<Tone>>();
                foreach (var key in keys)
                {
                    var phrases = new List<List<Tone>>();
                    var timeSignature = new TimeSignature
                    {
                        Beats = 4,
                        BeatType = NoteLength.Crotchet
                    };

                    for (var i = 0; i < motifs.Count; i++)
                    {
                        var randomInt1 = randomIntGenerator.Next(0, motifs.Count);
                        var randomInt2 = randomIntGenerator.Next(0, motifs.Count);
                        var alterChance = 1 / (double)(randomIntGenerator.Next(1, 10));
                        var phrase = key.DevelopMotif(motifs[randomInt1], motifs[randomInt2].Pitches, timeSignature, startOctave: 4, maxBars: 4, alterChance: alterChance);
                        phrases.Add(phrase);
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Section");
                    Console.WriteLine();

                    var phraseLengthOfSection = 4;

                    for (var i = 0; i < phraseLengthOfSection; i++)
                    {
                        Console.Write(" | ");
                        //var randomPhraseIndex = randomIntGenerator.Next(0, phrases.Count);
                        foreach (var tone in phrases[i])
                        {
                            Console.Write(tone.Note.ToString() + tone.Octave.ToString() + " ");
                            tone.Play();
                        }
                    }
                    Console.Write(" | ");
                    foreach (var tone in phrases[0])
                    {
                        Console.Write(tone.Note.ToString() + tone.Octave.ToString() + " ");
                        tone.Play();
                    }

                    Console.Write(" || ");
                    allPhrases.AddRange(phrases);
                }

                command = Console.ReadLine();

            }
        }
    }
}
