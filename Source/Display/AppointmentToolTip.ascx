<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppointmentToolTip.ascx.cs" Inherits="Engage.Dnn.Booking.AppointmentToolTip" %>

<div class="calendar-tooltip">
    <asp:Label ID="TitleLabel" runat="server" CssClass="title" />
    <asp:Label runat="server" CssClass="label" ResourceKey="Starts" /><asp:Label ID="StartsLabel" runat="server" CssClass="value" />
    <asp:Label runat="server" CssClass="label" ResourceKey="Ends" /><asp:Label ID="EndsLabel" runat="server" CssClass="value" />
    <asp:HyperLink ID="DetailsLink" runat="server" ResourceKey="Details" />
</div>