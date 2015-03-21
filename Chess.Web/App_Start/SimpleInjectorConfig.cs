using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

using Chess.Core.Events;
using Chess.Core.Settings;
using Chess.Infrastructure.Settings;
using Chess.Core.Authentication;
using Chess.Infrastructure.Authentication;
using Chess.Core.Repository;
using Chess.Infrastructure.Repository;

namespace Chess.Web.App_Start
{
    public class SimpleInjectorConfig
    {
        public static Container Initialize()
        {
            var container = new Container();

            // Register your types, for instance:
            RegisterTypes(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();
            container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            DomainEvents.SetContainer(container);

            return container;
        }

        private static void RegisterTypes(Container container)
        {
            container.RegisterManyForOpenGeneric(typeof(IHandleDomainEvent<>),
                container.RegisterAll, AppDomain.CurrentDomain.GetAssemblies());

            // Auth
            container.RegisterPerWebRequest<IAuthenticationSettings, AuthenticationSettings>();
            container.RegisterPerWebRequest<IUserManager, UserManager>();
            container.RegisterPerWebRequest<IPasswordHasher, PasswordHasher>();
            container.RegisterPerWebRequest<ISaltGenerator, SaltGenerator>();

            // Settings
            container.RegisterPerWebRequest<ISettingsRetriever, WebConfigSettingsRetriever>();
            
            // Repos
            container.RegisterPerWebRequest<IUserRepository, UserRepository>();
        }
    }
}