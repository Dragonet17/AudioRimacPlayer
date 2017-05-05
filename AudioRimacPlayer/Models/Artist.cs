using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace AudioRimacPlayer.Models
{
    public class Artist
    {
        public int ArtistId { get; protected set; }
        public string ArtistName { get;  set; }
        public string ArtistImagetUrl { get; protected set; }

        public static async Task<List<Artist>> GetArtistAsync(string search)
        {
            Uri urladdress =
               new Uri(
                   $"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={search}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            var jsons = await Song.GetJsonSongsAsync(urladdress);
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["results"]["artistmatches"]["artist"];
            int counter = jsonsearch.ToArray().Length;
            int addC = 0;

            List<Artist> artistsList = new List<Artist>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1 + addC; i++)
            {


                Artist artist = new Artist();

                artist.ArtistId = i;
                artist.ArtistName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                artist.ArtistName = CutString(artist.ArtistName, 35);



                artist.ArtistImagetUrl = jsonsearch[i]["image"][3]["#text"].ToString();

                if (artist.ArtistImagetUrl.IsEmpty())
                    artist.ArtistImagetUrl =
                        "https://lastfm-img2.akamaized.net/i/u/300x300/300a940c99336f7629444215d14c4dac.png";

                var checkArtist = artistsList.Where(s => s.ArtistName == artist.ArtistName).ToList();
                if (checkArtist.Count > 0)
                    addC = addC + 1;
                else artistsList.Add(artist);

            }



            return artistsList;
        }


        public static string CutString(string name, int numberofchars)
        {
            if (name.Length > numberofchars)
            {

                name = name.Remove(numberofchars, name.Length - numberofchars);
            }
            return name;
        }

        public static string EncodeStringUtf8(string name)
        {
            byte[] artistBytes = new byte[100];

            artistBytes = Encoding.Default.GetBytes(name);
            var encodingString = Encoding.UTF8.GetString(artistBytes);

            return encodingString;
        }


        public static List<Artist> GetArtist(string search)
        {
            Uri urladdress =
               new Uri(
                   $"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={search}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");

            WebClient wc = new WebClient();

            var jsons = wc.DownloadString(urladdress);
            var jsonobj = JObject.Parse(jsons);
            var jsonsearch = jsonobj["results"]["artistmatches"]["artist"];
            int counter = jsonsearch.ToArray().Length;
            int addC = 0;

            List<Artist> artistsList = new List<Artist>();
            if (counter > 10) counter = 10;
            for (int i = 0; i <= counter - 1 + addC; i++)
            {


                Artist artist = new Artist();

                artist.ArtistId = i;
                artist.ArtistName = EncodeStringUtf8(jsonsearch[i]["name"].ToString());
                artist.ArtistName = CutString(artist.ArtistName,35);



                artist.ArtistImagetUrl = jsonsearch[i]["image"][3]["#text"].ToString();

                if (artist.ArtistImagetUrl.IsEmpty())
                    artist.ArtistImagetUrl =
                        "https://lastfm-img2.akamaized.net/i/u/300x300/300a940c99336f7629444215d14c4dac.png";

                var checkArtist = artistsList.Where(s => s.ArtistName == artist.ArtistName).ToList();
                if (checkArtist.Count > 0)
                    addC = addC + 1;
                else artistsList.Add(artist);

            }



            return artistsList;
        }



       



        //public static async Task<string> GetArtist(string searchartist)
        //{
        //    searchartist = "gorniak";
        //    var httpClient = new HttpClient();
        //    Uri urladdress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={searchartist}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");
        //    var response = await httpClient.GetAsync(urladdress);
        //    var result = await response.Content.ReadAsStringAsync();
        //    Artist artist = new Artist();

        //    return artist.ArtistName;
        //}
        //public static Artist GetArtist(string searchartist)
        //{
        //    searchartist = "gorniak";
        //    var httpClient = new HttpClient();
        //    Uri urladdress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={searchartist}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");
        //    var response = await httpClient.GetAsync(urladdress);
        //    var result = await response.Content.ReadAsStringAsync();
        //    Artist artist = new Artist();

        //    return artist;
        //}


        //    Uri urladdress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist=gorniak&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");
        //    Artist artist = new Artist();

        //        using (WebClient wc = new WebClient())
        //        {
        //            var jsons = wc.DownloadString(urladdress);
        //var obj = JObject.Parse(jsons);
        //artist.ArtistName = obj["results"]["artistmatches"]["artist"][0]["name"].ToString();

        //}

        //public static List<Models.Artist> GetArtistsList(string search)
        // {
        //     var artistsJsonList = ArtistJson.GetArtistsJsonList(search);
        //     List<Models.Artist> artistsList = new List<Models.Artist>();

        //     for (int i = 0; i < artistsJsonList.Count; i++)
        //     {
        //         Models.Artist artist = new Models.Artist();
        //         artist.ArtistName = artistsJsonList[0].artist[i].name;
        //         artist.CovertUrl = artistsJsonList[0].artist[i].image[0].text;
        //         artistsList.Add(artist);
        //     }


        //     return artistsList;
        // }
    }
}