using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class Tone
    {
        public Note Note { get; set; }
        public int? Octave { get; set; }
        public NoteLength Length { get; set; }
        public double Volume { get; set; }
        public string Id => Note.ToString() + Octave.ToString();
    }
}
