using System.IO;
using System.Text;
using System.Web.Mvc;

namespace MinimalServer.Controllers
{
    public class IdentityServerController : Controller
    {
        public static class ViewNames
        {
            public const string Login = "Login";
        }

        protected const string masterName = "~/Views/Shared/_Layout.cshtml";

        public virtual Stream RenderViewToStream(string viewName, object model, string masterName = masterName)
        {
            ViewData.Model = model;
            MemoryStream memoryStream = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(memoryStream, Encoding.UTF8, 4096, true))
            {
                ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(ControllerContext, viewName, masterName);
                IView viewResult = viewEngineResult.View;
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult, ViewData, TempData, sw);
                viewResult.Render(viewContext, sw);
                viewEngineResult.ViewEngine.ReleaseView(ControllerContext, viewResult);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}