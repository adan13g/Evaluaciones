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
    public class HomeController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                var profesor = db.tbl_profesores.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                ViewBag.Profe = profesor.Profesor;
                ViewBag.Logo = profesor.image;
                ViewBag.id = profesor.id_profesor;

                return View(profesor);



            }
            else if (User.IsInRole("Alumno"))
            {
                var alumno = db.tbl_alumnos.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                ViewBag.alum = alumno.Alumno;
                ViewBag.id = alumno.matricula;

                ViewBag.Logo = alumno.image;
                return View(alumno);

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

            return PartialView(view);
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
            return PartialView(view);
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

      


        // GET: tbl_profesores/Edit/5
        public ActionResult EditProfe(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_profesores = db.tbl_profesores.Find(id);
            if (tbl_profesores == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_profesores.id_status);

            var view = ToView(tbl_profesores);
            return PartialView(view);
        }

        private ProfesoresView ToView(tbl_profesores tbl_profesores)
        {
            return new ProfesoresView
            {
                id_profesor = tbl_profesores.id_profesor,
                UserName = tbl_profesores.UserName,
                nombre = tbl_profesores.nombre,
                ape_pat = tbl_profesores.ape_pat,
                ape_mat = tbl_profesores.ape_mat,
                image = tbl_profesores.image,
                estate = tbl_profesores.estate,
                id_status = tbl_profesores.id_status,


            };
        }




        // POST: tbl_profesores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfe(ProfesoresView view)
        {
            if (!ModelState.IsValid)
            {
                var pic =  view.image;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder,  pic);
                }
                var profesor = ToProfesor(view);
                profesor.image =/* DateTime.Now.ToString("yyyyMMddHHmmss") + "-" +*/ pic;

                db.Entry(profesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", view.id_status);
            return PartialView(view);
        }
        private tbl_profesores ToProfesor(ProfesoresView view)
        {
            return new tbl_profesores
            {
                id_profesor = view.id_profesor,
                UserName = view.UserName,
                nombre = view.nombre,
                ape_pat = view.ape_pat,
                ape_mat = view.ape_mat,
                image = view.image,
                estate = view.estate,
                id_status = view.id_status,


            };
        }

    }
}