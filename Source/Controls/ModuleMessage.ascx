<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ModuleMessage.ascx.cs" Inherits="Engage.Dnn.Booking.Controls.ModuleMessage" EnableViewState="false" %>
<div class="<%=MessageStyle %>Message ModuleMessage">
        <div class="<%=MessageStyle %>Top mmTop"></div>
        <div class="<%=MessageStyle %>Body mmBody">
            <span class="<%=MessageStyle %>Icon mmIcon"><%=GetLocalizedText(MessageStyle, this)%></span>
            <div class="mmText">
                <p class="Normal"><asp:Label ID="messageLabel" runat="server" /></p>
            </div>
        </div>
        <div class="<%=MessageStyle %>Bt mmBt"></div>
</div>