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
    public class tbl_estimacionController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_estimacion
        public ActionResult Index()
        {
            var tbl_estimacion = db.tbl_estimacion.Include(t => t.tbl_grupos).Include(t => t.tbl_profesores).Include(t => t.tbl_subunidad);
            return View(tbl_estimacion.ToList());
        }

        // GET: tbl_estimacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_estimacion tbl_estimacion = db.tbl_estimacion.Find(id);
            if (tbl_estimacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_estimacion);
        }

        // GET: tbl_estimacion/Create
        public ActionResult Create()
        {
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_grupos = new SelectList(db.tbl_grupos.Where(g=>g.tbl_asignacion.FirstOrDefault().id_profesor==profesor.id_profesor), "id_grupos", "grupos");
            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p=>p.id_profesor==profesor.id_profesor), "id_profesor", "Profesor");

            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre");
            return View();
        }

        // POST: tbl_estimacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estimacion,fecha,tiempo,id_resultado,id_grupos,id_profesor")] tbl_estimacion tbl_estimacion)
        {
            if (ModelState.IsValid)
            {
                db.tbl_estimacion.Add(tbl_estimacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_grupos = new SelectList(db.tbl_grupos.Where(g => g.tbl_asignacion.FirstOrDefault().id_profesor == profesor.id_profesor), "id_grupos", "grupos", tbl_estimacion.id_grupos);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p=>p.id_profesor==profesor.id_profesor), "id_profesor", "Profesor", tbl_estimacion.id_profesor);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_estimacion.id_resultado);
            return View(tbl_estimacion);
        }

        // GET: tbl_estimacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_estimacion tbl_estimacion = db.tbl_estimacion.Find(id);
            if (tbl_estimacion == null)
            {
                return HttpNotFound();
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_grupos = new SelectList(db.tbl_grupos.Where(g => g.tbl_asignacion.FirstOrDefault().id_profesor == profesor.id_profesor), "id_grupos", "grupos", tbl_estimacion.id_grupos);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p => p.id_profesor == profesor.id_profesor), "id_profesor", "Profesor", tbl_estimacion.id_profesor);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_estimacion.id_resultado);
            return View(tbl_estimacion);
        }

        // POST: tbl_estimacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estimacion,fecha,tiempo,id_resultado,id_grupos,id_profesor")] tbl_estimacion tbl_estimacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_estimacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_grupos = new SelectList(db.tbl_grupos.Where(g => g.tbl_asignacion.FirstOrDefault().id_profesor == profesor.id_profesor), "id_grupos", "grupos", tbl_estimacion.id_grupos);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p => p.id_profesor == profesor.id_profesor), "id_profesor", "Profesor", tbl_estimacion.id_profesor);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_estimacion.id_resultado);
            return View(tbl_estimacion);
        }

        // GET: tbl_estimacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_estimacion tbl_estimacion = db.tbl_estimacion.Find(id);
            if (tbl_estimacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_estimacion);
        }

        // POST: tbl_estimacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_estimacion tbl_estimacion = db.tbl_estimacion.Find(id);
            db.tbl_estimacion.Remove(tbl_estimacion);
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
