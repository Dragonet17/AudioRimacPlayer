using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AudioRimacPlayer.Models
{
    public class Album : Artist
    {
        public int AlbumId { get; protected set; }
        public string AlbumName { get; protected set; }

        public string AlbumCovertUrl { get; protected set; }

        public static async Task<List<Album>> GetArtistAlbumsAsync(Artist artist)
        {
            Uri urladdress =
              new Uri(
                $"http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist={artist.ArtistName}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            

            var jsons = await Song.GetJsonSongsAsync(urladdress); 
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["topalbums"]["album"];
            int counter = jsonsearch.ToArray().Length;


            List<Album> albums = new List<Album>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1; i++)
            {
                //song.ArtistName = EncodeStringUtf8(jsonsearch[i]["artist"].ToString());
                //song.ArtistName = CutString(song.ArtistName, 26);

                Album album = new Album();
                album.AlbumId = i;
                album.ArtistName = artist.ArtistName;


                album.AlbumName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                album.AlbumName = CutString(album.AlbumName, 36);

                album.AlbumCovertUrl = EncodeStringUtf8(jsonsearch[i]["image"][3]["#text"].ToString());
                album.ArtistImagetUrl = artist.ArtistImagetUrl;
                albums.Add(album);

            }

            return albums;

        }


            public static async Task<List<Album>> GetArtistAlbums(Artist artist)
        {
            Uri urladdress =
              new Uri(
                $"http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist={artist.ArtistName}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            var jsons = await Song.GetJsonSongsAsync(urladdress);
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["topalbums"]["album"];
            int counter = jsonsearch.ToArray().Length;
          

            List<Album> albums = new List<Album>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1; i++)
            {
                //song.ArtistName = EncodeStringUtf8(jsonsearch[i]["artist"].ToString());
                //song.ArtistName = CutString(song.ArtistName, 26);

                Album album = new Album();
                album.AlbumId = i;
                album.ArtistName = artist.ArtistName;
               

                album.AlbumName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                album.AlbumName = CutString(album.AlbumName, 36);

                album.AlbumCovertUrl = EncodeStringUtf8(jsonsearch[i]["image"][3]["#text"].ToString());
                album.ArtistImagetUrl = artist.ArtistImagetUrl;
                albums.Add(album);

            }
           
            return albums;
        }

     
    }
}