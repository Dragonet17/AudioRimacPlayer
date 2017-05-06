using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AudioRimacPlayer.Models;

namespace AudioRimacPlayer.DAL
{
    public class PlayerSong
    {

        public int Id { get; set; }
        public string SongName { get; set; }
        public string SongArtist { get; set; }
        public string SongArtistImgUrl { get; set; }
        public string SongYouTubeVideoUrl { get; set; }
        public bool Deleted { get; set; }
        public static async Task<List<PlayerSong>> RenderTopList(PlayerContext topSongs)
        {

            //var favotiteSongs = await Task.Run(() => topSongs.PlayerSongs.Where(m => m.Deleted == false).ToList());
            var favotiteSongs =await topSongs.PlayerSongs.Where(s => s.Deleted == false).ToListAsync();
            return favotiteSongs;
        }

        public static void AddToTheTopList(FormCollection form, PlayerContext dbplayer)
        {
            string songName = form["SongName"];
            string artistName = form["ArtistName"];
            string artistImagetUrl = form["ArtistImagetUrl"];
            string youtubeUrl = form["YouTubeUrl"];

            PlayerSong playersong = new PlayerSong();

            var song = dbplayer.PlayerSongs.FirstOrDefault(s => s.SongYouTubeVideoUrl == youtubeUrl);

            if (song != null)
            {

                if (song.Deleted)
                {
                    dbplayer.PlayerSongs.Attach(song);
                    dbplayer.Entry(song).State = EntityState.Modified;
                    song.Deleted = false;
                    song.SongArtistImgUrl = artistImagetUrl;

                    dbplayer.SaveChanges();
                }


            }
            else
            {
                playersong.SongName = songName;
                playersong.SongArtist = artistName;
                playersong.SongArtistImgUrl = artistImagetUrl;
                playersong.SongYouTubeVideoUrl = youtubeUrl;
                playersong.Deleted = false;
                dbplayer.PlayerSongs.Add(playersong);

                dbplayer.SaveChanges();
            }



        }

        public static void AddToTheTopList(JsonSong jsonSong, PlayerContext dbplayer)
        {

            PlayerSong playersong = new PlayerSong();

            var song = dbplayer.PlayerSongs.FirstOrDefault(s => s.SongYouTubeVideoUrl == jsonSong.YouTubeUrl);

            if (song != null)
            {

                if (song.Deleted)
                {
                    dbplayer.PlayerSongs.Attach(song);
                    dbplayer.Entry(song).State = EntityState.Modified;
                    song.Deleted = false;
                    song.SongArtistImgUrl = jsonSong.ImgUrl;

                    dbplayer.SaveChanges();
                }


            }
            else
            {
                playersong.SongName = jsonSong.SongName;
                playersong.SongArtist = jsonSong.ArtistName;
                playersong.SongArtistImgUrl = jsonSong.ImgUrl;
                playersong.SongYouTubeVideoUrl = jsonSong.YouTubeUrl;
                playersong.Deleted = false;
                dbplayer.PlayerSongs.Add(playersong);

                dbplayer.SaveChanges();
            }



        }

        public static void RemoveSongFromTheTopList(int id, PlayerContext dbplayer)
        {
            var songToRemoving = dbplayer.PlayerSongs.FirstOrDefault(m => m.Id == id);
            dbplayer.PlayerSongs.Attach(songToRemoving);
            dbplayer.Entry(songToRemoving).State = EntityState.Modified;
            songToRemoving.Deleted = true;

            dbplayer.SaveChanges();
        }

    }
}