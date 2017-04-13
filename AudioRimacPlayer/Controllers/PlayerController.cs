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
        PlayerViewModel vm = new PlayerViewModel();
        public async Task<ActionResult> Player(string partialname, string search, int? id, string form)
        {
            if (Session["player"] != null)
            {
             vm = (PlayerViewModel)Session["player"];

            }
            vm.FormPartialName = PlayerViewModel.SetFormPartialName(form);

            try
            {
                if (Request.IsAjaxRequest())
                {

                    vm.PartialName = partialname;
                    

                    switch (partialname)
                    {
                        case "_Songs":
                            {
                                if (search !=null)
                                {
                                vm.Songs = await Models.Song.GetSongsListAsync(search);
                                }
                                
                                break;
                            }

                        case "_Artists":
                            {
                                if (search != null)
                                {
                                    vm.Artists = await Models.Artist.GetArtistAsync(search);
                                }
                                
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
                                return PartialView("Error");
                            }
                    }
                    Session["player"] = vm;
                    return RedirectToAction("RenderParialView");
                }
                Session["player"] = vm;
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ChangeForm()
        {
        

            return PartialView("_FormToSearch");
        }

        public ActionResult RenderParialView()
        {
            var vm = (PlayerViewModel)Session["player"];

            try
            {

                if (vm.PartialName == null)
                {
                    vm.PartialName = "_Empty";
                }


                return PartialView(vm.PartialName, vm);
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
    }
}