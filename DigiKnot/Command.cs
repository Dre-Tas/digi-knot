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

            // Write all these elements to db
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\atassera\source\repos\DigiKnot\DigiKnot\Assets.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connString);

            connection.Open();

            foreach (Element e in col)
            {
                string guid = e.UniqueId;
                //MessageBox.Show(uid);
                string assetId = e.LookupParameter("Asset ID").AsString();
                //MessageBox.Show(assetId);

                string sql = "insert into [Table]" +
                    " (Asset_ID, GUID)" +
                    " values(@assetId, @guid)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@assetId", assetId);
                    cmd.Parameters.AddWithValue("@guid", guid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Row inserted !! ");
                }
            }

            MessageBox.Show("Done! :)");

            //// Modify document within a transaction

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("Transaction Name");
            //    tx.Commit();
            //}

            return Result.Succeeded;
        }

        internal const string assetSql = "insert into Table (Asset_ID, GUID) values (@assetId, @guid);"
            //+ "SELECT SCOPE_IDENTITY()"
            ;
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
