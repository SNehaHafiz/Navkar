/*=======================
 Created By:Himanshu Tripathi
 Created Date: 13 Sept 2019
 Description: Import Receipt Controller
 ========================*/
$(document).ready(function () {
    var ImportReceiptCtrl = [];

    //=======Begin====Initialize Control================
    var btnPrint = $("#btnPrint");
    var txtReceiptNo = $("#txtReceiptNo");
    var txtReceiptDate = $("#txtReceiptDate");
    var ddlCategoryMode = $("#ddlCategoryMode");
    var ddlSearchCriteria = $("#ddlSearchCriteria");
    var ddlCustomerName1 = $("#hdnCustomerName");
    var txtAssessmentNo = $("#txtAssessmentNo");
    var txtIGMNo = $("#txtIGMNo");
    var txtBLNo = $("#txtBLNo");
    var txtItemNo = $("#txtItemNo");
    var txtBOENo = $("#txtBOENo");
    var txtBondNo = $("#txtBondNo");
    var txtShippingBillNo = $("#txtShippingBillNo");
    var ddlCustomerName = $("#hdnCustomerName");
    var btnShowInvoice = $("#btnShowInvoice");
    var tblInvoiceList = $("#tblInvoiceList");
    var ddlPaymentFrom = $("#ddlPaymentFrom");
    var ddlMode = $("#ddlMode");
    var txtAssmentAmount = $("#txtAssmentAmount");
    var txtPendingPendingChequeRTGS = $("#txtPendingPendingChequeRTGS");
    var txtRemarks = $("#txtRemarks");
    var btnSave = $("#btnSave");
    var btnPrint = $("#btnPrint");
    var divAssessmentNo = $("#divAssessmentNo");
    var divIGMNo = $("#divIGMNo");
    var divItemNo = $("#divItemNo");
    var divBOENo = $("#divBOENo");
    var divBLNo = $("#divBLNo");
    var divShippingBillNo = $("#divShippingBillNo");
    var divCustomerName = $("#hdnCustomerName");
    var divShowButton = $("#divShowButton");
    var divBondNo = $("#divBondNo");
    var btnclearValue = $("#btnclearValue");
    var btnShowSummary = $("#btnShowSummary");
    var txtSummaryFromDate = $("#txtSummaryFromDate");
    var txtSummaryToDate = $("#txtSummaryToDate");
    var ddlReceiptType = $("#ddlReceiptType");
    var tblPendingInvoiceList = $("#tblPendingInvoiceList");
    var divPendingInvoice = $("#divPendingInvoice");
    var tblReceiptSummaryDet = $("#tblReceiptSummaryDet");
    var ddlTDSPerc = $("#ddlTDSPerc"); 

    //=======End====Initialize Control================

    //=======Begin====Initialize Control to Object================
    ImportReceiptCtrl.PrintButton = btnPrint;
    ImportReceiptCtrl.ReceiptNo = txtReceiptNo;
    ImportReceiptCtrl.ReceiptType = ddlReceiptType;
    ImportReceiptCtrl.ReceiptDate = txtReceiptDate;
    ImportReceiptCtrl.CategoryMode = ddlCategoryMode;
    ImportReceiptCtrl.SearchCriteria = ddlSearchCriteria;
    ImportReceiptCtrl.AssessmentNo = txtAssessmentNo;
    ImportReceiptCtrl.IGMNo = txtIGMNo;
    ImportReceiptCtrl.BLNo = txtBLNo;
    ImportReceiptCtrl.ItemNo = txtItemNo;
    ImportReceiptCtrl.BOENo = txtBOENo;
    ImportReceiptCtrl.BondNo = txtBondNo;
    ImportReceiptCtrl.ShippingBillNo = txtShippingBillNo;
    ImportReceiptCtrl.CustomerName = ddlCustomerName;
    ImportReceiptCtrl.ShowInvoiceButton = btnShowInvoice;
    ImportReceiptCtrl.InvoiceList = tblInvoiceList;
    ImportReceiptCtrl.PaymentFrom = ddlPaymentFrom;
    ImportReceiptCtrl.Mode = ddlMode;
    ImportReceiptCtrl.AssmentAmount = txtAssmentAmount;
    ImportReceiptCtrl.PendingPendingChequeRTGS = txtPendingPendingChequeRTGS;
    ImportReceiptCtrl.Remarks = txtRemarks;
    ImportReceiptCtrl.SaveButton = btnSave;
    ImportReceiptCtrl.DivAssessmentNo = divAssessmentNo;
    ImportReceiptCtrl.DivIGMNo = divIGMNo;
    ImportReceiptCtrl.DivItemNo = divItemNo;
    ImportReceiptCtrl.DivBOENo = divBOENo;
    ImportReceiptCtrl.DivBLNo = divBLNo;
    ImportReceiptCtrl.DivShippingBillNo = divShippingBillNo;
    ImportReceiptCtrl.DivCustomerName = divCustomerName;
    ImportReceiptCtrl.DivShowButton = divShowButton;
    ImportReceiptCtrl.DivBondNo = divBondNo;
    ImportReceiptCtrl.ClearValueButton = btnclearValue;
    ImportReceiptCtrl.CustomerNameFrom = ddlCustomerName1;
    ImportReceiptCtrl.ShowSummaryButton = btnShowSummary;
    ImportReceiptCtrl.SummaryFromDate = txtSummaryFromDate;
    ImportReceiptCtrl.SummaryToDate = txtSummaryToDate;
    ImportReceiptCtrl.TablePendingInvoiceList = tblPendingInvoiceList;
    ImportReceiptCtrl.DivPendingInvoice = divPendingInvoice;
    ImportReceiptCtrl.TableReceiptSummaryDet = tblReceiptSummaryDet;
    ImportReceiptCtrl.DropTDSPercentage = ddlTDSPerc;
    //=======End====Initialize Control to Object================


    //=============Call Function On DOM Ready Event===============
    ValidateSearchCret(ImportReceiptCtrl);
    AutomCom(ImportReceiptCtrl);
    GetPartyNameReceiptBind(ImportReceiptCtrl);
    $("#ddlTDSPerc").val(4);
    //===========End==Call Function On DOM Ready Event============

    //=============Call Validate Function on Select Cretria=======
    ImportReceiptCtrl.SearchCriteria.change(function () {

        ValidateSearchCret(ImportReceiptCtrl);
    });
    //=========End====Call Validate Function on Select Cretria====

    //===========Begin==Call Categry Change=======
    ImportReceiptCtrl.CategoryMode.change(function () {
        GetPartyNameReceiptBind(ImportReceiptCtrl);
    });
    //===========End==Call Categry Change=========

    //====Call On Print Show Invoice Button ================
    ImportReceiptCtrl.ShowInvoiceButton.click(function () {
        if (Validation(ImportReceiptCtrl) == false) {
            return;
        };
        $("#tracker-loader").fadeIn("slow");
        DisplayInvoiceList(ImportReceiptCtrl);
        $("#tracker-loader").fadeOut("slow");
    });
    //==End==Call On Print Show Invoice Button =============

    //===Initial Bind Details==============================
    InitializeTable(ImportReceiptCtrl);
    //===End Bind Details==============================

    //====Call On Save Receipt Button ================
    ImportReceiptCtrl.SaveButton.click(function () {

        Save(ImportReceiptCtrl);
    });
    //==End==Call On Save Receipt Button =============

    //====Call On Clear Button ================
    ImportReceiptCtrl.ClearValueButton.click(function () {

        ClearControl(ImportReceiptCtrl);
    });
    //====Call On Clear Button ================

    //====Show Import Receipt Print Details================
    ImportReceiptCtrl.PrintButton.click(function () {
        PrintReceipt1(ImportReceiptCtrl);
    });
    //====End Import Receipt Print Details================
    //addReceivedAmt();

    //=============Call Receipt Summary Button=======
    ImportReceiptCtrl.ShowSummaryButton.click(function () {

        GetReceiptGenSummary(ImportReceiptCtrl);
    });
    //==========End===Call Receipt Summary Button=======

    //=============Call On Payment Mode Change Drop Down=======
    ImportReceiptCtrl.Mode.change(function (e) {

        var id = $(this);
        var text = this.options[e.target.selectedIndex].text;
        var ddlValue = this.options[e.target.selectedIndex].value;
        ValidatePaymentMode(ImportReceiptCtrl, ddlValue);
    });
    //=============End Call On Payment Mode Change Drop Down====

    //=============Call On TDS Amount Changes Text Box=======
    //ImportReceiptCtrl.TDS.change(function () {
    //    debugger;
    //    CalculateAutoTDSAmount(ImportReceiptCtrl);
    //});
    //===========End==Call On TDS Amount Changes Text Box======

    $("#aDivShow").click(function () {
        GetDailyConnection(ImportReceiptCtrl);
    });

    //=============Call On Show Invoice Pending Div=======
    $("#aDivShowInvoice").click(function () {
        $("#tracker-loader").fadeIn("slow");
        $("#DivReceiptGenDet").hide();
        $("#divPendingInvoice").show();
        GetPendingInvoiceList(ImportReceiptCtrl);
    });
    //========End=====Call On Show Invoice Pending Div=====

    ImportReceiptCtrl.PaymentFrom.change(function () {
        AutoCustomer(ImportReceiptCtrl);
    });
});

function PrintReceipt1(ControlCtrl) {
    debugger;
    var data1 = { 'receiptNo': ControlCtrl.ReceiptNo.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportReceipt/CreditReceiptPrint',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            window.print();
            var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            myWindow.document.write(response);
            //myWindow.print();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
};

function SaveValidation(ControlCtrl) {

    if (ControlCtrl.ReceiptNo.val() > 0) {
        alert("Receipt already Generated.");
        return false;
    }

    var checkboxarray = [];
    $('input[type=checkbox]').each(function () {
        if (this.checked) {
            checkboxarray.push($(this).val());
        }
    });

    if (checkboxarray.length == 0) {
        alert('Please Select Invoice');
        return false;
    };

    return true;
}

function Validation(ControlCtrl) {
    var retval = true;

    if (ControlCtrl.CategoryMode.val() == "") {
        alert('Please Select Category.');
        ControlCtrl.CategoryMode.focus();
        retval = false;
        return retval;
    };

    if (ControlCtrl.SearchCriteria.val() == 'BillingParty') {
        if (ControlCtrl.CustomerName.val() == "") {
            alert('Please Select Billing Party.');
            ControlCtrl.CustomerName.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'IGMItemNo') {
        if (ControlCtrl.IGMNo.val() == "" || ControlCtrl.ItemNo.val() == "") {
            alert('Please Enter IGM/Item No.');
            ControlCtrl.IGMNo.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BLNo') {
        if (ControlCtrl.BLNo.val() == "") {
            alert('Please Enter BL No.');
            ControlCtrl.BLNo.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BondNo') {
        if (ControlCtrl.BondNo.val() == "") {
            alert('Please Enter Bond No.');
            ControlCtrl.BondNo.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'ShippingBillNo') {
        if (ControlCtrl.ShippingBillNo.val() == "") {
            alert('Please Enter Shipping Bill No.');
            ControlCtrl.ShippingBillNo.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'AssessmentNo') {
        if (ControlCtrl.AssessmentNo.val() == "") {
            alert('Please Enter Assessment No.');
            ControlCtrl.AssessmentNo.focus();
            retval = false;
            return retval;
        };
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BOENo') {
        if (ControlCtrl.BOENo.val() == "") {
            alert('Please Enter BOE No.');
            ControlCtrl.BOENo.focus();
            retval = false;
            return retval;
        };
    }
    return retval;
};

function ValidateSearchCret(ControlCtrl) {

    if (ControlCtrl.SearchCriteria.val() == 'BillingParty') {
        ControlCtrl.DivCustomerName.show();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BLNo.val('');
        ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.BondNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'IGMItemNo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.show();
        ControlCtrl.DivItemNo.show();

        ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BLNo.val('');
        ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.BondNo.val('');
        ControlCtrl.CustomerName.val('');
        //ControlCtrl.ItemNo.val('');
        //ControlCtrl.IGMNo.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BLNo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.show();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.BondNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
        ControlCtrl.CustomerName.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BondNo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.show();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
        ControlCtrl.CustomerName.val('');
        ControlCtrl.BLNo.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'ShippingBillNo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.show();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BOENo.val('');
        //ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
        ControlCtrl.CustomerName.val('');
        ControlCtrl.BLNo.val('');
        ControlCtrl.BondNo.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'AssessmentNo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.show();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.hide();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        //ControlCtrl.AssessmentNo.val('');
        ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
        ControlCtrl.CustomerName.val('');
        ControlCtrl.BLNo.val('');
        ControlCtrl.BondNo.val('');
    }
    else if (ControlCtrl.SearchCriteria.val() == 'BOENo') {
        ControlCtrl.DivCustomerName.hide();
        ControlCtrl.DivAssessmentNo.hide();
        ControlCtrl.DivBLNo.hide();
        ControlCtrl.DivBOENo.show();
        ControlCtrl.DivShippingBillNo.hide();
        ControlCtrl.DivBondNo.hide();
        ControlCtrl.DivIGMNo.hide();
        ControlCtrl.DivItemNo.hide();

        ControlCtrl.AssessmentNo.val('');
        //ControlCtrl.BOENo.val('');
        ControlCtrl.ShippingBillNo.val('');
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
        ControlCtrl.CustomerName.val('');
        ControlCtrl.BLNo.val('');
        ControlCtrl.BondNo.val('');
    }
};

function DisplayInvoiceList(ControlCtrl) {

    var TotalReceiptAmount = 0;
    var TotalTDSAmount = 0;

    var data1 = { 'InvoiceType': ControlCtrl.CategoryMode.val(), 'AgentId': ControlCtrl.CustomerName.val(), 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val(), 'BLNo': ControlCtrl.BLNo.val(), 'BOENo': ControlCtrl.BOENo.val(), 'BondNo': ControlCtrl.BondNo.val(), 'ShippingBillNo': ControlCtrl.ShippingBillNo.val(), 'AssessmentNo': ControlCtrl.AssessmentNo.val(), 'TDSType': ControlCtrl.DropTDSPercentage.val()};
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportReceipt/GetInvoiceList',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.length == 0 || data == '[]') {
                ControlCtrl.InvoiceList.empty();
                InitializeTable(ControlCtrl);
                alert('Data Not Found.');
                $("#tracker-loader").fadeOut("slow");
                return;
            }
            debugger;
            $('#tblInvoiceList').dataTable({
                "destroy": true,
                "bLengthChange": false,
                "bInfo": false,
                "bPaginate": false,
                "bFilter": true,
                "aaSorting": [],
                "order": [],
                "aoColumnDefs": [
                    {
                        'bSortable': false,
                        'aTargets': [0]
                    }

                ],

                "aaData": data,
                "columns": [
                    {

                        "data": "AssessNo",
                        "render": function (data, type, row, meta) {
                            data = '<input type=\"checkbox\" name=\"checklist[]\" class=\"checklist\" onClick=\" ChkAmtValidate(this) \ "id=\"checklist\" ' + row.selected + '   value="' + data + '">';
                            return data;
                        }
                    },
                    {
                        "data": "InvoiceNo",
                        "render": function (data, type, row, meta) {

                            input = '<input type=\"hidden\" name=\"AssessYear\" class=\" form-control AssessYear\" id=\"AssessYear\"  value="' + row.AssessYear + '">';
                            return data + input;
                        }
                    },
                    { "data": "InvoiceDate" },
                    { "data": "InvoiceAmount" },
                    { "data": "CreditAmount" },
                    { "data": "PrevReceiptAmount" },
                    { "data": "ReceivalAmount" },
                    {
                        "data": "ReceivedAmount",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"text\" name=\"ReceivedAmt\" class=\" form-control ReceivedAmt\" disabled=\"disabled\" onChange=\" ValidateRecAmt(this)  \" id=\"ReceivedAmt\"  value="' + data + '">';
                            //if (ControlCtrl.ReceiptType.val() == "GeneralReceipt") {
                            //    input = '<input type=\"text\" name=\"ReceivedAmt\" class=\" form-control ReceivedAmt\" disabled=\"disabled\" onChange=\" ValidateRecAmt(this)  \" id=\"ReceivedAmt\"  value="' + data + '">';
                            //} else {
                            //    input = '<input type=\"text\" name=\"ReceivedAmt\" class=\" form-control ReceivedAmt\" onChange=\" ValidateRecAmt(this)  \" id=\"ReceivedAmt\"  value="' + data + '">';
                            //}

                            return input;
                        }
                    },
                    {
                        "data": "TDS",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"text\" name=\"TDS\" class=\" form-control TDS\" id=\"TDS\"  value="' + data + '">';
                            return input;
                        }
                    },
                    { "data": "NetReceivedAmount" },
                    { "data": "OS" }
                ],
                "rowCallback": function (row, data, dataIndex) {

                }

            })
            $("#tracker-loader").fadeOut("slow");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
    $("#tracker-loader").fadeOut("slow");
    debugger;
   
};

function addPaymentList(ControlCtrl) {
    var TotalTDSAmount = ControlCtrl.TDS.val();
    //if (TotalTDSAmount > 0){
    var PaymentId = ControlCtrl.Mode.val();
    var PaymentType = $('#ddlMode option:selected').html(); // ControlCtrl.Mode.text();
    var ReceiptAmt = ControlCtrl.AssmentAmount.val();
    var ModeNumber = ControlCtrl.ModeNo.val();
    var BankId = ControlCtrl.BankName.val();
    var BankName = ($('#ddlBankName option:selected').html()).replace('--Select--', ''); // ControlCtrl.BankName.text();
    var ModeDate = ControlCtrl.ModeDate.val();

    if (Validation(ControlCtrl) == false) {
        return;
    };

    if (SaveValidation(ControlCtrl) == false) {
        return;
    };


    if (PaymentId == '') {
        alert('Please select Payment Mode.');
        return;
        ControlCtrl.Mode.focus();
    }

    if (ValidatePaymentType(ControlCtrl) == false) {
        return;
    };

    if (ValidateCheck() == false) {
        return;
    };

    if (ReceiptAmt == '' || ReceiptAmt <= 0) {
        alert('Receipt amount cannot be less then zero.');
        return;
        ControlCtrl.AssmentAmount.focus();
    }

    //===============if Data Already Exist In Payment Details Table=================
    debugger;
    var data = [];
    var table = document.getElementById("tblPaymentDet");
    if (table.rows.length > 1) {
        try {
            for (var i = 1; i < table.rows.length; i++) {

                row = table.rows[i];
                var BankId1 = "";
                var BankName1 = "";
                var ModeDate1 = "";
                var ModeNo1 = "";
                var PaymentId1 = row.cells[1].childNodes[1].value;
                var PaymentType1 = row.cells[1].innerHTML;
                var Amount1 = row.cells[2].innerHTML;
                try {
                    ModeNo1 = row.cells[3].innerHTML;
                } catch (ex) { }
                try {
                    BankId1 = row.cells[4].childNodes[1].value;
                } catch (ex) { }
                try {
                    BankName1 = row.cells[4].innerHTML;
                } catch (ex) { }
                try {
                    ModeDate1 = row.cells[5].innerHTML;
                } catch (ex) { }

                data.push({ 'PaymentId': PaymentId1, 'PaymentType': PaymentType1, 'Amount': Amount1, 'ModeNo': ModeNo1, 'BankId': BankId1, 'BankName': BankName1, 'ModeDate': ModeDate1 });
            }
        }
        catch (ex) { }
    }
    //=============End==if Data Already Exist In Payment Details Table==============

    //$('#tblPaymentDet tbody tr').filter(function () {
    //    //$(this).toggle()
    //    debugger;
    //    var i1 = $(this).text().toLocaleLowerCase().indexOf('Cash (+)');
    //    alert(i1);
    //});

    //$('#tblPaymentDet').columns().every(function () {
    //    debugger;
    //    var that = this;

    //    if (that.search() == "Cash (+)") {
    //        alert(i1);
    //    }
    //});


    var data1 = { 'PaymentId': PaymentId, 'PaymentType': PaymentType, 'Amount': ReceiptAmt, 'ModeNo': ModeNumber, 'BankId': BankId, 'BankName': BankName, 'ModeDate': ModeDate };
    data.push(data1);

    $('#tblPaymentDet').dataTable({
        "destroy": true,
        "bLengthChange": false,
        "bInfo": false,
        "bPaginate": false,
        "bFilter": true,
        "aaSorting": [],
        "order": [],
        "aoColumnDefs": [
            {
                'bSortable': false,
                'aTargets': [0]
            }

        ],

        "aaData": data,
        "columns": [
            {

                "data": "PaymentId",
                "render": function (data, type, row, meta) {

                    data = '<a onclick="RemoveMode(' + data + ')" href="#" class="btn btn-sm btn-danger removebutton" title="Remove"><i class="fa fa-trash-o"></i></a>';
                    return data;
                }
            },
            {
                "data": "PaymentType",
                "render": function (data, type, row, meta) {
                    input = '<input type=\"hidden\" name=\"PaymentType\" class=\" form-control PaymentType\" id=\"PaymentType\"  value="' + row.PaymentId + '">';
                    return data + input;
                }
            },
            { "data": "Amount" },
            { "data": "ModeNo" },
            {
                "data": "BankName",
                "render": function (data, type, row, meta) {
                    input = '<input type=\"hidden\" name=\"BankId\" class=\" form-control BankId\" id=\"BankId\"  value="' + row.BankId + '">';
                    return data + input;
                }
            },
            { "data": "ModeDate" }
        ],
        "rowCallback": function (row, data, dataIndex) {

        }

    });
    //};

    debugger;
    CalculatePayAmt();
    ClearPayAddControl(ControlCtrl);
};

function CalculatePayAmt() {
    debugger;
    var calTotalRec = 0;
    var TotReceiptAmount = $("#txtReceiptAmount").val();
    if (TotReceiptAmount <= 0) {
        alert('Receipt amount cannot be Blank.');
        return;
    };

    var table = document.getElementById("tblPaymentDet");
    for (var i = 1; i < table.rows.length; i++) {
        row = table.rows[i];
        calTotalRec = parseFloat(calTotalRec) + parseFloat(row.cells[2].innerHTML);//.childNodes[0].value;
    };
    calTotalRec = parseFloat(TotReceiptAmount) - parseFloat(calTotalRec);
    $("#txtBalanceAmount").val(calTotalRec);

}

function ValidatePaymentMode(ControlCtrl, value) {
    if (value == '1' || value == '7') {
        ControlCtrl.ModeNo.attr('disabled', 'disabled');
        ControlCtrl.BankName.attr('disabled', 'disabled');
        ControlCtrl.ModeDate.attr('disabled', 'disabled');
    }
    else {
        ControlCtrl.ModeNo.removeAttr('disabled');
        ControlCtrl.BankName.removeAttr('disabled');
        ControlCtrl.ModeDate.removeAttr('disabled');
    }
    if (ControlCtrl.BalanceAmount.val() != undefined || ControlCtrl.BalanceAmount.val() != "") {
        ControlCtrl.AssmentAmount.val(ControlCtrl.BalanceAmount.val());
    }
}

function ValidatePaymentType(ControlCtrl) {
    if (ControlCtrl.Mode.val() != '1' && ControlCtrl.Mode.val() != '7' && ControlCtrl.Mode.val() != '8' && ControlCtrl.Mode.val() != '10') {
        if (ControlCtrl.BankName.val() == "" || ControlCtrl.ModeNo.val() == "" || ControlCtrl.ModeDate.val() == "") {
            alert("Please enter Cheque/DD/RTGS/NEFT Details.");
            return false;
        }
    }
    return true;
}

function ValidateCheck() {
    debugger;
    var result = true;
    var PaymentMode = $("#ddlMode");
    var txtModeNo = $("#txtModeNo").val();

    if (PaymentMode.val() == "2") {
        if (txtModeNo.length > 6) {
            alert("Cheque No not allowed more then 6 digit.");
            result = false;
            return result;
        }
    }

    if (PaymentMode.val() == "5" || PaymentMode.val() == "6") {
        if (txtModeNo.length < 8) {
            alert("RTGS/NEFT minimum length should be 8 digit.");
            result = false;
            return result;
        }
    }

    return result;
};

function addTDSAmt() {
    debugger;
    var TotalTDSAmount = 0;
    //Loop through all checked CheckBoxes in DataTable.
    var SelectedData = new Array();
    $('#tblInvoiceList tbody tr').each(function (i, row) {
        var data1 = {};
        if ($(this).find('input.check-list').is(':checked')) {
            $(this).find('input').each(function () {
                if (this.id == "TDSAmount") {
                    TotalTDSAmount = parseFloat(TotalTDSAmount) + parseFloat($(this).val());
                }
            })
        }
    });

    $("#txtTDSAmount").val(TotalTDSAmount);
};

function objectifyForm(formArray) {//serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

function Save(ControlCtrl) {
    debugger;
    if (Validation(ControlCtrl) == false) {
        return false;
    }

    if (SaveValidation(ControlCtrl) == false) {
        return false;
    }

    var itemcount = checkItemcount();
    if (itemcount == false) {
        return false;
    }

    var checkboxarray = [];
    var tablearray = [];
    var table = document.getElementById("tblInvoiceList");
    try {

        //=========================Invoice Details List for Save Data Into DataBase================
        $('input[type=checkbox]').each(function () {
            if (this.checked) {
                checkboxarray.push($(this).val());
            }
        });

        for (var i = 1; i < table.rows.length; i++) {

            row = table.rows[i];

            for (var k = 0; k < checkboxarray.length; k++) {
                var invoiceno = row.cells[0].childNodes[0].value;
                //debugger;
                if (invoiceno == checkboxarray[k]) {
                    var AssessNo = row.cells[0].childNodes[0].value;
                    var AssessYear = row.cells[1].childNodes[1].value;
                    var InvoiceDate = row.cells[2].innerHTML;
                    var InvoiceAmount = row.cells[3].innerHTML;
                    var CreditAmount = row.cells[4].innerHTML;
                    var PrevRecAmount = row.cells[5].innerHTML;
                    var ReceivalAmount = row.cells[6].innerHTML;
                    var ReceivedAmount = row.cells[7].childNodes[0].value;
                    var TDSAmount = row.cells[8].childNodes[0].value;
                    var NetRecAmount = row.cells[9].innerHTML;
                    var OSAmount = row.cells[10].innerHTML;

                    tablearray.push({ 'InvoiceNo': invoiceno, 'AssessNo': AssessNo, 'AssessYear': AssessYear, 'InvoiceDate': InvoiceDate, 'InvoiceAmount': InvoiceAmount, 'CreditAmount': CreditAmount, 'PrevReceiptAmount': PrevRecAmount, 'ReceivalAmount': ReceivalAmount, 'ReceivedAmount': ReceivedAmount, 'TDS': TDSAmount, 'NetReceivedAmount': NetRecAmount, 'OS': OSAmount })
                }
            }
        };
        //============End=============Invoice Details List for Save Data Into DataBase=============

        debugger;
        var cnf = confirm('Are you sure you want to save!');
        if (cnf == true) {
            $.ajax({
                type: 'POST',
                url: '/ImportReceipt/SaveCreditReceiptForm',
                data: '{invoiceData: ' + JSON.stringify(tablearray) + ',Category: ' + JSON.stringify(ControlCtrl.CategoryMode.val()) + ',ReceiptType: ' + JSON.stringify(ControlCtrl.ReceiptType.val()) + ',Remarks: ' + JSON.stringify(ControlCtrl.Remarks.val()) + ',PendingPendingChequeRTGS: ' + JSON.stringify(ControlCtrl.PendingPendingChequeRTGS.val())  + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                    debugger;
                    if (data.indexOf("Successfully") != -1) {
                        var d = new Date();
                        ControlCtrl.ReceiptNo.val(data.split('~')[1]);
                        ControlCtrl.ReceiptDate.val((d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes()));
                        alert(data.split('~')[0]);
                        $("#btnSave").attr('disabled', true);
                        //ClearControl(ControlCtrl);
                    }
                    else {
                        alert(data);
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        };
    }
    catch (err) {
        alert(err.Message);
    }

};

function checkItemcount() {

    var table1 = $('#tblInvoiceList').DataTable();

    if (table1.rows().count() == 0) {
        alert("Please add Invoice details!")
        return false;
    }

    return true;

}
function InitializeTable(ControlCtrl) {

    $("#tblInvoiceList").dataTable({
        "destroy": true,
        "bLengthChange": false,
        "bInfo": false,
        "bPaginate": false,
        "bFilter": true,
        "aaSorting": [],
        "order": [],
        "aoColumnDefs": [
            {
                'bSortable': false,
                'aTargets': [0]
            }

        ]


    })
};

function InitalizePaymentTBL() {
    $("#tblInvoiceList").dataTable({
        "destroy": true,
        "bLengthChange": false,
        "bInfo": false,
        "bPaginate": false,
        "bFilter": true,
        "aaSorting": [],
        "order": [],
        "aoColumnDefs": [
            {
                'bSortable': false,
                'aTargets': [0]
            }

        ]


    });
}

function GetReceiptGenSummary(ControlCtrl) {
    var FromDate = ControlCtrl.SummaryFromDate.val();
    var ToDate = ControlCtrl.SummaryToDate.val();
    debugger;
    $.ajax({
        url: '/ImportReceipt/getCreditReceiptSummaryDet',
        type: 'Post',
        data: '{FromDate: ' + JSON.stringify(FromDate) + ', ToDate: ' + JSON.stringify(ToDate) + '}',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data == "0" || data == "[]") {
                ControlCtrl.TableReceiptSummaryDet.empty();
                alert('No Receipt found.');
                return;
            }
            RepTableDataJson($(ControlCtrl.TableReceiptSummaryDet), data, "Receipt Summary", "ReceiptSummaryDet");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

function GetPendingInvoiceList(ControlCtrl) {

    var FromDate = ControlCtrl.SummaryFromDate.val();
    var ToDate = ControlCtrl.SummaryToDate.val();

    $.ajax({
        url: '/ImportReceipt/getPenginInvoiceList',
        type: 'Post',
        data: '',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            RepTableDataJson($(ControlCtrl.TablePendingInvoiceList), data, "Pending Invoice List", "PendingInvoice");
            $("#tracker-loader").fadeOut("slow");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

function GetDailyConnection(ControlCtrl) {

    $.ajax({
        url: '/ImportReceipt/DailyCollection',
        type: 'Post',
        data: '',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var parseJSONResult = jQuery.parseJSON(data);
            //fetch all records from JSON result and make row data set.
            var rowDataSet = [];
            var i = 0;
            $.each(parseJSONResult, function (key, value) {
                var rowData = [];
                var j = 0;
                $.each(parseJSONResult[i], function (key, value) {
                    rowData[j] = value;
                    j++;
                });
                rowDataSet[i] = rowData;

                i++;
            });
            $("#lblCash").text(rowDataSet[0][1]);
            $("#lblCheque").text(rowDataSet[1][1]);
            $("#lblDD").text(rowDataSet[2][1]);
            $("#lblPO").text(rowDataSet[3][1]);
            $("#lblNEFT").text(rowDataSet[4][1]);
            $("#lblTDS").text(rowDataSet[5][1]);
            $("#lblTotal").text(rowDataSet[6][1]);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};



function ValidateRecAmt(id) {

    var Currentindex = id.closest('tr').rowIndex;
    var TotalRecptAmount = 0;
    var TotalTDS = 0;


    var checkboxarray = [];
    $('input[type=checkbox]').each(function (index, value) {
        if (Currentindex == (index + 1)) {
            if (this.checked) {
                checkboxarray.push($(this).val());
                var table = document.getElementById("tblInvoiceList");
                var tablearray = [];
                for (var i = 1; i < table.rows.length; i++) {

                    if (i == Currentindex) {
                        row = table.rows[i];
                        //var invoiceno = row.cells[0].childNodes[0].value;
                        var AssessNo = row.cells[0].childNodes[0].value;
                        var AssessYear = row.cells[1].childNodes[1].value;
                        //var AssessNo = row.cells[1].innerHTML;
                        var InvoiceAmount = row.cells[3].innerHTML;
                        var CreditAmount = row.cells[4].innerHTML;
                        var PrevRecAmount = row.cells[5].innerHTML;
                        var ReceivalAmount = row.cells[6].innerHTML;
                        var ReceivedAmount = row.cells[7].childNodes[0].value;
                        var TDSAmount = row.cells[8].childNodes[0].value;
                        var NetRecAmount = row.cells[9].innerHTML;
                        var OSAmount = row.cells[10].innerHTML;

                        if (PrevRecAmount == "") {
                            PrevRecAmount = 0;
                        }
                        if (ReceivalAmount == "") {
                            ReceivalAmount = 0;
                        }

                        if (NetRecAmount == "") {
                            NetRecAmount = 0;
                        }

                        if (OSAmount == "") {
                            OSAmount = 0;
                        }

                        if (parseFloat(ReceivedAmount) > parseFloat(ReceivalAmount)) {
                            alert('Received Amount cannot be greater then Balance Amount. ');
                            row.cells[7].childNodes[0].value = "0";
                            row.cells[7].childNodes[0].focus();
                            return false;
                        };

                        row.cells[9].innerHTML = parseFloat(PrevRecAmount) + parseFloat(ReceivedAmount);
                        row.cells[10].innerHTML = parseFloat(ReceivalAmount) - parseFloat(ReceivedAmount);
                        TotalRecptAmount = parseFloat(TotalRecptAmount + ReceivedAmount);
                    }
                }
            }
            else {
                id.value = "0";
                id.focus();
                alert('Please select invoice before proceed.');
                return false;
            }
        }
    });

    var totamt = 0;
    var txtReceiptAmount = $("#txtReceiptAmount");
    if (txtReceiptAmount.val() == "") {
        txtReceiptAmount.val(0);
    }
    else {
        totamt = parseFloat(txtReceiptAmount.val());
    }

    totamt = parseFloat(totamt) + parseFloat(TotalRecptAmount);
    txtReceiptAmount.val(parseFloat(totamt));

}

function ValidateTDSAmt(id) {

    var Currentindex = id.closest('tr').rowIndex;
    var TotalRecptAmount = 0;
    var TotalTDS = 0;


    var checkboxarray = [];
    $('input[type=checkbox]').each(function (index, value) {
        if (Currentindex == (index + 1)) {
            if (this.checked) {
                checkboxarray.push($(this).val());
                var table = document.getElementById("tblInvoiceList");
                var tablearray = [];
                for (var i = 1; i < table.rows.length; i++) {

                    if (i == Currentindex) {
                        row = table.rows[i];
                        //var invoiceno = row.cells[0].childNodes[0].value;
                        var AssessNo = row.cells[0].childNodes[0].value;
                        var AssessYear = row.cells[1].childNodes[1].value;
                        //var AssessNo = row.cells[1].innerHTML;
                        var InvoiceAmount = row.cells[3].innerHTML;
                        var CreditAmount = row.cells[4].innerHTML;
                        var PrevRecAmount = row.cells[5].innerHTML;
                        var ReceivalAmount = row.cells[6].innerHTML;
                        var ReceivedAmount = row.cells[7].childNodes[0].value;
                        var TDSAmount = row.cells[8].childNodes[0].value;
                        var NetRecAmount = row.cells[9].innerHTML;
                        var OSAmount = row.cells[10].innerHTML;

                        if (PrevRecAmount == "") {
                            PrevRecAmount = 0;
                        }
                        if (ReceivalAmount == "") {
                            ReceivalAmount = 0;
                        }

                        if (NetRecAmount == "") {
                            NetRecAmount = 0;
                        }

                        if (OSAmount == "") {
                            OSAmount = 0;
                        }

                        if (parseFloat(ReceivedAmount) > parseFloat(ReceivalAmount)) {
                            alert('Received Amount cannot be greater then Balance Amount. ');
                            row.cells[8].childNodes[0].value = "0";
                            row.cells[8].childNodes[0].focus();
                            return false;
                        };

                        row.cells[9].innerHTML = parseFloat(PrevRecAmount) + parseFloat(ReceivedAmount);
                        row.cells[10].innerHTML = parseFloat(ReceivalAmount) - parseFloat(ReceivedAmount);
                        TotalTDS = parseFloat(TotalTDS + TDSAmount);
                    }
                }
            }
            else {
                id.value = "0";
                id.focus();
                alert('Please select invoice before proceed.');
                return false;
            }
        }
    });

    var totamtTDS = 0;
    var txtTDSAmount = $("#txtTDSAmount");

    if (txtTDSAmount.val() == "") {
        txtTDSAmount.val(0);
    }
    else {
        totamtTDS = parseFloat(txtTDSAmount.val());
    }

    totamtTDS = parseFloat(totamtTDS) + parseFloat(TotalTDS);
    txtTDSAmount.val(parseFloat(totamtTDS));
}

function ClearPayAddControl(ControlCtrl) {
    ControlCtrl.Mode.val('');
    ControlCtrl.ModeNo.val('');
    ControlCtrl.AssmentAmount.val('');
    ControlCtrl.BankName.val('');
    ControlCtrl.ModeDate.val('');
};

function CalculateAutoTDSAmount() {
    debugger;
    var TDSAmount = $("#txtTDSAmount").val();
    var data = [];
    if (TDSAmount > 0) {
        var data1 = { 'PaymentId': 7, 'PaymentType': 'TDS', 'Amount': TDSAmount, 'ModeNo': '', 'BankId': '', 'BankName': '', 'ModeDate': '' };
        data.push(data1);

        $('#tblPaymentDet').empty();

        $('#tblPaymentDet').dataTable({
            "destroy": true,
            "bLengthChange": false,
            "bInfo": false,
            "bPaginate": false,
            "bFilter": true,
            "aaSorting": [],
            "order": [],
            "aoColumnDefs": [
                {
                    'bSortable': false,
                    'aTargets': [0]
                }

            ],

            "aaData": data,
            "columns": [
                {

                    "data": "PaymentId",
                    "render": function (data, type, row, meta) {

                        data = '<a onclick="RemoveMode(' + data + ')" href="#" class="btn btn-sm btn-danger removebutton" title="Remove"><i class="fa fa-trash-o"></i></a>';
                        return data;
                    }
                },
                {
                    "data": "PaymentType",
                    "render": function (data, type, row, meta) {
                        input = '<input type=\"hidden\" name=\"PaymentType\" class=\" form-control PaymentType\" id=\"PaymentType\"  value="' + row.PaymentId + '">';
                        return data + input;
                    }
                },
                { "data": "Amount" },
                { "data": "ModeNo" },
                {
                    "data": "BankName",
                    "render": function (data, type, row, meta) {
                        input = '<input type=\"hidden\" name=\"BankId\" class=\" form-control BankId\" id=\"BankId\"  value="' + row.BankId + '">';
                        return data + input;
                    }
                },
                { "data": "ModeDate" }
            ],
            "rowCallback": function (row, data, dataIndex) {

            }

        });
        CalculatePayAmt();
    }

};

function ChkAmtValidate(id) {
    debugger;
    var checkboxarray = [];
    var totalReceivedAmount = 0;
    var totalTDSAmount = 0;

    if ($("#txtReceiptAmount").val() == "" || $("#txtReceiptAmount").val() == undefined) {
        totalReceivedAmount = 0;
    } else {
        totalReceivedAmount = $("#txtReceiptAmount").val();
    }

    if ($("#txtTDSAmount").val() == "" || $("#txtTDSAmount").val() == undefined) {
        totalTDSAmount = 0;
    } else {
        totalTDSAmount = $("#txtTDSAmount").val();
    }


    var Currentindex = id.closest('tr').rowIndex;

    $('input[type=checkbox]').each(function (index, value) {
        if (Currentindex == (index + 1)) {
            if (this.checked) {
                checkboxarray.push($(this).val());
                var table = document.getElementById("tblInvoiceList");
                var tablearray = [];
                for (var i = 1; i < table.rows.length; i++) {
                    if (i == Currentindex) {
                        row = table.rows[i];
                        //var invoiceno = row.cells[0].childNodes[0].value;
                        var PrevRecAmount = row.cells[5].innerHTML;
                        var ReceivalAmount = row.cells[6].innerHTML;
                        var ReceivedAmount = row.cells[7].childNodes[0].value;
                        var TDSAmount = row.cells[8].childNodes[0].value;
                        var NetRecAmount = row.cells[9].innerHTML;
                        var OSAmount = row.cells[10].innerHTML;

                        if (PrevRecAmount == "") {
                            PrevRecAmount = 0;
                        }
                        if (ReceivalAmount == "") {
                            ReceivalAmount = 0;
                        }

                        if (NetRecAmount == "") {
                            NetRecAmount = 0;
                        }

                        if (OSAmount == "") {
                            OSAmount = 0;
                        }
                        if (TDSAmount == "") {
                            TDSAmount = 0;
                        }
                        debugger;
                        row.cells[9].innerHTML = parseFloat(PrevRecAmount) + parseFloat(ReceivedAmount);
                        row.cells[10].innerHTML = parseFloat(ReceivalAmount) - parseFloat(ReceivedAmount);
                        totalTDSAmount = parseFloat(totalTDSAmount) + parseFloat(TDSAmount);
                        totalReceivedAmount = parseFloat(totalReceivedAmount) + (parseFloat(ReceivedAmount));
                        //totalReceivedAmount = parseFloat(totalReceivedAmount) + (parseFloat(ReceivedAmount) - parseFloat(TDSAmount));
                    }
                }
            }
            else {
                var table = document.getElementById("tblInvoiceList");
                var tablearray = [];
                for (var i = 1; i < table.rows.length; i++) {
                    if (i == Currentindex) {
                        row = table.rows[i];
                        //var invoiceno = row.cells[0].childNodes[0].value;
                        var PrevRecAmount = row.cells[5].innerHTML;
                        var ReceivalAmount = row.cells[6].innerHTML;
                        var ReceivedAmount = row.cells[7].childNodes[0].value;
                        var TDSAmount = row.cells[8].childNodes[0].value;
                        var NetRecAmount = row.cells[9].innerHTML;
                        var OSAmount = row.cells[10].innerHTML;

                        if (PrevRecAmount == "") {
                            PrevRecAmount = 0;
                        }
                        if (ReceivalAmount == "") {
                            ReceivalAmount = 0;
                        }

                        if (NetRecAmount == "") {
                            NetRecAmount = 0;
                        }

                        if (OSAmount == "") {
                            OSAmount = 0;
                        }

                        if (TDSAmount == "") {
                            TDSAmount = 0;
                        }
                        debugger;
                        row.cells[9].innerHTML = 0;// parseFloat(PrevRecAmount) + parseFloat(ReceivedAmount);
                        row.cells[10].innerHTML = 0;// parseFloat(ReceivalAmount) - parseFloat(ReceivedAmount);
                        totalTDSAmount = parseFloat(totalTDSAmount) - parseFloat(TDSAmount);
                        totalReceivedAmount = parseFloat(totalReceivedAmount) - (parseFloat(ReceivedAmount));
                        //totalReceivedAmount = parseFloat(totalReceivedAmount) - (parseFloat(ReceivedAmount) - parseFloat(TDSAmount));
                    }
                }
            }
        }
    });

    var checkboxarray = [];
    $('input[type=checkbox]').each(function () {
        if (this.checked) {
            checkboxarray.push($(this).val());
        }
    });

    debugger;
    if (totalReceivedAmount > 0) {
        //$("#txtReceiptAmount").val('');
        $("#txtReceiptAmount").val(parseFloat(totalReceivedAmount));
    }

    if (totalTDSAmount > 0) {
        //$("#txtTDSAmount").val('');
        $("#txtTDSAmount").val(parseFloat(totalTDSAmount));
    }

    if (checkboxarray.length == 0) {
        $("#txtReceiptAmount").val(parseFloat(0));
        $("#txtTDSAmount").val(parseFloat(0));
    }
    CalculateAutoTDSAmount();
};

//======================BEGIN=============Cancel Receipt Details======================================
function CancelRec(RecNo) {
    debugger;
    var txtSummaryFromDate = $("#txtSummaryFromDate").val();
    var txtSummaryToDate = $("#txtSummaryToDate").val();

    var r = confirm("Are you sure want to cancel this Receipt?");
    if (r == true) {

        //window.location = '@Url.Action("ImportReceipt", "CancelReceipt")?ReceiptNo=' + RecNo;

        var data = JSON.stringify({
            'ReceiptNo': RecNo,
            'FromDate': txtSummaryFromDate,
            'ToDate': txtSummaryToDate
        });

        $.ajax({
            type: "POST",
            url: "/ImportReceipt/CancelReceipt",
            data: data,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                debugger;
                $('#myModalContent').html(response);
                $('#myModal').modal('show');
            },
            error: function (errormessage) {
                //  alert("error");
                alert(errormessage.responseText);
            }
        });

    }

};
//======================END=============Cancel Receipt Details======================================

//======================BEGIN=============Print Receipt======================================
function PrintReceipt(id) {
    debugger;
    var data1 = { 'receiptNo': id };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportReceipt/CreditReceiptPrint',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            debugger;
            var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            myWindow.document.write(response);
            //myWindow.print();
            //window.print();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
};
//======================END=============Print Receipt======================================

function AutomCom(ControlCtrl) {

    $("#ddlCustomerName1").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/ImportReceipt/getAutoCustomerList",
                data: "{ 'prefixText': '" + request.term + "','CustomerType': '" + ControlCtrl.PaymentFrom.val() + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //debugger;
                    response($.map(data, function (item) {
                        //debugger;
                        return {
                            label: item.AGName,
                            val: item.AGID
                        };
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            })
        },
        select: function (e, i) {
            //debugger;
            $("#txtAutoCust").val(i.item.label);
            $("#hdnPayFromId").val(i.item.val);
        },
        minLength: 1
    });

};

function GetPartyNameReceiptBind(ControlCtrl) {
    debugger;
    var data1 = { 'Mode': ControlCtrl.CategoryMode.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportReceipt/getPartyNameReceipt',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            debugger;
            $("#ddlCustomerName").empty();
            $("#ddlCustomerName").append($("<option></option>").val('').html('--Select--'));
            $.each(response, function (index, value) {
                debugger;
                $("#ddlCustomerName").append($("<option></option>").val(value.AGID).html(value.AGName));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
};

function ClearControl(ControlCtrl) {
    debugger;
    ControlCtrl.ReceiptNo.val('');
    ControlCtrl.ReceiptType.val('Credit');
    ControlCtrl.ReceiptType.removeAttr('disabled');
    ControlCtrl.ReceiptDate.val('');
    ControlCtrl.CategoryMode.val('ImportInvoice');
    ControlCtrl.SearchCriteria.val('BillingParty');
    ControlCtrl.CustomerName.val('');
    ControlCtrl.InvoiceList.empty();
    ControlCtrl.IGMNo.val('');
    ControlCtrl.ItemNo.val('');
    ControlCtrl.BLNo.val('');
    ControlCtrl.BOENo.val('');
    ControlCtrl.BondNo.val('');
    ControlCtrl.ShippingBillNo.val('');
    ControlCtrl.AssessmentNo.val('');
    ControlCtrl.ReceiptAmount.val(0);
    ControlCtrl.Remarks.val('');
    InitializeTable(ControlCtrl);
    ControlCtrl.SaveButton.removeAttr('disabled');
};
//--========Auto Complete Function=======================
//$(document).ready(function () {
//    //debugger;
//    $("#txtAutoCust").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/ImportReceipt/getAutoCustomerList",
//                type: "POST",
//                dataType: "json",
//                data: { Prefix: request.term },
//                success: function (data) {
//                    //debugger;
//                    response($.map(data, function (item) {
//                        //debugger;
//                        return {
//                            label: item.AGName,
//                            val: item.AGID
//                        };
//                    }))
//                },
//                error: function (response) {
//                    alert(response.responseText);
//                },
//                failure: function (response) {
//                    alert(response.responseText);
//                }
//            })
//        },
//        select: function (e, i) {
//            //debugger;
//            $("#txtAutoCust").val(i.item.label);
//            alert(i.item.val);
//        },
//        minLength: 1
//    });
//});

//@Html.TextBox("txtAutoCust", null, new { @class = "form-control ", @id = "txtAutoCust", autocomplete = "off" })
//=--=========//--====End====Auto Complete Function=======================