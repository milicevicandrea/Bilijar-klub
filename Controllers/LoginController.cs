using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebApplication1.Config;
using System.Data;
using System.Net;
using System.Web.Security;
using System.Drawing.Printing;
using System.Configuration;
using WebApplication1.Hubs;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LiveBazy()
        {
            return View("LiveBazy");
        }


        private KlubContext _context;

        public LoginController()
        {
            _context = new KlubContext();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Login(UserModel userModel)
        {
            SecurityDAO daoService = new SecurityDAO();          
            int success = daoService.FindByUser(userModel);
            if (success == 1)
            {
                Session["Korisnik"] = userModel.Korisnik;
                return View("AdminHomePage", userModel);
            }
            else
                if (success == 2)
            {
                Session["Korisnik"] = userModel.Korisnik;
                return View("UserHomePage", userModel);
            }
            else
                return View();
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveProduct(Sto _sto)
        {
            if (!ModelState.IsValid)
                return View("Create", _sto);
            _context.Stolovi.Add(_sto);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }

        public ActionResult GetAll()
        {
            var stolovi = _context.Stolovi.ToList();
            return View(stolovi);
        }

        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var sto = _context.Stolovi.SingleOrDefault(c => c.Id == id);

            if (sto == null)
                return HttpNotFound();

            _context.Stolovi.Remove(sto);
            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Login");
        }


        public JsonResult Get()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["StoloviDbConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[BilijarskiSto],convert(varchar(11), [Datum], 120) as [Datum],[Vreme] FROM [dbo].[Stoes] WHERE [Id] <> 0", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listCus = reader.Cast<IDataRecord>()
                            .Select(x => new
                            {
                                Id = (int)x["Id"],
                                BilijarskiSto = (string)x["BilijarskiSto"],
                                Datum = x["Datum"].ToString(),
                                Vreme = (string)x["Vreme"],
                            }).ToList();
                    return Json(new { listCus = listCus }, JsonRequestBehavior.AllowGet);
                    // x["Datum"].ToString()
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CusHub.Show();
        }

    }
}