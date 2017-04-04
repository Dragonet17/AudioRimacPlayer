using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Threading.Tasks;
using System.Net.Http;
using AudioRimacPlayer.DAL;

namespace AudioRimacPlayer.Models
{
    public class Song : Album
    {

        public int SongId { get; private set; }
        //public string ArtistName { get; private set; }
        //public string AlbumName { get; private set; }

        public string SongName { get; private set; }

        //public string GenreName { get; private set; }
        //public string CovertUrl { get; private set; }

        public string YouTubeUrl { get; private set; }
        public Dictionary<int, string> albumSongs = new Dictionary<int, string>();


        public async static Task<Song> GetYouTubeVideoUrlForSong(Song song)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDkS9sfEA6DExEN_N621zEXslxXLTjzbOM",
                ApplicationName = "RimacAudioPlayer"
            });

            var searchListRequest =  youtubeService.Search.List("snippet");
            searchListRequest.Q =  $"{song.SongName} {song.ArtistName}";
            searchListRequest.MaxResults = 2;

            var searchListResponse =await searchListRequest.ExecuteAsync();
            song.YouTubeUrl =  searchListResponse.Items.First().Id.VideoId;
            return song;
        }

        public static async Task<string> GetJsonSongsAsync(Uri url)
        {
            HttpClient client = new HttpClient();            
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;

        }
        public static async Task<List<Song>> GetSongsListAsync(string search)
        {
            Uri urladdress =
              new Uri(
                  $"http://ws.audioscrobbler.com/2.0/?method=track.search&track={search}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            var jsons = await GetJsonSongsAsync(urladdress);
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["results"]["trackmatches"]["track"];
            int counter = jsonsearch.ToArray().Length;
            int addC = 0;

            List<Song> songList = new List<Song>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1 + addC; i++)
            {
                Song song = new Song();

                song.SongId = i;

                song.ArtistName = EncodeStringUtf8(jsonsearch[i]["artist"].ToString());
                song.ArtistName = CutString(song.ArtistName, 26);

                song.SongName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                song.SongName = CutString(song.SongName, 26);

                song.ArtistImagetUrl = jsonsearch[i]["image"][3]["#text"].ToString();

                if (song.ArtistImagetUrl.IsEmpty())
                    song.ArtistImagetUrl =
                        "https://lastfm-img2.akamaized.net/i/u/300x300/300a940c99336f7629444215d14c4dac.png";


                var checkSongTitle = songList.Where(s => s.SongName == song.SongName).ToList();
                if (checkSongTitle.Count > 0)
                    addC = addC + 1;

                else songList.Add(song);



            }

            //var songs = songList.DistinctBy(m => m.SongName).ToList();

            return  songList;
        }

        // DISABLED - the list of Songs without YoutubeVideoUrl
        public static List<Song> GetSongsList(string search)
        {
            Uri urladdress =
               new Uri(
                   $"http://ws.audioscrobbler.com/2.0/?method=track.search&track={search}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            //var jsons =  GetJsonSongsAsync(search, urladdress).Result;

            WebClient wc = new WebClient();

            var jsons = wc.DownloadString(urladdress);

            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["results"]["trackmatches"]["track"];
            int counter = jsonsearch.ToArray().Length;
            int addC = 0;

            List<Song> songList = new List<Song>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1 + addC; i++)
            {
                Song song = new Song();

                song.SongId = i;

                song.ArtistName = EncodeStringUtf8(jsonsearch[i]["artist"].ToString());
                song.ArtistName = CutString(song.ArtistName, 26);

                song.SongName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                song.SongName = CutString(song.SongName, 26);

                song.ArtistImagetUrl = jsonsearch[i]["image"][3]["#text"].ToString();

                if (song.ArtistImagetUrl.IsEmpty())
                    song.ArtistImagetUrl =
                        "https://lastfm-img2.akamaized.net/i/u/300x300/300a940c99336f7629444215d14c4dac.png";


                var checkSongTitle = songList.Where(s => s.SongName == song.SongName).ToList();
                if (checkSongTitle.Count > 0)
                    addC = addC + 1;
                else songList.Add(song);



            }

            //var songs = songList.DistinctBy(m => m.SongName).ToList();

            return songList;
        }

        public async static Task<Song> GetAlbumSongs(Album album)
        {
            Uri urladdress =
              new Uri(
                $"http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=f888d5f469cb97bf8a68b72c9c721cbc&artist={album.ArtistName}&album={album.AlbumName}&format=json");

            var jsons = await GetJsonSongsAsync(urladdress);
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["album"];

            int counter = jsonsearch.ToArray().Length;


            Song song = new Song();

            song.ArtistName = album.ArtistName;
            song.AlbumName = album.AlbumName;
            song.AlbumCovertUrl = jsonsearch["image"][3]["#text"].ToString();
            song.ArtistImagetUrl = album.ArtistImagetUrl;

            var trackArray = jsonsearch["tracks"]["track"];



            for (int i = 0; i < trackArray.ToArray().Length - 1; i++)
            {
                string albumTrack = string.Empty;
                albumTrack = EncodeStringUtf8(trackArray[i]["name"].ToString());
                albumTrack = CutString(albumTrack, 26);
                song.albumSongs.Add(i, albumTrack);

            }



            return song;
        }

        public async static Task<Song> GetYoutubeVideoUrlForAlbumSong(Song song, int id)
        {

            song.SongName = song.albumSongs[id];
            song = await GetYouTubeVideoUrlForSong(song);

            return song;
        }

       

    }
}