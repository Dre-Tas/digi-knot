#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace DigiKnot
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Views.Connect dbConnect = new Views.Connect();
            dbConnect.ShowDialog();

            // Get all NON built in parameters
            FilteredElementCollector paramColl = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID);

            // Get the Asset ID parameter
            Element param = paramColl
                .Where(p => p.Name == "Asset ID")
                .FirstOrDefault();

            // Create filter rule based on Asset ID parameter
            ParameterValueProvider pvp = new ParameterValueProvider(param.Id);
            FilterStringRuleEvaluator fsre = new FilterStringEquals();
            // Get only elements that have the Asset ID parameter different from the empty string
            FilterRule fRule2 = new FilterStringRule(pvp, fsre, "", false);
            ElementParameterFilter filter2 = new ElementParameterFilter(fRule2, true);

            // Filter elements in model by the parameter
            FilteredElementCollector col = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .WherePasses(filter2);

            SqlConnection connection = ViewModel.DbConnection.DbConnect();

            connection.Open();

            foreach (Element e in col)
            {
                // Get info to write
                string assetId = e.LookupParameter("Asset ID").AsString();
                string guid = e.UniqueId;

                // Define sql
                string sql = "insert into [Table]" +
                    " (Asset_ID, GUID)" +
                    " values(@assetId, @guid)";
                // Write each element in model to db
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@assetId", assetId);
                    cmd.Parameters.AddWithValue("@guid", guid);
                    cmd.ExecuteNonQuery();
                }
            }

            // Notify user
            MessageBox.Show("You are connected :)");

            // Return success
            return Result.Succeeded;
        }
    }

    class Asset
    {
        public string Asset_ID { get; set; }
        public string GUID { get; set; }

        public Asset(string assetId, string guid)
        {
            Asset_ID = assetId;
            GUID = guid;
        }
    }
}
