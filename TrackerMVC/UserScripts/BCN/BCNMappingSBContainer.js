/// <reference path="../../js/validation.js" />
/*=======================
 Created By:Himanshu Tripathi
 Created Date: 20 NOV 2019
 Description: BCN Mapping SB Container Controller
 ========================*/
$(document).ready(function () {
    
    var ControlBCNMappingSBContainer = [];

    ControlBCNMappingSBContainer.Condition = $("#ddlCondition");
    ControlBCNMappingSBContainer.StuffingRequestNo = $("#txtStuffingRequestNo");
    ControlBCNMappingSBContainer.StuffingRequestDate = $("#txtStuffingRequestDate");
    ControlBCNMappingSBContainer.ContainerNo = $("#txtContainerNo");
    ControlBCNMappingSBContainer.EntryId = $("#EntryId");
    ControlBCNMappingSBContainer.Size = $("#txtSize");
    ControlBCNMappingSBContainer.ContainerType = $("#txtContainerType");
    ControlBCNMappingSBContainer.ISOCode = $("#txtISOCode");
    ControlBCNMappingSBContainer.TareWeight = $("#txtTareWeight");
    ControlBCNMappingSBContainer.SBNo = $("#txtSBNo");
    ControlBCNMappingSBContainer.SBEntryId = $("#SBEntryId");
    ControlBCNMappingSBContainer.CargoDesc = $("#txtCargoDesc");
    ControlBCNMappingSBContainer.ShippingLine = $("#txtShippingLine");
    ControlBCNMappingSBContainer.Shipper = $("#txtShipper");
    ControlBCNMappingSBContainer.CHAName = $("#txtCHAName");
    ControlBCNMappingSBContainer.CustomerName = $("#txtCustomerName");
    ControlBCNMappingSBContainer.TableWagonMappingList = $("#tblWagonMappingList");
    ControlBCNMappingSBContainer.VIANo = $("#txtVIANo");
    ControlBCNMappingSBContainer.VesselName = $("#txtVesselName");
    ControlBCNMappingSBContainer.POL = $("#txtPOL");
    ControlBCNMappingSBContainer.CuttOfDate = $("#txtCuttOfDate");
    ControlBCNMappingSBContainer.Voyage = $("#txtVoyage");
    ControlBCNMappingSBContainer.POD = $("#txtPOD");
    ControlBCNMappingSBContainer.Remarks = $("#txtRemarks");
    ControlBCNMappingSBContainer.Rotation = $("#txtRotation");
    ControlBCNMappingSBContainer.ButtonSave = $("#btnSave");
    ControlBCNMappingSBContainer.ButtonShow = $("#btnShow");
    ControlBCNMappingSBContainer.ButtonclearValue = $("#btnclearValue");
    ControlBCNMappingSBContainer.ButtonAdd = $("#btnAdd");
    ControlBCNMappingSBContainer.Class = $("#hdnClass");
    ControlBCNMappingSBContainer.SBQty = $("#hdnSBQty");
    ControlBCNMappingSBContainer.CargoType = $("#hdnCargoType");
    ControlBCNMappingSBContainer.LEONo = $("#hdnLEONo");
    ControlBCNMappingSBContainer.LEODate = $("#hdnLEODate");
    ControlBCNMappingSBContainer.AgenSealNo = $("#txtAgenSealNo");
    ControlBCNMappingSBContainer.CustomSealNo = $("#txtCustomSealNo");
    ControlBCNMappingSBContainer.TrainNo = $("#txtTrainNo");
    ControlBCNMappingSBContainer.ButtonShowSummary = $("#btnShowSummary");
    ControlBCNMappingSBContainer.NetWeight = $("#txtNetWeight");
    ControlBCNMappingSBContainer.TotalPkgs = $("#txtTotalPkgs");
    
    LoadTable();

    //=======Start CLP Summary Controller===================
    ControlBCNMappingSBContainer.ButtonShowSummary.click(function () {
        CLPSummary();
    });
    //=======End CLP Summary Controller===================

    //=======Start Get Container Details From Controller===================
    ControlBCNMappingSBContainer.ButtonShow.click(function () {
        ContainerShow(ControlBCNMappingSBContainer);
    });
    //=======End Get Container Details From Controller===================

    //=======Start Get SB Details From Controller===================
    ControlBCNMappingSBContainer.SBNo.change(function () {
        ShippingBillShow(ControlBCNMappingSBContainer);
    });
    //=======End Get SB Details From Controller===================

    //============Start==Call Add Button For add Container & Shipping Bill Information In DataTable==================
    ControlBCNMappingSBContainer.ButtonAdd.click(function () {
        debugger;
        AddStuffing(ControlBCNMappingSBContainer);
    });
    //============End==Call Add Button For add Container & Shipping Bill Information In DataTable=====================

    //============Start==Call Add Button For add Container & Shipping Bill Information In DataTable==================
    ControlBCNMappingSBContainer.ButtonSave.click(function () {
        debugger;
        SaveBCNSBContainer(ControlBCNMappingSBContainer);
    });
    //============End==Call Add Button For add Container & Shipping Bill Information In DataTable=====================

    //============Start==Validate Train & Container Mapping Details==================
    ControlBCNMappingSBContainer.TrainNo.change(function () {
        debugger;
        GetBCNCLPSealDetOnChange(ControlBCNMappingSBContainer);
    });
    //============End==Validate Train & Container Mapping Details==================

    //============Start==Validate Train & Container Mapping Details==================
    ControlBCNMappingSBContainer.VIANo.change(function () {
        debugger;
        ViaChngs(ControlBCNMappingSBContainer);
    });
    //============End==Validate Train & Container Mapping Details==================

});

function ContainerShow(ControlCtrl) {
    $("#tracker-loader").fadeIn("slow");

    if (ControlCtrl.Condition.val() == "") {
        alert("Please Select Condition.");
        ControlCtrl.Condition.focus();
        return false;
    }

    if (ControlCtrl.ContainerNo.val() == "") {
        alert("Please Enter Container No.");
        ControlCtrl.ContainerNo.focus();
        return false;
    }

    var data1 = { 'ContainerNo': ControlCtrl.ContainerNo.val(), 'RRType': $("#ddlRRType").val() };
    data = JSON.stringify(data1);

    try {
        $.ajax({
            url: '/BCNWagon/ContainerShow',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                debugger;
                if (response.ContainerDetails.ErrMessage != "") {
                    alert(response.ContainerDetails.ErrMessage);
                    clearContainerData(ControlCtrl);
                }
                else {
                    var select = $("#txtTrainNo");
                    select.empty();
                    //select.append($('<option/>', {
                    //    value: "",
                    //    text: "--Select--"
                    //}));
                    debugger;
                    $.each(response.TrainList, function (data, value) {
                        debugger;
                        select.append($("<option></option>").val(value.TrainNo).html(value.TrainNo));
                    })
                    assingContainerData(ControlCtrl, response.ContainerDetails);
                    GetBCNCLPSealDet(ControlCtrl);
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    catch (ex) {
        $("#tracker-loader").fadeOut("slow");
    }
}

function ShippingBillShow(ControlCtrl) {
    $("#tracker-loader").fadeIn("slow");

    if (ControlCtrl.Condition.val() == "") {
        alert("Please Select Condition.");
        ControlCtrl.Condition.focus();
        return false;
    }

    if (ControlCtrl.SBNo.val() == "") {
        alert("Please Enter Shipping Bill No.");
        ControlCtrl.SBNo.focus();
        return false;
    }

    var data1 = { 'ShippingBillNo': ControlCtrl.SBNo.val() };
    data = JSON.stringify(data1);

    try {
        $.ajax({
            url: '/BCNWagon/ShippingBillShow',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response.ErrMessage != "") {
                    alert(response.ErrMessage);
                    clearSBData(ControlCtrl);
                }
                else {
                    assingSBData(ControlCtrl, response);
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    catch (ex) {
        $("#tracker-loader").fadeOut("slow");
    }
}

//============Start==Call Add Button For add Container & Shipping Bill Information In DataTable==================
function AddStuffing(ControlCtrl) {
    debugger;
    if (ControlCtrl.Condition.val() == "") {
        alert("Please Select Condition.");
        ControlCtrl.Condition.focus();
        return false;
    }

    if (ControlCtrl.SBNo.val() == "") {
        alert("Please Enter Shipping Bill No.");
        ControlCtrl.SBNo.focus();
        return false;
    }

    if (ControlCtrl.ContainerNo.val() == "") {
        alert("Please Enter Container No.");
        ControlCtrl.ContainerNo.focus();
        return false;
    }

    if (ControlCtrl.TrainNo.val() == "") {
        alert("Please Select Train No.");
        ControlCtrl.TrainNo.focus();
        return false;
    }

    var table = document.getElementById("tblWagonMappingList");

    if (ControlCtrl.Condition.val() == "1SB1Container") {
        if (table.rows.length > 2) {
            alert('You can Enter only Single Shipping Bill And Container Details. for :- ' + $("#ddlCondition option:selected").text());
            return false;
            //clearContainerData(ControlCtrl);
            //clearSBData(ControlCtrl);
        }
    }
    else {
        if (ValidateDuplicateData(ControlCtrl) == false) {
            return false;
        };
    }

    try {

        var pContanerNo = ControlCtrl.ContainerNo.val();
        var pEntryId = ControlCtrl.EntryId.val();
        var pSize = ControlCtrl.Size.val();
        var pContainerType = ControlCtrl.ContainerType.val();
        var pUNNo = "";
        var pTemp = "";
        var pISOCode = ControlCtrl.ISOCode.val();
        var pTareWeight = ControlCtrl.TareWeight.val();
        var pSBNo = ControlCtrl.SBNo.val();
        var pAgenSealNo = ControlCtrl.AgenSealNo.val();
        var pCustomSealNo = ControlCtrl.CustomSealNo.val();
        var pSBEntryId = ControlCtrl.SBEntryId.val();
        var pCargoDesc = ControlCtrl.CargoDesc.val();
        var pStuffingType = "";
        var pShipper = ControlCtrl.Shipper.val();
        var pClass = ControlCtrl.Class.val();
        var pSBQty = ControlCtrl.SBQty.val();
        var pCargoType = ControlCtrl.CargoType.val();
        var pLEONo = ControlCtrl.LEONo.val();
        var pLEODate = ControlCtrl.LEODate.val();
        var pTrainNo = ControlCtrl.TrainNo.val();
        var pNetWeight = ControlCtrl.NetWeight.val();
        var pTotalPkgs = ControlCtrl.TotalPkgs.val();
        var SrNo = 0;
        var ContainerNo = pContanerNo + "<input Name=EntryId type=hidden id=EntryId   value='" + pEntryId + "'>";
        var Size = pSize + "<input Name=Size type=hidden id=Size   value='" + pSize + "'>";
        var Type = pContainerType + "<input Name=Type type=hidden id=Type   value='" + pContainerType + "'>";
        var UNNo = pUNNo + "<input Name=UNNo type=hidden id=UNNo   value='" + pUNNo + "'>";
        var Temp = pTemp + "<input Name=Temp type=hidden id=Temp   value='" + pTemp + "'>";
        var ISOCode = pISOCode + "<input Name=ISOCode type=hidden id=ISOCode   value='" + pISOCode + "'>";
        var TareWeight = pTareWeight + "<input Name=TareWeight type=hidden id=TareWeight   value='" + pTareWeight + "'>";
        var SBNo = pSBNo + "<input Name=SBEntryId type=hidden id=SBEntryId   value='" + pSBEntryId + "'>";
        var TrainNo = pTrainNo + "<input Name=TrainNo type=hidden id=TrainNo   value='" + pTrainNo + "'>";
        var CargoDesc = pCargoDesc + "<input Name=CargoDesc type=hidden id=CargoDesc   value='" + pCargoDesc + "'>";
        var AgenSealNo = pAgenSealNo + "<input Name=AgenSealNo type=hidden id=AgenSealNo   value='" + pAgenSealNo + "'>";
        var CustomSealNo = pCustomSealNo + "<input Name=CustomSealNo type=hidden id=CustomSealNo   value='" + pCustomSealNo + "'>";
        var StuffingType = pStuffingType + "<input Name=StuffingType type=hidden id=StuffingType   value='" + pStuffingType + "'>";
        var Shipper = pShipper + "<input Name=Shipper type=hidden id=Shipper   value='" + pShipper + "'>";
        var Class = pClass + "<input Name=Class type=hidden id=Class   value='" + pClass + "'>";
        var SBQty = pSBQty + "<input Name=SBQty type=hidden id=SBQty   value='" + pSBQty + "'>";
        var CargoType = pCargoType + "<input Name=CargoType type=hidden id=CargoType   value='" + pCargoType + "'>";
        var LEONo = pLEONo + "<input Name=LEONo type=hidden id=LEONo   value='" + pLEONo + "'>";
        var LEODate = pLEODate + "<input Name=LEODate type=hidden id=LEODate   value='" + pLEODate + "'>";
        var NetWeight = pNetWeight + "<input Name=NetWeight type=hidden id=NetWeight   value='" + pNetWeight + "'>";
        var TotalPkgs = pTotalPkgs + "<input Name=TotalPkgs type=hidden id=TotalPkgs   value='" + pTotalPkgs + "'>";

        var t = $('#tblWagonMappingList').DataTable();

        if (!t.data().count()) {
            SrNo = 1;
        }
        else {
            SrNo=t.rows().count()+1;
        }

        t.row.add([
            "<button type=\"button\" class=\"btn btn-icon btn-primary btn-danger removebutton \" name=\"removebutton\" style=\"height:35px;\" onclick=\"CancelItem()\"><i class=\"fe fe-trash\" style=\"font-size: 16px;\"></i></button>",
            SrNo,
            ContainerNo,
            Size,
            Type,
            UNNo,
            Temp,
            ISOCode,
            TareWeight,
            SBNo,
            TrainNo,
            AgenSealNo,
            CustomSealNo,
            CargoDesc,
            StuffingType,
            Shipper,
            Class,
            SBQty,
            CargoType,
            LEONo,
            LEODate,
            NetWeight,
            TotalPkgs

        ]).draw(false);

    } catch (ex) {

    }

    if (ControlCtrl.Condition.val() != "1SB1Container") {
        clearContainerData(ControlCtrl);
        //clearSBData(ControlCtrl);
    }
}
//============End==Call Add Button For add Container & Shipping Bill Information In DataTable==================

function assingContainerData(ControlCtrl,data){
    debugger;
    ControlCtrl.ShippingLine.val(data.ShippingLine);
    ControlCtrl.EntryId.val(data.EntryId);
    ControlCtrl.Size.val(data.Size);
    ControlCtrl.ContainerType.val(data.ContainerType);
    ControlCtrl.TareWeight.val(data.TareWeight);
    ControlCtrl.ISOCode.val(data.ISOCode);
    ControlCtrl.CustomerName.val(data.CustomerName);
    ControlCtrl.TotalPkgs.val(data.TotalPkgs);
    ControlCtrl.NetWeight.val(data.NetWeight);
    //ControlCtrl.TrainNo.val(data.TrainNo);
}

function clearContainerData(ControlCtrl) {

    ControlCtrl.ShippingLine.val("");
    ControlCtrl.EntryId.val("");
    ControlCtrl.Size.val("");
    ControlCtrl.ContainerType.val("");
    ControlCtrl.TareWeight.val("");
    ControlCtrl.ISOCode.val("");
    ControlCtrl.CustomerName.val("");
    ControlCtrl.AgenSealNo.val("");
    ControlCtrl.CustomSealNo.val("");
    ControlCtrl.TrainNo.val("");
}

function assingSBData(ControlCtrl, data) {
    debugger;
    ControlCtrl.Shipper.val(data.Shipper);
    ControlCtrl.CargoDesc.val(data.CargoDesc);
    ControlCtrl.CHAName.val(data.CHAName);
    ControlCtrl.SBEntryId.val(data.SBEntryId);
    ControlCtrl.Class.val(data.ClassNo);
    ControlCtrl.SBQty.val(data.SBQty);
    ControlCtrl.CargoType.val(data.CargoType);
    ControlCtrl.LEONo.val(data.LEONo);
    ControlCtrl.LEODate.val(data.LEODate);
}

function clearSBData(ControlCtrl) {
    debugger;
    ControlCtrl.Shipper.val("");
    ControlCtrl.CargoDesc.val("");
    ControlCtrl.CHAName.val("");
    ControlCtrl.SBEntryId.val("");
    ControlCtrl.Class.val("");
    ControlCtrl.SBQty.val("");
    ControlCtrl.CargoType.val("");
    ControlCtrl.LEONo.val("");
    ControlCtrl.LEODate.val("");
}

function ValidateDuplicateData(ControlCtrl) {
    var result = true;
    var txtContainerNo = ControlCtrl.ContainerNo.val();
    var txtShippingBillNo = ControlCtrl.SBNo.val();

    try {
        var table = document.getElementById("tblWagonMappingList");
        if (table != null) {
            if (table.rows.length > 1) {
                for (var i = 1; i < table.rows.length; i++) {
                    row = table.rows[i];
                    var ContainerNo = "";
                    var ShippingBillNo = "";
                    ContainerNo = row.cells[1].data;
                    ShippingBillNo = row.cells[8].data;

                    if (txtContainerNo != "" && ContainerNo != "") {
                        if (txtContainerNo == ContainerNo) {
                            alert("Duplicate container found.");
                            result = false;
                            return result;
                        }
                    }

                    if (txtShippingBillNo != "" && ShippingBillNo != "") {
                        if (txtShippingBillNo == ShippingBillNo) {
                            alert("Duplicate Shipping Bill found.");
                            result = false;
                            return result;
                        }
                    }
                }
            }
        }
    }
    catch (ex) {
        //alert(ex.message);
        //result = false;
        //return result;
    }
    return result;
};

function ValidateOnSave(ControlCtrl) {
    var result = true;

    if (ControlCtrl.VIANo.val() == "") {
        alert("Please enter VIA No.");
        ControlCtrl.VIANo.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.VesselName.val() == "") {
        alert("Please enter VesselName.");
        ControlCtrl.VesselName.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.POL.val() == "") {
        alert("Please enter POL.");
        ControlCtrl.POL.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.CuttOfDate.val() == "") {
        alert("Please enter CuttOfDate.");
        ControlCtrl.CuttOfDate.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.Voyage.val() == "") {
        alert("Please enter Voyage No.");
        ControlCtrl.Voyage.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.POD.val() == "") {
        alert("Please enter POD No.");
        ControlCtrl.POD.focus();
        result = false;
        return result;
    }

    if (ControlCtrl.Rotation.val() == "") {
        alert("Please enter Rotation No.");
        ControlCtrl.Rotation.focus();
        result = false;
        return result;
    }

    return result;
};

function SaveBCNSBContainer(ControlCtrl) {
    debugger;
    try {

        if (CheckMultiRRType() == false) {
            return false;
        }

        if (ValidateOnSave(ControlCtrl) == false) {
            return false;
        }

        var itemcount = checkItemcount();
        if (itemcount=false) {
            alert("Please Add Container details");
        }

        var SelectedData = new Array();
        $('#tblWagonMappingList tbody tr').each(function (i, row) {
            var data = {};

            $(this).find('input').each(function () {

                data[this.id] = $(this).val();
            })
            SelectedData.push(data);

        });

        var data1 = { "ContData": SelectedData, "StuffingType": ControlCtrl.Condition.val(), "VIANo": ControlCtrl.VIANo.val(), "VesselName": ControlCtrl.VesselName.val(), "POL": ControlCtrl.POL.val(), "POD": ControlCtrl.POD.val(), "CutoffDate": ControlCtrl.CuttOfDate.val(), "Voyage": ControlCtrl.Voyage.val(), "Remarks": ControlCtrl.Remarks.val(), "Rotation": ControlCtrl.Rotation.val(), "RRType": $("#ddlRRType").val() };

        var data = JSON.stringify(data1);

        try {
            $.ajax({
                url: "/BCNWagon/BCNMappingSBSave",
                data: data,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response > "0") {
                        alert("Record saved successfully");
                        window.location = "/BCNWagon/BCNMappingSBContainer";
                        //Clear();
                    }
                    else {
                        alert("Record not saved successfully. Try Again!");
                    }
                },
                error: function (errormessage) {
                    alert("error");
                    alert(errormessage.responseText);
                }
            })
        }
        catch (ex) {
            alert(ex.message);
        }

    }
    catch (ex) {
        alert(ex.message);
    }
}

function checkItemcount() {

    var table = $('#tblWagonMappingList').DataTable();

    if (table.rows().count() == 0) {

        return false;
    }
    return true;
}

function ValidateTrainContData(ControlCtrl) {
    debugger;
    if (ControlCtrl.TrainNo.val() == "") {
        alert("Please enter Train No");
        return;
    }

    if (ControlCtrl.ContainerNo.val() == "") {
        alert("Please enter Container No");
        return;
    }

    var data1 = { "ContainerNo": ControlCtrl.ContainerNo.val(), "TrainNo": ControlCtrl.TrainNo.val() };

    var data = JSON.stringify(data1);

    try {
        $.ajax({
            url: "/BCNWagon/ValidateSBTrain",
            data: data,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != "Success") {
                    alert(response);
                    ControlCtrl.TrainNo.val('');
                }
            },
            error: function (errormessage) {
                alert("error");
                alert(errormessage.responseText);
            }
        })
    }
    catch (ex) {
        alert(ex.message);
    }
}

function ViaChngs(ControlCtrl) {
    debugger;
    if (ControlCtrl.VIANo.val() == "") {
        alert("Please enter VIA No");
        return;
    }

    var data1 = { "viaNo": ControlCtrl.VIANo.val() };

    var data = JSON.stringify(data1);

    try {
        $.ajax({
            url: "/BCNWagon/getVesselDetails",
            data: data,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                debugger;
                if (data.VesselID == 0) {
                    ControlCtrl.POD.val("");
                    ControlCtrl.CuttOfDate.val("");
                    ControlCtrl.VesselName.val('');
                    ControlCtrl.VIANo.val('');
                    ControlCtrl.POL.val('');
                    ControlCtrl.Voyage.val('');
                    ControlCtrl.Rotation.val('');
                    alert('Specified via no not found in vessel master. Cannot proceed.');
                }
                else {
                    ControlCtrl.POD.val(data.PortID);
                    ControlCtrl.CuttOfDate.val(data.berthingDateInstring);
                    ControlCtrl.VesselName.val(data.VesselID);
                    if (data.PortName == "BMCT") {
                        $("#txtPOL").val(5);
                    }
                    else if (data.PortName == "GTI") {
                        $("#txtPOL").val(2);
                    }
                    else if (data.PortName == "JNPT") {
                        $("#txtPOL").val(9);
                    }
                    else if (data.PortName == "NSICT") {
                        $("#txtPOL").val(3);
                    }
                    else if (data.PortName == "NSIGT") {
                        $("#txtPOL").val(4);
                    }
                    
                    //ControlCtrl.POL.val(data.PortName);
                    ControlCtrl.Voyage.val(data.Voyage);
                    ControlCtrl.Rotation.val(data.RotationNo);
                }
            },
            error: function (errormessage) {
                alert("error");
                alert(errormessage.responseText);
            }
        })
    }
    catch (ex) {
        alert(ex.message);
    }
}

function CLPSummary() {
    debugger;
    $("#tracker-loader").fadeIn("slow");

    var txtFromDate = $("#txtFromDate").val();
    var txtToDate = $("#txtToDate").val();

    var data1 = { "FromDate": txtFromDate, "ToDate": txtToDate };

    var data = JSON.stringify(data1);

    try {
        $.ajax({
            type: 'POST',
            url: '/BCNWagon/BCNCLPSummary',
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (data) {
                RepTableDataJson($("#tblCLPDet"), data.DocumentList, "BCN CLP Summary", "BCNCLPSummary");
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

function LoadTable() {
    $('#tblWagonMappingList').dataTable({
        "bLengthChange": false,
        "bInfo": false,
        "bPaginate": false,
        // "bFilter": false,
        "paging": false,
        "order": [],
        "aoColumnDefs": [
            {
                'bSortable': false,
                'aTargets': [0]
            }

        ]
    })
}

function CancelItem() {
    var table = $('#tblWagonMappingList').DataTable();
    $('#tblWagonMappingList tbody').on('click', '.removebutton', function () {
        table
            .row($(this).parents('tr'))
            .remove()
            .draw();

    });
}

function CheckMultiRRType() {
    var result = true;
    var TrainCount = 0;
    var TableCount = 0;

    if ($("#ddlRRType").val() != "MultipleRR") {
        result = true;
        return result;
    }

    TrainCount = $('#txtTrainNo option').length;
    var table = document.getElementById("tblWagonMappingList");
    TableCount = (table.rows.length)-1;

    if (TrainCount > 0) {
        if (TrainCount != TableCount) {
            alert("Please enter Second Train Container Details.");
            $('#txtTrainNo').focus();
            result = false;
            return result;
        }
    }
    
    return result;
};

function GetBCNCLPSealDet(ControlCtrl) {

    var TrainNo = $("#txtTrainNo").prop("selectedIndex", 0).val();

    var data1 = { 'TrainNo': TrainNo, 'ContainerNo': ControlCtrl.ContainerNo.val() };
    data = JSON.stringify(data1);

    try {
        $.ajax({
            url: '/BCNWagon/GetBCNCLPSealDet',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                debugger;
                try {
                    ControlCtrl.TotalPkgs.val(response.TotalPkgs);
                    ControlCtrl.NetWeight.val(response.TareWeight);
                    ControlCtrl.CustomSealNo.val(response.CustomSealNo);
                    ControlCtrl.AgenSealNo.val(response.AgenSealNo);
                } catch (ex) {}
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    catch (ex) {
        $("#tracker-loader").fadeOut("slow");
    }
}

function GetBCNCLPSealDetOnChange(ControlCtrl) {

    var TrainNo = $("#txtTrainNo").val();

    var data1 = { 'TrainNo': TrainNo, 'ContainerNo': ControlCtrl.ContainerNo.val() };
    data = JSON.stringify(data1);

    try {
        $.ajax({
            url: '/BCNWagon/GetBCNCLPSealDet',
            type: 'Post',
            data: data,
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                debugger;
                try {
                    ControlCtrl.TotalPkgs.val(response.TotalPkgs);
                    ControlCtrl.NetWeight.val(response.TareWeight);
                    ControlCtrl.CustomSealNo.val(response.CustomSealNo);
                    ControlCtrl.AgenSealNo.val(response.AgenSealNo);
                } catch (ex) { }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#tracker-loader").fadeOut("slow");
            }
        });
    }
    catch (ex) {
        $("#tracker-loader").fadeOut("slow");
    }
}
