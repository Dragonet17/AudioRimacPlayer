using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AudioRimacPlayer.ViewModel;

namespace AudioRimacPlayer.Models
{
    public class JsonSong
    {
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string ImgUrl { get; set; }
        public string YouTubeUrl { get; set; }

        public JsonSong(PlayerViewModel playerViewModel)
        {

            SongName = playerViewModel.MusicSong.SongName;
            ArtistName = playerViewModel.MusicSong.ArtistName;
            ImgUrl = playerViewModel.MusicSong.ArtistImagetUrl;
            YouTubeUrl = playerViewModel.MusicSong.YouTubeUrl;
        }
        
        public static JsonSong SerializeToJsonSongObject(string musicsong)
        { 
            var json_serializer = new JavaScriptSerializer();
            var jsonSong =
                (JsonSong)json_serializer.DeserializeObject(musicsong);
            return jsonSong;
        }
    }

    
}