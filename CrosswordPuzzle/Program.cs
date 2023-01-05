using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Presentors;
using CrosswordPuzzle.Services;

namespace CrosswordPuzzle
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            DBContext dBContext = new DBContext();
            DBActions dBActions = new DBActions(dBContext);

            
            MainFormPresentor mainFormPresentor = new MainFormPresentor(new Form1(), new MainFormService(dBActions));
            SizePresentor sizePresentor = new SizePresentor(new SizeForm(), new SizeService(dBActions));
            CustomizePresentor customizePresentor = new CustomizePresentor(new CustomizeForm(), new CustomizeService(dBActions));
            PuzzlePresentor puzzlePresentor = new PuzzlePresentor(new PuzzleForm(), new PuzzleService(dBActions));
            AnswersPresentor answersPresentor = new AnswersPresentor(new AnswersForm(null, null), new AnswersService(dBActions));

            mainFormPresentor.MainFormView.SwitchToSizeFormEvent += sizePresentor.SwitchToSizeForm;
            mainFormPresentor.MainFormView.SwitchToCustomizeFormEvent += customizePresentor.SwitchToCustomizeForm;
            sizePresentor.SizeFormView.SwitchToMainFormEvent += mainFormPresentor.SwitchToMainForm;
            customizePresentor.CustomizeFormView.SwitchToMainFormEvent += mainFormPresentor.SwitchToMainForm;
            sizePresentor.SizeFormView.SwitchToPuzzleFormEvent += puzzlePresentor.SwitchToPuzzleForm;
            puzzlePresentor.PuzzleView.SwitchToMainFormEvent += mainFormPresentor.SwitchToMainForm;
            puzzlePresentor.PuzzleView.SwitchToAnswersFormEvent += answersPresentor.SwitchToAnswersForm;

            mainFormPresentor.Run();

        }
    }
}