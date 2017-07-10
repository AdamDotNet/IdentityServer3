using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Validation;
using IdentityServer3.Core.ViewModels;
using MinimalServer.Controllers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MinimalServer.Config
{
    public class MvcViewService : IViewService
    {
        private readonly IdentityServerController controller;

        public MvcViewService()
        {
            controller = FakeControllerFactory.Create<IdentityServerController>();
        }

        public Task<Stream> Login(LoginViewModel model, SignInMessage message)
        {
            return Task.FromResult(controller.RenderViewToStream(IdentityServerController.ViewNames.Login, model));
        }

        #region Not Implemented

        public Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> Consent(ConsentViewModel model, ValidatedAuthorizeRequest authorizeRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> Error(ErrorViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> LoggedOut(LoggedOutViewModel model, SignOutMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> Logout(LogoutViewModel model, SignOutMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion Not Implemented
    }
}