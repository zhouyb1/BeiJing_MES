using System.Web.Mvc;

namespace Ayma.Application.Web.Areas.MesDev
{
    public class MesDevAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MesDev";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MesDev_default",
                "MesDev/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Ayma.Application.Web.Areas.MesDev.Controllers" }
            );
        }
    }
}