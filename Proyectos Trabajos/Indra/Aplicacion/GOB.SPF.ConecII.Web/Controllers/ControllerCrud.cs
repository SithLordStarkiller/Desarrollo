using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public class ControllerCrud<TEntity, TViewModel> : Controller, IControllerCrud<TEntity, TViewModel>
       where TEntity : class, new()
       where TViewModel : class, IPageBase, new()
    {
        public string ControllerName { get; set; }

        public ControllerCrud()
        {

        }

        public virtual ActionResult Index()
        {
            TViewModel find = new TViewModel();

            return View(FindItems(find));
        }

        public virtual IPagedView FindItems(TViewModel find)
        {
            ViewBag.Buscar = find;

            List<TEntity> items = FindPaged(find);
            IPagedView pagedView = new PagedView(items, find);
            if (items.Count == 0)
            {
                ViewBag.msj = "No se encontraron coincidencias...";
            }

            return pagedView;
        }

        public virtual List<TEntity> FindPaged(TViewModel find)
        {

            var identity = User == null ? null : User.Identity;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(ControllerName + "/Paged", find).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsStringAsync().Result;
                JObject _jObject = JObject.Parse(respuesta);

                JArray arrayItems = _jObject["items"] as JArray;
                List<TEntity> items = arrayItems.ToObject<List<TEntity>>();

                find.PageTotalItems = (int)_jObject["PageTotalItems"];

                return items;
            }
            return null;
        }


        public virtual PartialViewResult FindPartial(TViewModel find)
        {
            return PartialView("_Busqueda", (FindItems(find)));
        }

        public virtual PartialViewResult Find(TViewModel find)
        {
            if (!find.PageNumber.Equals(0))
                find.PageCurrent = find.PageNumber;

            return FindPartial(find);
        }

        public virtual PartialViewResult AddModifyItem(TViewModel find)
        {

            TEntity item = AddModifyGetItem(find);
            return PartialView("_Edicion", item);
        }

        public virtual TEntity AddModifyGetItem(TViewModel find)
        {
            TEntity item;
            if (find.ItemId.Equals(0))
            {
                ViewBag.titulo = "Agregar";
                item = new TEntity();
            }
            else
            {
                ViewBag.titulo = "Editar";
                item = FindById((int)find.ItemId);
            }
            ViewBag.Buscar = find;
            return item;
        }

        public TEntity FindById(int id)
        {
            var identity = User == null ? null : User.Identity;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(ControllerName + "/GetById", id).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsAsync<TEntity>().Result;
                return respuesta;
            }
            return null;
        }

        [HttpPost]
        public virtual ActionResult Index(TEntity item, TViewModel find)
        {

            bool resultado = Save(item, find);
            if (resultado == true)
                ViewBag.msj = "Se registró la información exitosamente";
            else
                ViewBag.msj = "No se registró la información, favor de intentar nuevamente...";

            return FindPartial(find);
        }

        public virtual bool Save(TEntity item, TViewModel find)
        {
            bool resultado = true;

            if (find.ItemId == 0)
                Insert(item);
            else
                Update(item);

            return resultado;
        }

        private void Update(TEntity item)
        {
            var identity = User == null ? null : User.Identity;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PutAsJsonAsync(ControllerName, item).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsAsync<TEntity>().Result;
            }
        }

        private void Insert(TEntity item)
        {
            var identity = User == null ? null : User.Identity;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(ControllerName, item).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsAsync<TEntity>().Result;
            }

        }

        protected List<DropDto> Drop(string Catalogo)
        {
            var identity = User == null ? null : User.Identity;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(Catalogo + "/Drop").Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsAsync<List<DropDto>>().Result;
                return respuesta;
            }
            return null;
        }
    }
}