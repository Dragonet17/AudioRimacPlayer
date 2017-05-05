﻿using AudioRimacPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AudioRimacPlayer.Controllers
{
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {

            return View();
        }

        [NonAction]
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
                                        await Models.Song.GetYoutubeVideoUrlForAlbumSong(playerViewModel.AlbumSongs, (int)(id));

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

                        case "_MusicSongs":
                        {
                            if (id != null)
                            {
                                var song = playerViewModel.Songs.ToList().Find(item => item.SongId == id);

                                playerViewModel.MusicSong =
                                    await Models.Song.GetYouTubeVideoUrlForSong(song);
                                return RedirectToAction("MusicPlayer");

                            }
                            break;
                        }

                        case "_MusicAlbumsSongs":
                        {
                            if (id == null && playerViewModel.AlbumSongs != null)
                            {

                                playerViewModel.MusicSong =
                                    await Models.Song.GetYoutubeVideoUrlForAlbumSong(playerViewModel.AlbumSongs, (int)(id));

                                return RedirectToAction("MusicPlayer");


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

        public ActionResult MusicPlayer()
        {
            return View();
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

        //Play Song


        // TOP LIST

        public ActionResult TopList()
        {
            return View();
        }

        public ActionResult Test()
        {
            
            return View();
        }
    }
}