using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBankApp.Models;
using CrystalDecisions.CrystalReports.Engine;
using PagedList;

namespace BloodBankApp.Controllers
{
    public class NewRegistrationsController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();


        [Authorize(Roles = "User")]
        // GET: NewRegistrations
        public ActionResult Index(int? pagePos)
        {
            int pageSize = 2;
            int pageNumber = (pagePos ?? 1);
            var newRegistrations = db.NewRegistrations.Include(n => n.BloodGroup).Include(n => n.Gender).ToList();
            return View(newRegistrations.ToPagedList(pageNumber, pageSize));
        }

        // GET: NewRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRegistration newRegistration = db.NewRegistrations.Find(id);
            if (newRegistration == null)
            {
                return HttpNotFound();
            }
            return View(newRegistration);
        }

        // GET: NewRegistrations/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            return View();
        }

        // POST: NewRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegId,EmailNumber,Password,DonorName,Phone,Age,Address,Qualification,Photo,ImageUrl,GroupId,GenderId")] NewRegistration newRegistration, HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid)
            {
                //Save image in folder

                string FileName = Path.GetFileName(FileUpload.FileName);
                string SaveLocation = Server.MapPath("~/NewRegPicture/" + FileName);
                FileUpload.SaveAs(SaveLocation);

                //save image name in database

                newRegistration.ImageUrl = "~/NewRegPicture/" + FileName;


            // byte image Save


                newRegistration.Photo = new byte[FileUpload.ContentLength];
                ViewBag.BloodDonorPic = FileUpload.InputStream.Read(newRegistration.Photo, 0, FileUpload.ContentLength);




                db.NewRegistrations.Add(newRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", newRegistration.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", newRegistration.GenderId);
            return View(newRegistration);
        }

        // GET: NewRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRegistration newRegistration = db.NewRegistrations.Find(id);
            if (newRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", newRegistration.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", newRegistration.GenderId);
            return View(newRegistration);
        }

        // POST: NewRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegId,EmailNumber,Password,DonorName,Phone,Age,Address,Qualification,Photo,ImageUrl,GroupId,GenderId")] NewRegistration newRegistration,HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid)
            {
                NewRegistration DbRecord = db.NewRegistrations.Where(r => r.RegId == newRegistration.RegId).FirstOrDefault();

                //Save image in folder

                string FileName = Path.GetFileName(FileUpload.FileName);
                string SaveLocation = Server.MapPath("~/NewRegPicture/" + FileName);
                FileUpload.SaveAs(SaveLocation);

                //save image name in database

                DbRecord.ImageUrl = "~/NewRegPicture/" + FileName;


                // byte image Save


                DbRecord.Photo = new byte[FileUpload.ContentLength];
                ViewBag.BloodDonorPic = FileUpload.InputStream.Read(DbRecord.Photo, 0, FileUpload.ContentLength);



                db.Entry(newRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", newRegistration.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", newRegistration.GenderId);
            return View(newRegistration);
        }

        // GET: NewRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRegistration newRegistration = db.NewRegistrations.Find(id);
            if (newRegistration == null)
            {
                return HttpNotFound();
            }
            return View(newRegistration);
        }

        // POST: NewRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewRegistration newRegistration = db.NewRegistrations.Find(id);
            db.NewRegistrations.Remove(newRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        [HttpGet]
        public ActionResult PrintNewRegistrationDetails(int id)
        {

            var RegDetails = (from g in db.BloodGroups
                                 join n in db.NewRegistrations on g.GroupId equals n.GroupId
                                 join gn in db.Genders on n.GenderId equals gn.GenderId
                                 where n.RegId == id
                                 select new
                                 {
                                     g.GroupId,
                                     g.GroupName,
                                     gn.GenderId,
                                     gn.GenderName,
                                     n.Address,
                                     n.Age,
                                     n.DonorName,
                                     n.EmailNumber,
                                     n.Password,
                                     n.ImageUrl,
                                     n.Phone,
                                     n.Photo,
                                     n.Qualification
                                     
                                 }
                                );


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PrintNewRegistrationDetails.rpt"));

            rd.SetDataSource(RegDetails);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "PrintRegistration.pdf");


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
