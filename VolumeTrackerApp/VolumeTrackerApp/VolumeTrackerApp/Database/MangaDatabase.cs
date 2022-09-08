using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VolumeTrackerApp.Models;

namespace VolumeTrackerApp.Database
{
    public class MangaDatabase
    {
        private readonly SQLiteAsyncConnection mangaDatabase;

        public MangaDatabase(string dbPath)
        {
            mangaDatabase = new SQLiteAsyncConnection(dbPath);
            mangaDatabase.CreateTableAsync<MangaObject>();
        }

        public Task<List<MangaObject>> GetMangaCollecting()
        {
            return mangaDatabase.Table<MangaObject>().ToListAsync();
        }

        public Task<int> SaveMangaAsync(MangaObject manga)
        {
            return mangaDatabase.InsertAsync(manga);
        }

        public bool MangaInList(int id)
        {
            List<MangaObject> Manga = mangaDatabase.Table<MangaObject>().ToListAsync().Result;

            foreach(MangaObject manga in Manga)
            {
                if(manga.id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public Task<int> DeleteManga(MangaObject manga)
        {
            return mangaDatabase.DeleteAsync(manga);
        }
    }
}
