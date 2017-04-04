using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AudioRimacPlayer.DAL
{


    public class PlayerContext : DbContext
    {
        public PlayerContext() : base("AudioRimacPlayer")
        {

        }
        public DbSet<PlayerSong> PlayerSongs { get; set; }
        public DbSet<SearchSong> SearchSongs { get; set; }


    }
}