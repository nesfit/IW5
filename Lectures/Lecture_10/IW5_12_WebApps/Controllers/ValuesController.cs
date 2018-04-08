using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using IW5_12_WebApps.Models;

namespace IW5_12_WebApps.Controllers
{
    // Use Fiddler to simulate web service calls.
    // The Athentication and Authorization similar rules are used.
    // ApiController doesnt share base class with Mvc Controller (Changed in MVC 6).
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Server needs to distinguish method Overloads - each method has to differ in parameter types and http method.
        // GET api/values/5
        public string Get(int id)
        {
            return $"value id was {id}";
        }

        // GET api/values?email=j@ahoj.cz
        // Content-Type: application/json; charset=utf-8
        [HttpGet] // not necessary for methods with name identical with http method.
        public JsonResult<ForgotViewModel> Get(string email)
        {
            var forgot = new ForgotViewModel()
            {
                Email =  email
            };

            return Json(forgot);
        }

        // POST api/values
        // Content-Type: application/json; charset=utf-8
        // request body: { "email":"j@ahoj.cz" }
        // [HttpPost]
        public void Post([FromBody]ForgotViewModel value)
        {
        }

        // PUT api/values/5
        // Content-Type: application/json; charset=utf-8
        // request body: "this is my content"
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}