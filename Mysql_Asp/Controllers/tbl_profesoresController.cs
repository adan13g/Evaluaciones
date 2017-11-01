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
    public class tbl_profesoresController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_profesores
        public ActionResult Index()
        {
            var tbl_profesores = db.tbl_profesores.Include(t => t.tbl_status);
            return View(tbl_profesores.ToList());
        }

        // GET: tbl_profesores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_profesores tbl_profesores = db.tbl_profesores.Find(id);
            if (tbl_profesores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_profesores);
        }

        // GET: tbl_profesores/Create
        public ActionResult Create()
        {
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfesoresView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var profesor = ToProfesor(view);
                profesor.image = pic;
                db.tbl_profesores.Add(profesor);
                db.SaveChanges();
                UsersHelper.CreateUserASP(view.UserName,"User",view.Password);
                return RedirectToAction("Index");
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus",view.id_status);
            return View(view);
        }

        private tbl_profesores ToProfesor(ProfesoresView view)
        {
            return new tbl_profesores
            {
                id_profesor=view.id_profesor,
                UserName=view.UserName,
                nombre=view.nombre,
                ape_pat=view.ape_pat,
                ape_mat=view.ape_mat,
                image=view.image,
                estate=view.estate,
                id_status=view.id_status,
                

            };
        }



        // GET: tbl_profesores/Edit/5
        public ActionResult Edit(string id)
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
            return View(view);
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
        public ActionResult Edit(ProfesoresView view)
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
                var profesor = ToProfesor(view);
                profesor.image = pic;

                db.Entry(profesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", view.id_status);
            return View(view);
        }

        // GET: tbl_profesores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_profesores tbl_profesores = db.tbl_profesores.Find(id);
            if (tbl_profesores == null)
            {
                return HttpNotFound();
            }
            return View(tbl_profesores);
        }

        // POST: tbl_profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_profesores tbl_profesores = db.tbl_profesores.Find(id);
            db.tbl_profesores.Remove(tbl_profesores);
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
