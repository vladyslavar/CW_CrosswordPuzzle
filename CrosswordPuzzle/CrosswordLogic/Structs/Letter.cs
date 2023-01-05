using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.CrosswordLogic.Structs
{
    public class Letter
    {
        public char letter;
        public Possition possition;
        public Letter(char letter, Possition possition)
        {
            this.letter = letter;
            this.possition = possition;
        }
    }
}
