using System;
using AudioRimacPlayer.Models;
using AudioRimacPlayer.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayerTest
{
    [TestClass]
    public class UnitTest1
    {
        private Song song;
    partial class Music: Song
    {
        public Music()
        {
            SongName = "Bon Jovi";
            ArtistName = "Livin' On A Prayer";
        }
    }

        public UnitTest1()
        {
            song = new Song
            {
                SongName = "Bon Jovi",
                ArtistName = "Livin' On A Prayer"
        };

        }


        [TestMethod]
        public void TestMethod1()
        {
            string videoUrl = ;
            string getvideoUrl;


        }
    }
}
