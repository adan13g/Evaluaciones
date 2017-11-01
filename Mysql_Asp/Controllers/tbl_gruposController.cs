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
    public class tbl_gruposController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_grupos
        public ActionResult Index()
        {
            var tbl_grupos = db.tbl_grupos.Include(t => t.tbl_status);
            return View(tbl_grupos.ToList());
        }

        // GET: tbl_grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_grupos tbl_grupos = db.tbl_grupos.Find(id);
            if (tbl_grupos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_grupos);
        }

        // GET: tbl_grupos/Create
        public ActionResult Create()
        {
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

        // POST: tbl_grupos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_grupos,grupos,id_status")] tbl_grupos tbl_grupos)
        {
            if (ModelState.IsValid)
            {
                db.tbl_grupos.Add(tbl_grupos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_grupos.id_status);
            return View(tbl_grupos);
        }

        // GET: tbl_grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_grupos tbl_grupos = db.tbl_grupos.Find(id);
            if (tbl_grupos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_grupos.id_status);
            return View(tbl_grupos);
        }

        // POST: tbl_grupos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_grupos,grupos,id_status")] tbl_grupos tbl_grupos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_grupos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_grupos.id_status);
            return View(tbl_grupos);
        }

        // GET: tbl_grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_grupos tbl_grupos = db.tbl_grupos.Find(id);
            if (tbl_grupos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_grupos);
        }

        // POST: tbl_grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_grupos tbl_grupos = db.tbl_grupos.Find(id);
            db.tbl_grupos.Remove(tbl_grupos);
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
