using CrosswordPuzzle.CrosswordLogic.Structs;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Presentors
{
    public class AnswersPresentor
    {
        public IAnswersFormView _answersFormView;
        public IAnswersService _answersService;

        public IAnswersFormView AnswersView { get { return _answersFormView; } }

        public AnswersPresentor(IAnswersFormView answersFormView, IAnswersService answersService)
        {
            _answersFormView = answersFormView;
            _answersService = answersService;
        }

        public void Run()
        {
            _answersFormView.FormRun();
        }
        public void SwitchToAnswersForm(Button button, List<Word> words)
        {
            _answersFormView.FormShow(button, words);
        }
    }
}
