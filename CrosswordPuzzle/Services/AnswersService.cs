using CrosswordPuzzle.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Services
{
    public interface IAnswersService
    { 
    
    }
    internal class AnswersService: IAnswersService
    {
        private DBActions _dbActions;
        public AnswersService(DBActions dbActions)
        {
            this._dbActions = dbActions;
        }
    }
}
