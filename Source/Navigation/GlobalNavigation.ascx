<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.GlobalNavigation" Codebehind="GlobalNavigation.ascx.cs" %>

<div id="divAdminButtons" class="AdminButtons">
    <asp:HyperLink ID="HomeLink" runat="server" CausesValidation="false" ImageUrl="~/DesktopModules/EngageBooking/Images/home.gif" ResourceKey="Home"/>
    <asp:HyperLink ID="SettingsLink" runat="server" CausesValidation="false" ImageUrl="~/DesktopModules/EngageBooking/Images/settings.gif" ResourceKey="Settings"/>
    <asp:HyperLink ID="AddAnEventLink" runat="server" CausesValidation="false" ImageUrl="~/DesktopModules/EngageBooking/Images/add-apntmnt.gif" ResourceKey="Add Appointment"/>
</div>