using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppMegaMind
{
    public static class clsHelper
    {
        public static List<SelectListItem> getState()
        {
            MegaMindDBEntities db = new MegaMindDBEntities();
            var retval = (from s in db.tblStates
                          select new SelectListItem
                          {
                              Value = s.Id.ToString(),
                              Text = s.Name
                          }).ToList();

            return retval;
        }
        //Order Table methods
        public static List<SelectListItem> getCity()
        {
            MegaMindDBEntities db = new MegaMindDBEntities();
            var retval = (from c in db.tblCities
                          select new SelectListItem
                          {
                              Value = c.Id.ToString(),
                              Text = c.Name
                          }).ToList();

            return retval;
        }

    }
}