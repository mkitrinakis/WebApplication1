using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class testValidation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lNow.Text = "Current time" + System.DateTime.Now; 
            TextBox tb2 = new TextBox() { ID = "tb2" };
            panel1.Controls.Add(tb2);
            RequiredFieldValidator cv2 = new RequiredFieldValidator();
            cv2.ControlToValidate = tb2.ID;
            cv2.EnableClientScript = true; 
            cv2.Text = "tb2 cannot be blank ";
            cv2.ValidateRequestMode = ValidateRequestMode.Enabled; 
            panel1.Controls.Add(cv2);

            panel1.Controls.Add(new Literal() { Text = "<br/.<br/>" });

            CheckBox cb = new CheckBox();
            cb.Attributes.Add ("onclick", "alert('changed');return(true)") ;
          //  cb.CheckedChanged += Cb_CheckedChanged;
            cb.AutoPostBack = true;
            cb.ID = "cb1";
            cb.Text = "Option 1";
            panel1.Controls.Add(cb); 


        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            L1.Text = "Changed from Checked"; 
        }

        protected void bSave_Click(object sender, EventArgs e)
        {
            L1.Text = "Submitted"; 
        }

        /*  DID NOT USE 
         * <script  type="text/javascript" 
        src="/aspnet_client/system_web/1_0_3617_0/WebUIValidation.js">
        </script>
        */ 
    }
}
 