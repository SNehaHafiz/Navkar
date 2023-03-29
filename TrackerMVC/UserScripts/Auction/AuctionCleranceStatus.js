/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 26 Sept 2019
 Description: Auction Notice Status Controller
 ========================*/

$(document).ready(function () {
    debugger;

    var AuctionNoticeStatusCtrl = [];
    //=======Begin====Initialize Control to Object================
    AuctionNoticeStatusCtrl.SearchOn = $("#ddlSearchOn");
    AuctionNoticeStatusCtrl.IGMNo = $("#txtIGMNo");
    AuctionNoticeStatusCtrl.ItemNo = $("#txtItemNo");
    AuctionNoticeStatusCtrl.ContainerNo = $("#txtContainerNo");
    AuctionNoticeStatusCtrl.ClearedBy = $("#ddlClearedBy");
    AuctionNoticeStatusCtrl.AuctionValue = $("#txtValue");
    AuctionNoticeStatusCtrl.DutyValue = $("#txtDutyValue");
    AuctionNoticeStatusCtrl.ButtonShow = $("#btnShow");
    AuctionNoticeStatusCtrl.ButtonSave = $("#btnSave");
    AuctionNoticeStatusCtrl.Buttonclear = $("#btnclearValue");
    AuctionNoticeStatusCtrl.ButtonAuctionClearDetails = $("#AuctionClearDetails");
    //=======End====Initialize Control to Object================

    //=============Beign Call Auction Notice Clear Details On Show Button Click Event===============
    AuctionNoticeStatusCtrl.ButtonAuctionClearDetails.click(function () {
        debugger;
        $("#divAuctionStatusList").show();
        $("#divUpdateAuctionStatus").hide();
        getAuctionClearList();
    });
    //=============End Call Auction Notice Clear Details On Show Button Click Event===============

    //=============Beign Call Auction Notice Status On Show Button Click Event===============
    AuctionNoticeStatusCtrl.ButtonShow.click(function () {
        debugger;
        BindAuctionNotice(AuctionNoticeStatusCtrl);
    });
    //=============End Call Auction Notice Status On Show Button Click Event===============

    //=============Beign Call Genrate Button Click Event===============
    AuctionNoticeStatusCtrl.ButtonSave.click(function () {
        Save(AuctionNoticeStatusCtrl);
    });
    //=============End Call Genrate Button Click Event===============

    //=============Beign Call Clear Button Click Event===============
    AuctionNoticeStatusCtrl.Buttonclear.click(function () {
        ClearControl(AuctionNoticeStatusCtrl);
    });
    //=============End Call Clear Button Click Event===============

    //=============Beign Call Search On Click Event===============
    AuctionNoticeStatusCtrl.SearchOn.change(function () {
        debugger;
        OnSearchChange(AuctionNoticeStatusCtrl);
    });
    //=============End Call Genrate Button Click Event===============

});

function Save(ControlCtrl) {
    //alert($("#tblContainerDet tbody tr").length);
    //=========Begin===Call Validation On Show Button============
    if (ValidationOnShow(ControlCtrl) == false) { return false; }
    //=========End===Call Validation On Show Button==============

    if (ControlCtrl.AuctionValue.val() == "") {
        alert('Please Enter Value.');
        ControlCtrl.AuctionValue.focus();
        return false;
    };

    if (ControlCtrl.DutyValue.val() == "") {
        alert('Please Enter Duty Value.');
        ControlCtrl.DutyValue.focus();
        return false;
    };



    debugger;
    $.ajax({
        type: 'POST',
        url: '/Auction/SaveAuctionClearDetails',
        data: '{IGMNo: ' + JSON.stringify(ControlCtrl.IGMNo.val()) + ', ItemNo: ' + JSON.stringify(ControlCtrl.ItemNo.val()) + ', ContainerNo: ' + JSON.stringify(ControlCtrl.ContainerNo.val()) + ', Value: ' + JSON.stringify(ControlCtrl.AuctionValue.val()) + ', DutyValue: ' + JSON.stringify(ControlCtrl.DutyValue.val()) + ', ClearRemarks: ' + JSON.stringify(ControlCtrl.ClearedBy.val()) + '}',
        //data: JSON.stringify(formData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            if (data == 0) {
                alert('Record Not Update Successfully');
            }
            else {
                alert('Record Update Successfully');
                window.location = "/Auction/AuctionCleranceStatus";
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

function BindAuctionNotice(ControlCtrl) {
    debugger;
    //=========Begin===Call Validation On Show Button============
    if (ValidationOnShow(ControlCtrl) == false) { return false; }
    //=========End===Call Validation On Show Button==============

    var data1 = { 'IGMNo': ControlCtrl.IGMNo.val(), 'ItemNo': ControlCtrl.ItemNo.val(), 'ContainerNo': ControlCtrl.ContainerNo.val() };
    debugger;
    data = JSON.stringify(data1);

    $.ajax({

        url: '/Auction/getUpdateStatus',
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
            RepTableDataJson($("#tblContainerDet"), data.DocumentList, "ContainerDetails", "ContainerDetails");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function getAuctionClearList() {

    $.ajax({

        url: '/Auction/getAuctionClearDetails',
        type: 'Post',
        data: '',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.DocumentList == 0) {
                alert('No Data Found.');
                return false;
            };
            RepTableDataJson($("#tblAuctionClearList"), data.DocumentList, "Auction Clear Details", "AuctionClearDetails");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function ValidationOnShow(ControlCtrl) {
    if (ControlCtrl.SearchOn.val() == "IGMItem") {
        if (ControlCtrl.IGMNo.val() == "" || ControlCtrl.IGMNo.val() == undefined) {
            alert('Please enter IGM No.');
            ControlCtrl.IGMNo.focus();
            return false;
        };

        if (ControlCtrl.ItemNo.val() == "" || ControlCtrl.ItemNo.val() == undefined) {
            alert('Please enter Item No.');
            ControlCtrl.ItemNo.focus();
            return false;
        };
    }
    else if (ControlCtrl.SearchOn.val() == "Container") {
        if (ControlCtrl.ContainerNo.val() == "" || ControlCtrl.ContainerNo.val() == undefined) {
            alert('Please enter Container No.');
            ControlCtrl.ContainerNo.focus();
            return false;
        };
    }

};

function OnSearchChange(ControlCtrl) {
    debugger;
    if (ControlCtrl.SearchOn.val() == "IGMItem") {
        $("#divIGM").show();
        $("#divItem").show();
        $("#divContainer").hide();
        ControlCtrl.ContainerNo.val('');
    }
    else if (ControlCtrl.SearchOn.val() == "Container") {
        $("#divIGM").hide();
        $("#divItem").hide();
        $("#divContainer").show();
        ControlCtrl.IGMNo.val('');
        ControlCtrl.ItemNo.val('');
    }
}

function AssignDataOnShow(ControCtrl, documentDet) {
    debugger;
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
    $("#tblContainerDet").dataTable({
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

    $("#tblAuctionStatusList").dataTable({
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

function ValidControl(ControlName) {
    if ($("#" + ControlName + "").val() == "") {
        $("#" + ControlName + "").removeClass("form-control is-valid state-valid")
        $("#" + ControlName + "").addClass("form-control is-invalid state-invalid")
    }
    else {
        $("#" + ControlName + "").removeClass("form-control is-invalid state-invalid")
        $("#" + ControlName + "").addClass("form-control is-valid state-valid")
    }
};

function AutomCom() {
    debugger;
    $("#txtRemarks").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Auction/getAutoRemarksList",
                data: "{ 'prefixText': '" + request.term + "','CustomerType': '" + '' + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //debugger;
                    response($.map(data, function (item) {
                        //debugger;
                        return {
                            label: item.Remarks,
                            val: item.RemarkId
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
            $("#txtRemarks").val(i.item.label);
        },
        minLength: 1
    });

};


function ClearControl(ControCtrl) {
    debugger;
    ControCtrl.IGMNo.val('');
    ControCtrl.ItemNo.val('');
    ControCtrl.AuctionValue.val('');
    ControCtrl.DutyValue.val('');
    ControCtrl.ClearedBy.val('');
    ControCtrl.IGMNo.removeClass("form-control is-invalid state-invalid")
    ControCtrl.IGMNo.addClass("form-control")

    ControCtrl.ItemNo.removeClass("form-control is-invalid state-invalid")
    ControCtrl.ItemNo.addClass("form-control")

    ControCtrl.AuctionValue.removeClass("form-control is-invalid state-invalid")
    ControCtrl.AuctionValue.addClass("form-control")

    ControCtrl.DutyValue.removeClass("form-control is-invalid state-invalid")
    ControCtrl.DutyValue.addClass("form-control")

    ControCtrl.ClearedBy.removeClass("form-control is-invalid state-invalid")
    ControCtrl.ClearedBy.addClass("form-control")
};