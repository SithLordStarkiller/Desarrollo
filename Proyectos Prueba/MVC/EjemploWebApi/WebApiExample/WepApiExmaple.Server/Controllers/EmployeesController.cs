using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
       // ReSharper disable once MethodOverloadWithOptionalParameter
        public HttpResponseMessage Get(string gender)
        {
            HttpResponseMessage message;

            gender = gender.ToUpper();

            try
            {
                using (var context = new AdventureWorksEntities())
                {
                    List<Employees> entity;

                    switch (gender)
                    {
                        case "ALL":

                            entity = context.Employees.ToList();

                            message = entity.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, "Employees not found");

                            break;

                        case "F":

                            entity = context.Employees.Where(x => x.Gender == gender).ToList();

                            message = entity.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, "Employees not found");

                            break;

                        case "M":

                            entity = context.Employees.Where(x => x.Gender == gender).ToList();

                            message = entity.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, "Employees not found");

                            break;

                        default:

                            message = Request.CreateResponse(HttpStatusCode.BadRequest, $"The parameter ({gender}) send is not valid");

                            break;
                    }
                }
            }
            catch (Exception e)
            {
                message = Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }


        public HttpResponseMessage Get()
        {
            HttpResponseMessage message;

            try
            {

                using (var context = new AdventureWorksEntities())
                {
                    var entity = context.Employees.ToList();

                    message = entity.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, "Employees not found");
                }
            }
            catch (Exception e)
            {
                message = Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }

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

        public HttpResponseMessage Get(decimal salary)
        {
            HttpResponseMessage message;

            try
            {

                using (var context = new AdventureWorksEntities())
                {
                    var entity = context.Employees.Where(x => x.Salary >= salary).ToList();

                    message = entity.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, entity) : Request.CreateResponse(HttpStatusCode.NotFound, "Employees not found");
                }
            }
            catch (Exception e)
            {
                message = Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }

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
                message = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }


        public HttpResponseMessage Put(int id, [FromBody] Employees value)
        {
            HttpResponseMessage message;

            try
            {
                using (var context = new AdventureWorksEntities())
                {
                    var entity = context.Employees.FirstOrDefault(x => x.Id == id);

                    if (entity != null)
                    {

                        entity.FirstName = value.FirstName;
                        entity.LastName = value.LastName;
                        entity.Gender = value.Gender;
                        entity.Salary = value.Salary;

                        context.Employees.AddOrUpdate(entity);

                        context.SaveChanges();

                        message = Request.CreateResponse(HttpStatusCode.Accepted, entity);

                        message.Headers.Location = new Uri(Request.RequestUri + value.Id.ToString());
                    }
                    else
                    {
                        message = Request.CreateResponse(HttpStatusCode.NotFound, $"Employee not found id = {id}");
                    }
                }
            }
            catch (Exception e)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }

        // DELETE: api/Employees/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage message;

            try
            {
                using (var context = new AdventureWorksEntities())
                {
                    var entity = context.Employees.FirstOrDefault(x => x.Id == id);

                    if (entity != null)
                    {

                        context.Employees.Remove(entity);

                        context.SaveChanges();

                        message = Request.CreateResponse(HttpStatusCode.Accepted, entity);
                    }
                    else
                    {
                        message = Request.CreateResponse(HttpStatusCode.NotFound, $"Employee not found id = {id}");
                    }
                }
            }
            catch (Exception e)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return message;
        }
    }
}
