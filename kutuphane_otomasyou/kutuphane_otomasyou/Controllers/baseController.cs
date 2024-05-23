using kutuphane_otomasyou.Models;
using kutuphane_otomasyou.Models.table.kisiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kutuphane_otomasyou.Controllers
{
    public class baseController : Controller
    {
        // GET: base
        protected kisi LoggedInUser;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                var userMail = User.Identity.Name;
                using (var db = new databaseContextcs())
                {
                    LoggedInUser = db.kisitablosu.SingleOrDefault(model => model.email.Equals(userMail));
                }
            }
        }
    }
}