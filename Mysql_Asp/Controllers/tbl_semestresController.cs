using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mysql_Asp.Models;

namespace Mysql_Asp.Controllers
{
    public class tbl_semestresController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_semestres
        public ActionResult Index()
        {
            var tbl_semestres = db.tbl_semestres.Include(t => t.tbl_status);
            return View(tbl_semestres.ToList());
        }

        // GET: tbl_semestres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_semestres tbl_semestres = db.tbl_semestres.Find(id);
            if (tbl_semestres == null)
            {
                return HttpNotFound();
            }
            return View(tbl_semestres);
        }

        // GET: tbl_semestres/Create
        public ActionResult Create()
        {
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

        // POST: tbl_semestres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_semestre,semestre,id_status")] tbl_semestres tbl_semestres)
        {
            if (ModelState.IsValid)
            {
                db.tbl_semestres.Add(tbl_semestres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_semestres.id_status);
            return View(tbl_semestres);
        }

        // GET: tbl_semestres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_semestres tbl_semestres = db.tbl_semestres.Find(id);
            if (tbl_semestres == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_semestres.id_status);
            return View(tbl_semestres);
        }

        // POST: tbl_semestres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_semestre,semestre,id_status")] tbl_semestres tbl_semestres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_semestres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_semestres.id_status);
            return View(tbl_semestres);
        }

        // GET: tbl_semestres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_semestres tbl_semestres = db.tbl_semestres.Find(id);
            if (tbl_semestres == null)
            {
                return HttpNotFound();
            }
            return View(tbl_semestres);
        }

        // POST: tbl_semestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_semestres tbl_semestres = db.tbl_semestres.Find(id);
            db.tbl_semestres.Remove(tbl_semestres);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
