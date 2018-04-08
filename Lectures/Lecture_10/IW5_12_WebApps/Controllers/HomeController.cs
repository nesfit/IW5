using System.Net;
using System.Web.Http.Results;
using System.Web.Mvc;
using IW5_12_WebApps.Models;

namespace IW5_12_WebApps.Controllers
{
    // How to get to the controller: http://saulius.sunauskas.com/2013/10/14/understanding-asp-net-mvc-request-processing-pipeline-visually/
    public class HomeController : Controller
    {
        private const string TempDataSessionKey = "MyTempData";

        public ActionResult Index()
        {
            this.Session[TempDataSessionKey] = "Temp value";
            return View();
        }

        // Parameters are part of the Request Url, Mvc tries to fill them from all possible places.
        // Non nullable type become required parameters.
        // See also Frombody attribute.
        public ActionResult UseParameters(ForgotViewModel forgot, int second)
        {
            string secondFromParams = this.Request.Params["email"];
            // this.ValidateModel();
            //this.TryUpdateModel(new ForgotViewModel());
            bool modelValid = this.ModelState.IsValid;
            return View("Index");
        }

        public ActionResult ReturnValues()
        {
            // return "Plain Page result."; // in case method returns string
            // return HttpNotFound("Nothing like this found."); // see also other methods.
            // return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Somethign wrong ..");
            // return View() - points to default view which name is identical with method name.
            // RedirectToAction("Index", "Home");

            // Response.Write("This is a result text.");
            // return PartialView("Error");
            
            return View("Index"); 
        }

        // Password policy can be changed in AccountViewModel and IdentityConfig.
        // Attributes  can be defined on controll or its mehod level
        [Authorize]
        //[AllowAnonymous]
        //[Authorize(Roles = "Admins")]
        public ActionResult Authenticated()
        {
            bool allow = this.User.IsInRole("Administrators");
            string userName = this.User.Identity.Name;
            return View("Index");
        }

        public ActionResult SessionState()
        {
            var fromSession = this.Session[TempDataSessionKey] as string;
            // var cookie = this.Request.Cookies[cookieKey];
            return View("Index");
        }
    }
}