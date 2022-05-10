using Lcapas.Core.Library;
using Lcapas.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Lcapas.AD.Views.WebSvc
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        private LcapasLogic lcapasLogic = new LcapasLogic();

        [WebMethod]
        public bool RunJobs()
        {
            bool success = false;
            try
            {
                TranscriptsManager manager = new TranscriptsManager();
                //string timestamp = DateTime.Now.ToString();
                //lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "RunJobs", "TimeStamp", timestamp);
            }
            catch (Exception ex)
            {
                lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "Runjobs", "Error", ex.ToString());
            }
            return success;
        }

        [WebMethod]
        public string TestService() {
            //
            string timestamp = DateTime.Now.ToShortDateString();
            // lcapasLogic.SaveException(Structs.Project.LcapasAdmin, Structs.Class.TranscriptsManager, "TestService", "TimeStamp", timestamp);
            return timestamp;
        }
    }
}
