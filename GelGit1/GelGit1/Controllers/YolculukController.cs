using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GelGit1.Models.Entity;

namespace GelGit1.Controllers
{
    public class YolculukController : Controller
    {

        gelgitdtbsEntities db = new gelgitdtbsEntities();
        // GET: Yolculuk




        [HttpGet]
        public ActionResult YeniYolculuk()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniYolculuk(Yolculuklar p1, YolculukDetaylari p2)
        {
            try
            {

                p1.SurucuID = (int)Session["ID"];
                db.Yolculuklar.Add(p1);
                db.YolculukDetaylari.Add(p2);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }

        }




        public ActionResult YolculukListele()
        {
            var degerler = db.YolculukDetaylari.ToList();
            

            return View(degerler);
        }


        public ActionResult Surucudetaylari(int id)
        {
            var degerler = db.Kullanicilar.Find(id);
            return View(degerler);
        }





        public ActionResult YolculukDetaylari(int id)
        {
            Class1 db2 = new Class1();

            var ylclkdty1 = db.YolculukDetaylari.Find(id);
            var ylclkdty2 = db.Yolculuklar.Where(i => i.YolculukID == id).SingleOrDefault();
            var a = db.Kullanicilar.Where(i => i.KullaniciID == ylclkdty2.SurucuID).SingleOrDefault();

            db2.YolculukDetaylaric = ylclkdty1;
            db2.Kullanicilarc = a;

            return View("YolculukDetaylari",db2);

        }


        public ActionResult YolculugaKaydol(int id)
        {
            Yolcular y = new Yolcular();          
            y.YolculukID = id;
            y.YolcuID = (int)Session["ID"];
            db.Yolcular.Add(y);
            db.SaveChanges();
            return RedirectToAction("Yolculuklarim", "Yolculuk");
        }


        public ActionResult Ilanlarim()
        {
            var degerler = db.YolculukDetaylari.ToList();


            return View(degerler);
        }



        public ActionResult Sil(int id)
        {
            var a = db.YolculukDetaylari.Find(id);
            db.YolculukDetaylari.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Yolculuklarim()
        {
            var degerler = db.Yolcular.ToList();


            return View(degerler);
        }


    }
}