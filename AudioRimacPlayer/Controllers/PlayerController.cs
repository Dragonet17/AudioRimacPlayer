using AudioRimacPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AudioRimacPlayer.DAL;
using AudioRimacPlayer.Models;

namespace AudioRimacPlayer.Controllers
{
    public class PlayerController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }


        public async Task<ActionResult> Player(string partialname, string search, int? id, string form)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();

            if (Session["player"] != null)
            {
                playerViewModel = (PlayerViewModel)Session["player"];
            }

            try
            {
                if (Request.IsAjaxRequest())
                {
                    playerViewModel.SetPartialName(partialname);
                    playerViewModel.SetFormPartialName(form, partialname);

                    switch (partialname)
                    {
                        case "_Songs":
                            {
                                if (search != null)
                                {
                                    playerViewModel.Songs = await Models.Song.GetSongsListAsync(search);
                                }


                                break;
                            }

                        case "_Artists":
                            {
                                if (search != null)
                                {
                                    playerViewModel.Artists = await Models.Artist.GetArtistAsync(search);
                                }


                                break;
                            }

                        case "_Albums":
                            {
                                if (id != null)
                                {
                                    var artist = playerViewModel.Artists.ToList().Find(item => item.ArtistId == id);
                                    playerViewModel.AlbumArtistName = artist.ArtistName;
                                    playerViewModel.Albums = await Models.Album.GetArtistAlbums(artist);
                                }

                                break;
                            }

                        case "_AlbumSongs":
                            {
                                if (id != null)
                                {
                                    var album = playerViewModel.Albums.ToList().Find(item => item.AlbumId == id);
                                    playerViewModel.AlbumSongs = await Models.Song.GetAlbumSongs(album);
                                }

                                break;
                            }

                        case "_MusicSongs":
                            {
                                if (id != null)
                                {
                                    var song = playerViewModel.Songs.ToList().Find(item => item.SongId == id);

                                    playerViewModel.MusicSong =
                                        await Models.Song.GetYouTubeVideoUrlForSong(song);
                                }
                                break;
                            }

                        case "_MusicAlbumsSongs":
                            {
                                if (id == null && playerViewModel.AlbumSongs != null)
                                {
                                    playerViewModel.MusicSong =
                                        await Models.Song.GetYoutubeVideoUrlForAlbumSong(playerViewModel.AlbumSongs,
                                            (int)(id));
                                }


                                break;
                            }
                        default:
                            {
                                return PartialView("Error");
                            }
                    }
                    Session["player"] = playerViewModel;
                    return RedirectToAction("RenderParialView");
                }
                Session["player"] = playerViewModel;
                return View(playerViewModel);
            }
            catch (Exception)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Error");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
        }


        public ActionResult ChangeForm()
        {
            return PartialView("_FormToSearch");
        }

        public async Task<ActionResult> Music(string partialname, string search, int? id, string form)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();

            if (Session["player"] != null)
            {
                playerViewModel = (PlayerViewModel)Session["player"];
            }

            try
            {
                if (Request.IsAjaxRequest())
                {
                    playerViewModel.SetPartialName(partialname);
                    playerViewModel.SetFormPartialName(form, partialname);

                    switch (partialname)
                    {
                        case "_Songs":
                            {
                                if (search != null)
                                {
                                    playerViewModel.Songs = await Models.Song.GetSongsListAsync(search);
                                }


                                break;
                            }

                        case "_Artists":
                            {
                                if (search != null)
                                {
                                    playerViewModel.Artists = await Models.Artist.GetArtistAsync(search);
                                }


                                break;
                            }

                        case "_Albums":
                            {
                                if (id != null)
                                {
                                    var artist = playerViewModel.Artists.ToList().Find(item => item.ArtistId == id);
                                    playerViewModel.AlbumArtistName = artist.ArtistName;
                                    playerViewModel.Albums = await Models.Album.GetArtistAlbums(artist);
                                }

                                break;
                            }

                        case "_AlbumSongs":
                            {
                                if (id != null)
                                {
                                    var album = playerViewModel.Albums.ToList().Find(item => item.AlbumId == id);
                                    playerViewModel.AlbumSongs = await Models.Song.GetAlbumSongs(album);
                                }

                                break;
                            }


                        default:
                            {
                                return PartialView("Error");
                            }
                    }
                    Session["player"] = playerViewModel;
                    return RedirectToAction("RenderParialView");
                }
                Session["player"] = playerViewModel;
                return View(playerViewModel);
            }
            catch (Exception)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Error");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
        }


        public ActionResult RenderParialView()
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();

            playerViewModel = (PlayerViewModel)Session["player"];

            try
            {
                return PartialView(playerViewModel.PartialName, playerViewModel);
            }
            catch (Exception)
            {
                return PartialView("_Error");
            }
        }
        
        public async Task<ActionResult> MusicSong(string partialname, int id)
        {
            var playerViewModel = new PlayerViewModel();

            if (Session["player"] != null)
            {
                playerViewModel = (PlayerViewModel)Session["player"];
            }
            else
            {
                return RedirectToAction("Music");
            }
            try
            {
                switch (partialname)
                {
                    case "_MusicSongs":
                        {
                            var song = playerViewModel.Songs.ToList().Find(item => item.SongId == id);

                            playerViewModel.MusicSong =
                                await Models.Song.GetYouTubeVideoUrlForSong(song);

                            break;
                        }

                    case "_MusicAlbumsSongs":
                        {

                            playerViewModel.MusicSong =
                                await Models.Song.GetYoutubeVideoUrlForAlbumSong(playerViewModel.AlbumSongs, id);
                            break;
                        }

                }
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Player");

            }

            Session["player"] = playerViewModel;
            return RedirectToAction("MusicPlayer");
        }

        public JsonResult GetMusicSong()
        {
            try
            {
                var playerViewModel = (PlayerViewModel)Session["player"];
                if (Session["player"] != null)
                {
                    playerViewModel = (PlayerViewModel)Session["player"];
                }
                else
                {
                    return null;
                }
                JsonSong song = new JsonSong(playerViewModel);
              
                return Json(song, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return null;
            }



        }

        private readonly PlayerContext _dbplayer = new PlayerContext();
        public bool CheckIfSongIsInTopList()
        {
            var jsonSong = (JsonSong)GetMusicSong().Data;
            
            var song = _dbplayer.PlayerSongs.FirstOrDefault(s => s.SongYouTubeVideoUrl == jsonSong.YouTubeUrl && s.Deleted == false);

            return song != null;
        }
        //Play Song


        public ActionResult MusicPlayer()
        {
            var playerViewModel = (PlayerViewModel)Session["player"];
            if (playerViewModel == null)
            {
                return RedirectToAction("Music");
            }
            ViewBag.CheckIfSongExistsInDb = CheckIfSongIsInTopList();
            return View();
        }



        // TOP LIST

        public async Task<ActionResult> TopList()
        {
            var topSongs = await PlayerSong.RenderTopList(_dbplayer);

            return View(topSongs);
        }

        [HttpPost]
        public async Task<ActionResult>  AddSongToTheTopList()
        {
            var jsonSong = (JsonSong)GetMusicSong().Data;
            
            await  Task.Factory.StartNew(() =>
            {
               PlayerSong.AddToTheTopList(jsonSong,_dbplayer);
            });

            return  new EmptyResult();
        }

       
        public async Task<ActionResult> RemoveSongFromTheTopList(int id)
        {
            await Task.Factory.StartNew(() =>
            {
                PlayerSong.RemoveSongFromTheTopList(id, _dbplayer);
            });

            return new EmptyResult();
        }


        public ActionResult Test()
        {
            return View();
        }


        public ActionResult Search()
        {

            return View("Search");
        }
    }
}