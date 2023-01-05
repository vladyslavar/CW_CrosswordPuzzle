using CrosswordPuzzle.CrosswordLogic;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Presentors
{
    public class PuzzlePresentor
    {
        IPuzzleService _puzzleService;
        IPuzzleView _view;

        public IPuzzleView PuzzleView { get { return _view; } }

        public PuzzlePresentor(IPuzzleView view, IPuzzleService puzzleService)
        {
            _puzzleService = puzzleService;
            _view = view;

            _view.LoadDbHandler += LoadDB;
            _view.CloseDbHandler += CloseDB;
            _view.InitDictionariesHandler += InitializeDictionaries;
            _view.SetCrosswordPuzzleHandler += SetPuzzle;
        }

        public void Run()
        {
            _view.FormRun();
        }
        public void LoadDB()
        {
            _puzzleService.LoadDB();
        }
        public void CloseDB()
        {
            _puzzleService.CloseDB();
        }
        public void SwitchToPuzzleForm(string theme, int size)
        {
            _view.FormShow(theme, size);
        }
        public Dictionaries InitializeDictionaries(int size, string theme)
        {
            return _puzzleService.InitializeDictionaries(size, theme);
        }
        public TrimmedData SetPuzzle(Dictionary<string, string> obligatoryDictionary, Dictionary<string, string> additionalDictionary)
        {
            return _puzzleService.SetPuzzle(obligatoryDictionary, additionalDictionary);
        }
    }
    public class Dictionaries
    {
        public Dictionary<string, string> obligatoryDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> additionalDictionary = new Dictionary<string, string>();
        public Dictionaries(Dictionary<string, string> obligatoryDictionary, Dictionary<string, string> additionalDictionary)
        {
            this.obligatoryDictionary = obligatoryDictionary;
            this.additionalDictionary = additionalDictionary;
        }
    }
}
