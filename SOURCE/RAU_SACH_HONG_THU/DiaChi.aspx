﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="DiaChi.aspx.cs" Inherits="RAU_SACH_HONG_THU.DiaChi_TrangChu" %>

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
	<div class="wrap-p clearfix" style="min-height: 300px; margin-top: 10px; word-wrap: break-word;">
        <h5 style="margin-bottom: 3px; color: #60A90A">BẢN ĐỒ</h5>
        <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <div class="loading" style="background: url(images/ajax-loading.gif) no-repeat;">
                <iframe id="iframe_bando" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1957.6045367754361!2d106.62564800000003!3d11.09779000000001!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zMTHCsDA1JzUyLjAiTiAxMDbCsDM3JzMyLjMiRQ!5e0!3m2!1svi!2s!4v1412616502090" width="100%" height="500px" frameborder="0" style="border:0"></iframe>
            </div>
        <br /><br />
    </div>
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