#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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

            foreach (Element e in col)
            {
                string uid = e.UniqueId;
                MessageBox.Show(uid);
                string assetId = e.LookupParameter("Asset ID").AsString();
                MessageBox.Show(assetId);
            }

            //// Modify document within a transaction

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("Transaction Name");
            //    tx.Commit();
            //}

            return Result.Succeeded;
        }
    }
}
