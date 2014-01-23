using System.Web.Http;
using WebDrive.App_Start;
using WebDrive.Controllers;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Services;
using WebDrive.Infrastructure;
using WebDrive.Repositories;
using WebDrive.Service;
using log4net;
using log4net.Core;
using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WebDrive.App_Start
{
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
            kernel.Bind<IReadDocumentService>().To<DocumentReader>();
            kernel.Bind<IBreadcrumbService>().To<BreadcrumbService>();
            kernel.Bind<IMetaDataRepository>().To<MetaDataRepository>();
            kernel.Bind<IMongoDbHelper>().To<MongoDbHelper>().InSingletonScope();
            kernel.Bind<IConfig>().To<Config>();
            kernel.Bind<ILog>().To<LogImpl>();
            kernel.Bind<IFileDirectoryService>().To<FileDirectoryService>();
        }        
    }
}
