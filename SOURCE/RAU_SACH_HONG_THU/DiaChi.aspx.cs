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
    public partial class DiaChi_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Đăng Ký - " + this.Page.Title;
        }
    }
}
