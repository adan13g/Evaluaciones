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
    public class tbl_statusController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_status
        public ActionResult Index()
        {
            return View(db.tbl_status.ToList());
        }

        // GET: tbl_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_status tbl_status = db.tbl_status.Find(id);
            if (tbl_status == null)
            {
                return HttpNotFound();
            }
            return View(tbl_status);
        }

        // GET: tbl_status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_status/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_status,estatus")] tbl_status tbl_status)
        {
            if (ModelState.IsValid)
            {
                db.tbl_status.Add(tbl_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_status);
        }

        // GET: tbl_status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_status tbl_status = db.tbl_status.Find(id);
            if (tbl_status == null)
            {
                return HttpNotFound();
            }
            return View(tbl_status);
        }

        // POST: tbl_status/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_status,estatus")] tbl_status tbl_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_status);
        }

        // GET: tbl_status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_status tbl_status = db.tbl_status.Find(id);
            if (tbl_status == null)
            {
                return HttpNotFound();
            }
            return View(tbl_status);
        }

        // POST: tbl_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_status tbl_status = db.tbl_status.Find(id);
            db.tbl_status.Remove(tbl_status);
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
