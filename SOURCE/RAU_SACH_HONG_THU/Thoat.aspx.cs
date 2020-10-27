using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace RAU_SACH_HONG_THU
{
    public partial class Thoat_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "THOÁT - " + this.Page.Title;

            label_tieudebaiviet.Text = "THOÁT";
            label_noidungbaiviet.Text = "ĐANG THOÁT....";

            Session["id_thanh_vien"] = null;
            Session["ma_quyen"] = null;
            Session["captcha"] = null;
            Session.Timeout = 3600;
            Response.Redirect("Default.aspx");
        }
    }
}
