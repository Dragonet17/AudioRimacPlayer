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
        public Song MusicSong { get; set; }

        public string PartialName { get; set; }

        public string FormPartialName { get; set; }
        
        public string AlbumArtistName { get; set; }

        public PlayerViewModel()
        {
            this.FormPartialName = "_Songs";
            this.PartialName = "_Empty";
        }

        public void SetPartialName(string partialname)
        {
            if (partialname == "_Songs" ||
                partialname == "_Artists" ||
                partialname == "_Albums" ||
                partialname == "_AlbumSongs")
            {
                this.PartialName = partialname;
            }
            else if (partialname == "_MusicAlbumsSongs" || partialname == "_MusicSongs")
            {
                this.PartialName = "_Music";
            }
            else
            {
                throw new Exception();
            }
        }

        public  void SetFormPartialName(string form, string partialname)
        {
            if (form == "_Songs" && 
                partialname == "_Songs")
            {
                this.FormPartialName = "_Songs";


            }
            if (form == "_Artists" && (
                partialname == "_Artists" || 
                partialname == "_Albums" || 
                partialname == "_AlbumSongs"))
            {
                this.FormPartialName = "_Artists";

            }
            
        }


    }
}