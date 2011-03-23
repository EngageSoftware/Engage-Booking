<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ManageAppointmentTypes.ascx.cs" Inherits="Engage.Dnn.Booking.ManageAppointmentTypes" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="engage" Namespace="Engage.Controls" Assembly="Engage.Framework" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>

<engage:ModuleMessage runat="server" ID="SuccessModuleMessage" MessageType="Success" CssClass="CategorySaveSuccessMessage"/>

<telerik:RadGrid ID="AppointmentTypesGrid" runat="server" AutoGenerateColumns="false" AllowMultiRowEdit="true" ValidationSettings-ValidationGroup="ManageAppointmentTypes">
    <MasterTableView DataKeyNames="Id" EditMode="InPlace" CommandItemDisplay="Top">
        <Columns>
            <telerik:GridEditCommandColumn UniqueName="EditButtons" ItemStyle-CssClass="buttons-col" />
            <telerik:GridTemplateColumn UniqueName="Name" ItemStyle-CssClass="name-col">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Name") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' MaxLength="250" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" ValidationGroup="ManageAppointmentTypes" 
                        ResourceKey="NameRequired" ForeColor="" CssClass="NormalRed" Display="None" />
                    <asp:CustomValidator runat="server" ControlToValidate="NameTextBox" ValidationGroup="ManageAppointmentTypes"
                        ResourceKey="NameUnique" ForeColor="" CssClass="NormalRed" Display="None" OnServerValidate="UniqueNameValidator_ServerValidate" />
                </EditItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridButtonColumn UniqueName="Delete" ItemStyle-CssClass="delete-col" CommandName="Delete" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

<engage:ValidationSummary runat="server" ValidationGroup="ManageAppointmentTypes" />