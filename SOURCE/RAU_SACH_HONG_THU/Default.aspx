<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="Default.aspx.cs" Inherits="RAU_SACH_HONG_THU.Default_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        body, div#body{
	        background: #92CB43;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="body">
	<div class="wrap-p clearfix">

		<div id="slider">
			<div class="photo-holder">
				<div id="number_slideshow" class="number_slideshow">
					<ul>
                        <asp:Repeater ID="repeater_list_data_slider1" runat="server">
                            <ItemTemplate>
                                <li style="width: 100%; text-align: center">
                                    <div class="title"><%# HTML_Encode(Eval("ten_sp")) %></div>
                                    <a href='SanPham.aspx?idsp=<%# Eval("id_sp") %>'>
                                        <img src='<%# Xu_Ly_Tach_Img_From_Noi_Dung(Eval("gioi_thieu")) %>'/>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
					</ul>
					<ul class="number_slideshow_nav">
                        <asp:Repeater ID="repeater_list_data_slider2" runat="server">
                            <ItemTemplate>
                                <li><a href="#"><%# Eval("stt") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
					</ul>
					<div style="clear: both"></div>
				</div>
			</div>
			<div class="shadow"></div>                
		</div>

        <asp:Label ID="label_thongbao" runat="server" ForeColor="Red" BackColor="white"></asp:Label>

        <asp:Repeater ID="repeater_list_data" runat="server">
            <HeaderTemplate>
                <div id="row">
            </HeaderTemplate>
            <ItemTemplate>
			        <div class="post fst-c">
				        <div class="photo-holder">
					        <a href="SanPham.aspx?idsp=<%# Eval("id_sp") %>" title=""><img width="320px" src='<%# Xu_Ly_Tach_Img_From_Noi_Dung(Eval("gioi_thieu")) %>' alt="" title=""/></a>
				        </div>
				        <div class="info">
					        <a href="SanPham.aspx?idsp=<%# Eval("id_sp") %>" title="">
						        <h4><%# HTML_Encode(Eval("ten_sp")) %></h4>
						        <table style="width: 98%; margin: 0px;">
							        <tr>
								        <td width="200px">
									        <span class="percent">Giá sản phẩm / <%# HTML_Encode(Eval("ten_dvt")) %></span>
								        </td>
								        <td width="1px" style="text-align: center;"><i class="ico-bought"></i></td>
								        <td></td>
							        </tr>
							        <tr>
								        <td>
									        <span class="price"><%# Xu_Ly_Money(Eval("don_gia")) %><sup>đ</sup></span>
								        </td>
								        <td style="text-align: center;"><%# Eval("luot_xem") %></td>
								        <td><a href="GioHang.aspx?ThemSP=<%# Eval("id_sp") %>"><span class="more-btn"><img src="images/icon_cart.gif" /></span></a></td>
							        </tr>
						        </table>
					        </a>
				        </div>
			        </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

	</div>
</div>
</asp:Content>
