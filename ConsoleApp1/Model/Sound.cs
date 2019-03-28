using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleApp1.Model
{
    public static class Sound
    {
        public static double FrequencyByTone(Tone tone)
        {
            var lookup = new Dictionary<string, double>
            {
                {
                    Note.C.ToString() + "2", 65.41
                },
                {
                    Note.CsharpDflat.ToString() + "2", 69.30
                },
                {
                    Note.D.ToString() + "2", 73.42
                },
                {
                    Note.DsharpEflat.ToString() + "2", 77.78
                },
                {
                    Note.E.ToString() + "2", 82.41
                },
                {
                    Note.F.ToString() + "2", 87.31
                },
                {
                    Note.FsharpGflat.ToString() + "2", 92.50
                },
                {
                    Note.G.ToString() + "2", 98.00
                },
                {
                    Note.GsharpAflat.ToString() + "2", 103.83
                },
                {
                    Note.A.ToString() + "2", 110.00
                },
                {
                    Note.AsharpBflat.ToString() + "2", 116.54
                },
                {
                    Note.B.ToString() + "2", 123.47
                },

                {
                    Note.C.ToString() + "3", 130.81
                },
                {
                    Note.CsharpDflat.ToString() + "3", 138.59
                },
                {
                    Note.D.ToString() + "3", 146.83
                },
                {
                    Note.DsharpEflat.ToString() + "3", 155.56
                },
                {
                    Note.E.ToString() + "3", 164.81
                },
                {
                    Note.F.ToString() + "3", 174.61
                },
                {
                    Note.FsharpGflat.ToString() + "3", 185.00
                },
                {
                    Note.G.ToString() + "3", 196.00
                },
                {
                    Note.GsharpAflat.ToString() + "3", 207.65
                },
                {
                    Note.A.ToString() + "3", 220.00
                },
                {
                    Note.AsharpBflat.ToString() + "3", 233.08
                },
                {
                    Note.B.ToString() + "3", 246.94
                },

                {
                    Note.C.ToString() + "4", 261.63
                },
                {
                    Note.CsharpDflat.ToString() + "4", 277.18
                },
                {
                    Note.D.ToString() + "4", 293.66
                },
                {
                    Note.DsharpEflat.ToString() + "4", 311.13
                },
                {
                    Note.E.ToString() + "4", 329.63
                },
                {
                    Note.F.ToString() + "4", 349.23
                },
                {
                    Note.FsharpGflat.ToString() + "4", 369.99
                },
                {
                    Note.G.ToString() + "4", 392.00
                },
                {
                    Note.GsharpAflat.ToString() + "4", 415.30
                },
                {
                    Note.A.ToString() + "4", 440.00
                },
                {
                    Note.AsharpBflat.ToString() + "4", 466.16
                },
                {
                    Note.B.ToString() + "4", 493.88
                },

                {
                    Note.C.ToString() + "5", 523.25
                },
                {
                    Note.CsharpDflat.ToString() + "5", 554.37
                },
                {
                    Note.D.ToString() + "5", 587.33
                },
                {
                    Note.DsharpEflat.ToString() + "5", 622.25
                },
                {
                    Note.E.ToString() + "5", 659.25
                },
                {
                    Note.F.ToString() + "5", 698.46
                },
                {
                    Note.FsharpGflat.ToString() + "5", 739.99
                },
                {
                    Note.G.ToString() + "5", 783.99
                },
                {
                    Note.GsharpAflat.ToString() + "5", 830.61
                },
                {
                    Note.A.ToString() + "5", 880.00
                },
                {
                    Note.AsharpBflat.ToString() + "5", 932.33
                },
                {
                    Note.B.ToString() + "5", 987.77
                },

                {
                Note.C.ToString() + "6", 1046.50
                },
                {
                    Note.CsharpDflat.ToString() + "6", 1108.73
                },
                {
                    Note.D.ToString() + "6", 1174.66
                },
                {
                    Note.DsharpEflat.ToString() + "6", 1244.51
                },
                {
                    Note.E.ToString() + "6", 1318.51
                },
                {
                    Note.F.ToString() + "6", 1396.91
                },
                {
                    Note.FsharpGflat.ToString() + "6", 1479.98
                },
                {
                    Note.G.ToString() + "6", 1567.98
                },
                {
                    Note.GsharpAflat.ToString() + "6", 1661.22
                },
                {
                    Note.A.ToString() + "6", 1760.00
                },
                {
                    Note.AsharpBflat.ToString() + "6", 1864.66
                },
                {
                    Note.B.ToString() + "6", 1975.53
                }
            };

            return lookup[tone.Id];
        }
        public static void Play(double frequency, double milliseconds = 400)
        {
            var sineMilliseconds = new SignalGenerator()
                {
                    Gain = 0.2,
                    Frequency = frequency,
                    Type = SignalGeneratorType.Sin
                }
                .Take(TimeSpan.FromMilliseconds(milliseconds));
            using (var wo = new WaveOutEvent())
            {
                wo.Init(sineMilliseconds);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1);
                }
            }
        }

        public static void Play(this Tone tone, double milliseconds)
        {
            if (tone.Note == Note.Rest)
            {
                Thread.Sleep((int)tone.Length);
            }
            else
            {
                Play(FrequencyByTone(tone), milliseconds);
            }
        }
        
    }

}
