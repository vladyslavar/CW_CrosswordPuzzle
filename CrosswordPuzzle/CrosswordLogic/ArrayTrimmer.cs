using CrosswordPuzzle.CrosswordLogic.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.CrosswordLogic
{
    public class ArrayTrimmer
    {
        public TrimmedData Trim(List<List<char>> input, List<Word> words)
        {
            int top = Int32.MaxValue, right = -1, left = Int32.MaxValue, bottom = -1;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Count; j++)
                {
                    if (input[i][j] != '\0')
                    {
                        if (i < top) { top = i; }
                        if (j > right) { right = j; }
                        if (i > bottom) { bottom = i; }
                        if (j < left) { left = j; }
                    }
                }
            }
            //array
            List<List<char>> ret = new List<List<char>>();
            for (int i = top; i <= bottom; i++)
            {
                ret.Add(new List<char>());
                for (int j = left; j <= right; j++)
                {
                    ret[i - top].Add(input[i][j]);
                }
            }
            //words
            foreach(var word in words)
            {
                word.possition.X = word.possition.X - top;
                word.possition.Y = word.possition.Y - left;
            }
            TrimmedData trimmedData = new TrimmedData(ret, words);
            return trimmedData;
        }
    }
    public class TrimmedData
    {
        public List<List<char>> array;
        public List<Word> words;
        public TrimmedData(List<List<char>> array, List<Word> words)
        {
            this.array = array;
            this.words = words;
        }   
    }

}
