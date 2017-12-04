using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOB.SPF.ConecII.Web.Models.Usuarios;
using GOB.SPF.ConecII.Interfaces;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesUsuarios<TUser> :
        IUserStore<TUser, int>,
        IUserPasswordStore<TUser, int>,
        IUserLockoutStore<TUser, int>,
        IUserTwoFactorStore<TUser, int>
        where TUser : class, IUsuario
    {
        public async Task CreateAsync(TUser user)
        {
            await ServiceClient.PostObjectAsync("Usuarios", "CrearUsuario", user);
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<TUser> FindByIdAsync(int userId)
        {
            return await ServiceClient.GetObjectAsync<TUser>("Usuarios", "ObtenerUsuarioPorId",
                new[] { new KeyValuePair<string, string>("id", userId.ToString()) });
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            return await ServiceClient.GetObjectAsync<TUser>("Usuarios", "ObtenerUsuarioPorLogin",
                new[] { new KeyValuePair<string, string>("login", userName) });
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(TUser user)
        {
            return await ServiceClient.GetObjectAsync<string>("Usuarios", "ObtenerClave",
                new[] { new KeyValuePair<string, string>("login", user.Login) });
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public async Task<bool> HasPasswordAsync(TUser user)
        {
            var password = await ServiceClient.GetObjectAsync<string>("Usuarios", "ObtenerClave",
                new[] { new KeyValuePair<string, string>("login", user.Login) });

            return string.IsNullOrEmpty(password);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            await ServiceClient.PutObjectAsync("Usuarios", "ActualizarClave", "login", user.Login, passwordHash);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(TUser user)
        {
            throw new NotImplementedException();
        }
    }
}