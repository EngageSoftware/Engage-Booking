<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Engage.Dnn.Booking"%>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Approval" CodeBehind="Approval.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="approval">
    <engage:ModuleMessage ID="ConflictingAppointmentsMessage" runat="server" MessageType="Error" Visible="false" />
    <engage:ModuleMessage ID="ApprovalMessage" runat="server" MessageType="Success" Visible="false" />
    <asp:MultiView ID="ApprovalMultiview" runat="server" ActiveViewIndex="0">
        <asp:View ID="ApprovalsListView" runat="server">
            <script type="text/javascript">
                var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
                pageRequestManager.add_beginRequest(function(sender, args) {
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    if (args.get_postBackElement().id.indexOf('Approval') != -1) {
                        hideActiveToolTip();
                    }
                });

                function hideActiveToolTip() {
                    if (Telerik.Web.UI.ReadToolTipController) {
                        var controller = Telerik.Web.UI.RadToolTipController.getInstance();
                        if (controller) {
                            var tooltip = controller.get_activeToolTip();
                            if (tooltip) {
                                tooltip.hide();
                            }
                        }
                    }
                }
            </script>
        	<div class="bulk-selection">
        	    <h4>Pending Approvals</h4>
                <asp:LinkButton ID="AcceptAppointmentsButton" runat="server" CssClass="approval-accept-link" ResourceKey="Accept Selected Items" />
                <asp:LinkButton ID="DeclineAppointmentsButton" runat="server" CssClass="approval-decline-link" ResourceKey="Decline Selected Items" />
            </div>
            
            <asp:GridView ID="AppointmentsGrid" runat="server" GridLines="None" BorderWidth="0" BorderStyle="None" AutoGenerateColumns="false" CssClass="approval-grid" AlternatingRowStyle-CssClass="alternate">
                <EmptyDataTemplate>
                    <div class="approval-empty help-txt"><%=Localize("All Approved.Text") %></div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#(Container.DataItemIndex + 1).ToString("00", CultureInfo.CurrentCulture) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" CssClass="header-checkbox" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectionCheckBox" runat="server" CssClass="select-checkbox" />
                            <asp:HiddenField ID="AppointmentIdHiddenField" runat="server" Value='<%#((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="SelectLink" runat="server" CssClass="approval-select-link" Text='<%#Eval("Title") %>' NavigateUrl='<%#this.BuildLinkUrl(this.ModuleId, ControlKey.AppointmentDetails, "appointmentId=" + ((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture)) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StartDateTime" HeaderText="Date and Time" DataFormatString="{0:dddd, MMM dd, yyyy} at {0:h:mm tt}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandName="Accept" CommandArgument='<%#((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture) %>' CssClass="approval-accept-link" ResourceKey="Accept" />
                            <asp:LinkButton runat="server" CommandName="Decline" CommandArgument='<%#((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture) %>' CssClass="approval-decline-link" ResourceKey="Decline" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <dnn:PagingControl ID="PagingControl" runat="server" CssClass="approval-paging"/>
            <telerik:radtooltipmanager runat="server" id="PendingAppointmentToolTipManager" width="300" height="150"
                animation="None" position="BottomRight" HideEvent="LeaveTooltip" text="Loading..." AutoTooltipify="false" />
        </asp:View>
        <asp:View ID="ProvideDeclineReasonView" runat="server">
            <asp:Repeater ID="DeclineReasonRepeater" runat="server">
                <HeaderTemplate>
                    <div class="approval-decline-reasons">
                        <asp:Label runat="server" ResourceKey="Reason for Decline" CssClass="approval-decline-instructions help-txt" />
                </HeaderTemplate>
                <ItemTemplate>
                        <div class="approval-decline-reason">
                            <asp:Label runat="server" ResourceKey="Decline Reason For" CssClass="approval-decline-reason-label" /><asp:Label runat="server" CssClass="approval-decline-reason-value" Text='<%#Eval("Title") %>' />
                            <asp:HiddenField ID="DeclinedAppointmentIdHiddenField" runat="server" Value='<%#((int)Eval("AppointmentId")).ToString(CultureInfo.InvariantCulture) %>' />
                            <asp:TextBox ID="DeclineReasonTextBox" runat="server" TextMode="MultiLine" CssClass="approval-decline-reason-textbox" Rows="5" Columns="40"/>
                        </div>
                </ItemTemplate>
                <FooterTemplate>
                		<asp:LinkButton runat="server" CssClass="approval-decline-reason-button-submit" ResourceKey="SubmitDeclineReasonButton" OnClick="SubmitDeclineReasonButton_Click" />
                        <asp:LinkButton runat="server" CssClass="approval-decline-reason-button-cancel" ResourceKey="CancelDeclineButton" OnClick="CancelDeclineButton_Click" />
                    </div>
                </FooterTemplate>
            </asp:Repeater>            
        </asp:View>
    </asp:MultiView>
</div>