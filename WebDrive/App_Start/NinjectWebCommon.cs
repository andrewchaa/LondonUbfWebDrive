using System.Configuration;
using System.Web.Http;
using LondonUbfWebDrive.Controllers;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;
using LondonUbfWebDrive.Infrastructure;
using LondonUbfWebDrive.Repositories;
using LondonUbfWebDrive.Service;

[assembly: WebActivator.PreApplicationStartMethod(typeof(LondonUbfWebDrive.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(LondonUbfWebDrive.App_Start.NinjectWebCommon), "Stop")]

namespace LondonUbfWebDrive.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IReadDocumentService>().To<ReadDocumentService>();
            kernel.Bind<IBreadcrumbService>().To<BreadcrumbService>();
            kernel.Bind<IMetaDataRepository>().To<MetaDataRepository>();
            kernel.Bind<IMongoDbHelper>().To<MongoDbHelper>().InSingletonScope();
            kernel.Bind<IConfigService>().To<ConfigService>();
        }        
    }
}
