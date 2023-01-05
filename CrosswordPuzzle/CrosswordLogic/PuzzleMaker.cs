using CrosswordPuzzle.CrosswordLogic.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.CrosswordLogic
{
    public class PuzzleMaker
    {

        //private Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "longword", "blablabla its meaning" }, { "tense", "blablabla its meaning2" }, { "something", "1111111" }, { "woos", "54565" } };
        
        private List<Letter> settedLetter = new List<Letter>();
        private List<List<char>> chars = new List<List<char>>();
        List<KeyValuePair<string, string>> unsettable = new List<KeyValuePair<string, string>>();


        public List<Word> words = new List<Word>();
        private void SetChars(Dictionary<string, string> dictionary)
        {
            int size = dictionary.Count * 10;
            for (int i = 0; i < size; i++)
            {
                List<char> charList = new List<char>();
                for (int j = 0; j < size; j++)
                {
                    charList.Add('\0');
                }
                chars.Add(charList);
            }
        }
        public List<List<char>> Puzzle(Dictionary<string, string> maindictionary, Dictionary<string, string> additionalDictionary)
        {
            if (maindictionary == null) maindictionary = new Dictionary<string, string>();
            if(additionalDictionary == null) additionalDictionary = new Dictionary<string, string>();

            int horizontalNumering = 1;
            int verticalNumering = 1;
            SetChars(maindictionary);
            maindictionary = SortDictionary(maindictionary);

            List<KeyValuePair<string, string>> dictionary = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> pair in maindictionary)
            {
                dictionary.Add(new KeyValuePair<string, string>(pair.Key.Clone().ToString(), pair.Value.Clone().ToString()));
            }

            while (dictionary.Count > 0)
            {
                KeyValuePair<string, string> neededWord = dictionary.First();
                //dictionary.Remove(neededWord.Key);
                if (neededWord.Key.Length < chars.Count)
                {
                    //first word
                    if (settedLetter.Count == 0)
                    {
                        //!!!!SEt IN CENTER!!!!!!
                        Random random = new Random();
                        int x = -1, y = -1;
                        while (x < 0 || x > chars.Count - neededWord.Key.Length
                            && y < 0 || y > chars.Count - neededWord.Key.Length)
                        {
                            x = random.Next(chars.Count);
                            y = random.Next(chars[0].Count);
                        }
                        for (int i = 0; i < neededWord.Key.Length; i++)
                        {
                            //set first word horizontaly
                            if (i == 0)
                            {
                                words.Add(new Word(neededWord.Key, new Possition(x, y + i), true, horizontalNumering, neededWord.Value));
                                horizontalNumering++;
                            }
                            chars[x][y + i] = neededWord.Key.ToCharArray()[i];
                            settedLetter.Add(new Letter(neededWord.Key.ToCharArray()[i], new Possition(x, y + i)));
                        }
                        dictionary.Remove(neededWord);
                        //Display(chars);
                    }
                    else
                    {
                        bool isPlaced = false;
                        for (int i = 0; i < neededWord.Key.Length; i++)
                        {
                            foreach (var letter in settedLetter)
                            {
                                if (neededWord.Key.ToCharArray()[i] == letter.letter)
                                {
                                    //preious word is placed horizontal
                                    // this one is placed verticaly
                                    if (PreviousWordPlacedHorizontaly(letter))
                                    {
                                        int startX = letter.possition.X - i;
                                        int startY = letter.possition.Y;

                                        if (startX > 0 && startX + neededWord.Key.Length < chars.Count)
                                        {
                                            if (CheckIfFits(neededWord, startX, startY, chars, false))
                                            {
                                                for (int j = 0; j < neededWord.Key.Length; j++)
                                                {
                                                    if (j == 0)
                                                    {
                                                        words.Add(new Word(neededWord.Key, new Possition(startX + j, startY), false, verticalNumering, neededWord.Value));
                                                        verticalNumering++;
                                                    }
                                                    chars[startX + j][startY] = neededWord.Key.ToCharArray()[j];
                                                    settedLetter.Add(new Letter(neededWord.Key.ToCharArray()[j], new Possition(startX + j, startY)));

                                                }
                                                DeleteNeighbourLetters(letter, settedLetter);
                                                isPlaced = true;
                                                //Display(chars);
                                                break;
                                            }

                                        }
                                    }
                                    //preious word is placed verticaly
                                    // this one is placed horizontal
                                    if (!PreviousWordPlacedHorizontaly(letter))
                                    {
                                        int startX = letter.possition.X;
                                        int startY = letter.possition.Y - i;

                                        if (startY > 0 && startY + neededWord.Key.Length < chars.Count)
                                        {
                                            if (CheckIfFits(neededWord, startX, startY, chars, true))
                                            {
                                                for (int j = 0; j < neededWord.Key.Length; j++)
                                                {
                                                    if (j == 0)
                                                    {
                                                        words.Add(new Word(neededWord.Key, new Possition(startX, startY + j), true, horizontalNumering, neededWord.Value));
                                                        horizontalNumering++;
                                                    }
                                                    chars[startX][startY + j] = neededWord.Key.ToCharArray()[j];
                                                    settedLetter.Add(new Letter(neededWord.Key.ToCharArray()[j], new Possition(startX, startY + j)));
                                                }
                                                DeleteNeighbourLetters(letter, settedLetter);
                                                isPlaced = true;
                                            }
                                            //Display(chars);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (isPlaced)
                            {
                                dictionary.Remove(neededWord);
                                //dictionary.Add(neededWord.Key, neededWord.Value);
                                break; 
                            }
                        }
                        if (!isPlaced)
                        {
                            if(additionalDictionary.Count > 0)
                            {
                                dictionary.Remove(neededWord);
                                dictionary.Add(additionalDictionary.First());
                                additionalDictionary.Remove(additionalDictionary.First().Key);
                            }
                            else
                            {
                                if (dictionary.Count == unsettable.Count) break;
                                else
                                {
                                    unsettable.Clear();
                                    foreach(var word in dictionary)
                                    {
                                        unsettable.Add(word);
                                    }
                                    dictionary.Remove(neededWord);
                                    dictionary.Add(neededWord);
                                }
                            }
                        }
                    }
                }
                
            }
            //WritePDF(chars);
            return chars;
        }

        private bool PreviousWordPlacedHorizontaly(Letter letter)
        {
            //preious word is placed horizontal
            if (letter.possition.Y > 0 && letter.possition.Y < chars[0].Count - 1 /*19*/)
            {
                if (chars[letter.possition.X][letter.possition.Y + 1] != 0 ||
                chars[letter.possition.X][letter.possition.Y - 1] != 0)
                    return true;
            }
            else if (letter.possition.Y <= 0)
            {
                if (chars[letter.possition.X][letter.possition.Y + 1] != 0)
                    return true;
            }
            else if (letter.possition.Y >= chars[0].Count -1/*19*/)
            {
                if (chars[letter.possition.X][letter.possition.Y - 1] != 0)
                    return true;
            }
            //preious word is placed verticaly
            if (letter.possition.X > 0 && letter.possition.X < chars[0].Count - 1 /*19*/)
            {
                if (chars[letter.possition.X + 1][letter.possition.Y] != 0 ||
                    chars[letter.possition.X - 1][letter.possition.Y] != 0)
                    return false;
            }
            else if (letter.possition.X <= 0)
            {
                if (chars[letter.possition.X + 1][letter.possition.Y] != 0)
                    return false;
            }
            else if (letter.possition.X >= chars[0].Count - 1 /*19*/)
            {
                if (chars[letter.possition.X - 1][letter.possition.Y] != 0)
                    return false;
            }
            return true;
        }
        private bool CheckIfFits(KeyValuePair<string, string> neededWord, int startX, int startY, List<List<char>> chars, bool isHorizontal)
        {
            bool ifFits = true;
            if (isHorizontal)
            {
                for (int i = 0; i < neededWord.Key.Length; i++)
                {
                    if (chars[startX][startY + i] != 0 && chars[startX][startY + i] != neededWord.Key[i])
                    {
                        ifFits = false;
                    }
                    
                    if(startX < chars[0].Count - 1 && startX > 0)
                    {
                        if (chars[startX][startY + i] != neededWord.Key[i] && (chars[startX + 1][startY + i] != 0 || chars[startX - 1][startY + i] != 0))
                        {
                            ifFits = false;
                        }
                    }
                    
                }
                if(startY > 0)
                { if (chars[startX][startY - 1] != 0) ifFits = false; }
                
                if(startY > 0 && startX < chars[0].Count - 1)
                {
                    if(chars[startX - 1][startY - 1] != 0) ifFits = false; 
                }

                if (startY < chars[0].Count - 1)
                { if (chars[startX][startY + neededWord.Key.Length] != 0) ifFits = false; }
            }
            else
            {
                for (int i = 0; i < neededWord.Key.Length; i++)
                {
                    
                    if (chars[startX + i][startY] != 0 && chars[startX + i][startY] != neededWord.Key[i])
                    {
                        ifFits = false;
                    }
                    if(startY < chars[0].Count - 1 && startY > 0)
                    {
                        if (chars[startX + i][startY] != neededWord.Key[i] && (chars[startX + i][startY + 1] != 0 || chars[startX + i][startY - 1] != 0))
                        {
                            ifFits = false;
                        }
                    }
                    
                    
                }
                if (startX > 0)
                { if (chars[startX - 1][startY] != 0) ifFits = false; }

                if(startX > 0 && startY < chars[0].Count - 1)
                {
                    if (chars[startX + 1][startY + 1] != 0) ifFits = false;
                }

                if (startX < chars[0].Count - 1)
                { if (chars[startX + neededWord.Key.Length][startY] != 0) ifFits = false; }

                
                
            }
            return ifFits;
        }
        private List<Letter> DeleteNeighbourLetters(Letter letter, List<Letter> settedLetter)
        {
            List<Possition> possitionsToDelete = new List<Possition>()
            {
                new Possition(letter.possition.X, letter.possition.Y),
                new Possition(letter.possition.X + 1, letter.possition.Y),
                new Possition(letter.possition.X - 1, letter.possition.Y),
                new Possition(letter.possition.X, letter.possition.Y + 1),
                new Possition(letter.possition.X, letter.possition.Y - 1),
            };
            List<Letter> remainLetters = new List<Letter>();
            foreach (Possition possition in possitionsToDelete)
            {
                List<Letter> delete = new List<Letter>();
                for (int i = 0; i < settedLetter.Count; i++)
                {
                    if (possition.X == settedLetter[i].possition.X && possition.Y == settedLetter[i].possition.Y)
                    {
                        delete.Add(settedLetter[i]);
                    }
                }
                foreach (var index in delete)
                {
                    settedLetter.Remove(index);
                }
            }
            return remainLetters;
        }
        private Dictionary<string, string> SortDictionary(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> sortedDictionary = new Dictionary<string, string>();
            while (dictionary.Count > 0)
            {
                KeyValuePair<string, string> currentLongest = new KeyValuePair<string, string>("", "");

                foreach (var word in dictionary)
                {
                    if (word.Key.Length > currentLongest.Key.Length)
                    {
                        currentLongest = word;
                    }
                }
                sortedDictionary.Add(currentLongest.Key, currentLongest.Value);
                dictionary.Remove(currentLongest.Key);
            }
            return sortedDictionary;
        }
        private void Display(List<List<char>> chars)
        {
            for (int i = 0; i < chars.Count; i++)
            {
                for (int j = 0; j < chars[0].Count; j++)
                {
                    if (chars[i][j] == 0) Console.Write(" ");
                    else Console.Write(chars[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
