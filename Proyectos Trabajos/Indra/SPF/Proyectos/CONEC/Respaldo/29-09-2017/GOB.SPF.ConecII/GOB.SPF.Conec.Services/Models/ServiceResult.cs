using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.Conec.Services.Models
{
  public class ServiceResult<TEntity> where TEntity : class
  {
    public List<TEntity> Data { get; set; }
    public TEntity Entity { get; set; }


    public bool HasErrors { get; set; }
    public string Messages { get; set; }
    public List<TEntity> Errors { get; set; }

  }
}