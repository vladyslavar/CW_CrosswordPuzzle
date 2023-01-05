///using CrosswordPuzzle.CrosswordLogic.Structs;
using CrosswordPuzzle.DataBase.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.DataBase
{
    public  class DBActions
    {
        private DBContext? dbContext;
        public DBActions(DBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public void Load()
        {
            this.dbContext?.Database.EnsureCreated();
        }
        public void Close()
        {
            this.dbContext?.Dispose();
            this.dbContext = null;
        }

        // ADD
        public void AddWord(DBEntities.Word word)
        {
            dbContext?.Words.Add(word);
            dbContext?.SaveChanges();
        }
        public void AddCategory(DBEntities.Category category)
        {
            dbContext?.Categories.Add(category);
            dbContext?.SaveChanges();
        }

        // GET
        public IQueryable<DBEntities.Word> GetAllWords()
        {
            var words = dbContext?.Words;
            return words;
        }
        public DBEntities.Word GetWordById(int id)
        {
            var words = dbContext?.Words.Where(n => n.Id == id).FirstOrDefault();
            return words;
        }
        public IQueryable<DBEntities.Word> GetWordByName(string name)
        {
            var words = dbContext?.Words.Where(n => n.Name == name);
            return words;
        }
        public IQueryable<DBEntities.Word> GetWordByNameAndMeaning(string name, string meaning)
        {
            var words = dbContext?.Words.Where(n => n.Name == name && n.Meaning == meaning);
            return words;
        }
        public IQueryable<DBEntities.Word> GetWordByCategory(int category)
        {
            var words = dbContext?.Words.Where(n => n.CategoryId == category);
            return words;
        }

        public IQueryable<DBEntities.Category> GetAllCategories()
        {
            var categories = dbContext?.Categories;
            return categories;
        }
        public IQueryable<DBEntities.Category> GetCategoryById(int id)
        {
            var categories = dbContext?.Categories.Where(n => n.Id == id);
            return categories;
        }
        public IQueryable<DBEntities.Category> GetCategoryByName(string name)
        {
            var categories = dbContext?.Categories.Where(n => n.Name == name);
            return categories;
        }

        // DELETE
        public void DeleteWord(int id)
        {
            var wordToDelete = dbContext?.Words.Where(n => n.Id == id).FirstOrDefault();
            dbContext?.Words.Remove(wordToDelete);
            dbContext?.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            var categoryToDelete = dbContext?.Categories.Where(n => n.Id == id).FirstOrDefault();
            dbContext?.Categories.Remove(categoryToDelete);
            dbContext?.SaveChanges();
        }
    }
}
