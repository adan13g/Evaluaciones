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
    [Authorize(Roles ="User")]
    public class tbl_preguntasController : Controller
    {
        private ModeloDatos db = new ModeloDatos();



        // GET: tbl_preguntas
        public ActionResult Index()
        {
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var tbl_preguntas = db.tbl_preguntas.
                Include(t => t.tbl_profesores).
                Include(m=>m.tbl_subunidad.tbl_unidad.tbl_materias).
                Include(m => m.tbl_subunidad.tbl_unidad).
                Include(t => t.tbl_status).Include(t => t.tbl_subunidad).Where(p=>p.id_profesor==profesor.id_profesor);
            return View(tbl_preguntas.ToList());
        }
        public ActionResult CreateRespuesta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_preguntas = db.tbl_preguntas.Find(id);
            if (tbl_preguntas == null)
            {
                return HttpNotFound();
            }
            var view = new tbl_respuestas { id_pregunta = tbl_preguntas.id_pregunta };

            return View(view);
        }

        // POST: tbl_respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRespuesta(tbl_respuestas tbl_respuestas)
        {
            if (ModelState.IsValid)
            {
                db.tbl_respuestas.Add(tbl_respuestas);
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", tbl_respuestas.id_pregunta));
            }
            return View(tbl_respuestas);
        }


        public ActionResult EditRespuesta(int? id)
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

        // POST: tbl_respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRespuesta( tbl_respuestas tbl_respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_respuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", tbl_respuestas.id_pregunta));
            }
            return View(tbl_respuestas);
        }

        // GET: tbl_preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_preguntas tbl_preguntas = db.tbl_preguntas.Find(id);
            if (tbl_preguntas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_preguntas);
        }

        // GET: tbl_preguntas/Create
        public ActionResult Create()
        {
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p=>p.id_profesor==profesor.id_profesor), "id_profesor", "Profesor");
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            ViewBag.siglema= new SelectList(db.tbl_materias, "siglema", "nombre");
           ViewBag.id_unidad = new SelectList(db.tbl_unidad.Where(u => u.siglema == db.tbl_materias.FirstOrDefault().siglema), "id_unidad", "nombre");
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad.Where(r=>r.id_unidad==db.tbl_unidad.FirstOrDefault().id_unidad), "id_resultado", "nombre");
            return View();
        }

        // POST: tbl_preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pregunta,pregunta,id_profesor,id_resultado,id_status")] tbl_preguntas tbl_preguntas)
        {
            if (ModelState.IsValid)
            {
                db.tbl_preguntas.Add(tbl_preguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p => p.id_profesor == profesor.id_profesor), "id_profesor", "Profesor", tbl_preguntas.id_profesor);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_preguntas.id_status);
           
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_preguntas.id_resultado);
            return View(tbl_preguntas);
        }

        // GET: tbl_preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_preguntas tbl_preguntas = db.tbl_preguntas.Find(id);
            if (tbl_preguntas == null)
            {
                return HttpNotFound();
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p => p.id_profesor == profesor.id_profesor), "id_profesor", "Profesor", tbl_preguntas.id_profesor);
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre",tbl_preguntas.tbl_subunidad.tbl_unidad.tbl_materias.siglema);
            ViewBag.id_unidad = new SelectList(db.tbl_unidad, "id_unidad", "nombre",tbl_preguntas.tbl_subunidad.tbl_unidad.id_unidad);

            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_preguntas.id_resultado);

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_preguntas.id_status);
            var view = ToView(tbl_preguntas);
            return View(view);
        }

        private PreguntasView ToView(tbl_preguntas tbl_preguntas)
        {
            return new PreguntasView
            {
                id_pregunta=tbl_preguntas.id_pregunta,
                pregunta=tbl_preguntas.pregunta,
                id_profesor=tbl_preguntas.id_profesor,
                id_resultado=tbl_preguntas.id_resultado,
                id_status=tbl_preguntas.id_status,
                siglema=tbl_preguntas.tbl_subunidad.tbl_unidad.tbl_materias.siglema,
                id_unidad=tbl_preguntas.tbl_subunidad.tbl_unidad.id_unidad

            };
        }

        // POST: tbl_preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pregunta,pregunta,id_profesor,id_resultado,id_status")] tbl_preguntas tbl_preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_preguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(p => p.id_profesor == profesor.id_profesor), "id_profesor", "Profesor", tbl_preguntas.id_profesor);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_preguntas.id_status);
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_preguntas.id_resultado);
            return View(tbl_preguntas);
        }

        // GET: tbl_preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_preguntas tbl_preguntas = db.tbl_preguntas.Find(id);
            if (tbl_preguntas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_preguntas);
        }

        // POST: tbl_preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_preguntas tbl_preguntas = db.tbl_preguntas.Find(id);
            db.tbl_preguntas.Remove(tbl_preguntas);
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
