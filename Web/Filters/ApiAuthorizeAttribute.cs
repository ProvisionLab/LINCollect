using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac;
using Autofac.Integration.Mvc;
using Linconnect.Controllers.api.Result;
using Web.Managers.Interfaces;

namespace Web.Filters
{
    public class ApiAuthorizeAttribute: AuthorizationFilterAttribute
    {
        public override async void OnAuthorization(HttpActionContext actionContext)
        {
            var tokenManager = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<ITokenManager>();
            if (!await tokenManager.ValidateToken())
            {
                actionContext.Response = actionContext.Request.CreateResponse(FailedOperationResult.Unauthorized);
            }
        }
    }
}