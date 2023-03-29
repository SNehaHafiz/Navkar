/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 26 Sept 2019
 Description: Auction Notice Status Controller
 ========================*/
$(document).ready(function () {
    debugger;
    var RRDownloadCtrl = [];
    //=======Begin====Initialize Control to Object================
    RRDownloadCtrl.TrainNo = $("#TrainNo");
    RRDownloadCtrl.FileType = $("#ddlFileType");
    RRDownloadCtrl.RRWagonContNo = $("#RRWagonContNo");
    RRDownloadCtrl.RRFile = $("#RRFile");
    RRDownloadCtrl.FromStation = $("#ddlFromStation");
    RRDownloadCtrl.ToStation = $("#ddlToStation");
    RRDownloadCtrl.Commodity = $("#txtCommodity");
    RRDownloadCtrl.FreightAmount = $("#txtFreightAmount");
    RRDownloadCtrl.PartyName = $("#ddlPartyName");
    RRDownloadCtrl.WagonRRNo = $("#txtRRNo");
    RRDownloadCtrl.ArrivalDate = $("#txtArrivalDate");
    RRDownloadCtrl.ButtonImport = $("#btnImport");
    RRDownloadCtrl.ButtonSave = $("#btnSave");
    //=======End====Initialize Control to Object================

    //=============Beign Call ButtonImport Details On Show Button Click Event===============
    RRDownloadCtrl.ButtonImport.click(function () {
        debugger;
        getRRDownload(RRDownloadCtrl);
    });
    //=============End Call ButtonImport Details On Show Button Click Event===============

    //=============Beign Call Button Save Details On Show Button Click Event===============
    RRDownloadCtrl.ButtonSave.click(function () {
        debugger;
        SaveRRDownloadData(RRDownloadCtrl);
    });
    //=============End Call Button Save Details On Show Button Click Event===============
});

function ValidationSave(ControlCtrl) {
    debugger;
    var retVal = true;
    try {
        if (ControlCtrl.FileType.val() == "BCN Wagon") {
            if (ControlCtrl.FromStation.val() == "") {
                alert("From Station is Blank.");
                retVal = false;
                return retVal;
            }

            if (ControlCtrl.ToStation.val() == "") {
                alert("To Station is Blank.");
                retVal = false;
                return retVal;
            }

            if (ControlCtrl.Commodity.val() == "") {
                alert("Commodity is Blank.");
                retVal = false;
                return retVal;
            }

            if (ControlCtrl.PartyName.val() == "") {
                alert("PartyName is Blank.");
                retVal = false;
                return retVal;
            }

            if (ControlCtrl.WagonRRNo.val() == "") {
                alert("RRWagonNo is Blank.");
                retVal = false;
                return retVal;
            }

            if (ControlCtrl.ArrivalDate.val() == "") {
                alert("ArrivalDate is Blank.");
                ControlCtrl.ArrivalDate.focus();
                retVal = false;
                return retVal;
            }
        }
        else {
            if (ControlCtrl.FileType.val() == "BCN Container") {
                if (ControlCtrl.RRWagonContNo.val() == "") {
                    alert("RR No is Blank.");
                    ControlCtrl.RRWagonContNo.focus();
                    retVal = false;
                    return retVal;
                }
            }
        }

        if (ControlCtrl.TrainNo.val() == "") {
            alert("Train Number is Blank.");
            ControlCtrl.TrainNo.focus();
            retVal = false;
            return retVal;
        }
    }
    catch (ex) {
        alert(ex.message);
        retVal = false;
    }

    return retVal;
}

function SaveRRDownloadData(ControlCtrl) {
    debugger;
    if (ValidationSave(ControlCtrl) == false) {
        return;
    }

    var viewModel = objectifyForm($('#BCNRRForm').serializeArray());

    try {
        $.ajax({
            url: '/BCNWagon/SaveRRDownloadForm',
            type: 'POST',
            data: '{viewModel: ' + JSON.stringify(viewModel) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (data) {
                debugger;
                alert(data);
                window.location = "/BCNWagon/RRDownload";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    } catch (ex) {
        alert(ex.message);
    }
}

function objectifyForm(formArray) {//serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

function ValidateTrainDet(ControlCtrl) {
    var retVal = true;
    if (ControlCtrl.FileType.val() == "") {
        alert("Please select File Type.");
        retVal = false;
        return retVal;
    }

    if (ControlCtrl.TrainNo.val() == "") {
        alert("Train Number cannot be Blank.");
        retVal = false;
        return retVal;
    }

    if (ControlCtrl.FileType.val() == "BCN Container") {
        if (ControlCtrl.RRWagonContNo.val() == "") {
            alert("RR Number cannot be Blank.");
            retVal = false;
            return retVal;
        }
    }

    var data1 = { 'TrainNo': ControlCtrl.TrainNo.val(), 'FileType': ControlCtrl.FileType.val(), 'RRNo': ControlCtrl.RRWagonContNo.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/BCNWagon/ValidateTrain',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != "") {
                alert(data);
                retVal = false;
                return retVal;
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    return retVal;
}

function getRRDownload(ControlCtrl) {
    debugger;
    if (window.FormData !== undefined) {

        if (ValidateTrainDet(ControlCtrl) == false) {
            return false;
        }

        var fileUpload = $("#fileImport").get(0);
        var files = fileUpload.files;

        // Create FormData object
        var fileData = new FormData();
        if (files.length == 0) {
            alert("Please select File!")
            return false;
        }
        else {
            //   alert(document.getElementById('fileJOAttachment').files[0].size / 1024)
            var iSize = document.getElementById('fileImport').files[0].size / 1024
            $('#hdnFileCount').val(files.length);

            iSize = (Math.round((iSize / 1024) * 100) / 100)
          
            if (iSize > 5) {
                alert("Selected file size more than 5 MB")
                return false;
            }
            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {

                fileData.append(files[i].name, files[i]);
            }

            if (ControlCtrl.TrainNo.val() == '') {
                alert("Please Enter Train No");
                ControlCtrl.TrainNo.focus();
                return false;
            }

            $('#btnImport').attr('disabled', 'disabled');

            // Adding one more key to FormData object
            fileData.append('TrainNo', ControlCtrl.TrainNo.val());
            fileData.append('FileType', ControlCtrl.FileType.val());
            debugger;
            
            $.ajax({
                url: '/BCNWagon/UploadRRFile',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (Data) {
                    debugger;
                    try {
                        if (Data =="File imported Successfully!") {
                            
                            if (ControlCtrl.FileType.val() == "BCN Container") {
                                ShowSelectedContValue(ControlCtrl, Data);
                            }
                            else {
                                ShowSelectedValue(ControlCtrl, Data);
                            }
                            $('#btnImport').removeAttr('disabled', 'disabled');
                        }
                        else {
                            $("#divIgmErrorShow").css('visibility', 'visible');
                            alert(Data);
                        }
                    }
                    catch (ex) {
                        //$('#btnImport').removeAttr('disabled');
                        alert(ex.validationMessage);
                    }
                }
            });
            //$('#btnImport').removeAttr('disabled');

        }
    }
}

function ShowSelectedValue(ControlCtrl,data) {
    debugger;
    var IGMFileId = data;

    //var Igm = JSON.stringify(IGMFileId);
    $.ajax({
        url: '/BCNWagon/GridViewBindDetailsNew',
        type: 'Post',
        data: '{ igm:' + JSON.stringify(IGMFileId) + '}',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.length == 0) {
                alert("No Record found")
            }
            else {
                alert("File Download successfully.")
            }

            //-=======Display Total Container Details with Teus
            try {
                ControlCtrl.FromStation.val(data.RRdata.FromStation);
                ControlCtrl.ToStation.val(data.RRdata.ToStation);
                ControlCtrl.WagonRRNo.val(data.RRdata.RRWagonNo);
                var dateString = data.RRdata.ArrivalDate.substr(6);
                var currentTime = new Date(parseInt(dateString));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                var date = day + " " + month + " " + year + "00:00";
                //ControlCtrl.ArrivalDate.val(date);
            }
            catch (ex) { }
            //-=======End Total Container Details with Teus
            debugger;
            //$("#tblInvoiceList").empty();

            var TotWagon = data.WagonSum.counter;
            var TotPackage = data.WagonSum.Package;
            var TotCCWeight = data.WagonSum.CCWeight;

            $('#tblInvoiceList').dataTable({
                "destroy": true,
                "bLengthChange": false,
                "aaData": data.WagonList,
                "bInfo": false,
                "bPaginate": true,
                "bFilter": true,
                "paging": true,

                "columns": [
                    { "data": "SRNo" },
                    { "data": "WagonType" },
                    { "data": "WagonNo" },
                    { "data": "Wongly" },
                    { "data": "PCCWeight" },
                    {"data": "CCWeight"},
                    {"data": "TareWeight"},
                    {"data": "Pkgs"}
                ]

            });

            $("#lblWTotalWagon").text(TotWagon);
            $("#lblWTotalPackage").text(TotPackage);
            $("#lblWTotalWeight").text(parseInt(TotCCWeight));


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    });

}

function ShowSelectedContValue(ControlCtrl, data) {
    debugger;
    var IGMFileId = data;
    var TotCont = 0;
    var TotPackage = 0;
    var TotWeight = 0;


    //var Igm = JSON.stringify(IGMFileId);
    $.ajax({
        url: '/BCNWagon/GridViewBindContDetailsNew',
        type: 'Post',
        data: '{ RRNo:' + JSON.stringify(ControlCtrl.RRWagonContNo.val()) + '}',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.length == 0) {
                alert("No Record found");
                return;
            }
            else {
                alert("File Download successfully.")
            }

            TotCont = data.WagonSum.counter;
            TotPackage = data.WagonSum.Package;
            TotWeight = data.WagonSum.CCWeight;

            $('#tblContList').dataTable({
                "destroy": true,
                "bLengthChange": false,
                "aaData": data.WagonList,
                "bInfo": false,
                "bPaginate": true,
                "bFilter": true,
                "paging": true,

                "columns": [
                    { "data": "SRNo" },
                    { "data": "ContainerNo" },
                    { "data": "BookingNo" },
                    { "data": "WTRepNo" },
                    { "data": "Date" },
                    { "data": "CCWeight" },
                    { "data": "Pkgs" },
                    { "data": "PCCWeight" },
                    { "data": "TareWeight" },
                    { "data": "NetWeight" },
                    { "data": "CustomSealNo" },
                    { "data": "AgentSealNo" },
                ]

            });

            $("#lblCTotalCont").text(TotCont);
            $("#lblCTotalPackage").text(TotPackage);
            $("#lblCTotalWeight").text(TotWeight);


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    });

}