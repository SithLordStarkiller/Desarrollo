using GOB.SPF.ConecII.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models.Usuarios
{
    public class UiUsuario:IUsuario
    {
        public bool Activo { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaInical { get; set; }
        public int Id { get; set; }
        public int IdExterno { get; set; }
        public Guid? IdPersona { get; set; }
        public string Login { get; set; }
        public string UserName { get { return Login; } set { Login = value; } }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UiUsuario, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //userIdentity.AddClaim()
            // Add custom user claims here 
            //userIdentity.AddClaim(new Claim(...))

            return userIdentity;
        }

        public async Task<int> GetUserIdAsync()
        {
            return await Task.FromResult(this.Id);
        }
    }
}