using NLog;
//using Payoneer.Payoneer.Hotels.Model.ExampleDomain;
using Payoneer.Payoneer.Hotels.Service;
using Payoneer.ServicesInfra.DependencyInjection.Registering;
using Payoneer.ServicesInfra.DependencyInjection.Unity;
using Payoneer.ServicesInfra.HealthMonitoring;
using Payoneer.ServicesInfra.HealthMonitoring.Sql;
using Payoneer.ServicesInfra.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;
using Unity;

namespace Payoneer.Payoneer.Hotels.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> LazyContainer =
          new Lazy<IUnityContainer>(() =>
          {
              var myContainer = new UnityContainer();
              RegisterTypes(myContainer);
              return myContainer;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => LazyContainer.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="unityContainer">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            LogManager.GetLogger(typeof(UnityConfig).FullName).Info("Registering components...");

            
            var di = new UnityRegistrar(unityContainer);
            di.ConnectToServiceLocator();

            RegisterDbContexts(di);

            RegisterHealthMonitors(di);

            // One instance of my Controllers
            //di.RegisterAsSingleInstance<IExampleService>(() => new ExampleService());
            di.RegisterAsSingleInstance<IHotelService>(() => new HotelService());
            di.RegisterAsSingleInstance<ICustomerService>(() => new CustomerService());
            di.RegisterAsSingleInstance<IRoomService>(() => new RoomService());
            di.RegisterAsSingleInstance<IReservationService>(() => new ReservationService());
        }

        private static void RegisterHealthMonitors(DiRegistrar di)
        {
            HealthMonitorAggregator.RegisterHealthMonitor<SqlHealthMonitor>(di);
        }

        private static void RegisterDbContexts(DiRegistrar di)
        {
            di.RegisterAsTransient<IUnitOfWork>(() => new UnitOfWork());
            //di.RegisterAsTransient<IDictionariesEntities, DictionariesEntities>();
            di.RegisterAsTransient<IHotelContext, HotelContext>();

        }
    }
}