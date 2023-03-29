/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 26 Sept 2019
 Description: BCN Wagon Mapping Controller
 ========================*/
$(document).ready(function () {
    var ControlBCNWagonMapping = [];
    ControlBCNWagonMapping.TrainNo = $("#TrainNo");
    ControlBCNWagonMapping.RRWagonContNo = $("#RRWagonContNo");
    ControlBCNWagonMapping.Show = $("#btnShow");
    ControlBCNWagonMapping.ButtonSave = $("#btnSave");
    ControlBCNWagonMapping.ButtonPrint = $("#btnPrint");
    ControlBCNWagonMapping.TableWagonMappingList = $("#tblWagonMappingList");
    ControlBCNWagonMapping.ButtonShowSummary = $("#btnShowSummary");
    ControlBCNWagonMapping.NewWagonNo = $("#txtNewWagonNo");
    ControlBCNWagonMapping.WagonPkgs = $("#txtWagonPkgs");
    ControlBCNWagonMapping.WagonWT = $("#txtWagonWT");
    ControlBCNWagonMapping.ContainerNo = $("#txtContainerNo");
    ControlBCNWagonMapping.StuffPkgs = $("#txtStuffPkgs");
    ControlBCNWagonMapping.StuffWT = $("#txtStuffWT");
    ControlBCNWagonMapping.OldWagonNo = $("#txtOldWagonNo");
    ControlBCNWagonMapping.AddNewDet = $("#btnAddNewDet");
    
    $(document).ready(function () {
        //==========Start==========On Show Button Click====================
        ControlBCNWagonMapping.ButtonShowSummary.click(function () {
            TrainSummary();
        });
        //==========End==========On Show Button Click====================
        //==========Start==========On Show Button Click====================
        ControlBCNWagonMapping.Show.click(function () {
            showBCNWagonMapping(ControlBCNWagonMapping);
        });
        //==========End==========On Show Button Click====================
        //==========Start==========On Add New Button Click====================
        ControlBCNWagonMapping.ButtonSave.click(function () {
            SaveBCNMapping(ControlBCNWagonMapping);
        });
        //==========End==========On Save Button Click====================
        ////==Start==Print BCN NOC Details================
        //ControlBCNWagonMapping.ButtonPrint.click(function () {
        //    PrintNOC(ControlBCNWagonMapping);
        //});
        ////==End==Print BCN NOC Details================
    });

});

function SaveBCNMapping(ControlBCNWagonMapping) {
    debugger;
    var table = document.getElementById("tblWagonMappingList");
    var tablearray = [];

    for (var i = 1; i < table.rows.length; i++) {

        row = table.rows[i];
        var ContEntryId = row.cells[1].childNodes[1].value;
        //debugger;
        var WagonNo = row.cells[1].childNodes[0].data;
        var ContainerNo = row.cells[6].childNodes[0].data;
        var StuffPkgs = row.cells[8].childNodes[0].value;
        var StuffWt = row.cells[9].childNodes[0].value;
        var Remarks = row.cells[10].childNodes[0].value;

        tablearray.push({ 'WagonNo': WagonNo, 'EntryId': ContEntryId, 'ContainerNo': ContainerNo, 'StuffPkgs': StuffPkgs, 'StuffWt': StuffWt, 'Remarks': Remarks })
    };
    debugger;
    $.ajax({
        type: 'POST',
        url: '/BCNWagon/SaveBCNMapping',
        data: '{containerData: ' + JSON.stringify(tablearray) + ', TrainNo: ' + JSON.stringify(ControlBCNWagonMapping.TrainNo.val()) + ',WagonRRNo: ' + JSON.stringify(ControlBCNWagonMapping.RRWagonContNo.val()) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            debugger;
            if (data > 0) {
                alert('Record Saved Successfully.');
            }
            else {
                alert('Record Not Saved Successfully.');
            }
            
        },
        error: function (errormessage) {
            debugger;
            alert('Error:- '+errormessage.responseText);
        }
    });
}

function showBCNWagonMapping(ControlBCNWagonMapping) {
    var TotalReceiptAmount = 0;
    var TotalTDSAmount = 0;

    var data1 = { 'TrainNo': ControlBCNWagonMapping.TrainNo.val(), 'WagonRRNo': ControlBCNWagonMapping.RRWagonContNo.val() };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/BCNWagon/ShowBCNWagonMapping',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.WagonContainerList.length == 0 || data.WagonContainerList == '[]') {
                ControlCtrl.InvoiceList.empty();
                InitializeTable(ControlCtrl);
                alert('Data Not Found.');
                return;
            }

            $('#tblWagonMappingList').dataTable({
                "destroy": true,
                "bLengthChange": false,
                "bInfo": false,
                "bPaginate": true,
                "bFilter": true,
                "paging": false,
                "aaSorting": [],
                "order": [],
                "aoColumnDefs": [
                    {
                        'bSortable': false,
                        'aTargets': [0]
                    }

                ],

                "aaData": data.WagonContainerList,
                "columns": [
                    { "data": "SRNo" },
                    {
                        "data": "WagonNo",
                        "render": function (data, type, row, meta) {

                            input = '<input type=\"hidden\" name=\"EntryId\" class=\" form-control EntryId\" id=\"ContEntryId\"  value="' + row.EntryId + '">';
                            return data + input;
                        }
                    },
                    { "data": "Pkgs" },
                    { "data": "CCWeight" },
                    { "data": "RemainingPkgs" },
                    { "data": "RemainingWt" },
                    {
                        "data": "ContainerNo",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"hidden\" name=\"ContainerNo\" class=\" form-control ContainerNo\" \" id=\"ContainerNo\"  value="' + data + '">';

                            return data + input;
                        }
                    },
                    { "data": "Date" },
                    {
                        "data": "StuffPkgs",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"text\" name=\"StuffPkgs\" class=\" form-control StuffPkgs\" \" id=\"StuffPkgs\"  value="' + data + '">';

                            return input;
                        }
                    },
                    {
                        "data": "StuffWt",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"text\" name=\"StuffWt\" class=\" form-control StuffWt\"  \" id=\"StuffWt\"  value="' + data + '">';

                            return input;
                        }
                    },
                    {
                        "data": "Remarks",
                        "render": function (data, type, row, meta) {
                            input = '<input type=\"text\" name=\"Remarks\" class=\" form-control Remarks\"  \" id=\"Remarks\"  value="' + data + '">';

                            return input;
                        }
                    },
                ],
                "rowCallback": function (row, data, dataIndex) {

                }

            })
            $("#tracker-loader").fadeOut("slow");
        },
        error: function (error) {
            let str = error.responseText;
            var a = str.indexOf("<title>") + 7;
            var b = str.indexOf("</title>");
            str = str.substring(a, b);
            alert("Something went wrong: " + str);
        }
    });
    $("#tracker-loader").fadeOut("slow");
}

function TrainSummary() {
    debugger;
    $("#tracker-loader").fadeIn("slow");

    var txtFromDate ="" ;
    var txtToDate = "";
    var txtTrainNo = "";

    if ($("#ddlCategory").val() == "DateRange") {
        txtFromDate = $("#txtFromDate").val();
        txtToDate = $("#txtToDate").val();

        if (txtFromDate == "") {
            alert("From Date cannot be Blank.");
            return;
        }

        if (txtToDate == "") {
            alert("To Date cannot be Blank.");
            return;
        }
    }
    else if ($("#ddlCategory").val() == "TrainNo") {
        txtTrainNo = $("#txtTrainNo").val();

        if (txtTrainNo == "") {
            alert("Train No cannot be Blank.");
            return;
        }
    }

    var data1 = { "FromDate": txtFromDate, "ToDate": txtToDate,"TrainNo": txtTrainNo };

    var data = JSON.stringify(data1);

    try {
        $.ajax({
            type: 'POST',
            url: '/BCNWagon/BCNWagonMappingSummary',
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (data) {
                RepTableDataJson($("#tblTrainDet"), data.DocumentList, "BCN Wagon Summary", "BCNWagonSummary");
                $("#tracker-loader").fadeOut("slow");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    catch (ex) {
        $("#tracker-loader").fadeOut("slow");
        alert(ex.message);
    }
}


