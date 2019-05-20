#region Namespaces
using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.UI;
#endregion

namespace DigiKnot
{
    class App : IExternalApplication
    {
        // define a method that will create our tab and button
        static void AddDigiKnotPanel(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            string tabName = "DigiKnot";
            application.CreateRibbonTab(tabName);

            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // Add new ribbon panels
            RibbonPanel digiKnotPanel =
                application.CreateRibbonPanel(tabName, "Setup");

            // create push button for ModelAudit
            PushButtonData bData = new PushButtonData(
                "cmddigKnotConnect",
                "Connect DB",
                thisAssemblyPath,
                "DigiKnot.Command");

            PushButton pb = digiKnotPanel.AddItem(bData) as PushButton;
            pb.ToolTip = "Enstablish connection to SQL database";
            BitmapImage pbImage = new BitmapImage(new Uri
                ("pack://application:,,,/DigiKnot;component/Resources/connect.png"));
            pb.LargeImage = pbImage;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddDigiKnotPanel(application);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
