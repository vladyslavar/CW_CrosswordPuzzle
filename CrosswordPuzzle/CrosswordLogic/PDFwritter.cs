using CrosswordPuzzle.CrosswordLogic.Structs;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CrosswordPuzzle.CrosswordLogic
{
    public class PDFwritter
    {
        private List<Word> words;
        private List<List<char>> chars;

        private PdfDocument pdfDocument;
        public PDFwritter(List<Word> words,  List<List<char>> chars)
        {
            this.words = words;
            this.chars = chars;
        }
        public void WritePDF()
        {
            pdfDocument = new PdfDocument();
            PdfPage puzzlePage = pdfDocument.AddPage();
            XGraphics puzzlegfx = XGraphics.FromPdfPage(puzzlePage);
            XFont puzzleFont = new XFont("Verdana", 16);

            XStringFormat puzzleFormat = new XStringFormat();
            puzzleFormat.Alignment = XStringAlignment.Center;
            puzzleFormat.LineAlignment = XLineAlignment.Center;

            XFont numFont = new XFont("Verdana", 8);
            DrawPDFPuzzle(puzzlePage, puzzlegfx, puzzleFont, puzzleFormat, numFont);

            PdfPage questionPage = pdfDocument.AddPage();
            XGraphics questiongfx = XGraphics.FromPdfPage(questionPage);
            XFont questionFont = new XFont("Verdana", 14);
            XFont questionTitleFont = new XFont("Verdana", 16, XFontStyle.Bold);
            XFont questionMainTitleFont = new XFont("Verdana", 18, XFontStyle.Bold);
            DrawPDFPuzzleInfo(true, questionPage, questiongfx, questionFont, questionTitleFont, questionMainTitleFont);

            PdfPage answersPage = pdfDocument.AddPage();
            XGraphics answersfx = XGraphics.FromPdfPage(answersPage);
            DrawPDFPuzzleInfo(false, answersPage, answersfx, questionFont, questionTitleFont, questionMainTitleFont);

            string fileName = "Puzzle.pdf";
            pdfDocument.Save(fileName);
        }
        private void DrawPDFPuzzle(PdfPage page, XGraphics gfx, XFont font, XStringFormat format, XFont numFont)
        {
            int startTop = 20;
            int startLeft = 20;
            int sizeW = Convert.ToInt32((page.Width - 80) / chars[0].Count);
            int sizeH = Convert.ToInt32((page.Height - 80) / chars[0].Count);
            int size;
            if (sizeH < sizeW) size = sizeH;
            else size = sizeW;

            foreach (Word word in words)
            {
                int top = startTop + (word.possition.X * size);
                int left = startLeft + (word.possition.Y * size);

                if (word.horizontal)
                {
                    //draw num
                    gfx.DrawString(word.numering.ToString(), numFont, XBrushes.Black, new XPoint(left - numFont.Height, top + (size / 2)));
                    foreach(var letter in word.word.ToCharArray())
                    {
                        XRect rectangle = new XRect(new XPoint(left, top), new XSize(size, size));
                        gfx.DrawRectangle(XPens.Black, rectangle);
                        //gfx.DrawString(letter.ToString(), font, XBrushes.Black, rectangle, format);
                        left += size;
                    }
                }
                if(!word.horizontal)
                {
                    gfx.DrawString(word.numering.ToString(), numFont, XBrushes.Black, new XPoint(left + (size / 2) - (numFont.Height / 2), top - (size / 2) + (size / 4)));
                    foreach (var letter in word.word.ToCharArray())
                    {
                        XRect rectangle = new XRect(new XPoint(left, top), new XSize(size, size));
                        gfx.DrawRectangle(XPens.Black, rectangle);
                        //gfx.DrawString(letter.ToString(), font, XBrushes.Black, rectangle, format);
                        top += size;
                    }
                }
            }

        }
        private PdfPage CreateNewPage()
        {
            PdfPage page = new PdfPage();
            page = pdfDocument.AddPage();
            return page;
        }
        // questions == true --- draw puzzle questions
        // questions == false --- draw puzzle answers
        private void DrawPDFPuzzleInfo(bool questions, PdfPage inPage, XGraphics gfx, XFont mainFont, XFont titleFont, XFont mainTitleFont)
        {
            PdfPage page = inPage;
            int leftMargin = 20;
            int rightMargin = 40;
            XTextFormatter tf = new XTextFormatter(gfx);
            int mainOffset = mainFont.Height;
            int titleOffset = titleFont.Height;
            int mainTitleOffset = mainTitleFont.Height;
            int currentHeight = 10;

           
            XStringFormat strFormat = new XStringFormat();
            if(questions)
                tf.DrawString("Questions", mainTitleFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
            if (!questions)
                tf.DrawString("Answers", mainTitleFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
            currentHeight += mainTitleOffset;

            tf.DrawString(Environment.NewLine + "HORIZONTAL: " + Environment.NewLine, titleFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
            currentHeight += titleOffset *2;
            foreach (var word in words)
            {
                if (word.horizontal)
                {
                    if (currentHeight > page.Height - rightMargin)
                    {
                        currentHeight = 10;
                        page = CreateNewPage();
                        gfx = XGraphics.FromPdfPage(page);
                        tf = new XTextFormatter(gfx);
                    }

                    if(questions)
                    {
                        tf.DrawString(word.numering + ". " + word.meaning + Environment.NewLine, mainFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
                        XSize size = gfx.MeasureString(word.numering + ". " + word.meaning, mainFont);
                        int offsetsNum = (int)Math.Ceiling(size.Width / (page.Width - (rightMargin * 2)));
                        currentHeight += (mainOffset * offsetsNum);
                    }
                    if (!questions)
                    {
                        tf.DrawString(word.numering + ". " + word.word + Environment.NewLine, mainFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
                        currentHeight += (mainOffset);
                    }
                }
            }

            if (currentHeight > page.Height - rightMargin)
            {
                currentHeight = 10;
                page = CreateNewPage();
                gfx = XGraphics.FromPdfPage(page);
                tf = new XTextFormatter(gfx);
            }
            tf.DrawString(Environment.NewLine + "VERTICAL: " + Environment.NewLine, titleFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
            currentHeight += (titleOffset *2);
            
            foreach (var word in words)
            {
                if (!word.horizontal)
                {
                    if (currentHeight > page.Height - rightMargin)
                    {
                        currentHeight = 10;
                        page = CreateNewPage();
                        gfx = XGraphics.FromPdfPage(page);
                        tf = new XTextFormatter(gfx);
                    }

                    if(questions)
                    {
                        tf.DrawString(word.numering + ". " + word.meaning + Environment.NewLine, mainFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
                        XSize size = gfx.MeasureString(word.numering + ". " + word.meaning, mainFont);
                        int offsetsNum = (int)Math.Ceiling(size.Width / (page.Width - (rightMargin * 2)));
                        currentHeight += (mainOffset * (offsetsNum));
                    }
                    if (!questions)
                    {
                        tf.DrawString(word.numering + ". " + word.word + Environment.NewLine, mainFont, XBrushes.Black, new XRect(new XPoint(leftMargin, currentHeight), new XSize(page.Width - rightMargin, page.Height)));
                        currentHeight += (mainOffset);
                    }
                }
            }

        }
    }
}
