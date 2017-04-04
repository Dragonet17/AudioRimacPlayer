using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioRimacPlayer.Models
{
    public class json
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

    }
}