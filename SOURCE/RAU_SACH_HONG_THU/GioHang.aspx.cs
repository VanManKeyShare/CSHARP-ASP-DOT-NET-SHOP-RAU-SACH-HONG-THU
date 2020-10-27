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
    public partial class GioHang_TrangChu : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        public string Xu_Ly_Money(object money_obj)
        {
            string money_truockhixuly = Convert.ToString(money_obj);
            string money_saukhixuly = "0";
            Int64 money = 0;
            bool check_money = Int64.TryParse(money_truockhixuly, out money);
            if (check_money == true)
            {
                money_saukhixuly = String.Format("{0:#,##}", money);
            }
            return money_saukhixuly;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Giỏ Hàng - " + this.Page.Title;

            if (Session["id_thanh_vien"] == null)
            {
                label_thongbao.Text = "*** BẠN CHƯA ĐĂNG NHẬP ĐỂ THÊM VÀ XEM GIỎ HÀNG" + "<br/><br/>";
                return;
            }

            id_thanh_vien = Session["id_thanh_vien"].ToString();

            if(!IsPostBack)
            {
                // LẤY ID SẢN PHẨM TỪ BIẾN TRUYỀN TRONG URL //

                string id_sp = "";

                if (Request.QueryString["ThemSP"] != null && Request.QueryString["ThemSP"].ToString() != "")
                {
                    id_sp = Request.QueryString["ThemSP"].ToString().ToLower();
                }

                // THÊM SẢN PHẨM VÀO GIỎ HÀNG //

                if (id_sp != "")
                {
                    // TIẾN HÀNH NHẬP VÀO CSDL //

                    ClassCSDL vmk_csdl2 = new ClassCSDL();

                    vmk_csdl2.sql_query = "" +
                        " if not exists (select id_sp from gio_hang where id_tv = @id_tv and id_sp = @id_sp)" +
                        "     insert into gio_hang(id_tv, id_sp, don_gia, so_luong, id_dvt)" + 
                        "         values (@id_tv, @id_sp, (select don_gia from san_pham where id_sp = @id_sp), 1, (select id_dvt from san_pham where id_sp = @id_sp))" +
                        " else" +
                        "     update gio_hang set so_luong = so_luong + 1 where id_tv = @id_tv and id_sp = @id_sp"
                    ;

                    DataTable sql_param2 = vmk_csdl2.sql_param;
                    sql_param2.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
                    sql_param2.Rows.Add("@id_sp", id_sp, SqlDbType.Int);
                    vmk_csdl2.sql_param = sql_param2;

                    int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

                    Response.Redirect("GioHang.aspx");
                }

                // LẤY DỮ LIỆU GIỎ HÀNG TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select gio_hang.id_sp, san_pham.ten_sp, gio_hang.don_gia, gio_hang.so_luong, gio_hang.thanh_tien, (select ten_dvt from don_vi_tinh where id_dvt = san_pham.id_dvt) as ten_dvt" +
                    " from gio_hang, san_pham" +
                    " where" + 
                    " gio_hang.id_sp = san_pham.id_sp" + 
                    " and id_tv = @id_tv"
                ;

                sql_datasource.SelectParameters.Clear();
                sql_datasource.SelectParameters.Add("id_tv", DbType.String, id_thanh_vien.ToString());
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    Int64 thanh_tien_gio_hang = 0;

                    foreach (DataRow dr in vmk_dataview.ToTable().Rows)
                    {
                        thanh_tien_gio_hang += Convert.ToInt64(dr[4]);
                    }

                    repeater_list_data.DataSource = sql_datasource;
                    repeater_list_data.DataBind();

                    btn_dathang.Visible = true;
                    label_thanhtiengiohang.Visible = true;
                    label_thanhtiengiohang.Text = Xu_Ly_Money(thanh_tien_gio_hang);
                }
                else
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("GIỎ HÀNG CHƯA CÓ SẢN PHẨM NÀO", "", false);
                    return;
                }
            }
        }

        protected void repeater_list_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_item = (HiddenField)e.Item.FindControl("id_item");
            TextBox txt_soluong = (TextBox)e.Item.FindControl("txt_soluong");

            if (e.CommandName == "xoa")
            {
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete from gio_hang where id_sp = @id_sp";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "edit_soluong")
            {
                int so_luong;
                bool check_soluong = int.TryParse(txt_soluong.Text, out so_luong);
                if (check_soluong == false)
                {
                    so_luong = 1;
                }

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "update gio_hang" +
                    " set so_luong = @so_luong" +
                    " where id_sp = @id_sp"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                sql_param.Rows.Add("@so_luong", so_luong, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            Response.Redirect("GioHang.aspx");
        }

        protected void btn_dathang_Click(object sender, EventArgs e)
        {
            if (id_thanh_vien == "") { Response.Redirect("GioHang.aspx"); }

            // TẠO ĐƠN HÀNG VÀ LẤY ID ĐƠN HÀNG //

            ClassCSDL vmk_csdl1 = new ClassCSDL();

            // QUERY FOR SQL SERVER 2005+ //

            vmk_csdl1.sql_query = "insert into don_hang (id_tv, ngay_dh, thang_dh, nam_dh, thanh_tien)" +
                " output inserted.id_dh" +
                " values (@id_tv, @ngay_dh, @thang_dh, @nam_dh, (select sum(thanh_tien) from gio_hang where id_tv = @id_tv))"
            ;

            DataTable sql_param1 = vmk_csdl1.sql_param;
            sql_param1.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            sql_param1.Rows.Add("@ngay_dh", DateTime.Today.Day, SqlDbType.TinyInt);
            sql_param1.Rows.Add("@thang_dh", DateTime.Today.Month, SqlDbType.TinyInt);
            sql_param1.Rows.Add("@nam_dh", DateTime.Today.Year, SqlDbType.SmallInt);
            vmk_csdl1.sql_param = sql_param1;

            string id_dh = vmk_csdl1.VMK_SQL_INSERT_RETURN_OUTPUT();

            // CHUYỂN GIỎ HÀNG SANG ĐƠN HÀNG CHI TIẾT //

            ClassCSDL vmk_csdl2 = new ClassCSDL();
            vmk_csdl2.sql_query = "insert into don_hang_chi_tiet(id_dh, id_sp, don_gia, so_luong, id_dvt)" +
                " select @id_dh, id_sp, don_gia, so_luong, id_dvt from gio_hang where id_tv = @id_tv"
            ;

            DataTable sql_param2 = vmk_csdl2.sql_param;
            sql_param2.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            sql_param2.Rows.Add("@id_dh", id_dh, SqlDbType.Int);
            vmk_csdl2.sql_param = sql_param2;

            int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

            // XÓA GIỎ HÀNG //

            ClassCSDL vmk_csdl3 = new ClassCSDL();
            vmk_csdl3.sql_query = "delete from gio_hang where id_tv = @id_tv";

            DataTable sql_param3 = vmk_csdl3.sql_param;
            sql_param3.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            vmk_csdl3.sql_param = sql_param3;

            int sql_status3 = vmk_csdl3.VMK_SQL_INSERT_DELETE_UPDATE();

            Response.Redirect("DatHangThanhCong.aspx");
        }
    }
}
