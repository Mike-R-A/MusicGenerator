using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ConsoleApp1.Model;

namespace MusicGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tonic = Note.C;
            Console.WriteLine("Music Generator");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Scale");
            Console.WriteLine();

            var key = Key.MinorHarmonic(tonic);

            key.ForEach(n =>
            {
                Console.WriteLine(n.ToString());
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Chord");
            Console.WriteLine();

            var chord = key.Chord(5, 4);

            chord.ForEach(n =>
            {
                Console.WriteLine(n.ToString());
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Mode");
            Console.WriteLine();

            var mix = Key.Mode(Key.Major(tonic), 5);
            var mixChord = mix.Chord(1, 4);
            var mixinv = Key.Mode(mixChord, 4);

            Console.WriteLine();
            Console.WriteLine("Inversion");
            mixinv.ForEach(n =>
            {
                Console.WriteLine(n.ToString());
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Key Range");
            Console.WriteLine();

            var keyRange = Key.KeyRange(mix, 2);

            keyRange.ForEach(n =>
            {
                Console.WriteLine(n.Note.ToString() + " " + n.Octave.ToString());
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Motifs");
            Console.WriteLine();

            var motifs = new List<Motif>();

            var randomIntGenerator = new Random();

            for (var i = 0; i < 10; i++)
            {
                var randomLength = randomIntGenerator.Next(1, 6);
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
            keys.Add(Key.Major(Note.C));
            //keys.Add(Key.Major(Note.C).Mode(2));
            //keys.Add(Key.Major(Note.C).Mode(3));
            //keys.Add(Key.MinorHarmonic(Note.F).Chord(1, 4));
            //keys.Add(Key.MinorHarmonic(Note.F).Chord(1, 4).Mode(2));
            //keys.Add(Key.MinorHarmonic(Note.F).Chord(1, 4).Mode(3));
            //keys.Add(Key.Major(Note.D));

            foreach (var motif in motifs)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Next Motif");
                foreach (var k in keys)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Next Key");
                    Console.WriteLine();
                    Console.WriteLine();
                    for (var i = 0; i < motif.Pitches.Count; i++)
                    {
                        Console.Write((motif.Pitches[i]+1).ToString() + " ");
                    }
                    Console.WriteLine();

                    var appliedMotif = k.ApplyMotif(motif);

                    foreach (var tone in appliedMotif)
                    {
                        Console.Write(tone.Note.ToString() + tone.Octave.ToString() + " ");
                    }

                    Console.WriteLine();
                }
            }

            var phrases = new List<List<Tone>>();

            for (var i = 0; i < motifs.Count; i++)
            {
                var randomInt1 = randomIntGenerator.Next(0, motifs.Count);
                var randomInt2 = randomIntGenerator.Next(0, motifs.Count);
                var alterChance = 1 / (double)(randomIntGenerator.Next(1, 10));
                var phrase = keys[0].DevelopMotif(motifs[randomInt1], motifs[randomInt2].Pitches, alterChance: alterChance);
                phrases.Add(phrase);
            }


            //var phrases1 = new List<List<Tone>>();

            //for (var i = 0; i < motifs.Count; i++)
            //{
            //    var randomInt1 = randomIntGenerator.Next(0, motifs.Count);
            //    var randomInt2 = randomIntGenerator.Next(0, motifs.Count);
            //    var phrase = keys[1].DevelopMotif(motifs[randomInt1], motifs[randomInt2]);
            //    phrases1.Add(phrase);
            //}
            //var phrases1 = new List<List<Tone>>();

            //for (var i = 0; i < phrases.Count; i++)
            //{
            //    var randomInt1 = randomIntGenerator.Next(0, phrases.Count);
            //    var randomInt2 = randomIntGenerator.Next(0, phrases.Count);
            //    var phrase = keys[0].DevelopMotif(phrases[randomInt1], phrases[randomInt2]);
            //    phrases.Add(phrase);
            //}

            Console.WriteLine();
            Console.WriteLine("Scale Phrases");
            Console.WriteLine();

            foreach (var phrase in phrases)
            {
                Console.WriteLine();
                Console.WriteLine("Phrase");
                Console.WriteLine();
                foreach (var tone in phrase)
                {
                    Console.Write(tone.Note.ToString() + tone.Octave.ToString() + " ");
                    tone.Play(tone.Length);
                }
                
                var totalLength = phrase.Select(t => t.Length).Sum();
                Thread.Sleep((int)NoteLength.Crotchet);
                Console.WriteLine(totalLength + " milliseconds");
            }

            //Console.WriteLine();
            //Console.WriteLine("Chord Phrases");
            //Console.WriteLine();

            //foreach (var phrase in phrases1)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Phrase");
            //    Console.WriteLine();
            //    foreach (var tone in phrase)
            //    {
            //        Console.Write(tone.Note.ToString() + tone.Octave.ToString() + " ");
            //    }
            //    Console.WriteLine();
            //}



            Console.ReadLine();
        }
    }
}
