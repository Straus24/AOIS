using System.Data.Entity;

namespace UdpServer
{
    class SongContext:DbContext
    {
        public SongContext() : base("DbConnection") 
        { }
        public DbSet<Song> songs { get; set; }
    }
}
