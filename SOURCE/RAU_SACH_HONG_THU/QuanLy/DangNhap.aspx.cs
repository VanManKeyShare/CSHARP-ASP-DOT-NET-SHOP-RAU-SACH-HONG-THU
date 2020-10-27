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
    public partial class DangNhap : System.Web.UI.Page
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
            if (kiem_tra_dang_nhap() == true)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            label_thongbao.Text = "";
        }

        protected void btn_dang_nhap_Click(object sender, EventArgs e)
        {
            String tai_khoan = "";
            String mat_khau_from_client = "";
            String captcha_from_client = "";

            String id_thanh_vien = "";
            String ma_quyen = "";
            bool khoa = false;
            String mat_khau_from_csdl = "";

            tai_khoan = txt_tai_khoan.Text.Trim();
            mat_khau_from_client = txt_mat_khau.Text.Trim();
            captcha_from_client = txt_captcha.Text.Trim();

            // TẠO MỚI CLASS CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            if (tai_khoan == "" || mat_khau_from_client == "" || captcha_from_client == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // KIỂM TRA CAPTCHA //

            if (Session["captcha"] == null)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG KIỂM TRA ĐƯỢC CAPTCHA");
                return;
            }
            if (captcha_from_client != Session["captcha"].ToString())
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("MÃ XÁC NHẬN KHÔNG ĐÚNG");
                txt_captcha.Text = "";
                return;
            }
            Session["captcha"] = null;

            // LẤY THÔNG TIN TỪ CSDL //

            vmk_csdl.sql_query = "select top(1) mat_khau, id_tv, ma_quyen, khoa from thanh_vien where email = @email or account_name = @account_name";

            DataTable sql_param = vmk_csdl.VMK_SQL_PARAM();
            sql_param.Rows.Add("@email", tai_khoan, SqlDbType.VarChar);
            sql_param.Rows.Add("@account_name", tai_khoan, SqlDbType.VarChar);
            vmk_csdl.sql_param = sql_param;

            DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

            if (BANG_KQ.Rows.Count != 0)
            {
                mat_khau_from_csdl = BANG_KQ.Rows[0][0].ToString();
                id_thanh_vien = BANG_KQ.Rows[0][1].ToString();
                ma_quyen = BANG_KQ.Rows[0][2].ToString();
                khoa = Convert.ToBoolean(BANG_KQ.Rows[0][3]);
            }

            if (khoa == true)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("TÀI KHOẢN NÀY ĐÃ BỊ KHÓA");
                return;
            }

            // KIỂM TRA QUYỀN HẠN //

            ArrayList danh_sach_quyen_cho_phep = new ArrayList();
            danh_sach_quyen_cho_phep.Add("Q001");
            danh_sach_quyen_cho_phep.Add("Q002");

            if (danh_sach_quyen_cho_phep.IndexOf(ma_quyen) == -1)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN KHÔNG ĐƯỢC PHÉP ĐĂNG NHẬP KHU VỰC NÀY");
                return;
            }

            // KIỂM TRA MẬT KHẨU //

            if (ClassMain.VMK_CHECK_MD5(mat_khau_from_client, mat_khau_from_csdl) == true && id_thanh_vien != "")
            {
                Session["id_thanh_vien"] = id_thanh_vien;
                Session["ma_quyen"] = ma_quyen;
                Session.Timeout = 3600;
                Response.Redirect("Default.aspx");
                return;
            }

            label_thongbao.Text = ClassMain.TAO_THONG_BAO("TÀI KHOẢN HOẶC MẬT KHẨU KHÔNG ĐÚNG");
        }
    }
}
