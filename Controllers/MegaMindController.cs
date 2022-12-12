using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppMegaMind.Models;

namespace WebAppMegaMind.Controllers
{
    public class MegaMindController : Controller
    {
        MegaMindDBEntities db = new MegaMindDBEntities();
        // GET: MegaMind
        public ActionResult Index()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();

            List<sp_GetEmployeeList_Result1> employeeList_Results = db.sp_GetEmployeeList().ToList();

            foreach(var record in employeeList_Results)
            {
                employeeModels.Add(new EmployeeModel
                {
                    Id=record.Id,
                    Name = record.Name,
                    Email = record.Email,
                    PhoneNo = record.PhoneNo,
                    State=record.State,
                    City=record.City,
                    Agree=true
                });
            }
            return View(employeeModels);
        }
        
        public  ActionResult Manage(int id)
        {
            EmployeeModel model = new EmployeeModel();
            model = (from e in db.tblEmployees
                     where e.Id == id
                     select new EmployeeModel
                     {
                         Id = e.Id,
                         Name = e.Name,
                         Email = e.Email,
                         PhoneNo = e.PhoneNo,
                         Address = e.Address,
                         State = e.State,
                         City = e.City,
                         Agree =true
                     }).FirstOrDefault();
                if (model ==null)
                {
                    model = new EmployeeModel();
                }
           
                return View(model);
               
        }
        
        [HttpPost]
        public ActionResult Manage(EmployeeModel employeeModel)
        {
            tblEmployee tblEmployee = (from e in db.tblEmployees
                                       where e.Id == employeeModel.Id
                                       select e).FirstOrDefault();
            //List<tblEmployee> tblEmployees = db.tblEmployees.Where(x => x.Id == employeeModel.Id).ToList();
            try
            {
                if (employeeModel.Id == 0)
                {
                    tblEmployee = new tblEmployee();
                    tblEmployee.Id = employeeModel.Id;
                    tblEmployee.Name = employeeModel.Name;
                    tblEmployee.Email = employeeModel.Email;
                    tblEmployee.PhoneNo = employeeModel.PhoneNo;
                    tblEmployee.Address = employeeModel.Address;
                    tblEmployee.State =Convert.ToInt32(employeeModel.State);
                    tblEmployee.City =Convert.ToInt32(employeeModel.City);

                    tblEmployee.Agree = employeeModel.Agree;
                    
                    db.tblEmployees.Add(tblEmployee);
                    db.SaveChanges();
                    ViewBag.message = "Record Saved Successfully!";
                    //return Json(new { result = "OK", data = "", message = "Record saved successfully!" }, JsonRequestBehavior.AllowGet);
                    return JavaScript("Data Inserted Successfully!");
                }
                else
                {
                    //tblEmployee.Id = employeeModel.Id;
                    tblEmployee.Name = employeeModel.Name;
                    tblEmployee.Email = employeeModel.Email;
                    tblEmployee.PhoneNo = employeeModel.PhoneNo;
                    tblEmployee.Address = employeeModel.Address;
                    tblEmployee.State = employeeModel.State;
                    tblEmployee.City = employeeModel.City;
                    //tblEmployee.Agree = employeeModel.Agree;
                    db.SaveChanges();
                    //return Json(new { result = "OK", data = "", message = "Record updated successfully!" }, JsonRequestBehavior.AllowGet);
                    return JavaScript("Data Updated Successfully!");
                }
            }
            catch(Exception ex)
            {
                //return Json(new { result = "ERROR", data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
                return JavaScript("Data was not inserted Successfully!");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            tblEmployee objtbl = (from e in db.tblEmployees
                                  where e.Id == id
                                  select e).FirstOrDefault();
            try
            {
                if (objtbl != null)
                {
                    db.tblEmployees.Remove(objtbl);
                    db.SaveChanges();
                }
                return Json(new { result = "OK", data = "", message = "Record deleted successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCityList(int stateid)
        {
            List<tblCity> lstCity = new List<tblCity>();
            
                this.db.Configuration.ProxyCreationEnabled = false;

                lstCity = db.tblCities.Where(a => a.StateId == stateid).ToList();
                ViewBag.CityOptions = new SelectList(lstCity, "Id", "Name");
                return PartialView("CityOptionspartial");
            
        }
    }
}