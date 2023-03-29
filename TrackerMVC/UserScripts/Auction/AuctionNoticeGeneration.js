/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 26 Sept 2019
 Description: Auction Notice Generation Controller
 ========================*/

$(document).ready(function () {
    debugger;
   
    var AuctionNoticeGenerationCtrl = [];
     //=======Begin====Initialize Control to Object================
    AuctionNoticeGenerationCtrl.AuctionId = $("#txtAuctionId");
    AuctionNoticeGenerationCtrl.IGMNo = $("#txtIGMNo");
    AuctionNoticeGenerationCtrl.JoNo = $("#txtJoNo"); 
    AuctionNoticeGenerationCtrl.ItemNo = $("#txtItemNo");
    AuctionNoticeGenerationCtrl.IGMDate = $("#txtIGMDate");
    AuctionNoticeGenerationCtrl.Date = $("#txtDate");
    AuctionNoticeGenerationCtrl.NoticeId = $("#txtNoticeId");
    AuctionNoticeGenerationCtrl.NoticeDate = $("#txtNoticeDate");
    AuctionNoticeGenerationCtrl.NoticeType = $("#txtNoticeType");
    AuctionNoticeGenerationCtrl.Agent = $("#txtAgent");
    AuctionNoticeGenerationCtrl.CargoDesc = $("#txtCargoDesc");
    AuctionNoticeGenerationCtrl.ImporterName = $("#txtImporterName");
    AuctionNoticeGenerationCtrl.NotifyParty = $("#txtNotifyParty");
    AuctionNoticeGenerationCtrl.NoOfPkgs = $("#txtNoOfPkgs");
    AuctionNoticeGenerationCtrl.PkgsType = $("#txtPkgsType");
    AuctionNoticeGenerationCtrl.Weight = $("#txtWeight");
    AuctionNoticeGenerationCtrl.UOM = $("#txtUOM");
    AuctionNoticeGenerationCtrl.BLNo = $("#txtBLNo");
    AuctionNoticeGenerationCtrl.BLDate = $("#txtBLDate");
    AuctionNoticeGenerationCtrl.TableAuctionNoticeGenList = $("#tblAuctionNoticeGenList");
    AuctionNoticeGenerationCtrl.ButtonGenerateFirstNotice = $("#btnGenerateFirstNotice");
    AuctionNoticeGenerationCtrl.ButtonGenerateSecondNotice = $("#btnGenerateSecondNotice");
    AuctionNoticeGenerationCtrl.ButtonclearValue = $("#btnclearValue");
    AuctionNoticeGenerationCtrl.ButtonShow = $("#btnShow");
    AuctionNoticeGenerationCtrl.ChkSelectAll = $("#chkSelectAllC");
    AuctionNoticeGenerationCtrl.AuctionId = $("#txtAuctionId");
    AuctionNoticeGenerationCtrl.AuctionId2 = $("#txtAuctionId2");
    AuctionNoticeGenerationCtrl.PkgsType = $("#txtPkgsType");
    //AuctionNoticeGenerationCtrl.ButtonPrintFirstNotice = $("#btnPrintFirstNotice");
    //AuctionNoticeGenerationCtrl.ButtonPrintSecondNotice = $("#btnPrintSecondNotice");
    AuctionNoticeGenerationCtrl.ButtonShowSummary = $("#btnShowSummary");
    AuctionNoticeGenerationCtrl.DDLNoticeType = $("#ddlNoticeType");
    AuctionNoticeGenerationCtrl.ButtonAutoGenerate = $("#btnAutoGenerate");
    AuctionNoticeGenerationCtrl.AutoNoticeType = $("#ddlAutoNoticeType");
    AuctionNoticeGenerationCtrl.ReportDate = $("#txtRepDate");
     //=======End====Initialize Control to Object================

    InitalizeTBL();
    //===========Get Query String Data=============================
    var QIGMNo = $("#hdnIgmNo").val();
    var QItemNo = $("#hdnItemNo").val();
    if (QIGMNo !== "") {
        AuctionNoticeGenerationCtrl.IGMNo.val(QIGMNo);
        AuctionNoticeGenerationCtrl.ItemNo.val(QItemNo);
        //AuctionNoticeGenerationCtrl.ButtonShow.focus();
        BindAuctionNoticeDet(AuctionNoticeGenerationCtrl);
    }
    //===========Get Query String Data=============================

    //=============Beign Call Auction Notice Generation On Show Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonShow.click(function () {
        BindAuctionNoticeDet(AuctionNoticeGenerationCtrl);
    });
    //=============End Call Auction Notice Generation On Show Button Click Event===============

    //=============Beign Call Auction Auto Generate On Show Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonAutoGenerate.click(function () {
        autoGenerateNotice(AuctionNoticeGenerationCtrl);
    });
    //=============Beign Call Auction Auto Generate On Show Button Click Event===============

    //=============Beign Call Auction Summary List  Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonShowSummary.click(function () {
        getAuctionUpdatedList(AuctionNoticeGenerationCtrl);
    });
    //=============End Call Auction Summary List Button Click Event===============

    //=============Beign Call Clear Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonclearValue.click(function () {
        ClearData(AuctionNoticeGenerationCtrl);
    });
    //=============End Call Clear Button Click Event===============

    //=============Beign Call Genrate First Notice Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonGenerateFirstNotice.click(function () {
        Save(AuctionNoticeGenerationCtrl,'First');
    });
    //=============End Call Genrate First Notice Button Click Event===============

    //=============Beign Call Genrate Second Notice Button Click Event===============
    AuctionNoticeGenerationCtrl.ButtonGenerateSecondNotice.click(function () {
        Save(AuctionNoticeGenerationCtrl,'Second');
    });
    //=============End Call Genrate Second Notice Button Click Event===============

    //==Begin==Select All Item From Table==============
    AuctionNoticeGenerationCtrl.ChkSelectAll.click(function () {
        debugger;
        var chkAll = this;
        var chkRows = AuctionNoticeGenerationCtrl.TableAuctionNoticeGenList.find("#checklist");
        chkRows.each(function () {
            debugger;
            $(this)[0].checked = chkAll.checked;
        });
    });
    //==End==Select All Item From Table==============

    //==Begin==On Change Notice Type==============
    AuctionNoticeGenerationCtrl.DDLNoticeType.change(function () {
        NoticeTypeChange(AuctionNoticeGenerationCtrl);
    });
   //==End==On Change Notice Type==============
});

function Save(ControlCtrl,NoticeType) {
    debugger;
    //=========Begin===Call Validation On Show Button============
    if (ValidationOnShowButton(ControlCtrl) == false) { return false; }
    //=========End===Call Validation On Show Button==============
    //if (ControlCtrl.AuctionId.val()>0) {
    //    alert('Auction Notice Generated Successfully.');
    //    return false;
    //}

    if (ControlCtrl.Date.val() == "") {
        alert("Auction Notice Date cannot be Blank.");
        return false;
    }

    var checkboxarray = [];
    var tablearray = [];
    var table = document.getElementById("tblAuctionNoticeGenList");

    //==================Begin=======Container Details List for Save Data Into DataBase==============
    $('input[type=checkbox]').each(function () {
        if (this.checked) {
            checkboxarray.push($(this).val());
        }
    });

    if (checkboxarray.length <= 0) {
        alert('Please select at list one container from list.');
        return false;
    };

    for (var i = 1; i < table.rows.length; i++) {
        row = table.rows[i];
        for (var k = 0; k < checkboxarray.length; k++) {
            var invoiceno = row.cells[0].childNodes[0].value;
            //debugger;
            if (invoiceno == checkboxarray[k]) {
                var ContainerNo = row.cells[1].innerHTML;
                var ContainerSize = row.cells[2].innerHTML;
                var ContainerType = row.cells[3].innerHTML;
                var ArrivalDate = row.cells[4].innerHTML;
                var Pkgs = row.cells[5].innerHTML;
                var Weigth = row.cells[6].innerHTML;

                tablearray.push({ 'ContainerNO': ContainerNo, 'Size': ContainerSize, 'Type': ContainerType, 'ArrivalDate': ArrivalDate, 'PKGS': Pkgs, 'Weight': Weigth})
            }
        }
    };
    //==================End=======Container Details List for Save Data Into DataBase================

    //==================Begin=======Document Details List for Save Data Into DataBase==============
    var viewModel = objectifyForm($('#AuctionForm').serializeArray());
    //==================End=======Document Details List for Save Data Into DataBase================
    debugger;
    $.ajax({
        type: 'POST',
        url: '/Auction/SaveAuctionForm',
        data: '{viewModel: ' + JSON.stringify(viewModel) + ', containerDetails: ' + JSON.stringify(tablearray) + ', NoticeType: ' + JSON.stringify(NoticeType) + ', NoticeDate: ' + JSON.stringify(ControlCtrl.Date.val()) + '}',
        //data: JSON.stringify(formData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            debugger;
            if (data == 0) {
                alert('Record not Saved Successfully');
            }
            else {
                if (data.ErrorList > 0) {
                    $("#divAutionShow").css('visibility', 'visible');
                }
                getSaveDataResponce(data.DataList);
                alert('Record Saved Successfully');
                //========Begin===Auto Print Auction Notice======================
                if (NoticeType == "First" && $("#txtAuctionId").val()>0) {
                    PrintAuctionFirstNotice(ControlCtrl);
                }
                else if (NoticeType == "Second" && $("#txtAuctionId2").val() > 0) {
                    PrintAuctionSecondNotice(ControlCtrl);
                }
                //========End===Auto Print Auction Notice========================
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

function autoGenerateNotice(ControlCtrl) {
    debugger;
    $("#tracker-loader").fadeIn("slow");
    if (ControlCtrl.AutoNoticeType.val() == "") {
        alert("Please select Notice Type.");
        ControlCtrl.AutoNoticeType.val();
        return false;
    };
    
    $.ajax({
        type: 'POST',
        url: '/Auction/GenerateAutoAuction',
        data: '{AutoNoticeType: ' + JSON.stringify(ControlCtrl.AutoNoticeType.val()) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            debugger;
            if (data.ErrorList == 0) {
                alert('Notice Not Generated Successfully');
            }
            else {
                alert('Notice Generated Successfully');
                RepTableDataJson($("#tblAutoNoticeGenList"), data.DataList, "Aution Notice Generation", "AutionNoticeList");
            }
            $("#tracker-loader").fadeOut("slow");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
}

function getSaveDataResponce(tableData) {
    var parseJSONResult = jQuery.parseJSON(tableData);
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
    if (rowDataSet[0][1] == 'First') {
        if (rowDataSet[0][0]>0) {
            $("#txtAuctionId").val(rowDataSet[0][0]);
        };
    }
    else if (rowDataSet[0][1] == 'Second') {
        if (rowDataSet[0][0] > 0) {
            $("#txtAuctionId2").val(rowDataSet[0][0]);
        };
    };
};

function BindAuctionNoticeDet(ControlCtrl) {
    debugger;
    //=========Begin===Call Validation On Show Button============
    if (ValidationOnShowButton(ControlCtrl) == false) { return false; }
    //=========End===Call Validation On Show Button==============

    var data1 = { 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val() };
    debugger;
    data = JSON.stringify(data1);

    $.ajax({

        url: '/Auction/CheckHoldNew',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data != "") {
                alert(data);
                 
                return;
            } else {
                BindAuctionNoticeDetNothold(ControlCtrl)
            }

           
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function BindAuctionNoticeDetNothold(ControlCtrl) {
    debugger;
    //=========Begin===Call Validation On Show Button============
    if (ValidationOnShowButton(ControlCtrl) == false) { return false; }
    //=========End===Call Validation On Show Button==============

    var data1 = { 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val() };
    debugger;
    data = JSON.stringify(data1);

    $.ajax({

        url: '/Auction/getAuctionGenList',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.ContainerList.length == 0 || data.ContainerList == "[]") {
                alert("No Data Found or Container Arrival date is less then 30 Days.");
                return;
            };

            AssignDataOnShow(ControlCtrl, data.DocumentList);

            $('#tblAuctionNoticeGenList').dataTable({
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

                "aaData": data.ContainerList,
                "columns": [
                    {

                        "data": "ContainerNO",
                        "render": function (data, type, row, meta) {
                            data = '<input type=\"checkbox\" name=\"checklist[]\" class=\"checklist\" "id=\"checklist\" ' + row.selected + '   value="' + data + '">';
                            return data;
                        }
                    },
                    { "data": "ContainerNO" },
                    { "data": "Size" },
                    { "data": "Type" },
                    { "data": "ArrivalDate" },
                    { "data": "PKGS" },
                    { "data": "Weight" }
                ],
                "rowCallback": function (row, data, dataIndex) {

                }

            })
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};


function ValidationOnShowButton(ControlCtrl) {
    if (ControlCtrl.IGMNo.val() == "" || ControlCtrl.IGMNo.val() == undefined) {
        ControlCtrl.IGMNo.focus();
        alert('Please entry IGM No.');
        return false;
    };

    if (ControlCtrl.ItemNo.val() == "" || ControlCtrl.ItemNo.val() == undefined) {
        ControlCtrl.ItemNo.focus();
        alert('Please entry Item No.');
        return false;
    };
};

function AssignDataOnShow(ControCtrl, documentDet) {
    debugger;
    ControCtrl.JoNo.val(documentDet[0]["JoNo"]);
    ControCtrl.IGMNo.val(documentDet[0]["IGMNo"]);
    ControCtrl.ItemNo.val(documentDet[0]["ItemNo"]);
    ControCtrl.IGMDate.val(documentDet[0]["IGMDate"]);
    ControCtrl.NoticeId.val(documentDet[0]["NoticeId"]);
    ControCtrl.NoticeDate.val(documentDet[0]["NoticeDate"]);
    ControCtrl.NoticeType.val(documentDet[0]["NoticeType"]);
    ControCtrl.Agent.val(documentDet[0]["Agent"]);
    ControCtrl.CargoDesc.val(documentDet[0]["CargoDescription"]);
    ControCtrl.ImporterName.val(documentDet[0]["ImporterName"]);
    ControCtrl.NotifyParty.val(documentDet[0]["NotifyParty"]);
    ControCtrl.NoOfPkgs.val(documentDet[0]["NoOfPkgs"]);
    ControCtrl.PkgsType.val(documentDet[0]["PackageType"]);
    ControCtrl.Weight.val(documentDet[0]["Weight"]);
    ControCtrl.UOM.val(documentDet[0]["UOM"]);
    ControCtrl.BLNo.val(documentDet[0]["BLNo"]);
    ControCtrl.BLDate.val(documentDet[0]["BLDate"]);
};

function objectifyForm(formArray) {//serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
};

function InitalizeTBL() {
    $("#tblAuctionNoticeGenList").dataTable({
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
};

//======================Print Auction Notice Generated List By IGM/Item No=========================
function PrintNotice(id) {
    debugger;
    var NoticeType = $("#ddlNoticeType").val();
    var IGMNo = "";
    var ItemNo = "";
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("tblNoticeDet");

    if (NoticeType=="") {
        alert('Please select Notice Type');
        return false;
    }

    if (table.rows.length <= 0) {
        alert('No Data Found.');
        return false;
    }

    var tablearray = [];
    for (var i = 1; i < table.rows.length; i++) {
        if (i == Currentindex) {
            row = table.rows[i];
            NoticeType = row.cells[1].innerHTML;
            AuctionId = row.cells[2].innerHTML;
            IGMNo = row.cells[4].innerHTML;
            ItemNo = row.cells[5].innerHTML;
            break;
        }
    };

    if (IGMNo == "" || ItemNo=="") {
        alert('Please select IGM/Item.');
        return false;
    };
   

    if (NoticeType == "First Notice" || NoticeType == "IGM") {

        var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
        data = JSON.stringify(data1);

        $.ajax({
            url: '/Auction/FirstAuctionNoticePrint',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                //window.print();
                var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
                myWindow.close();
                var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
                myWindow.document.write(response);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    else if (NoticeType == "Second Notice" || NoticeType == "IGM") {

        var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
        data = JSON.stringify(data1);

        $.ajax({
            url: '/Auction/SecondAuctionNoticePrint',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                //window.print();
                var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
                myWindow.close();
                var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
                myWindow.document.write(response);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    };
};
//===================End===Print Auction Notice Generated List By IGM/Item No======================

function PrintAuctionFirstNotice(ControlCtrl) {
    debugger;
    var data1 = { 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/FirstAuctionNoticePrint',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'width=500','height=500','resizable=no');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'width=500', 'height=500', 'resizable=no');
            myWindow.document.write(response);
            //var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            //myWindow.close();
            //var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            //myWindow.document.write(response);
            //myWindow.print();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
};

function PrintAuctionSecondNotice(ControlCtrl) {
    debugger;
    var data1 = { 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/SecondAuctionNoticePrint',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'width=500', 'height=500', 'resizable=no');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'width=500', 'height=500', 'resizable=no');
            myWindow.document.write(response);
            //var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            //myWindow.close();
            //var myWindow = window.open("", "MsgWindow1", 'width=' + screen.availWidth + ',height=' + screen.availHeight + ',resizable=no');
            //myWindow.document.write(response);
            //myWindow.print();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
};

function getAuctionUpdatedList(ControlCtrl) {
    debugger;
    if (ControlCtrl.DDLNoticeType.val() == "") {
        alert("Please select Notice Type");
        ControlCtrl.DDLNoticeType.focus();
        $("#tblNoticeDet").empty();
        return false;
    }
    var data1 = { 'Search': ControlCtrl.DDLNoticeType.val(), 'ReportDate': ControlCtrl.ReportDate.val(), 'IGMNo': $("#txtRIGMNo").val(), 'ItemNo': $("#txtRItemNo").val()};
    data = JSON.stringify(data1);
    $.ajax({

        url: '/Auction/getAuctionNoticeGeneratedList',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.DocumentList == 0) {
                alert('No Data Found.');
                return false;
            };
            RepTableDataJson($("#tblNoticeDet"), data.DocumentList, "Notice Generated List", "NoticeGeneratedList");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function ClearData(ControlCtrl) {
    debugger;
    ControlCtrl.IGMNo.val('');
    ControlCtrl.ItemNo.val('');
    ControlCtrl.IGMDate.val('');
    ControlCtrl.NoticeId.val('');
    ControlCtrl.NoticeDate.val('');
    ControlCtrl.JoNo.val('');
    ControlCtrl.AuctionId.val('');
    ControlCtrl.AuctionId2.val('');
    ControlCtrl.Agent.val('');
    ControlCtrl.CargoDesc.val('');
    ControlCtrl.ImporterName.val('');
    ControlCtrl.NotifyParty.val('');
    ControlCtrl.NoOfPkgs.val('');
    ControlCtrl.PkgsType.val('');
    ControlCtrl.Weight.val('');
    ControlCtrl.UOM.val('');
    ControlCtrl.BLNo.val('');
    ControlCtrl.BLDate.val('');
    $("#divAutionShow").removeAttr('visibility', 'hidden');
    ControlCtrl.TableAuctionNoticeGenList.empty();
    InitalizeTBL();
};

//======================Print NOC Letter List By IGM/Item No============================
function PrinNOCLetter(id) {
    debugger;
    var IGMNo = "";
    var ItemNo = "";
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("tblNoticeDet");

    if (table.rows.length <= 0) {
        alert('No Data Found.');
        return false;
    }

    var tablearray = [];
    for (var i = 1; i < table.rows.length; i++) {
        if (i == Currentindex) {
            row = table.rows[i];
            AuctionId = row.cells[2].innerHTML;
            IGMNo = row.cells[4].innerHTML;
            ItemNo = row.cells[5].innerHTML;
            break;
        }
    };

    if (IGMNo == "" || ItemNo == "") {
        alert('Please select IGM/Item.');
        return false;
    };


    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/NOCLetter',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.document.write(response);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
    
};
//=================End=====Print NOC Letter List By IGM/Item No=========================
//======================Print NOC Letter Hold List By IGM/Item No============================
function PrinNOCHoldLetter(id) {
    debugger;
    var IGMNo = "";
    var ItemNo = "";
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("tblNoticeDet");

    if (table.rows.length <= 0) {
        alert('No Data Found.');
        return false;
    }

    var tablearray = [];
    for (var i = 1; i < table.rows.length; i++) {
        if (i == Currentindex) {
            row = table.rows[i];
            AuctionId = row.cells[2].innerHTML;
            IGMNo = row.cells[4].innerHTML;
            ItemNo = row.cells[5].innerHTML;
            break;
        }
    };

    if (IGMNo == "" || ItemNo == "") {
        alert('Please select IGM/Item.');
        return false;
    };


    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/NOCHoldLetter',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.document.write(response);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });

};
//=================End=====Print NOC Letter Hold List By IGM/Item No=========================

//=================Begin=====Print Annexture Custom List By IGM/Item No=======================
function PrinAnnCustom(id) {
    debugger;
    var IGMNo = "";
    var ItemNo = "";
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("tblNoticeDet");

    if (table.rows.length <= 0) {
        alert('No Data Found.');
        return false;
    }

    var tablearray = [];
    for (var i = 1; i < table.rows.length; i++) {
        if (i == Currentindex) {
            row = table.rows[i];
            AuctionId = row.cells[2].innerHTML;
            IGMNo = row.cells[4].innerHTML;
            ItemNo = row.cells[5].innerHTML;
            break;
        }
    };

    if (IGMNo == "" || ItemNo == "") {
        alert('Please select IGM/Item.');
        return false;
    };


    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/AnnextureCustom',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.document.write(response);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });

};
//=================End=====Print Annexture Custom List By IGM/Item No=========================

//=================Begin=====Print IGM Item List By IGM/Item No=========================
function PrinIGMAuc(id) {
    debugger;
    var IGMNo = "";
    var ItemNo = "";
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("tblNoticeDet");

    if (table.rows.length <= 0) {
        alert('No Data Found.');
        return false;
    }

    var tablearray = [];
    for (var i = 1; i < table.rows.length; i++) {
        if (i == Currentindex) {
            row = table.rows[i];
            AuctionId = row.cells[2].innerHTML;
            IGMNo = row.cells[4].innerHTML;
            ItemNo = row.cells[5].innerHTML;
            break;
        }
    };

    if (IGMNo == "" || ItemNo == "") {
        alert('Please select IGM/Item.');
        return false;
    };


    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo, 'AuctionId': AuctionId };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/Auction/AuctionIGMPrint',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            //window.print();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.close();
            var myWindow = window.open("", "MsgWindow1", 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
            myWindow.document.write(response);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });

};
//=================End=====Print IGM Item List By IGM/Item No=========================
function NoticeTypeChange(ControlCtrl) {
    if (ControlCtrl.DDLNoticeType.val() == "IGM") {
        $("#divNoticeDate").hide();
        $("#divIGMNo").show();
        $("#divItemNo").show();
        $("#txtRIGMNo").val('');
        $("#txtRItemNo").val('');
        $("#txtRepDate").val('');
    }
    else {
        $("#divNoticeDate").show();
        $("#divIGMNo").hide();
        $("#divItemNo").hide();
        $("#txtRIGMNo").val('');
        $("#txtRItemNo").val('');
        $("#txtRepDate").val('');
    }
};