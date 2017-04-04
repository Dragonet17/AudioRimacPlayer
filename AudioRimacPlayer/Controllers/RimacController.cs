using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using AudioRimacPlayer.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AudioRimacPlayer.DAL;
using System.Data.Entity;

namespace AudioRimacPlayer.Controllers
{
    public class RimacController : Controller
    {
        // GET: Rimac
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        [ActionName("404")]
        public ActionResult Error404()
        {
            return View("Index");
        }



        public ActionResult Songs()
        {
            List<Song> songs = new List<Song>();
            this.ViewBag.Visible = true;

            if (Session["songs"] == null)
            {
                this.ViewBag.Visible = false;

            }

            songs = (List<Song>)Session["songs"];

            return View(songs);

        }


        //[HttpPost]
        //public ActionResult GetSongName(string search)
        //{
        //    this.ViewBag.Visible = true;

        //    // set empty View
        //    if (search.IsEmpty())
        //        this.ViewBag.Visible = false;
        //    try
        //    {
        //        var songs = Models.Song.GetSongsList(search);
        //        Session["songs"] = songs;
        //        return this.RedirectToAction("Songs");


        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("Error");
        //    }





        //}

        public async Task<ActionResult> GetSongNameAsync(string search)
        {
            this.ViewBag.Visible = true;

            // set empty View
            if (search.IsEmpty())
                this.ViewBag.Visible = false;
            try
            {
                var songs = await Models.Song.GetSongsListAsync(search);
                Session["songs"] = songs;
                return RedirectToAction("Songs");
                

            }
            catch (Exception)
            {

                return RedirectToAction("Error");
            }





        }

        public async Task<ActionResult> PlayMusic(int? id)
        {

            try
            {
                List<Song> songs = (List<Song>)Session["songs"];

                if (songs == null)
                {
                    return RedirectToAction("Songs");
                }


                var song = songs.Find(item => item.SongId == id);
                song = await Models.Song.GetYouTubeVideoUrlForSong(song);
                ViewBag.Song = true;
                ViewBag.AlbumSong = false;
                return View(song);

            }
            catch (Exception)
            {

                return RedirectToAction("Error");

            }


        }

        public ActionResult Artists()
        {
            List<Artist> artists = new List<Artist>();
            this.ViewBag.Visible = true;

            if (Session["artists"] == null)
            {
                this.ViewBag.Visible = false;

            }

            artists = (List<Artist>)Session["artists"];


            return View(artists);
        }

        [HttpPost]
        public async  Task<ActionResult> GetArtistName(string artistsearch)
        {
            //var  artistList = Models.Artist.GetArtistsList(artistsearch);


            this.ViewBag.Visible = true;

            // set empty View
            if (artistsearch.IsEmpty())
                this.ViewBag.Visible = false;
            try
            {
                var artists =await Models.Artist.GetArtistAsync(artistsearch);
                Session["artists"] = artists;
                return View("Artists", artists);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }


        }


        public async Task<ActionResult> GetArtistAlbums(int? id)
        {

            try
            {
                List<Artist> artists = (List<Artist>)Session["artists"];

                if (artists == null)
                {
                    return RedirectToAction("Artists");
                }
                //if (id == null || id > artists.Count - 1 || id < 0 || id.GetType() != typeof(int))
                //{
                //    return RedirectToAction("Error");
                //}

                var artist = artists.Find(item => item.ArtistId == id);
                var albums = await Models.Album.GetArtistAlbums(artist);
                Session["albums"] = albums;
                this.ViewBag.AlbumName = albums.Select(m => m.ArtistName).FirstOrDefault();
                return View("Albums", albums);

            }
            catch (Exception)
            {

                return RedirectToAction("Error");

            }
        }

        public async Task<ActionResult> GetAlbumSongs(int? id)
        {
            try
            {
                List<Album> albums = (List<Album>)Session["albums"];

                if (albums == null)
                {
                    return RedirectToAction("Artists");
                }


                var album = albums.Find(item => item.AlbumId == id);
                var albumSongs = await Models.Song.GetAlbumSongs(album);
                Session["albumsongs"] = albumSongs;

                return View("AlbumSongs", albumSongs);

            }
            catch (Exception)
            {

                return RedirectToAction("Error");

            }



        }

        public async Task<ActionResult> PlayAlbumSong(int id)
        {
            try
            {
                Song songs = (Song)Session["albumsongs"];

                if (songs == null)
                {
                    return RedirectToAction("Artists");
                }
                var song = await Song.GetYoutubeVideoUrlForAlbumSong(songs, id);

                ViewBag.Song = false;
                ViewBag.AlbumSong = true;
                return View("PlayMusic", song);

            }
            catch (Exception)
            {

                return RedirectToAction("Error");

            }

        }

        PlayerContext dbplayer = new PlayerContext();

        public  ActionResult TopList()
        {
            var topSongs = PlayerSong.RenderTopList(dbplayer).Result;
            //var topSongs = Task.Run(PlayerSong.RenderTopList(dbplayer)) ;

            return View(topSongs);
        }


        public  ActionResult Top()
        {
            

            return View();
        }
        public  ActionResult RenderTopList()
        {
            var topSongs = dbplayer.PlayerSongs.Where(s=>s.Deleted==false).ToList();
            return PartialView("_TopList", topSongs);
        }

        public ActionResult AddToTopList(FormCollection form)
        {
            PlayerSong.AddToTheTopList(form, dbplayer);
            return RedirectToAction("TopList");
        }

        public ActionResult RemoveSongFromTheTopList(int id,FormCollection form)
        {
            PlayerSong.RemoveSongFromTheTopList(id, dbplayer);
            var songs = dbplayer.PlayerSongs.Where(s => s.Deleted == false).ToList();
            return PartialView("_TopList", songs);
        }
        // Render TopList (Partial View) using action, but first of all we can remove song from Top List

        public ActionResult PlaySongFromTopList(PlayerSong playerSong)
        {

            return View();
        }
    }
}