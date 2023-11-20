

namespace UdpServer
{
    class DataController
    {
        public int AddSong(Song song)
        {
            using(SongContext db  = new SongContext())
            {
                db.songs.Add(song);
                db.SaveChanges();
                int id = db.songs.OrderBy(s => s.Id).First().Id;
                return id;
            }
        }
        public void DeleteSong(int index)
        {
            using(SongContext db = new SongContext())
            {
                Song song = db.songs.Find(index);
                if (song == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    db.songs.Remove(song);
                    db.SaveChanges();
                }    
            }
        }
        public void DeleteSongs()
        {
            using (SongContext db = new SongContext()) 
            {
                db.songs.RemoveRange(db.songs);
                db.SaveChanges();
            }
        }
        public Song GetSong(int index)
        {
            using (SongContext db = new SongContext())
            {
                return db.songs.FirstOrDefault(p => p.Id == index);
            }
        }
        public List<Song> GetSongs()
        {
            using(SongContext db = new SongContext())
            {
                return db.songs.ToList();
            }
        }

        public void DeleteDB()
        {
            using (SongContext db = new SongContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE Songs");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Songs', RESEED, 0)");
                db.SaveChanges();
            }
        }
    }
}
