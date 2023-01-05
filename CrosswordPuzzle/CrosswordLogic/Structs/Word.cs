using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.CrosswordLogic.Structs
{
    public class Word
    {
        public string word;
        public Possition possition;
        public bool horizontal;
        public int numering;
        public string meaning;
        public Word(string word, Possition possition, bool IsHorizontal, int numering, string meaning)
        {
            this.word = word;
            this.possition = possition;
            this.horizontal = IsHorizontal;
            this.numering = numering;
            this.meaning = meaning;
        }
    }
}
