using System.Web;
using System.Web.Optimization;

namespace MongoMagno
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/bottomscripts")
                            .Include("~/Scripts/jquery.js")
                            .Include("~/Scripts/bootstrap.js")
                            .Include("~/Scripts/angular.js")
                            .Include("~/Scripts/ui-bootstrap.js")
                            .Include("~/Scripts/format.js")
                            .Include("~/Scripts/underscore.js"));

            bundles.Add(new ScriptBundle("~/Scripts/MMWorkspace")
                            .Include("~/Client/MMWorkspace/Scripts/app.js")
                            .Include("~/Client/MMWorkspace/Scripts/apiServer.js")
                            .Include("~/Client/MMWorkspace/Scripts/mainController.js")
                            .Include("~/Client/MMWorkspace/Scripts/selectServerController.js")
                            .Include("~/Client/MMWorkspace/Scripts/webStorage.js"));
        }
    }
}