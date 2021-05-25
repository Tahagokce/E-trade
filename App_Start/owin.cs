using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Abc.MvcWebUI.App_Start.owin))]

namespace Abc.MvcWebUI.App_Start
{
    public class owin
    {
        public void Configuration(IAppBuilder app)
        {
            // Uygulamanızı nasıl yapılandıracağınız hakkında daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=316888 adresini ziyaret edin
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",  
                LoginPath = new PathString("/Account/Login")        // kullanıcı yetkisini aşan bir yere gitmek istediğinde onu burada ki path'a yönlendiricez
            });
        }
    }
}
