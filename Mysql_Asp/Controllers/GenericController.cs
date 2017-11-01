using Mysql_Asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mysql_Asp.Controllers
{
    public class GenericController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

    
       
        public JsonResult GetUnidades(string siglema)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var unidades = db.tbl_unidad.Where(m => m.siglema == siglema);
            return Json(unidades);
        }
        public JsonResult GetSubUnidades(int id_unidad)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var subunidades = db.tbl_subunidad.Where(s => s.id_unidad == id_unidad);
                return Json(subunidades);

            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Generic
        public ActionResult Index()
        {
            return View();
        }
    }
}