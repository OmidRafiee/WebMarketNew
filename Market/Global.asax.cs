using StructureMap.Web.Pipeline;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebMarket.DataLayer.Context;
using WebMarket.DataLayer.Migrations;
using WebMarket.IocConfig;




namespace Market
{
    public class MvcApplication :HttpApplication
    {
        #region Application_Start
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                SetDbInitializer();

                //Set current Controller factory as StructureMapControllerFactory
                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

                AutoMapperConfig.Configuration.Configure ();
            }
            catch
            {
                HttpRuntime.UnloadAppDomain(); // سبب ری استارت برنامه و آغاز مجدد آن با درخواست بعدی می‌شود
                throw;
            }
        }
        #endregion

        #region Private Members
        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
                }

                return SampleObjectFactory.Container.GetInstance(controllerType) as Controller;
            }
        }

        private static void SetDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MarketDbContext,Configuration> ());
            SampleObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }

        #endregion
        
        #region Application_EndRequest
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
        #endregion
    }
}
