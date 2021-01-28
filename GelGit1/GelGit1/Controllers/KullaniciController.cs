using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GelGit1.Models.Entity;
using System.Data;
using System.Data.SqlClient;

namespace GelGit1.Controllers
{
    public class KullaniciController : Controller
    {
        gelgitdtbsEntities db = new gelgitdtbsEntities();

        // GET: Kullanici
        


        [HttpGet]
        public ActionResult YeniKullanici()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniKullanici(Kullanicilar p1,Kullaniciİletisim p2, KullaniciArac p3,KullaniciOkulB p4)
       {
            try {
                var varmi = db.Kullanicilar.Where(i => i.UserName == p1.UserName).SingleOrDefault();
                if(varmi != null)
                {
                    return View();
                }


              db.Kullanicilar.Add(p1);
              db.Kullaniciİletisim.Add(p2);
            db.KullaniciArac.Add(p3);
            db.KullaniciOkulB.Add(p4);           
            db.SaveChanges();
            Session["UserName"] = p1.UserName;
            Session["ID"] = p1.KullaniciID;
            return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }

        }


        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }




        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

         [HttpPost]
         public ActionResult Login(Kullanicilar p1)
        {
            try
            {
                var u = db.Kullanicilar.Where(i => i.UserName == p1.UserName).SingleOrDefault();
               
             
                if (u == null)
                {
                   return View(); 
                                                    
                }
                            
                if (u.Parola == p1.Parola)
                {
                    Session["UserName"] = p1.UserName;
                    Session["ID"] = u.KullaniciID;
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }


        }




        public ActionResult Edit(int id)
        {
            var kisi = db.Kullanicilar.Where(i => i.KullaniciID == id).SingleOrDefault();
            return View(kisi);
        }

        [HttpPost]
        public ActionResult Edit(int id,Kullanicilar p1)
        {
            try
            {
                var kisi = db.Kullanicilar.Where(i => i.KullaniciID == id).SingleOrDefault();
                kisi.İsim = p1.İsim;
                kisi.Soyİsim = p1.Soyİsim;
                kisi.UserName = p1.UserName;
                kisi.Parola = p1.Parola;
                kisi.AracDurumu = p1.AracDurumu;
                kisi.Kullaniciİletisim.TelNo = p1.Kullaniciİletisim.TelNo;
                kisi.Kullaniciİletisim.EPosta = p1.Kullaniciİletisim.EPosta;
                kisi.KullaniciOkulB.OkulAdi = p1.KullaniciOkulB.OkulAdi;
                kisi.KullaniciOkulB.Bolumu = p1.KullaniciOkulB.Bolumu;
                kisi.KullaniciOkulB.OkulNo = p1.KullaniciOkulB.OkulNo;
                kisi.KullaniciArac.AracMarka = p1.KullaniciArac.AracMarka;
                kisi.KullaniciArac.AracModel = p1.KullaniciArac.AracModel;
                kisi.KullaniciArac.AracRenk = p1.KullaniciArac.AracRenk;
                
                if (p1.AracDurumu == false)
                {
                    kisi.KullaniciArac.AracMarka = null;
                    kisi.KullaniciArac.AracModel = null;
                    kisi.KullaniciArac.AracRenk = null;
                }

                db.SaveChanges();
                return RedirectToAction("Profil", "Kullanici");
            }
            catch
            {
                return View(p1);
            }
        }


        public ActionResult Details(int id)
        {
            var kisi = db.Kullanicilar.Where(i => i.KullaniciID == id).SingleOrDefault();
            return View(kisi);
        }


        public ActionResult Profil()
        {
            string kullaniciadi = Session["UserName"].ToString();
            var kisi = db.Kullanicilar.Where(i => i.UserName == kullaniciadi).SingleOrDefault();
            return View(kisi);
        }








    }
}