using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mysql_Asp.Models;
using Mysql_Asp.Classes;

namespace Mysql_Asp.Controllers
{
    public class tbl_alumnosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_alumnos
        public ActionResult Index()
        {
            var tbl_alumnos = db.tbl_alumnos.Include(t => t.tbl_grupos).Include(t => t.tbl_status);
            return View(tbl_alumnos.ToList());
        }

        // GET: tbl_alumnos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_alumnos tbl_alumnos = db.tbl_alumnos.Find(id);
            if (tbl_alumnos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_alumnos);
        }

        // GET: tbl_alumnos/Create
        public ActionResult Create()
        {
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos");
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

        // POST: tbl_alumnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlumnosView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}+{2}", folder, pic+ DateTime.Now.ToString("d"));
                }
                var alumno = ToAlumno(view);
                alumno.image = pic;
                db.tbl_alumnos.Add(alumno);
                db.SaveChanges();
                UsersHelper.CreateUserASP(view.UserName, "Alumno", view.Password);

                return RedirectToAction("Index");
            }

            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", view.id_grupos);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus",view.id_status);
            return View(view);
        }

        private tbl_alumnos ToAlumno(AlumnosView view)
        {
            return new tbl_alumnos
            {
                matricula = view.matricula,
                UserName = view.UserName,
                nombre = view.nombre,
                ape_pat = view.ape_pat,
                ape_mat = view.ape_mat,
                image = view.image,
                estate = view.estate,
                id_status = view.id_status,
                id_grupos=view.id_grupos


            };
        }

        // GET: tbl_alumnos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_alumnos = db.tbl_alumnos.Find(id);
            if (tbl_alumnos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", tbl_alumnos.id_grupos);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_alumnos.id_status);
            var view = ToView(tbl_alumnos);

            return View(view);
        }

        private AlumnosView ToView(tbl_alumnos tbl_alumnos)
        {
            return new AlumnosView
            {
                matricula = tbl_alumnos.matricula,
                UserName = tbl_alumnos.UserName,
                nombre = tbl_alumnos.nombre,
                ape_pat = tbl_alumnos.ape_pat,
                ape_mat = tbl_alumnos.ape_mat,
                image = tbl_alumnos.image,
                estate = tbl_alumnos.estate,
                id_status = tbl_alumnos.id_status,
                id_grupos = tbl_alumnos.id_grupos


            };

        }

        // POST: tbl_alumnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlumnosView view)
        {
            if (!ModelState.IsValid)
            {
                var pic = view.image;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var alumno = ToAlumno(view);
                alumno.image = pic;

                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", view.id_grupos);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", view.id_status);
            return View(view);
        }

        // GET: tbl_alumnos/Delete/5
        public ActionResult Delete(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_alumnos tbl_alumnos = db.tbl_alumnos.Find(id);
            if (tbl_alumnos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_alumnos);
        }

        // POST: tbl_alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_alumnos tbl_alumnos = db.tbl_alumnos.Find(id);
            db.tbl_alumnos.Remove(tbl_alumnos);
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
