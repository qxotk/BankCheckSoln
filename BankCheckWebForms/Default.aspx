<%@ Page Title="Bank Check Sample App Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BankCheckWebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        input
        {
            /*Override the ASP.NET sample app max width (was preventing Payee from growing to the appropriate size)*/
            max-width: 1200px !important;
        }
    </style>
    <div class="m-3 border border-dark bg-light p-3">
        <asp:Panel ID="pnlAlert" Visible="false" runat="server">
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <asp:Label ID="lblAlert" runat="server" />
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <div class="d-flex flex-nowrap pb-3">
            <h2>Bank Check Entry</h2>
        </div>
        <div class="d-flex flex-wrap flex-md-nowrap">
            <div class="d-flex flex-nowrap justify-self-stretch input-group m-1 ml-md-0 mr-md-1 my-md-1" style="max-width: 600px;">
                <label class="sr-only" for="inlineFormInputName">Payee Name</label>
                <div class="input-group-prepend">
                    <div class="input-group-text">Payee</div>
                </div>
                <asp:TextBox ID="txtCheckPayee" CssClass="form-control" required="required" placeholder="Full Name" runat="server" />
            </div>
            <div class="d-flex flex-nowrap input-group m-1" style="min-width: 200px !important; max-width: 240px !important" >
                <label class="sr-only" for="inlineFormInputName">Issue Date</label>
                <div class="input-group-prepend">
                    <div class="input-group-text">Date</div>
                </div>
                <asp:TextBox ID="txtCheckIssueDate" CssClass="form-control" TextMode="Date" required="required" runat="server" />
            </div>
            <div class="d-flex flex-nowrap input-group m-1 ml-md-1 mr-md-0 my-md-1" style="min-width: 200px !important; max-width: 200px !important">
                <label class="sr-only" for="inlineFormInputName">Check Amount</label>
                <div class="input-group-prepend">
                    <div class="input-group-text">Amount</div>
                </div>
                <asp:TextBox ID="txtCheckAmount" CssClass="form-control text-right" required="required" placeholder="0.00" runat="server" />
            </div>
        </div>
        <div class="d-flex flex-nowrap pt-3">
            <asp:Button ID="btnViewCheck" CssClass="btn btn-primary mr-1" Text="View Printed Check" OnClick="btnViewCheck_Click" runat="server" />
            <asp:Button ID="btnReset" CssClass="btn btn-secondary" Text="Reset" OnClick="btnReset_Click" runat="server" />
        </div>
    </div>
    <asp:Panel ID="pnlCheckResult" Visible="false" runat="server">
        <div class="m-3 border border-dark bg-info p-3">
            <div class="d-flex flex-nowrap pb-3">
                <h2>Bank Check Display</h2>
            </div>
            <div class="d-flex flex-no-wrap justify-content-end mb-3">
                <span class="mr-2">Date:</span><asp:Label ID="lblDateIssued" CssClass="border border-dark bg-light px-2 text-left" runat="server" />
            </div>
            <div class="d-flex justify-content-between mb-3">
                <div class="d-flex flex-nowrap w-100">
                    <span class="mr-2" style="width: 180px !important;">Pay to the Order of:</span><asp:Label ID="lblPayee" CssClass="mr-2 border border-dark px-2 bg-light text-left w-100" runat="server" />
                </div>
                <div class="d-flex flex-nowrap">
                    <span class="mr-2">Amount:</span><asp:Label ID="lblNumericAmount" CssClass="border border-dark px-2 bg-light text-right" runat="server" />
                </div>
            </div>
            <div class="d-flex flex-nowrap mb-3 w-100">
                <asp:Label ID="lblTextAmount" CssClass="mr-2 text-left w-100" runat="server" />
            </div>
        </div>
    </asp:Panel>

    <script>
        $(function () {
            $("form").on("submit", function () {
                return document.querySelector("form").checkValidity();
            });
        });
    </script>
</asp:Content>
