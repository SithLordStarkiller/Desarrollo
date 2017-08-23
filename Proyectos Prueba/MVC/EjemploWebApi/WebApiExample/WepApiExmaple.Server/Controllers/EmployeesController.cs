using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiExample.DataAccess;
using WebApiExample.Models;

namespace WepApiExmaple.Server.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET: api/Employees
        public IEnumerable<Employees> Get()
        {
            using (var context = new AdventureWorksEntities())
            {
                return context.Employees.ToList();
            }
        }

        // GET: api/Employees/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage message;

            try
            {

                using (var context = new AdventureWorksEntities())
                {
                    var entity = context.Employees.FirstOrDefault(x => x.Id == id);

                    message = entity != null ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, $"Employee not found id = {id}");
                }
            }
            catch (Exception e)
            {
                message = Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }

        // POST: api/Employees
        public HttpResponseMessage Post([FromBody] Employees value)
        {
            HttpResponseMessage message;

            try
            {
                using (var context = new AdventureWorksEntities())
                {
                    context.Employees.Add(value);
                    context.SaveChanges();

                    message = Request.CreateResponse(HttpStatusCode.Created, value);

                    message.Headers.Location = new Uri(Request.RequestUri + value.Id.ToString());
                }
            }
            catch (Exception e)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

            return message;
        }

        // PUT: api/Employees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employees/5
        public void Delete(int id)
        {
        }
    }
}
