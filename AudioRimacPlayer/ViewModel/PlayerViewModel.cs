using AudioRimacPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioRimacPlayer.ViewModel
{
    public class PlayerViewModel
    {
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public Song AlbumSongs { get; set; }

        public IEnumerable<Song> Songs { get; set; }

        public string PartialName { get; set; }

        public static  string FormPartialName { get; set; }

        //public enum PartialNames
        //{
        //    _Songs,
        //    _Artists,
        //    _Albums,
        //    _AlbumsSongs,
        //    _Empty
        //}

    }
}