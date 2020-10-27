using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;

namespace RAU_SACH_HONG_THU
{
    public partial class Default_TrangChu : System.Web.UI.Page
    {
        public string HTML_Encode(object data, bool replace_newline = false)
        {
            return ClassMain.HTML_Encode(data, replace_newline);
        }

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

        public string Xu_Ly_Tach_Img_From_Noi_Dung(object data)
        {
            string data_source = Convert.ToString(data);
            string url_of_img = "";

            Regex regExp = new Regex(@"\[img(.*?)\](.+?)\[/img\]");
            Match regExp_Url_Img = regExp.Match(data_source);
            url_of_img = regExp_Url_Img.Groups[2].ToString();

            return url_of_img;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = this.Page.Title;

            if (!IsPostBack)
            {
                // LẤY DANH SÁCH SẢN PHẨM MỚI NHẤT TỪ CSDL VÀ ĐƯA LÊN GIAO DIỆN //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select id_sp, san_pham.id_dm, ten_dm, ten_sp, gioi_thieu, ngay_sp, thang_sp, nam_sp, don_gia, luot_xem, (select ten_dvt from don_vi_tinh where id_dvt = san_pham.id_dvt) as ten_dvt" +
                    " from san_pham, danh_muc" +
                    " where san_pham.id_dm = danh_muc.id_dm" +
                    " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                ;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count != 0)
                {
                    repeater_list_data.DataSource = BANG_KQ;
                    repeater_list_data.DataBind();
                }
                else
                {
                    label_thongbao.Text = "KHÔNG CÓ DỮ LIỆU <br /><br />";
                    return;
                }

                // LẤY DANH SÁCH SẢN PHẨM XEM NHIỀU TỪ CSDL VÀ ĐƯA LÊN SLIDER //

                ClassCSDL vmk_csdl2 = new ClassCSDL();
                vmk_csdl2.sql_query = "select top(5) id_sp, ten_sp, gioi_thieu" +
                    " from san_pham" +
                    " order by luot_xem desc, nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                ;

                DataTable BANG_KQ2 = vmk_csdl2.VMK_SQL_SELECT();
                if (BANG_KQ2.Rows.Count != 0)
                {
                    BANG_KQ2.Columns.Add("stt", typeof(Int16));
                    for (int i = 0; i < BANG_KQ2.Rows.Count; i++)
                    {
                        BANG_KQ2.Rows[i][3] = i + 1;
                    }
                    repeater_list_data_slider1.DataSource = BANG_KQ2;
                    repeater_list_data_slider1.DataBind();
                    repeater_list_data_slider2.DataSource = BANG_KQ2;
                    repeater_list_data_slider2.DataBind();
                }
            }
        }
    }
}
