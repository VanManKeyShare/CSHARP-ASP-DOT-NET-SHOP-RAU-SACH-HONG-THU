using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RAU_SACH_HONG_THU
{
    public partial class Thoat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id_thanh_vien"] = null;
            Session["ma_quyen"] = null;
            Session["captcha"] = null;
            Session.Timeout = 3600;
            Response.Redirect("DangNhap.aspx");
        }
    }
}
