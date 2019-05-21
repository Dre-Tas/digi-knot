using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiKnot.ViewModel
{
    [Transaction(TransactionMode.Manual)]
    public class ShowData : IExternalCommand
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

            Views.UserControl1 view = new Views.UserControl1();
            view.ShowDialog();

            uidoc.ShowElements(doc.GetElement("f26b5330-f10a-41c6-b291-67e1749339cf-000e2371"));

            return Result.Succeeded;
        }
    }
}
