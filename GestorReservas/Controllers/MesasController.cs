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
    public class MesasController : Controller
    {
        private GestorReservasContext db = new GestorReservasContext();

        // GET: Mesas
        public ActionResult Index()
        {
            return View(db.Mesas.ToList());
        }

        // GET: Mesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // GET: Mesas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mesas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero,PrecioSeña")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                String numero = mesa.Numero;

                Mesa me = db.Mesas.Where(m => m.Numero == numero).FirstOrDefault();

                try
                {
                    if(me != null)
                    {
                        return BadRequest();
                    }
                db.Mesas.Add(mesa);
                db.SaveChanges();
                }
                catch (NotImplementedException)
                {
                    MessageBox.Show("Numero de mesa ya registrado.");
                }
                
                return RedirectToAction("Index");
            }

            return View(mesa);
        }

        private ActionResult BadRequest()
        {
            throw new NotImplementedException();
        }

        // GET: Mesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,PrecioSeña")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mesa mesa = db.Mesas.Find(id);
            db.Mesas.Remove(mesa);
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
