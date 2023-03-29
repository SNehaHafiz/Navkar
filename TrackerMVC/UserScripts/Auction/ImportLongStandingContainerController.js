/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 26 Sept 2019
 Description: Auction Import Long Standing Controller
 ========================*/

$(document).ready(function () {
    var ImportLongStandingCtrl = [];

     //=======Begin====Initialize Control to Object================
    ImportLongStandingCtrl.SearchIn = $("#ddlSearchIn");
    ImportLongStandingCtrl.SearchText = $("#txtSearchText");
    ImportLongStandingCtrl.AsOnDate = $("#txtAsOnDate");
    ImportLongStandingCtrl.FromDay = $("#txtFromDay");
    ImportLongStandingCtrl.ToDay = $("#txtToDay");
    ImportLongStandingCtrl.ButtonShow = $("#btnShow");
     //=======End====Initialize Control to Object================

    InitalizeTBL();
    //=============Beign Call Import Long Standing Container On Show Button Click Event===============
    ImportLongStandingCtrl.ButtonShow.click(function () {
        debugger;
        if (chkLessGreater(ImportLongStandingCtrl.FromDay.val(), ImportLongStandingCtrl.ToDay.val()) == false) {
            ImportLongStandingCtrl.FromDay.val(0);
            ImportLongStandingCtrl.ToDay.val(0);
            return false;
        }
        getImportLongStandingContainer(ImportLongStandingCtrl);
    });
    //=============End Call Import Long Standing Container On Show Button Click Event===============
    

});

function getImportLongStandingContainer(ControlCtrl) {
    debugger;

    if (ControlCtrl.SearchIn.val() == "" || ControlCtrl.SearchIn.val() == undefined) {
        alert("Please select Search In.");
        ControlCtrl.SearchIn.focus();
        return;
    };


    var data1 = { 'AsOnDate': ControlCtrl.AsOnDate.val(), 'Fromday': ControlCtrl.FromDay.val(), 'Today': ControlCtrl.ToDay.val(), 'Criteria': ControlCtrl.SearchIn.val(), 'Search': ControlCtrl.SearchText.val() };
    debugger;
    data = JSON.stringify(data1);

    // alert(data);
    $.ajax({

        url: '/Auction/getImportLongSContainer',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            $("#reportTable").empty();
            if (data == "" || data == "[]") {
                alert("No Data Found");
                return;
            };
            
            RepTableDataJson($("#reportTable"), data, "Long Standing Container", "DailyCollectionList");
            //var table = document.getElementById("reportTable");
            //Textboxvalue(Activity)
            $("#tracker-loader").fadeOut("slow");
            AuctionIdentity();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function AuctionIdentity() {
    var table = document.getElementById("reportTable");
    for (var i = 1; i < table.rows.length; i++) {
        debugger;
        row = table.rows[i];
        var FirstNotice = row.cells[2].innerHTML;
        var SecondNotice = row.cells[3].innerHTML;
        if (FirstNotice == 'First Notice') {
            row.bgColor = "#409eef";
            row.color = "#ffffff";
                //("background-color", "red");
        };
        if (SecondNotice == 'Second Notice') {
            row.bgColor = "#847a7f";
            row.color = "#ffffff";
            //("background-color", "red");
        };

    }
}

function ViewAuctionGen(id) {
    debugger;
   
    var Currentindex = id.closest('tr').rowIndex;
    var table = document.getElementById("reportTable");
    var tablearray = [];
    var IGMNo = "";
    var ItemNo = "";

    for (var i = 1; i < table.rows.length; i++) {
        //debugger;
        if (i == Currentindex) {
            row = table.rows[i];
            IGMNo = row.cells[7].innerHTML;
            ItemNo = row.cells[8].innerHTML;
        }
    }
    debugger;

    var data1 = { 'IGMNo': IGMNo, 'ItemNo': ItemNo };
    debugger;
    data = JSON.stringify(data1);

    // alert(data);
    $.ajax({

        url: '/Auction/ViewAuctionNoticeGen',
        type: 'Post',
        data: data,
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            window.location = "/Auction/AuctionNoticeGeneration?IGMNo="+IGMNo+"&ItemNo="+ItemNo+"";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            $("#tracker-loader").fadeOut("slow");
        }


    });
};

function InitalizeTBL() {
    $("#reportTable").dataTable({
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