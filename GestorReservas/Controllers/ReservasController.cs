using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using GestorReservas.Data;
using GestorReservas.Models;

namespace GestorReservas.Controllers
{
    public class ReservasController : Controller
    {
        private GestorReservasContext db = new GestorReservasContext();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Cliente).Include(r => r.Mesa);
            return View(reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.MesaId = new SelectList(db.Mesas, "Id", "Numero");
            return View();
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,ClienteId,MesaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {

                DateTime f = reserva.Fecha;
                Console.WriteLine("Se crea una reserva para esta fecha" + reserva.Fecha);

                Reserva re = db.Reservas.Where(r => r.Fecha == f).FirstOrDefault();
                try
                {
                    if (re != null)
                    {
                        return UnprocessableEntity();
                    }
                    db.Reservas.Add(reserva);
                    db.SaveChanges();
                } catch (NotImplementedException)
                {
                    MessageBox.Show("No se puede reservar una mesa ya ocupada.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", reserva.ClienteId);
            ViewBag.MesaId = new SelectList(db.Mesas, "Id", "Numero", reserva.MesaId);
            return View(reserva);
        }

        private ActionResult UnprocessableEntity()
        {
            throw new NotImplementedException();
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", reserva.ClienteId);
            ViewBag.MesaId = new SelectList(db.Mesas, "Id", "Numero", reserva.MesaId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,ClienteId,MesaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                //llamar funcion booleana pasando reserva por parametro, si encuentra una igual devuelve falso si no devuelve true.
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", reserva.ClienteId);
            ViewBag.MesaId = new SelectList(db.Mesas, "Id", "Numero", reserva.MesaId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
