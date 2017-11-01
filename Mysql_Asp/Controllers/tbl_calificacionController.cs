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
    public class tbl_calificacionController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_calificacion
        public ActionResult Index()
        {
            var tbl_calificacion = db.tbl_calificacion.Include(t => t.tbl_alumnos).Include(t => t.tbl_asignacion).Include(t => t.tbl_subunidad);
            return View(tbl_calificacion.ToList());
        }

        // GET: tbl_calificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_calificacion tbl_calificacion = db.tbl_calificacion.Find(id);
            if (tbl_calificacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_calificacion);
        }

        // GET: tbl_calificacion/Create
        public ActionResult Create()
        {
            ViewBag.matricula = new SelectList(db.tbl_alumnos, "matricula", "UserName");
            ViewBag.id_asignacion = new SelectList(db.tbl_asignacion, "id_asignacion", "id_asignacion");
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre");
            return View();
        }

        // POST: tbl_calificacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( tbl_calificacion tbl_calificacion)
        {
            if (ModelState.IsValid)
            {
                db.tbl_calificacion.Add(tbl_calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.matricula = new SelectList(db.tbl_alumnos, "matricula", "UserName", tbl_calificacion.matricula);
            ViewBag.id_asignacion = new SelectList(db.tbl_asignacion, "id_asignacion", "siglema", tbl_calificacion.id_asignacion);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_calificacion.id_resultado);
            return View(tbl_calificacion);
        }

        // GET: tbl_calificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_calificacion tbl_calificacion = db.tbl_calificacion.Find(id);
            if (tbl_calificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.matricula = new SelectList(db.tbl_alumnos, "matricula", "UserName", tbl_calificacion.matricula);
            ViewBag.id_asignacion = new SelectList(db.tbl_asignacion, "id_asignacion", "siglema", tbl_calificacion.id_asignacion);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_calificacion.id_resultado);
            return View(tbl_calificacion);
        }

        // POST: tbl_calificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_calificacion,calificacion,Date,matricula,id_asignacion,id_resultado")] tbl_calificacion tbl_calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.matricula = new SelectList(db.tbl_alumnos, "matricula", "UserName", tbl_calificacion.matricula);
            ViewBag.id_asignacion = new SelectList(db.tbl_asignacion, "id_asignacion", "siglema", tbl_calificacion.id_asignacion);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_calificacion.id_resultado);
            return View(tbl_calificacion);
        }

        // GET: tbl_calificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_calificacion tbl_calificacion = db.tbl_calificacion.Find(id);
            if (tbl_calificacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_calificacion);
        }

        // POST: tbl_calificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_calificacion tbl_calificacion = db.tbl_calificacion.Find(id);
            db.tbl_calificacion.Remove(tbl_calificacion);
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
