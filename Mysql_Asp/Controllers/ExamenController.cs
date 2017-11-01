using Mysql_Asp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mysql_Asp.Controllers
{
    public class ExamenController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        public static int carrera;
        public static string materia;
        public static string profesor;
        public static int ResultadoA;
        public static string FechaActual;
        DateTime fechaHoy = DateTime.Now;
        public static bool Mensaje;
       tbl_preguntas preguntas = new tbl_preguntas();


        public ActionResult Home()
        {
            var alumno = db.tbl_alumnos.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var grupos= db.tbl_grupos.Where(g => g.id_grupos == alumno.id_grupos).FirstOrDefault();
            ViewBag.id_grupos = grupos.grupos;

            //var IDasignacion = db.tbl_asignacion.Where(c=>c.id_grupos==grupos.id_grupos);
           var query = from employee in db.tbl_asignacion
                        join student in db.tbl_carreras
                        on new { employee.id_carrera }
                        equals new { student.id_carrera }
                        join groups in db.tbl_grupos
                         on new { employee.id_grupos }
                        equals new { groups.id_grupos }
                        where groups.id_grupos == grupos.id_grupos
                       select new { OwnerName = employee.id_grupos }; 

               ViewBag.id_carrera = new SelectList(db.tbl_carreras.Where(c => c.tbl_asignacion.FirstOrDefault().id_grupos == grupos.id_grupos), "id_carrera", "carrera");
            ViewBag.siglema = new SelectList(db.tbl_materias.Where(m=>m.tbl_asignacion.FirstOrDefault().id_grupos==grupos.id_grupos), "siglema", "nombre");
            ViewBag.id_unidad = new SelectList(db.tbl_unidad.Where(u => u.siglema == db.tbl_materias.FirstOrDefault().siglema), "id_unidad", "nombre");
            ViewBag.id_resultado = new SelectList(db.tbl_subunidad.Where(r => r.id_unidad == db.tbl_unidad.FirstOrDefault().id_unidad), "id_resultado", "nombre");

            ViewBag.id_profesor = new SelectList(db.tbl_profesores.Where(m => m.tbl_asignacion.FirstOrDefault().id_grupos == grupos.id_grupos), "id_profesor", "Profesor");
            ViewBag.showSuccessAlert =false;

            return View();

        }

        [HttpPost]
        public ActionResult Home(int id_carrera,string siglema,string id_profesor,int id_resultado)
        {
            FechaActual = fechaHoy.ToString("d");
            carrera = id_carrera;
            materia = siglema;
            profesor = id_profesor;
            ResultadoA = id_resultado;


            ViewBag.showSuccessAlert = Mensaje;

            return RedirectToAction("Index");


        }

        // GET: Examen
        public ActionResult Index()
        {


            var FechaAsignada = db.tbl_estimacion.Where(f => f.id_resultado == ResultadoA && f.id_profesor == profesor).FirstOrDefault();
           var evaluacion = new Evaluacion() ;

            //var r= evaluacion.Preguntas.Distinct().OrderBy(en => Guid.NewGuid()).Take(10).Where(p => p.id_resultado == ResultadoA && p.id_profesor == profesor);
            var r = db.tbl_preguntas.ToList().Distinct().OrderBy(en => Guid.NewGuid()).Take(10).Where(p => p.id_resultado == ResultadoA && p.id_profesor == profesor);
            //list.Distinct().OrderBy(en => Guid.NewGuid()).Take(10).Where(p => p.id_resultado == ResultadoA && p.id_profesor == profesor).ToList();

            ViewBag.Lista= preguntas.Listar();
            //evaluacion.Preguntas.Add(pregunta);
            ////ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre");
            //if (pregunta.Count() == 0)
            //{
            //    return RedirectToAction("Home");

            //}
            //else
            //{
            //    if (FechaActual != FechaAsignada.fecha.ToString("d"))
            //    {
            //        Mensaje = true;

            //        return RedirectToAction("Home");

            //    }
            return View();

            //}


            //var randomizer = new Random();
            //var results = from en in db.tbl_preguntas
            //              let rand = randomizer.Next()
            //              orderby rand
            //              select en;
            //var pregunta = db.tbl_preguntas.Where(p=>p.id_profesor==profesor.id_profesor);

        }
        //[HttpPost]
        //public ActionResult Index(string id_carrera)
        //{
        //    var r = db.tbl_preguntas.Distinct().
        // ToList().OrderBy(en => Guid.NewGuid()).Take(10);

        //    //ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre");

        //    //List<tbl_preguntas> list = new List<tbl_preguntas>();       
        //    return View(r);
        //}


        //public ActionResult Create(tbl_respuestas res)
        //{
        //    var r= res.id_pregunta.ToString();
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calificacion(string variable)
        {
            //var YourRadioButton = pregunta.RespuestaSeleccionada;


            var calificacion = new tbl_calificacion();

            var alumno = db.tbl_alumnos.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var idAsignacion = db.tbl_asignacion.Where(c => c.id_profesor == profesor && c.siglema==materia && c.id_profesor==profesor).FirstOrDefault();
            var fecha = DateTime.Today;

            var matri = alumno.matricula;
            var cali = 4;
            var asig = idAsignacion.id_asignacion;
            var resul = ResultadoA;
            if (!ModelState.IsValid)
            {
          
                calificacion.Date = fecha;
                calificacion.id_asignacion = asig;
                calificacion.id_resultado = resul;
                calificacion.matricula = matri;
                calificacion.calificacion = cali;
                db.tbl_calificacion.Add(calificacion);
                db.SaveChanges();
              
                return RedirectToAction(string.Format("Details/{0}",calificacion.id_calificacion)); //PRG Pattern
            }
            return View();
        }
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
        public JsonResult Getpreguntas(int[] boton1)
        {
            int count = 0;
            foreach(int a in boton1){
                count++;
            }
            db.Configuration.ProxyCreationEnabled = false;

            //var respuesta= db.tbl_unidad.Where(m => m.id_unidad==boton1);
            return Json(count);
        }
        //public void Guardar(tbl_respuestas model, int[] valor = null)
        //{
        //    if (valor != null)
        //    {
        //        foreach (int v in valor)
        //        {
        //            model.puntos.Add(new  { id = v });
        //        }
        //    }
        //}
        //public ActionResult Create()
        //{

        //    return View();
        //    //return View();
        //}

        //// POST: tbl_calificacion/Create
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(tbl_calificacion tbl_calificacion)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        var alumno = ToAlumno(tbl_calificacion);
        //        db.tbl_calificacion.Add(alumno);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    //ViewBag.matricula = new SelectList(db.tbl_alumnos, "matricula", "UserName", tbl_calificacion.matricula);
        //    //ViewBag.id_asignacion = new SelectList(db.tbl_asignacion, "id_asignacion", "siglema", tbl_calificacion.id_asignacion);
        //    //ViewBag.id_resultado = new SelectList(db.tbl_subunidad, "id_resultado", "nombre", tbl_calificacion.id_resultado);
        //    return View(tbl_calificacion);
        //}

        //private tbl_calificacion ToAlumno(tbl_calificacion tbl_calificacion)
        //{
        //    var usuario = db.tbl_alumnos.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        //    var fecha = DateTime.Today;
        //    return new tbl_calificacion
        //    { 
        //        calificacion=8,
        //    id_resultado = 3,
        //        id_asignacion=1,
        //        matricula=usuario.matricula,
        //        Date=fecha
        //    };

        //}
    }
}