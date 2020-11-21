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
using PagedList.Mvc;
using PagedList;

namespace BloodBankApp.Controllers
{
    public class BloodRequestsController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();

        // GET: BloodRequests
        public ActionResult Index(int? pagePos)
        {
            int pageSize = 2;
            int pageNumber = (pagePos ?? 1);
            var bloodRequests = db.BloodRequests.Include(b => b.BloodGroup).Include(b => b.Gender).ToList();
            return View(bloodRequests.ToPagedList(pageNumber, pageSize));
        }

        // GET: BloodRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodRequest bloodRequest = db.BloodRequests.Find(id);
            if (bloodRequest == null)
            {
                return HttpNotFound();
            }
            return View(bloodRequest);
        }

        // GET: BloodRequests/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            return View();
        }

        // POST: BloodRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,PatientAge,Problem,Address,HospitalName,BloodRequestDate,BloodNeedDate,Countity,Photo,PhotoPathUrl,GroupId,GenderId")] BloodRequest bloodRequest, HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid)
            {

                //Save image in folder

                string FileName = Path.GetFileName(FileUpload.FileName);
                string SaveLocation = Server.MapPath("~/PictureFolder/" + FileName);
                FileUpload.SaveAs(SaveLocation);

                //save image name in database

                bloodRequest.PhotoPathUrl = "~/PictureFolder/" + FileName;


                // byte image Save


                bloodRequest.Photo = new byte[FileUpload.ContentLength];
                ViewBag.BloodDonorPic = FileUpload.InputStream.Read(bloodRequest.Photo, 0, FileUpload.ContentLength);


                db.BloodRequests.Add(bloodRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", bloodRequest.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", bloodRequest.GenderId);
            return View(bloodRequest);
        }

        // GET: BloodRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodRequest bloodRequest = db.BloodRequests.Find(id);
            if (bloodRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", bloodRequest.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", bloodRequest.GenderId);
            return View(bloodRequest);
        }

        // POST: BloodRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,PatientAge,Problem,Address,HospitalName,BloodRequestDate,BloodNeedDate,Countity,Photo,PhotoPathUrl,GroupId,GenderId")] BloodRequest bloodRequest,HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid)
            {

               BloodRequest DbRecord =db.BloodRequests.Where(b => b.id == bloodRequest.id).FirstOrDefault();

                //Save image in folder
                try
                {
                    if (FileUpload.FileName!=null)
                    {
                        string FileName = Path.GetFileName(FileUpload.FileName);
                        string SaveLocation = Server.MapPath("~/PictureFolder/" + FileName);
                        FileUpload.SaveAs(SaveLocation);

                        //save image name in database

                        DbRecord.PhotoPathUrl = "~/PictureFolder/" + FileName;


                        // byte image Save


                        DbRecord.Photo = new byte[FileUpload.ContentLength];
                        ViewBag.BloodDonorPic = FileUpload.InputStream.Read(bloodRequest.Photo, 0, FileUpload.ContentLength);

                    }

                }
                catch (Exception)
                {

                    throw;
                }
                
                db.Entry(DbRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");





            }
            ViewBag.GroupId = new SelectList(db.BloodGroups, "GroupId", "GroupName", bloodRequest.GroupId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", bloodRequest.GenderId);
            return View(bloodRequest);
        }

        // GET: BloodRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodRequest bloodRequest = db.BloodRequests.Find(id);
            if (bloodRequest == null)
            {
                return HttpNotFound();
            }
            return View(bloodRequest);
        }

        // POST: BloodRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodRequest bloodRequest = db.BloodRequests.Find(id);
            db.BloodRequests.Remove(bloodRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ActionResult IndividualReport(int id)
        {

            var bloodRequests = (from g in db.BloodGroups
                                 join r in db.BloodRequests on g.GroupId equals r.GroupId
                                 join gn in db.Genders on r.GenderId equals gn.GenderId
                                 where r.id==id
                                 select new
                                 {
                                     g.GroupId, g.GroupName, gn.GenderId, gn.GenderName, r.Address, r.BloodGroup, r.BloodNeedDate, r.BloodRequestDate, r.Countity, r.HospitalName, r.Name, r.id, r.PatientAge, r.Phone, r.Photo, r.PhotoPathUrl, r.Problem
                                 }
                                );


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PrintReport.rpt"));

            rd.SetDataSource(bloodRequests);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BloodReport.pdf");


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
