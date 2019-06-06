using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            l1.Text = System.DateTime.Now.ToString();
            Control c1 = LoadControl("c1.ascx");
            p1.Controls.Add(c1); 
        }

        protected void b1_Click(object sender, EventArgs e)
        {
            Label1.Text = "Clicked ... on" + System.DateTime.Now.ToString(); 
        }
    }
}