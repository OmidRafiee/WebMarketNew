
namespace Market
{
    using Microsoft.Owin.Security.DataProtection;

    using Owin;

    using StructureMap.Web;

    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            configureAuth(app);
        }

        private static void configureAuth(Owin.IAppBuilder app)
        {
            WebMarket.IocConfig.SampleObjectFactory.Container.Configure(config =>
            {
                config.For<Microsoft.Owin.Security.DataProtection.IDataProtectionProvider>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use(() => app.GetDataProtectionProvider());
            });

            WebMarket.IocConfig.SampleObjectFactory.Container.GetInstance<WebMarket.ServiceLayer.Interfaces.IApplicationUserManager>().SeedDatabase();

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login"),
                Provider = new Microsoft.Owin.Security.Cookies.CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = WebMarket.IocConfig.SampleObjectFactory.Container.GetInstance<WebMarket.ServiceLayer.Interfaces.IApplicationUserManager>().OnValidateIdentity()
                },
                CookieName = "Iris_Store",
            });

            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.TwoFactorCookie, System.TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.CreatePerOwinContext(
               () => WebMarket.IocConfig.SampleObjectFactory.Container.GetInstance<WebMarket.ServiceLayer.Interfaces.IApplicationUserManager>());

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //    clientId: "",
            //    clientSecret: "");

        }
    }
}