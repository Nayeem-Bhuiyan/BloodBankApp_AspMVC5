using BloodBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankApp.ViewModel;

namespace BloodBankApp.Controllers
{
    public class SearchController : Controller
    {

        private BloodBankDbContext db = new BloodBankDbContext();


        // GET: Search
        public ActionResult Search_Index()
        {
            using (BloodBankDbContext db = new BloodBankDbContext())
            {
               
            
              return View();
            }
        }






    
        // POST: Search/Create
        [HttpPost]
        public JsonResult SearchBloodRequestDetails(VmSearchParameter_BloodRequest vm)
        {
            List<VmBloodRequest> ListData = new List<VmBloodRequest>();

            var DbSearchResult = (from b in db.BloodRequests
                                 join gn in db.Genders on b.GenderId equals gn.GenderId
                                 join g in db.BloodGroups on b.GroupId equals g.GroupId
                                 where gn.GenderName.ToString().ToLower().StartsWith(vm.GenderName.ToString().ToLower()) || g.GroupName.ToString().ToLower().StartsWith(vm.GroupName.ToString().ToLower()) || b.Address.ToString().ToLower().StartsWith(vm.Address.ToString().ToLower()) || b.BloodNeedDate.ToString().ToLower().StartsWith(vm.BloodNeedDate.ToString().ToLower()) || b.BloodRequestDate.ToString().ToLower().StartsWith(vm.BloodRequestDate.ToString().ToLower()) || b.HospitalName.ToString().ToLower().StartsWith(vm.HospitalName.ToString().ToLower()) || b.Phone.ToString().ToLower().StartsWith(vm.Phone.ToString().ToLower()) || b.Problem.ToString().ToLower().StartsWith(vm.Problem.ToString().ToLower()) || b.Name.ToString().ToLower().StartsWith(vm.Name.ToString().ToLower())

                                 select new 
                                 {
                                     b.id,
                                     b.Name,
                                     b.Phone,
                                     b.Problem,
                                     b.Address,
                                     b.HospitalName,
                                     b.BloodRequestDate,
                                     b.BloodNeedDate,
                                     g.GroupName,
                                     gn.GenderName
                                 }).ToList();

            foreach (var item in DbSearchResult)
            {
                VmBloodRequest data = new VmBloodRequest()
                {
                   id=item.id,
                    Name = item.Name,
                    Phone = item.Phone,
                    Problem = item.Problem,
                    Address = item.Address,
                    HospitalName = item.HospitalName,
                    BloodRequestDate = item.BloodRequestDate,
                    BloodNeedDate = item.BloodNeedDate,
                    GroupName = item.GroupName,
                    GenderName = item.GenderName
                };

                ListData.Add(data);

            }
            
            return Json(ListData);
        }














        [HttpPost]
        public JsonResult SearchRegitrationDetails(VmSearchParameter_Registration vm)
        {
            List<VmDonorRegistration> ListData = new List<VmDonorRegistration>();

            var DbSearchResult = (from gn in db.Genders
                                  join n in db.NewRegistrations on gn.GenderId equals n.GenderId
                                  join g in db.BloodGroups on n.GroupId equals g.GroupId
                                  where gn.GenderName.ToString().ToLower().StartsWith(vm.GenderName.ToString().ToLower()) || g.GroupName.ToString().ToLower().StartsWith(vm.GroupName.ToString().ToLower()) || n.Address.ToString().ToLower().StartsWith(vm.Address.ToString().ToLower()) || n.DonorName.ToString().ToLower().StartsWith(vm.DonorName.ToString().ToLower()) || n.EmailNumber.ToString().ToLower().StartsWith(vm.EmailNumber.ToString().ToLower()) || n.Password.ToString().ToLower().StartsWith(vm.Password.ToString().ToLower()) || n.Qualification.ToString().ToLower().StartsWith(vm.Qualification.ToString().ToLower()) || n.Phone.ToString().ToLower().StartsWith(vm.Phone.ToString().ToLower())

                                  select new 
                                  {
                                     
                                      g.GroupName,
                                      g.GroupId,
                                      gn.GenderName,
                                      gn.GenderId,
                                      n.Address,
                                      n.Age,
                                      n.DonorName,
                                      n.EmailNumber,
                                      n.ImageUrl,
                                      n.Password,
                                      n.Phone,
                                      n.Photo,
                                      n.Qualification,
                                      n.RegId,
                                     
                                  }).ToList();

            foreach (var item in DbSearchResult)
            {
                VmDonorRegistration data = new VmDonorRegistration()
                {
                                    GroupName = item.GroupName,
                                    GroupId =   item.GroupId,
                                     GenderName  =item.GenderName,
                                     GenderId  = item.GenderId,
                                     Address   = item.Address,
                                     Age       = item.Age,
                                     DonorName = item.DonorName,
                                     EmailNumber = item.EmailNumber,
                                     ImageUrl   = item.ImageUrl,
                                     Password   = item.Password,
                                     Phone      = item.Phone,
                                     Photo      = item.Photo,
                                     Qualification = item.Qualification,
                                     RegId= item.RegId,
                };

                ListData.Add(data);

            }

            return Json(ListData);
        }





        // GET: Search/Edit/5
        public ActionResult Search(int id)
        {
           
            return View();
        }

        // POST: Search/DeleteConfirm/5
        [HttpPost]
        public JsonResult DeleteConfirm(int id)
        {
            var DbSearchResult = db.BloodRequests.Where(b => b.id == id).FirstOrDefault();

            db.BloodRequests.Remove(DbSearchResult);
            db.SaveChanges();

            return Json("Deleted Data Successfilly!!");
        }





    }
}
