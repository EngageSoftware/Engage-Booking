<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Actions.SortAction" CodeBehind="SortAction.ascx.cs" %>
<asp:RadioButtonList ID="SortRadioButtonList" runat="server" AutoPostBack="True" CssClass="Normal" RepeatDirection="Horizontal">
    <asp:ListItem Selected="True" Value="EventStart" ResourceKey="DateListItem" />
    <asp:ListItem Value="Title" ResourceKey="TitleListItem" />
</asp:RadioButtonList>