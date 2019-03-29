using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class TimeSignature
    {
        public int Beats { get; set; }
        public NoteLength BeatType { get; set; }
        public double BarTime => Beats * (double) BeatType;
    }
}
