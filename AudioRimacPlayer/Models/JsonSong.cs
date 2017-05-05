using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioRimacPlayer.Models
{
    public class JsonSong
    {
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string ImgUrl { get; set; }
        public string YouTubeUrl { get; set; }
    }
}