<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Engage.Dnn.Booking"%>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.DirectApproval" CodeBehind="DirectApproval.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>

<div class="approval">
    <engage:ModuleMessage ID="ApprovalMessage" runat="server" MessageType="Error" TextResourceKey="Error.Text" />
</div>