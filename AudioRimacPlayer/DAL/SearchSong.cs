using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioRimacPlayer.DAL
{
    public class SearchSong
    {

        public int Id { get; set; }
        
        public string SongSearching { get; set; }

        public string ArtistSearching { get; set; }

    }
}