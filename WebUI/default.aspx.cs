using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApacheLib;
using WebUI.Services;

namespace WebUI
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ApacheLib.SysSettings.Init(new FileService(), new ApacheSettings());
                var vm = new ApacheLib.ViewModels.MainWindowVM();

                rptList.DataSource = vm.HostFileEntries;
                rptList.DataBind();
            }

        }
    }
}