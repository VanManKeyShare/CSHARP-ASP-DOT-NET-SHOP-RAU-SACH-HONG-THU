using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RAU_SACH_HONG_THU
{
    public partial class Admin_Default : System.Web.UI.Page
    {
        private bool kiem_tra_dang_nhap()
        {
            if (Session["id_thanh_vien"] != null && Session["ma_quyen"] != null)
            {
                return true;
            }
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // KIỂM TRA ĐĂNG NHẬP //

            if (kiem_tra_dang_nhap() == false)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
        }
    }
}
