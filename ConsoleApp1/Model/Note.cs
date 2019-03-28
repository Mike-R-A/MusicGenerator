using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public enum Note
    {
        A,
        AsharpBflat,
        B,
        C,
        CsharpDflat,
        D,
        DsharpEflat,
        E,
        F,
        FsharpGflat,
        G,
        GsharpAflat
    }

    public enum NoteLength
    {
        Semibreve = 960,
        DottedMinim = 840,
        Minim = 480,
        DottedCrotchet = 360,
        Crotchet = 240,
        DottedQuaver = 180,
        Quaver = 120,
        SemiQuaver = 60
    }
}
