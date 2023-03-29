/*=======================
 Created By:Himanshu Tripathi
 Created Date: 13 Sept 2019
 Description: Import Spliting Controller
 ========================*/
$(document).ready(function () {
    //debugger;
    var ImportSplitingCtrl = [];

    //=======Begin====Initialize Control================
    var txtIGMNo = $("#txtIGMNo");
    var txtItemNo = $("#txtItemNo");
    var btnShow = $("#btnShow");
    var btnSave = $("#btnSave");
    var tblContainerDet = $("#tblContainerDet");
    var tblItemDet = $("#tblItemDet");
    var ddlSplitType = $("#ddlSplitType");
    var txtMSItemNo = $("#txtSplitValue");
    var txtSplitBlNo = $("#txtSplitBlNo");
    var btnclearValue = $("#btnclearValue");
    var btnShowSummary = $("#btnShowSummary");
    var tblSplitingSummaryDet = $("#tblSplitingSummaryDet");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var chkSelectAllC = $("#chkSelectAllC");
    var chkSelectAllI = $("#chkSelectAllI");
    //=======End====Initialize Control================

    //=======Begin====Initialize Control to Object================
    ImportSplitingCtrl.IGMNo = txtIGMNo;
    ImportSplitingCtrl.ItemNo = txtItemNo;
    ImportSplitingCtrl.ShowButton = btnShow;
    ImportSplitingCtrl.SaveButtom = btnSave;
    ImportSplitingCtrl.tblContainer = tblContainerDet;
    ImportSplitingCtrl.tblItem = tblItemDet;
    ImportSplitingCtrl.SplitType = ddlSplitType;
    ImportSplitingCtrl.SplitBlNo = txtSplitBlNo;
    ImportSplitingCtrl.MSItemNo = txtMSItemNo;
    ImportSplitingCtrl.btnclearValue = btnclearValue;
    ImportSplitingCtrl.ShowSummary = btnShowSummary;
    ImportSplitingCtrl.tblSplitingSummary = tblSplitingSummaryDet;
    ImportSplitingCtrl.FromDate = txtFromDate;
    ImportSplitingCtrl.ToDate = txtToDate;
    ImportSplitingCtrl.SelectAllContainer = chkSelectAllC;
    ImportSplitingCtrl.SelectAllItem = chkSelectAllI;
    //=======End====Initialize Control Object================

    //==Begin==Select All Container From Table==============
    ImportSplitingCtrl.SelectAllContainer.click(function () {
        debugger;
        var chkAll = this;
        var chkRows = ImportSplitingCtrl.tblContainer.find("#chkSelect");
        chkRows.each(function () {
            debugger;
            $(this)[0].checked = chkAll.checked;
        });
    });
    //==End==Select All Container From Table==============

    //==Begin==Select All Item From Table==============
    ImportSplitingCtrl.SelectAllItem.click(function () {
        debugger;
        var chkAll = this;
        var chkRows = ImportSplitingCtrl.tblItem.find("#chkSelect");
        chkRows.each(function () {
            debugger;
            $(this)[0].checked = chkAll.checked;
        });
    });
    //==End==Select All Item From Table==============


    //==Begin==Call On Show Button for Getting Data From Controller
    ImportSplitingCtrl.ShowButton.click(function () {
        //debugger;
        //===Begin Validate Control==================
        if (ValidateControl(ImportSplitingCtrl) == false) {
            return;
        };
    //===End Validate Control==================
        ShowContainerDet(ImportSplitingCtrl);
    });
    //==End==Call On Show Button for Getting Data From Controller

    //==Begin==Save data==============================================
    ImportSplitingCtrl.SaveButtom.click(function () {
        //debugger;
        Save(ImportSplitingCtrl);
    });
    //==End==Save data================================================

    //===Initial Bind Details==============================
    InitializeTable(ImportSplitingCtrl);
    //===End Bind Details==============================

    //===Clear Control==============================
    ImportSplitingCtrl.btnclearValue.click(function () {
        debugger;
        ClearControl(ImportSplitingCtrl);
    });
    //===End Clear Control==============================

    //====Show Spliting Summary Log Details================
    ImportSplitingCtrl.ShowSummary.click(function () {
        //debugger;
        ShowContainerSummary(ImportSplitingCtrl);
    });
    //====End Spliting Summary Log Details================

});

function InitializeTable(ControlCtrl) {
    $(ControlCtrl.tblContainer).DataTable({
        retrieve: true,
        paging: false
    });

    $(ControlCtrl.tblItem).DataTable({
        retrieve: true,
        paging: false
    });
};

function ValidateControl(ControlCtrl) {
    var result = true;

    if (ControlCtrl.IGMNo.val() == "") {
        alert("Please enter IGM No.");
        ControlCtrl.IGMNo.focus();
        result = false;
        return result;
    };

    if (ControlCtrl.ItemNo.val() == "") {
        alert("Please enter Item No.");
        ControlCtrl.ItemNo.focus();
        result = false;
        return result;
    };

    return result;
}

function ShowContainerDet(ControlCtrl) {
    debugger;
    var IGMNo = ControlCtrl.IGMNo.val();
    var ItemNo = ControlCtrl.ItemNo.val();
    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportSpliting/GetImportSplitingData',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data.message != "") {
                alert(data.message);
                ControlCtrl.tblContainer.empty();
                ControlCtrl.tblItem.empty();
                return;
            }

            if (data.Containerlst == 0) {
                alert('Data not found.');
                ControlCtrl.tblContainer.empty();
                ControlCtrl.tblItem.empty();
                return;
            }
            BindContainerDetails(ControlCtrl, data);
            BindItemDetails(ControlCtrl, data);
            //RepTableDataJson($(ControlCtrl.tblContainer), data.Containerlst, "ContainerDetails", "ContainerDetails");
            //RepTableDataJson($(ControlCtrl.tblItem), data.ItemLst, "ItemDetails", "ItemDetails");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
}

function BindContainerDetails(ControlCtrl,data) {
    debugger;
    $(ControlCtrl.tblContainer).dataTable({
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

        "aaData": data.Containerlst,
        "columns": [
            {

                "data": "ContainerNO",
                "render": function (data, type, row, meta) {
                    data = '<input type=\"checkbox\" name=\"chkSelect\" class=\"check-list\" \ "id=\"chkSelect\" value="' + data + '">';
                    
                    return data;
                }
            },
            { "data": "ContainerNO" },
            { "data": "Size" },
            { "data": "Type" },
            { "data": "FCLLCType" },
            {
                "data": "PKGS",
                "render": function (data, type, row, meta) {
                    input = '<input type=\"text\" name=\"PKGS\" class=\" form-control \" " id=\"PKGS\"  value="' + data + '">';

                    return input;
                }
            },
            {
                "data": "Weight",
                "render": function (data, type, row, meta) {
                    input = '<input type=\"text\" name=\"Weight\" class=\" form-control TDS\" "\ id=\"Weight\"  value="' + data + '">';
                    return input;
                }
            }
        ],
        "rowCallback": function (row, data, dataIndex) {

        }

    })
};

function BindItemDetails(ControlCtrl, data) {
    debugger;
    $(ControlCtrl.tblItem).dataTable({
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

        "aaData": data.ItemLst,
        "columns": [
            {

                "data": "ItemNo",
                "render": function (data, type, row, meta) {
                    data = '<input type=\"checkbox\" name=\"chkSelect\" class=\"check-list\" \ "id=\"chkSelect\" value="' + data + '">';

                    return data;
                }
            },
            { "data": "ItemNo" },
            { "data": "NoOfPkgs" },
            { "data": "Weight" }
        ],
        "rowCallback": function (row, data, dataIndex) {

        }

    })
};

function ShowContainerSummary(ControlCtrl) {
    debugger;
    var FromDate = ControlCtrl.FromDate.val();
    var ToDate = ControlCtrl.ToDate.val();
    var data1 = { 'FromDate': FromDate, 'ToDate': ToDate };
    data = JSON.stringify(data1);

    $.ajax({
        url: '/ImportSpliting/ExportToExcelSplitingSummary',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
          
            if (data.Containerlst == 0) {
                alert('Data not found.');
                ControlCtrl.tblContainer.empty();
                ControlCtrl.tblItem.empty();
                return;
            }

            RepTableDataJson($(ControlCtrl.tblSplitingSummary), data.Containerlst, "ContainerDetailsS", "ContainerDetailsS");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }
    });
}

function ClearControl(ControlCtrl) {
    debugger;
    ControlCtrl.IGMNo.val('');
    ControlCtrl.ItemNo.val('');
    ControlCtrl.MSItemNo.val('');
    ControlCtrl.SplitType.val('');
    $(ControlCtrl.tblContainer).empty();
    $(ControlCtrl.tblItem).empty();
    InitializeTable(ControlCtrl);
}

function Save(ControlCtrl) {
    debugger;
    var IGMNo = ControlCtrl.IGMNo.val();
    var MainItemNo = ControlCtrl.ItemNo.val();
    var SplitType = ControlCtrl.SplitType.val();
    var MSItemNo = ControlCtrl.MSItemNo.val();
    var SplitBLNo = ControlCtrl.SplitBlNo.val();
    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo };
    var containerList = "";
    var ItemList = "";

    //Loop through all checked CheckBoxes in DataTable.
    var checkboxarraycnt = [];
    var checkboxarrayItem = [];

    var tablearraycnt = [];
    var tablearrayItem = [];

    var tablecnt = document.getElementById("tblContainerDet");
    var tableItem = document.getElementById("tblItemDet");

    var oTableCnt = ControlCtrl.tblContainer.dataTable();
    var oTableItem = ControlCtrl.tblItem.dataTable();

    //========================Start Container Details=================================
    debugger;
    $("input:checked", oTableCnt.fnGetNodes()).each(function () {
        checkboxarraycnt.push($(this).val());
    });
    

    for (var i = 1; i < tablecnt.rows.length; i++) {
        debugger;
        row = tablecnt.rows[i];
        for (var k = 0; k < checkboxarraycnt.length; k++) {
            var containerNo = row.cells[0].childNodes[0].value;
            debugger;
            if (containerNo == checkboxarraycnt[k]) {
                var ContainerNO = row.cells[1].innerHTML;
                var Size = row.cells[2].innerHTML;
                var Type = row.cells[3].innerHTML;
                var FCLLCType = row.cells[4].innerHTML;
                var PKGS = row.cells[5].childNodes[0].value;
                var Weight = row.cells[6].childNodes[0].value;

                tablearraycnt.push({ 'ContainerNO': ContainerNO, 'Size': Size, 'Type': Type, 'FCLLCType': FCLLCType, 'PKGS': PKGS, 'Weight': Weight })
            }
        }
    };
    //========================End Container Details=================================

    //========================Start Item Details=================================
    $("input:checked", oTableItem.fnGetNodes()).each(function () {
        checkboxarrayItem.push($(this).val());
    });

    for (var i = 1; i < tableItem.rows.length; i++) {
        debugger;
        row = tableItem.rows[i];
        for (var k = 0; k < checkboxarrayItem.length; k++) {
            var ItemNoChk = row.cells[0].childNodes[0].value;
            debugger;
            if (ItemNoChk == checkboxarrayItem[k]) {
                var ItemNo = row.cells[1].innerHTML;
                var NoOfPkgs = row.cells[2].innerHTML;
                var Weight = row.cells[3].innerHTML;

                tablearrayItem.push({ 'ItemNo': ItemNo, 'NoOfPkgs': NoOfPkgs, 'Weight': Weight })
            }
        }
    };
    //========================End Item Details=================================
    debugger;
    if (tablearrayItem.length<=0) {
        alert('Please select at list one record from item.');
        return;
    };

    if (tablearraycnt.length <= 0) {
        alert('Please select at list one record from container.');
        return;
    };

    if (SplitType == "") {
        alert('Please select Split/Merge Type');
        ControlCtrl.SplitType.focus();
        return;
    }

    if (MSItemNo == "") {
        alert('Please enter Item no for Split/Merge');
        ControlCtrl.MSItemNo.focus();
        return;
    }

    $.ajax({
        url: "/ImportSpliting/SubmitData",
        data: '{IGMNo: ' + JSON.stringify(IGMNo) + ', ItemNo: ' + JSON.stringify(MainItemNo) + ', MSItemNo: ' + JSON.stringify(MSItemNo) + ', SpBLNo: ' + JSON.stringify(SplitBLNo) + ', SplitType: ' + JSON.stringify(SplitType) + ', containerDet: ' + JSON.stringify(tablearraycnt) + ', ItemDet: ' + JSON.stringify(tablearrayItem) + '}',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data.DataList == 0) {
                alert("Data not saved successfully");
                var cnf = confirm('Are you want Merge/Split another Item ?');
                if (cnf == true) {
                    ControlCtrl.ItemNo.focus();
                    ControlCtrl.ItemNo.val('');
                    ControlCtrl.MSItemNo.val('');
                }
                else {
                    ClearControl(ControlCtrl);
                    return;
                }
            }
            else {
                alert("Data saved successfully");
                window.location = "/ImportSpliting/ImportSpliting";
                return;
            }
            
        },
        error: function (errormessage) {
            alert("error");
            alert(errormessage.responseText);
        }
    });
}

////============Get Select Container Details and add in list====================
//var rowcollectionCnt = oTableCnt.$("#chkSelect:checked", { "page": "all" });
//rowcollectionCnt.each(function (index, elem) {
//    //debugger;
//    if (containerList == "") {
//        containerList = $(elem).val();
//    }
//    else {
//        containerList = containerList + ',' + $(elem).val();
//    }
//});
////============End Select Container Details and add in list====================

////============Get Select Item Details and add in list====================
//var rowcollectionItem = oTableItem.$("#chkSelect:checked", { "page": "all" });
//rowcollectionItem.each(function (index, elem) {
//    //debugger;
//    if (ItemList == "") {
//        ItemList = $(elem).val();
//    }
//    else {
//        ItemList = ItemList + ',' + $(elem).val();
//    }
//});
//    //debugger;
//    //alert('Container :- ' + containerList + ' Item :- ' + ItemList);
//    //============End Select Item Details and add in list====================