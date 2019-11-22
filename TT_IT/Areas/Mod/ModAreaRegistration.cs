using System.Web.Mvc;

namespace TT_IT.Areas.Mod
{
    public class ModAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Mod";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Mod_default",
                "Mod/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TT_IT.Areas.Mod.Controllers" }
            );
        }
    }
}