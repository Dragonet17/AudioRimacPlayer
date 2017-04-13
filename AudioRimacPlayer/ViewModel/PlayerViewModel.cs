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

        public string FormPartialName { get; set; }




        public static string SetFormPartialName(string form)
        {
            string[] name = {"_Songs","_Artists" };
            if (form == "_Songs" && name[0] != "_Songs")
            {
                Array.Reverse(name);
                
            }
            if (form == "_Artists" && name[0] != "_Artists")
            {
                Array.Reverse(name);
               
            }

            return name[0];


        }
    }
}