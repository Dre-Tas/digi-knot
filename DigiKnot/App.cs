#region Namespaces
using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.UI;
using System.Data.SqlClient;
using System.Collections.Generic;
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

        #region WIP
        //void Application_DocumentSaving(object sender,
        //    Autodesk.Revit.DB.Events.DocumentSavingEventArgs e)
        //{
        //    SqlConnection connection = ViewModel.DbConnection.DbConnect();

        //    connection.Open();

        //    string selectSql = "select Asset_ID from [Table]";

        //    // Write each element in model to db
        //    using (SqlCommand cmd = new SqlCommand(selectSql, connection))
        //    {
        //        List<string> read = new List<string>();
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            read.Add(reader.GetString(0));
        //        }
        //    }
        #endregion
    }
}
