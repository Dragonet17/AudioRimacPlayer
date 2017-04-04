using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace AudioRimacPlayer.Models
{
    public class ArtistJson
    {

        public class Rootobject
        {
            public Results results { get; set; }
        }

        public class Results
        {
            public OpensearchQuery opensearchQuery { get; set; }
            public string opensearchtotalResults { get; set; }
            public string opensearchstartIndex { get; set; }
            public string opensearchitemsPerPage { get; set; }
            public Artistmatches artistmatches { get; set; }
            public Attr attr { get; set; }
        }

        public class OpensearchQuery
        {
            public string text { get; set; }
            public string role { get; set; }
            public string searchTerms { get; set; }
            public string startPage { get; set; }
        }

        public class Artistmatches
        {
            public Artist[] artist { get; set; }
        }

        public class Artist
        {
            public string name { get; set; }
            public string listeners { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
            public string streamable { get; set; }
            public Image[] image { get; set; }
        }

        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }

        public class Attr
        {
            public string _for { get; set; }
        }

        public static ArtistJson GetArtistsJsonList(string search)
        {
            Uri urladdress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={search}&api_key=f888d5f469cb97bf8a68b72c9c721cbc&format=json");
            // pobieranie danych
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(urladdress);

            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            var txt = response.ToString();
            // pobierz 
            using (var sr = new StreamReader(response.GetResponseStream()))
            {

                text = sr.ReadToEnd();
            }

            // konwersja danych do listy
            string jsonString = text;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var artistsList = serializer.Deserialize<Models.ArtistJson>(text);
            // Zmieniam format daty na DD.MM.YYYY

            return artistsList;
        }


    }
}