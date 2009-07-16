<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Display.Approval" CodeBehind="Approval.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>

<div class="approval">
    <asp:LinkButton ID="AcceptItemsButton" runat="server" CssClass="approval-accept-link' ResourceKey="Accept Selected Items" />
    <asp:LinkButton ID="DeclineItemsButton" runat="server" CssClass="approval-decline-link' ResourceKey="Decline Selected Items" />
    <asp:GridView ID="AppointmentsGrid" runat="server" GridLines="None" AutoGenerateColumns="false"
        CssClass="approval-grid" AlternatingRowStyle-CssClass="alternate" RowStyle-CssClass="row">
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
            <asp:BoundField HeaderText="Name" /><%--DataField="Name" --%>
            <asp:BoundField DataField="StartDateTime" HeaderText="Date and Time" DataFormatString="{0:dddd, MMM dd, yyyy} at {0:h:mm tt}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Accept" CommandArgument='<%#Eval("AppointmentId") %>' CssClass="approval-accept-link" ResourceKey="Accept" />
                    <asp:LinkButton runat="server" CommandName="Decline" CommandArgument='<%#Eval("AppointmentId") %>' CssClass="approval-decline-link" ResourceKey="Decline" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <dnn:PagingControl ID="PagingControl" runat="server" PageSize="10" CssClass="approval-paging"/>
</div>