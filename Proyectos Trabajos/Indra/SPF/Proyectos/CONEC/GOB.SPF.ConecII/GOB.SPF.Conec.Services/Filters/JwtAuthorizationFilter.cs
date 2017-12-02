using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GOB.SPF.Conec.Services.Filters
{
    internal class JwtAuthorizationFilter:AuthorizationFilterAttribute
    {
        private const string privateKey = "sw6p!eg?3uwr2jufr*6eth@qE";
        private const string issuer = "GOB.SPF";
        private const string audience = "GOB.SPF.CONECII";
        private SymmetricSecurityKey sign = new SymmetricSecurityKey(Encoding.Default.GetBytes(privateKey));
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (SkipAuthorization(actionContext)) return;

            var request = actionContext;
            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization == null)
            {
                request.Response = request.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                request.Response.Content = new StringContent("Missed authorization header.");
                return;
            }
            if (authorization.Scheme != "Bearer")
            {
                request.Response = request.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                request.Response.Content = new StringContent("Wrong authentication type.");
                return;
            }
            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                request.Response = request.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                request.Response.Content = new StringContent("Missed authetication token.");
                return;
            }
            if (await ValidateTokenAsync(authorization.Parameter))
            {
                SetPrincipal(new Models.AppUser(new Models.AppIdentity("Bearer", true, "AppUser")));
            }
            else
            {
                request.Response = request.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                request.Response.Content = new StringContent("Invalid authetication token.");
                return;
            }
            await base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
        private Task<bool> ValidateTokenAsync(string TokenString)
        {
            var result = false;

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(TokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidAudience = audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = sign
                };

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);

                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }

            return Task.FromResult(result);
        }
        private bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}