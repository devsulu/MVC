using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppMegaMind.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public bool Agree { get; set; }
        public string ref_State { get; set; }
        public string ref_City { get; set; }
        public IList<SelectListItem> StateNames { get; set; }
        public IList<SelectListItem> CityNames { get; set; }
    }
}