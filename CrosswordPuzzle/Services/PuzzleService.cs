using CrosswordPuzzle.CrosswordLogic;
using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Presentors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Services
{

    public interface IPuzzleService
    {
        public void LoadDB();
        public void CloseDB();
        public Dictionaries InitializeDictionaries(int size, string theme);
        public TrimmedData SetPuzzle(Dictionary<string, string> obligatoryDictionary, Dictionary<string, string> additionalDictionary);
    }

    public class PuzzleService : IPuzzleService
    {
        private DBActions _dbActions;
        public PuzzleService(DBActions dbActions)
        {
            this._dbActions = dbActions;
        }
        public void LoadDB()
        {
            _dbActions.Load();
        }
        public void CloseDB()
        {
            _dbActions.Close();
        }

        public Dictionaries InitializeDictionaries(int size, string theme)
        {
            Dictionary<string, string> obligatoryDictionary = new Dictionary<string, string>();
            Dictionary<string, string> additionalDictionary = new Dictionary<string, string>();

            int themeId;
            var t = _dbActions.GetCategoryByName(theme).FirstOrDefault();
            if(t != null)
            {
                themeId = t.Id;
                var availableWords = _dbActions.GetWordByCategory(themeId).ToList();
                int numOfWords = availableWords.Count();
                Random random = new Random();
                for (int i = 0; i < (size + (size / 3)); i++)
                {
                    DataBase.DBEntities.Word oblWord;
                    bool oblNotAdded = true;
                    while (oblNotAdded)
                    {
                        do
                        {
                            int wordId = random.Next(1, numOfWords);
                            oblWord = availableWords.ElementAt(wordId);
                        } while (oblWord == null);
                        try
                        {
                            obligatoryDictionary.Add(oblWord.Name, oblWord.Meaning);
                            oblNotAdded = false;
                        }
                        catch (System.ArgumentException)
                        {
                            oblNotAdded = true;
                        }
                    }
                }
                for (int i = 0; i < (size / 3); i++)
                {
                    additionalDictionary.Add(obligatoryDictionary.ElementAt(size).Key, obligatoryDictionary.ElementAt(size).Value);
                    obligatoryDictionary.Remove(obligatoryDictionary.ElementAt(size).Key);
                }
            }
            
            Dictionaries dictionaries = new Dictionaries(obligatoryDictionary, additionalDictionary);
            return dictionaries;
        }
        public TrimmedData SetPuzzle(Dictionary<string, string> obligatoryDictionary, Dictionary<string, string> additionalDictionary)
        {
            PuzzleMaker puzzleMaker = new PuzzleMaker();
            ArrayTrimmer trimmer = new ArrayTrimmer();

            List<List<char>> puzzleArr = puzzleMaker.Puzzle(obligatoryDictionary, additionalDictionary);
            List<CrosswordPuzzle.CrosswordLogic.Structs.Word > words = puzzleMaker.words;

            TrimmedData data = trimmer.Trim(puzzleArr, words);
            return data;
        }
    }
}
