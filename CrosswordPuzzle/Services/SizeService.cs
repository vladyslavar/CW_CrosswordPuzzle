using CrosswordPuzzle.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Services
{
    public interface ISizeService
    {
        public void LoadDB();
        public void CloseDB();
        public int MaxAvailableForPuzzleWords(int allPossibleWords);
    }
    public  class SizeService : ISizeService
    {
        private DBActions _dbActions;
        public SizeService(DBActions dbActions)
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
        public int MaxAvailableForPuzzleWords(int allPossibleWords)
        {
            if (allPossibleWords <= 14)
            {
                return -1;
            }
            else
            {
                if (allPossibleWords > 14 && allPossibleWords < 60)
                {
                    int sav = allPossibleWords / 3;
                    int lefted = allPossibleWords % 5;
                    int availwords = allPossibleWords - lefted- sav;
                    return availwords;
                }
                else if (allPossibleWords >= 60)
                {
                    return 50;
                }
                else return -1;
            }

        }
    }
}
