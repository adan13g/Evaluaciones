using Mysql_Asp.Classes;
using Mysql_Asp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mysql_Asp.Controllers
{
    public class PerfilController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: Perfil
        public ActionResult Index(string id)
        {
            var alumno = db.tbl_alumnos.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_alumnos = db.tbl_alumnos.Find(id);
            if (tbl_alumnos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_alumnos);
        }
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
            if (ModelState.IsValid)
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
                id_grupos = view.id_grupos


            };
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