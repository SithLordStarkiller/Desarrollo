using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class ResultPage<T>
    {
        public ResultPage()
        {
            this.List = new List<T>();
            this.Errors = new List<Error>();
            this.Page = new Paging();
        }

        public bool Success { get; set; }

        public List<T> List { get; set; }

        public T Entity { get; set; }

        public List<Error> Errors { get; set; }
        [JsonIgnore]
        public Paging Page { get; set; }
    }
}