using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mysql_Asp.Models;
using System.Threading.Tasks;

namespace Mysql_Asp.Controllers
{
    public class tbl_materiasController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_materias
        public ActionResult Index()
        {
            var tbl_materias = db.tbl_materias.Include(t => t.tbl_semestres).Include(t => t.tbl_status);
            return View(tbl_materias.ToList());
        }
        public async Task<ActionResult> DeleteSubUnidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sub = await db.tbl_subunidad.FindAsync(id);
            if (sub == null)
            {
                return HttpNotFound();
            }

            db.tbl_subunidad.Remove(sub);
            await db.SaveChangesAsync();


            return RedirectToAction(string.Format("DetailsUnidad/{0}", sub.id_unidad));
        }
        public ActionResult EditSubunidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_subunidad tbl_subunidad = db.tbl_subunidad.Find(id);
            if (tbl_subunidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            return View(tbl_subunidad);
        }

        // POST: tbl_subunidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubunidad(tbl_subunidad tbl_subunidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_subunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(string.Format("DetailsUnidad/{0}", tbl_subunidad.id_unidad));
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            return View(tbl_subunidad);
        }
        public ActionResult CreateSubUnidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unidad = db.tbl_unidad.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            var view = new tbl_subunidad { id_unidad = unidad.id_unidad };

            return View(view);
        }

        // POST: tbl_subunidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubUnidad(tbl_subunidad tbl_subunidad)
        {
            if (ModelState.IsValid)
            {
                db.tbl_subunidad.Add(tbl_subunidad);
                db.SaveChanges();
                return RedirectToAction(string.Format("DetailsUnidad/{0}", tbl_subunidad.id_unidad));
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            return View(tbl_subunidad);
        }

        public ActionResult DetailsUnidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_unidad tbl_unidad = db.tbl_unidad.Find(id);
            if (tbl_unidad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unidad);
        }
        // GET: tbl_materias/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_materias tbl_materias = db.tbl_materias.Find(id);
            if (tbl_materias == null)
            {
                return HttpNotFound();
            }
            return View(tbl_materias);
        }


        public ActionResult CreateUnidad(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_materias = db.tbl_materias.Find(id);
            if (tbl_materias == null)
            {
                return HttpNotFound();
            }
            var view= new tbl_unidad{ siglema= tbl_materias.siglema  };
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View(view);
        }

        // POST: tbl_unidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUnidad(tbl_unidad tbl_unidad)
        {
            if (ModelState.IsValid)
            {
                db.tbl_unidad.Add(tbl_unidad);
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}",tbl_unidad.siglema));
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }


        public async Task<ActionResult> DeleteUnidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unidad = await db.tbl_unidad.FindAsync(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }

            db.tbl_unidad.Remove(unidad);
          await  db.SaveChangesAsync();
        

            return RedirectToAction(string.Format("Details/{0}",unidad.siglema));
        }

        public ActionResult EditUnidad(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_unidad = db.tbl_unidad.Find(id);
            if (tbl_unidad == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }

        // POST: tbl_unidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnidad(tbl_unidad tbl_unidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_unidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", tbl_unidad.siglema));
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }
        // GET: tbl_materias/Create
        public ActionResult Create()
        {
            ViewBag.id_semestre = new SelectList(db.tbl_semestres, "id_semestre", "semestre");
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

        // POST: tbl_materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "siglema,nombre,id_semestre,id_status")] tbl_materias tbl_materias)
        {
            if (ModelState.IsValid)
            {
                db.tbl_materias.Add(tbl_materias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_semestre = new SelectList(db.tbl_semestres, "id_semestre", "semestre", tbl_materias.id_semestre);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_materias.id_status);
            return View(tbl_materias);
        }

        // GET: tbl_materias/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_materias tbl_materias = db.tbl_materias.Find(id);
            if (tbl_materias == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_semestre = new SelectList(db.tbl_semestres, "id_semestre", "semestre", tbl_materias.id_semestre);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_materias.id_status);
            return View(tbl_materias);
        }

        // POST: tbl_materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "siglema,nombre,id_semestre,id_status")] tbl_materias tbl_materias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_materias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_semestre = new SelectList(db.tbl_semestres, "id_semestre", "semestre", tbl_materias.id_semestre);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_materias.id_status);
            return View(tbl_materias);
        }

        // GET: tbl_materias/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_materias tbl_materias = db.tbl_materias.Find(id);
            if (tbl_materias == null)
            {
                return HttpNotFound();
            }
            return View(tbl_materias);
        }

        // POST: tbl_materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_materias tbl_materias = db.tbl_materias.Find(id);
            db.tbl_materias.Remove(tbl_materias);
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
