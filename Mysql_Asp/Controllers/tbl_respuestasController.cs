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
    public class tbl_respuestasController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_respuestas
        public ActionResult Index()
        {
            var tbl_respuestas = db.tbl_respuestas.Include(t => t.tbl_preguntas);
            return View(tbl_respuestas.ToList());
        }

        // GET: tbl_respuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_respuestas tbl_respuestas = db.tbl_respuestas.Find(id);
            if (tbl_respuestas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_respuestas);
        }

        // GET: tbl_respuestas/Create
        public ActionResult Create()
        {
            ViewBag.id_pregunta = new SelectList(db.tbl_preguntas, "id_pregunta", "pregunta");
            return View();
        }

        // POST: tbl_respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_respuesta,respuesta,puntos,id_pregunta")] tbl_respuestas tbl_respuestas)
        {
            if (ModelState.IsValid)
            {
                db.tbl_respuestas.Add(tbl_respuestas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pregunta = new SelectList(db.tbl_preguntas, "id_pregunta", "pregunta", tbl_respuestas.id_pregunta);
            return View(tbl_respuestas);
        }

        // GET: tbl_respuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_respuestas tbl_respuestas = db.tbl_respuestas.Find(id);
            if (tbl_respuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pregunta = new SelectList(db.tbl_preguntas, "id_pregunta", "pregunta", tbl_respuestas.id_pregunta);
            return View(tbl_respuestas);
        }

        // POST: tbl_respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_respuesta,respuesta,puntos,id_pregunta")] tbl_respuestas tbl_respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_respuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pregunta = new SelectList(db.tbl_preguntas, "id_pregunta", "pregunta", tbl_respuestas.id_pregunta);
            return View(tbl_respuestas);
        }

        // GET: tbl_respuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_respuestas tbl_respuestas = db.tbl_respuestas.Find(id);
            if (tbl_respuestas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_respuestas);
        }

        // POST: tbl_respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_respuestas tbl_respuestas = db.tbl_respuestas.Find(id);
            db.tbl_respuestas.Remove(tbl_respuestas);
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
