<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="Captcha.aspx.cs" Inherits="RAU_SACH_HONG_THU.Captcha_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        div#main-menu > ul#menu-box
        {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="body">
	CAPTCHA
</div>
</asp:Content>

<asp:Content ID="FootContent" runat="server" ContentPlaceHolderID="FootContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#main-menu').hover(function () {
                $('#menu-box', this).show();
            }, function () {
                $('#menu-box', this).hide();
            });
        });
    </script>
</asp:Content>