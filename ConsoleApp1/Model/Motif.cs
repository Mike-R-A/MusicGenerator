using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class Motif
    {
        public Motif()
        {
            Pitches = new List<int>();
            Rhythm = new List<NoteLength>();
        }
        public List<int> Pitches { get; set; }
        public List<NoteLength> Rhythm { get; set; }
    }
}
