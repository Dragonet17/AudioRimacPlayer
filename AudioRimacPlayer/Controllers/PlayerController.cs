using AudioRimacPlayer.ViewModel;
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

        public ActionResult Error()
        {
            return View();
        }

        public async Task<ActionResult> Player(string parialname, string search, int? id)
        {
            PlayerViewModel vm = new PlayerViewModel();
            PlayerViewModel.FormPartialName = "_Songs";

            try
            {
                if (Request.IsAjaxRequest())
                {
                    vm.PartialName = parialname;
                    switch (parialname)
                    {
                        case "_Songs":
                            {
                                vm.Songs = await Models.Song.GetSongsListAsync(search);
                                break;
                            }

                        case "_Artist":
                            {
                                vm.Artists = await Models.Artist.GetArtistAsync(search);
                                break;
                            }

                        case "albums":
                            {
                                var artist = vm.Artists.ToList().Find(item => item.ArtistId == id);
                                vm.Albums = await Models.Album.GetArtistAlbums(artist);
                                break;
                            }

                        case "albumssongs":
                            {
                                var album = vm.Albums.ToList().Find(item => item.AlbumId == id);
                                vm.AlbumSongs = await Models.Song.GetAlbumSongs(album);

                                break;
                            }

                        default:
                            {
                                return RedirectToAction("Error");
                            }
                    }
                    Session["player"] = vm;
                    return RedirectToAction("RenderParialView");
                }
                vm.PartialName = "_Empty";
                Session["player"] = vm;
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult RenderParialView(string form)
        {
            var vm = (PlayerViewModel)Session["player"];

            try
            {

                if (vm.PartialName == null)
                {
                    vm.PartialName = "_Empty";
                }
                if (form!=null)
                {
                PlayerViewModel.FormPartialName = form;

                }
               
                return PartialView(vm.PartialName, vm);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }



        }

        //Play Song


        // TOP LIST

        public ActionResult TopList()
        {
            return View();
        }
    }
}