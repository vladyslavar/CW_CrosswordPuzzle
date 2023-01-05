using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Presentors
{
    internal class SizePresentor
    {
        ISizeView _sizeView;
        ISizeService _sizeService;

        public ISizeView SizeFormView { get { return _sizeView; } }
        public SizePresentor(ISizeView sizeView, ISizeService sizeService)
        {
            _sizeView = sizeView;
            _sizeService = sizeService;

            _sizeView.LoadDbHandler = LoadDB;
            _sizeView.CloseDbHandler = CloseDB;
            _sizeView.MaxSizeValueHandler = GetMaxSizeValue;
        }
        public void Run()
        {
            _sizeView.FormRun();
        }
        public void LoadDB()
        {
            _sizeService.LoadDB();
        }
        public void CloseDB()
        {
            _sizeService.CloseDB();
        }
        public void SwitchToSizeForm(string theme, int size)
        {
            _sizeView.FormShow(theme, size);
        }
        public int GetMaxSizeValue(int allPossibleWords)
        {
            return _sizeService.MaxAvailableForPuzzleWords(allPossibleWords);
        }
    }
}
