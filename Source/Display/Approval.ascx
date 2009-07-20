<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Engage.Dnn.Booking"%>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Approval" CodeBehind="Approval.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>

<div class="approval">
    <asp:LinkButton ID="AcceptItemsButton" runat="server" CssClass="approval-accept-link' ResourceKey="Accept Selected Items" />
    <asp:LinkButton ID="DeclineItemsButton" runat="server" CssClass="approval-decline-link' ResourceKey="Decline Selected Items" />
    <asp:GridView ID="AppointmentsGrid" runat="server" GridLines="None" AutoGenerateColumns="false"
        CssClass="approval-grid" AlternatingRowStyle-CssClass="alternate" SelectedRowStyle-CssClass="selected">
        <EmptyDataTemplate>
            <div class="approval-empty"><%=Localize("All Approved.Text") %></div>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="AppointmentId" DataFormatString="{0:00}" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox runat="server" CssClass="header-checkbox" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="SelctionCheckBox" runat="server" CssClass="select-checkbox" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:LinkButton ID="SelectLink" runat="server" CommandName="Select" CommandArgument='<%# ((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture) %>' CssClass="approval-select-link" Text='<%#Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StartDateTime" HeaderText="Date and Time" DataFormatString="{0:dddd, MMM dd, yyyy} at {0:h:mm tt}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Accept" CommandArgument='<%#Eval("AppointmentId") %>' CssClass="approval-accept-link" ResourceKey="Accept" />
                    <asp:LinkButton runat="server" CommandName="Decline" CommandArgument='<%#Eval("AppointmentId") %>' CssClass="approval-decline-link" ResourceKey="Decline" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:PlaceHolder ID="AppointmentDetailsPlaceHolder" runat="server" Visible="false">
        <fieldset>
            <asp:Label runat="server" CssClass="approval-detail-label" ResourceKey="Date and Time" /> <asp:Label ID="DetailDateAndTimeLabel" runat="server" CssClass="approval-detail-value" />
            <asp:Label runat="server" CssClass="approval-detail-label" ResourceKey="Full Name" /> <asp:Label ID="DetailFullNameLabel" runat="server" CssClass="approval-detail-value" />
            <asp:Label runat="server" CssClass="approval-detail-label" ResourceKey="Phone" /> 
                <asp:Label runat="server" CssClass="approval-detail-value-label" ResourceKey="Type" /> <asp:Label ID="DetailPhoneTypeLabel" runat="server" CssClass="approval-detail-value" />
                <asp:Label runat="server" CssClass="approval-detail-value-label" ResourceKey="Number" /> <asp:Label ID="DetailPhoneNumberLabel" runat="server" CssClass="approval-detail-value" />
        </fieldset>
        <fieldset>
            <legend><%=this.Localize("Special Participants") %></legend>
            <asp:Label runat="server" CssClass="approval-detail-label" ResourceKey="Total Number of Participants" /> <asp:Label ID="DetailNumberOfParticipantsLabel" runat="server" CssClass="approval-detail-value" />
            <asp:Label runat="server" CssClass="approval-detail-label" ResourceKey="Names" /> <asp:Label ID="DetailNamesLabel" runat="server" CssClass="approval-detail-value" Text="We don't keep track of this field..." />
        </fieldset>
    </asp:PlaceHolder>
            
    <dnn:PagingControl ID="PagingControl" runat="server" CssClass="approval-paging"/>
</div>