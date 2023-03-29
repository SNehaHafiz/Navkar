using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BO = BombayToolsDataLayer.CustodianJson;
using EN = TrackerMVCEntities.CustodianJson;
using ASR = TrackerMVCEntities.CustodianJsonASR;
using AR = TrackerMVCEntities.CustodianJsonAR;
using DP = TrackerMVCEntities.CustodianJsonDP;
using DT = TrackerMVCEntities.CustodianJsonDT;

namespace BombayToolBusinessLayer.CutodianJson
{
    public class CustodianJsonBusinessLayer
    {
        BO.CustodianJsonDataLayer CustodianJsonDataLayer = new BO.CustodianJsonDataLayer();

        public EN.CustodianJson GenerateJsonSF(string FileType, string ContainerNo)
        {
            EN.CustodianJson custodianJson = new EN.CustodianJson();
            EN.EGM eGM = new EN.EGM();
            EN.HeaderField headerField = new EN.HeaderField();
            EN.master master = new EN.master();
            EN.Declaration declaration = new EN.Declaration();
            EN.Location location = new EN.Location();
            List<EN.CargoContainer> cargoContainers = new List<EN.CargoContainer>();
            List<EN.CargoDetails> cargoDetails = new List<EN.CargoDetails>();
            EN.DigSign digSign = new EN.DigSign();

            DataSet dataSet = new DataSet();
            dataSet = CustodianJsonDataLayer.GenerateJsonSF(FileType, ContainerNo);

            DataTable dtHeaderField = new DataTable();
            DataTable dtDeclaration = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCargoContainer = new DataTable();
            DataTable dtCargoDetails = new DataTable();
            DataTable dtFileDetails = new DataTable();
            DataTable dtDigSign = new DataTable();

            dtHeaderField = dataSet.Tables[0];
            dtDeclaration = dataSet.Tables[1];
            dtLocation = dataSet.Tables[2];
            dtCargoContainer = dataSet.Tables[3];
            dtCargoDetails = dataSet.Tables[4];
            dtFileDetails = dataSet.Tables[5];
            dtDigSign = dataSet.Tables[6];


            if (dtHeaderField != null)
            {
                foreach (DataRow row in dtHeaderField.Rows)
                {
                    headerField.indicator = Convert.ToString(row["indicator"]);
                    headerField.messageID = Convert.ToString(row["messageID"]);
                    headerField.sequenceOrControlNumber = Convert.ToInt32(row["sequenceOrControlNumber"]);
                    headerField.date = Convert.ToString(row["date"]);
                    headerField.time = Convert.ToString(row["time"]);
                    headerField.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    headerField.senderID = Convert.ToString(row["senderID"]);
                    headerField.receiverID = Convert.ToString(row["receiverID"]);
                    headerField.versionNo = Convert.ToString(row["versionNo"]);
                }
            }
            if (dtDeclaration != null)
            {
                foreach (DataRow row in dtDeclaration.Rows)
                {
                    declaration.messageType = Convert.ToString(row["messageType"]);
                    declaration.portOfReporting = Convert.ToString(row["portOfReporting"]);
                    declaration.jobNo = Convert.ToInt32(row["jobNo"]);
                    declaration.jobDate = Convert.ToString(row["jobDate"]);
                    declaration.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    master.declaration = declaration;
                }
            }
            if (dtLocation != null)
            {
                foreach (DataRow row in dtLocation.Rows)
                {
                    location.reportingPartyType = Convert.ToString(row["reportingPartyType"]);
                    location.reportingPartyCode = Convert.ToString(row["reportingPartyCode"]);
                    location.reportingLocationCode = Convert.ToString(row["reportingLocationCode"]);
                    location.reportingLocationName = Convert.ToString(row["reportingLocationName"]);
                    location.authorisedPersonPAN = Convert.ToString(row["authorisedPersonPAN"]);
                    master.location = location;
                }
            }
            if (dtCargoContainer != null)
            {
                foreach (DataRow row in dtCargoContainer.Rows)
                {
                    EN.CargoContainer cargoContainer = new EN.CargoContainer();

                    cargoContainer.messageType = Convert.ToString(row["messageType"]);
                    cargoContainer.equipmentSequenceNo = Convert.ToInt32(row["equipmentSequenceNo"]);
                    cargoContainer.equipmentID = Convert.ToString(row["equipmentID"]);
                    cargoContainer.equipmentType = Convert.ToString(row["equipmentType"]);
                    cargoContainer.equipmentSize = Convert.ToString(row["equipmentSize"]);
                    cargoContainer.equipmentLoadStatus = Convert.ToString(row["equipmentLoadStatus"]);
                    cargoContainer.finalDestinationLocation = Convert.ToString(row["finalDestinationLocation"]);
                    cargoContainer.eventDate = Convert.ToString(row["eventDate"]);
                    cargoContainer.equipmentSealType = Convert.ToString(row["equipmentSealType"]);
                    cargoContainer.equipmentSealNumber = Convert.ToString(row["equipmentSealNumber"]);
                    cargoContainer.equipmentStatus = Convert.ToString(row["equipmentStatus"]);
                    cargoContainer.equipmentQuantity = Convert.ToInt32(row["equipmentQuantity"]);
                    cargoContainer.equipmentQUC = Convert.ToString(row["equipmentQUC"]);
                    foreach (DataRow cargoRow in dtCargoDetails.Rows)
                    {
                        if (Convert.ToString(row["equipmentID"]) == Convert.ToString(cargoRow["containerNo"]))
                        {
                            EN.CargoDetails cargo = new EN.CargoDetails();

                            cargo.messageType = Convert.ToString(cargoRow["messageType"]);
                            cargo.cargoSequenceNo = Convert.ToInt32(cargoRow["cargoSequenceNo"]);
                            cargo.documentType = Convert.ToString(cargoRow["documentType"]);
                            cargo.documentSite = Convert.ToString(cargoRow["documentSite"]);
                            cargo.documentNumber = Convert.ToInt32(cargoRow["documentNumber"]);
                            cargo.documentDate = Convert.ToString(cargoRow["documentDate"]);
                            cargo.shipmentLoadStatus = Convert.ToString(cargoRow["shipmentLoadStatus"]);
                            cargo.packageType = Convert.ToString(cargoRow["packageType"]);
                            cargo.quantity = Convert.ToInt32(cargoRow["quantity"]);
                            cargo.packetsFrom = Convert.ToInt32(cargoRow["packetsFrom"]);
                            cargo.packetsTo = Convert.ToInt32(cargoRow["packetsTo"]);
                            cargo.packUQC = Convert.ToString(cargoRow["packUQC"]);
                            cargo.mcinPcin = Convert.ToString(cargoRow["mcinPcin"]);

                            cargoContainer.cargoDetails.Add(cargo);

                        }
                    }
                    master.cargoContainer.Add(cargoContainer);
                }
            }
            if (dtFileDetails != null)
            {
                foreach (DataRow row in dtFileDetails.Rows)
                {
                    custodianJson.fileName = Convert.ToString(row["FileName"]);
                    custodianJson.filePath = Convert.ToString(row["FilePath"]);
                    custodianJson.moveFilePath = Convert.ToString(row["MoveFilePath"]);
                    custodianJson.fileNo = Convert.ToInt32(row["FileNo"]);

                }
            }
            if (dtDigSign != null)
            {
                foreach (DataRow row in dtDigSign.Rows)
                {
                    digSign.startSignature = Convert.ToString(row["startSignature"]);
                    digSign.startCertificate = Convert.ToString(row["startCertificate"]);
                    digSign.signerVersion = Convert.ToString(row["signerVersion"]);
                }
            }
            eGM.headerField = headerField;
            eGM.master = master;
            //eGM.digSign = digSign;
            custodianJson.EGM = eGM;
            return custodianJson;
        }

        public ASR.CustodianJson GenerateJsonASR(string FileType, string ContainerNo, string FilterType)
        {
            ASR.CustodianJson custodianJson = new ASR.CustodianJson();
            ASR.EGM eGM = new ASR.EGM();
            ASR.HeaderField headerField = new ASR.HeaderField();
            ASR.Master master = new ASR.Master();
            ASR.Declaration declaration = new ASR.Declaration();
            ASR.Location location = new ASR.Location();
            List<ASR.CargoContainer> cargoContainers = new List<ASR.CargoContainer>();
            List<ASR.CargoDetails> cargoDetails = new List<ASR.CargoDetails>();
            List<ASR.CargoItnry> cargoItnry = new List<ASR.CargoItnry>();
            ASR.DigSign digSign = new ASR.DigSign();


            DataSet dataSet = new DataSet();
            dataSet = CustodianJsonDataLayer.GenerateJsonASR(FileType, ContainerNo, FilterType);

            DataTable dtHeaderField = new DataTable();
            DataTable dtDeclaration = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCargoContainer = new DataTable();
            DataTable dtCargoDetails = new DataTable();
            DataTable dtCargoIntry = new DataTable();
            DataTable dtFileDetails = new DataTable();
            DataTable dtDigSign = new DataTable();

            dtHeaderField = dataSet.Tables[0];
            dtDeclaration = dataSet.Tables[1];
            dtLocation = dataSet.Tables[2];
            dtCargoContainer = dataSet.Tables[3];
            dtCargoDetails = dataSet.Tables[4];
            dtCargoIntry = dataSet.Tables[5];
            dtFileDetails = dataSet.Tables[6];
            dtDigSign = dataSet.Tables[7];


            if (dtHeaderField != null)
            {
                foreach (DataRow row in dtHeaderField.Rows)
                {
                    headerField.indicator = Convert.ToString(row["indicator"]);
                    headerField.messageID = Convert.ToString(row["messageID"]);
                    headerField.sequenceOrControlNumber = Convert.ToInt32(row["sequenceOrControlNumber"]);
                    headerField.date = Convert.ToString(row["date"]);
                    headerField.time = Convert.ToString(row["time"]);
                    headerField.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    headerField.senderID = Convert.ToString(row["senderID"]);
                    headerField.receiverID = Convert.ToString(row["receiverID"]);
                    headerField.versionNo = Convert.ToString(row["versionNo"]);
                }
            }
            if (dtDeclaration != null)
            {
                foreach (DataRow row in dtDeclaration.Rows)
                {
                    declaration.messageType = Convert.ToString(row["messageType"]);
                    declaration.portOfReporting = Convert.ToString(row["portOfReporting"]);
                    declaration.jobNo = Convert.ToInt32(row["jobNo"]);
                    declaration.jobDate = Convert.ToString(row["jobDate"]);
                    declaration.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    master.declaration = declaration;
                }
            }
            if (dtLocation != null)
            {
                foreach (DataRow row in dtLocation.Rows)
                {
                    location.reportingPartyType = Convert.ToString(row["reportingPartyType"]);
                    location.reportingPartyCode = Convert.ToString(row["reportingPartyCode"]);
                    location.reportingLocationCode = Convert.ToString(row["reportingLocationCode"]);
                    location.reportingLocationName = Convert.ToString(row["reportingLocationName"]);
                    location.authorisedPersonPAN = Convert.ToString(row["authorisedPersonPAN"]);
                    master.location = location;
                }
            }
            if (dtCargoDetails != null)
            {
                foreach (DataRow cargoRow in dtCargoDetails.Rows)
                {
                    ASR.CargoDetails cargo = new ASR.CargoDetails();

                    cargo.messageType = Convert.ToString(cargoRow["messageType"]);
                    cargo.cargoSequenceNo = Convert.ToInt32(cargoRow["cargoSequenceNo"]);
                    cargo.documentType = Convert.ToString(cargoRow["documentType"]);
                    cargo.documentSite = Convert.ToString(cargoRow["documentSite"]);
                    cargo.documentNo = Convert.ToInt32(cargoRow["documentNo"]);
                    cargo.documentDate = Convert.ToString(cargoRow["documentDate"]);
                    cargo.shipmentLoadStatus = Convert.ToString(cargoRow["shipmentLoadStatus"]);
                    cargo.packageType = Convert.ToString(cargoRow["packageType"]);
                    cargo.quantity = Convert.ToInt32(cargoRow["quantity"]);
                    cargo.MCINPCIN = Convert.ToString(cargoRow["MCINPCIN"]);


                    if (dtCargoContainer != null)
                    {
                        foreach (DataRow row in dtCargoContainer.Rows)
                        {
                            if (Convert.ToString(row["SBNo"]) == Convert.ToString(cargoRow["documentNo"]))
                            {
                                ASR.CargoContainer cargoContainer = new ASR.CargoContainer();

                                cargoContainer.messageType = Convert.ToString(row["messageType"]);
                                cargoContainer.equipmentSequenceNo = Convert.ToInt32(row["equipmentSequenceNo"]);
                                cargoContainer.equipmentID = Convert.ToString(row["equipmentID"]);
                                cargoContainer.equipmentType = Convert.ToString(row["equipmentType"]);
                                cargoContainer.equipmentSize = Convert.ToString(row["equipmentSize"]);
                                cargoContainer.equipmentLoadStatus = Convert.ToString(row["equipmentLoadStatus"]);
                                cargoContainer.finalDestinationLocation = Convert.ToString(row["finalDestinationLocation"]);
                                cargoContainer.eventDate = Convert.ToString(row["eventDate"]);
                                cargoContainer.equipmentSealType = Convert.ToString(row["equipmentSealType"]);
                                cargoContainer.equipmentSealNumber = Convert.ToString(row["equipmentSealNumber"]);
                                cargoContainer.equipmentStatus = Convert.ToString(row["equipmentStatus"]);

                                cargo.cargoContainer.Add(cargoContainer);
                            }

                        }
                    }
                    if (dtCargoIntry != null)
                    {
                        foreach (DataRow row in dtCargoIntry.Rows)
                        {
                            ASR.CargoItnry cargoInt = new ASR.CargoItnry();

                            cargoInt.prtOfCallSeqNmbr = Convert.ToInt32(row["prtOfCallSeqNmbr"]);
                            cargoInt.prtOfCallCdd = Convert.ToString(row["prtOfCallCdd"]);
                            cargoInt.prtOfCallName = Convert.ToString(row["prtOfCallName"]);
                            cargoInt.nxtPrtOfCallCdd = Convert.ToString(row["nxtPrtOfCallCdd"]);
                            cargoInt.nxtPrtOfCallName = Convert.ToString(row["nxtPrtOfCallName"]);
                            cargoInt.modeOfTrnsprt = Convert.ToString(row["modeOfTrnsprt"]);

                            cargoItnry.Add(cargoInt);
                            cargo.cargoItnry.Add(cargoInt);

                        }
                    }

                    cargoDetails.Add(cargo);
                }
                master.cargoDetails = cargoDetails;

            }
            if (dtFileDetails != null)
            {
                foreach (DataRow row in dtFileDetails.Rows)
                {
                    custodianJson.fileName = Convert.ToString(row["FileName"]);
                    custodianJson.filePath = Convert.ToString(row["FilePath"]);
                    custodianJson.moveFilePath = Convert.ToString(row["MoveFilePath"]);
                    custodianJson.fileNo = Convert.ToInt32(row["FileNo"]);

                }
            }
            if (dtDigSign != null)
            {
                foreach (DataRow row in dtDigSign.Rows)
                {
                    digSign.startSignature = Convert.ToString(row["startSignature"]);
                    digSign.startCertificate = Convert.ToString(row["startCertificate"]);
                    digSign.signerVersion = Convert.ToString(row["signerVersion"]);
                }
            }
            eGM.headerField = headerField;
            eGM.master = master;
            //eGM.digSign = digSign;

            custodianJson.EGM = eGM;
            return custodianJson;
        }

        public AR.CustodianJson GenerateJsonAR(string FileType, string ContainerNo)
        {
            AR.CustodianJson custodianJson = new AR.CustodianJson();
            AR.EGM eGM = new AR.EGM();
            AR.HeaderField headerField = new AR.HeaderField();
            AR.Master master = new AR.Master();
            AR.Declaration declaration = new AR.Declaration();
            AR.Location location = new AR.Location();
            List<AR.CargoContainer> cargoContainers = new List<AR.CargoContainer>();
            List<AR.SupportingDocuments> supportingDocuments = new List<AR.SupportingDocuments>();
            AR.DigSign digSign = new AR.DigSign();
            AR.TransportMeansType transportMeansType = new AR.TransportMeansType();
            AR.Events events = new AR.Events();
            AR.CIM cIM = new AR.CIM();


            DataSet dataSet = new DataSet();
            dataSet = CustodianJsonDataLayer.GenerateJsonAR(FileType, ContainerNo);

            DataTable dtHeaderField = new DataTable();
            DataTable dtDeclaration = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCargoContainer = new DataTable();
            DataTable dtTransportMeans = new DataTable();
            DataTable dtEvents = new DataTable();
            DataTable dtCIM = new DataTable();
            DataTable dtSupportingDocuments = new DataTable();
            DataTable dtFileDetails = new DataTable();
            DataTable dtDigSign = new DataTable();

            dtHeaderField = dataSet.Tables[0];//single
            dtDeclaration = dataSet.Tables[1];//single
            dtLocation = dataSet.Tables[2];//single
            dtCargoContainer = dataSet.Tables[3];//list
            dtTransportMeans = dataSet.Tables[4];//single
            dtEvents = dataSet.Tables[5];//single
            dtCIM = dataSet.Tables[6];//single
            dtSupportingDocuments = dataSet.Tables[7];//list
            dtFileDetails = dataSet.Tables[8];//single
            dtDigSign = dataSet.Tables[9];//list


            if (dtHeaderField != null)
            {
                foreach (DataRow row in dtHeaderField.Rows)
                {
                    headerField.indicator = Convert.ToString(row["indicator"]);
                    headerField.messageID = Convert.ToString(row["messageID"]);
                    headerField.sequenceOrControlNumber = Convert.ToInt32(row["sequenceOrControlNumber"]);
                    headerField.date = Convert.ToString(row["date"]);
                    headerField.time = Convert.ToString(row["time"]);
                    headerField.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    headerField.senderID = Convert.ToString(row["senderID"]);
                    headerField.receiverID = Convert.ToString(row["receiverID"]);
                    headerField.versionNo = Convert.ToString(row["versionNo"]);
                }
            }
            if (dtDeclaration != null)
            {
                foreach (DataRow row in dtDeclaration.Rows)
                {
                    declaration.messageType = Convert.ToString(row["messageType"]);
                    declaration.portOfReporting = Convert.ToString(row["portOfReporting"]);
                    declaration.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    declaration.jobNo = Convert.ToInt32(row["jobNo"]);
                    declaration.jobDate = Convert.ToString(row["jobDate"]);
                    master.declaration = declaration;
                }
            }
            if (dtLocation != null)
            {
                foreach (DataRow row in dtLocation.Rows)
                {
                    location.reportingPartyType = Convert.ToString(row["reportingPartyType"]);
                    location.reportingPartyCode = Convert.ToString(row["reportingPartyCode"]);
                    location.nextDestinationOfUnlading = Convert.ToString(row["nextDestinationOfUnlading"]);
                    location.reportingLocationCode = Convert.ToString(row["reportingLocationCode"]);
                    location.authorisedPersonPAN = Convert.ToString(row["authorisedPersonPAN"]);
                    location.bondNumber = Convert.ToString(row["bondNumber"]);
                    master.location = location;
                }
            }
            if (dtCargoContainer != null)
            {
                foreach (DataRow row in dtCargoContainer.Rows)
                {

                    AR.CargoContainer cargoContainer = new AR.CargoContainer();

                    cargoContainer.messageType = Convert.ToString(row["messageType"]);
                    cargoContainer.equipmentSequenceNo = Convert.ToInt32(row["equipmentSequenceNo"]);
                    cargoContainer.containerID = Convert.ToString(row["containerID"]);
                    cargoContainer.equipmentType = Convert.ToString(row["equipmentType"]);
                    cargoContainer.equipmentSize = Convert.ToString(row["equipmentSize"]);
                    cargoContainer.equipmentLoadStatus = Convert.ToString(row["equipmentLoadStatus"]);
                    cargoContainer.equipmentStatus = Convert.ToString(row["equipmentStatus"]);

                    cargoContainers.Add(cargoContainer);

                }
            }
            master.cargoContainer = cargoContainers;
            if (dtTransportMeans != null)
            {
                foreach (DataRow row in dtTransportMeans.Rows)
                {


                    transportMeansType.transportMeansType = Convert.ToString(row["transportMeansType"]);
                    transportMeansType.transportMeansNumber = Convert.ToString(row["transportMeansNumber"]);
                    transportMeansType.totalEquipments = Convert.ToInt32(row["totalEquipments"]);

                }
            }
            master.transportMeans = transportMeansType;

            if (dtEvents != null)
            {
                foreach (DataRow row in dtEvents.Rows)
                {
                    events.actualTimeOfArrival = Convert.ToString(row["actualTimeOfArrival"]);

                }
            }
            master.events = events;

            if (dtCIM != null)
            {
                foreach (DataRow row in dtCIM.Rows)
                {
                    cIM.CIMNumber = Convert.ToInt32(row["CIMNumber"]);
                    cIM.CIMDate = Convert.ToString(row["CIMDate"]);
                }
            }

            master.CIM = cIM;
            if (dtSupportingDocuments != null)
            {
                foreach (DataRow row in dtSupportingDocuments.Rows)
                {
                    AR.SupportingDocuments supportingDocument = new AR.SupportingDocuments();

                    supportingDocument.messageType = Convert.ToString(row["messageType"]);
                    supportingDocument.equipmentSerialNumber = Convert.ToInt32(row["equipmentSerialNumber"]);
                    supportingDocument.documentSerialNumber = Convert.ToInt32(row["documentSerialNumber"]);
                    supportingDocument.documentTypeCode = Convert.ToString(row["documentTypeCode"]);
                    supportingDocument.ICEGATEUserID = Convert.ToString(row["ICEGATEUserID"]);
                    supportingDocument.IRNNumber = Convert.ToInt32(row["IRNNumber"]);
                    supportingDocument.documentReferenceNumber = Convert.ToString(row["documentReferenceNumber"]);
                    supportingDocuments.Add(supportingDocument);
                }
            }

            master.supportingDocuments = supportingDocuments;

            if (dtFileDetails != null)
            {
                foreach (DataRow row in dtFileDetails.Rows)
                {
                    custodianJson.fileName = Convert.ToString(row["FileName"]);
                    custodianJson.filePath = Convert.ToString(row["FilePath"]);
                    custodianJson.moveFilePath = Convert.ToString(row["MoveFilePath"]);
                    custodianJson.fileNo = Convert.ToInt32(row["FileNo"]);

                }
            }
            if (dtDigSign != null)
            {
                foreach (DataRow row in dtDigSign.Rows)
                {
                    digSign.startSignature = Convert.ToString(row["startSignature"]);
                    digSign.startCertificate = Convert.ToString(row["startCertificate"]);
                    digSign.signerVersion = Convert.ToString(row["signerVersion"]);
                }
            }
            eGM.headerField = headerField;
            eGM.master = master;
            eGM.digSign = digSign;

            custodianJson.EGM = eGM;
            return custodianJson;
        }

        public DP.CustodianJson GenerateJsonDP(string FileType, string ContainerNo)
        {
            DP.CustodianJson custodianJson = new DP.CustodianJson();
            DP.EGM eGM = new DP.EGM();
            DP.HeaderField headerField = new DP.HeaderField();
            DP.Master master = new DP.Master();
            DP.Declaration declaration = new DP.Declaration();
            DP.Location location = new DP.Location();
            List<DP.CargoContainer> cargoContainers = new List<DP.CargoContainer>();
            List<DP.CargoDocument> cargoDocument = new List<DP.CargoDocument>();
            List<DP.SupportingDocuments> supportingDocuments = new List<DP.SupportingDocuments>();
            DP.DigSign digSign = new DP.DigSign();
            DP.TransportMeansType transportMeansType = new DP.TransportMeansType();
            DP.Events events = new DP.Events();
            DP.CIM cIM = new DP.CIM();


            DataSet dataSet = new DataSet();
            dataSet = CustodianJsonDataLayer.GenerateJsonDP(FileType, ContainerNo);

            DataTable dtHeaderField = new DataTable();
            DataTable dtDeclaration = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCargoContainer = new DataTable();
            DataTable dtCargoDocument = new DataTable();
            DataTable dtTransportMeans = new DataTable();
            DataTable dtEvents = new DataTable();
            DataTable dtCIM = new DataTable();
            DataTable dtSupportingDocuments = new DataTable();
            DataTable dtFileDetails = new DataTable();
            DataTable dtDigSign = new DataTable();

            dtHeaderField = dataSet.Tables[0];//single
            dtDeclaration = dataSet.Tables[1];//single
            dtLocation = dataSet.Tables[2];//single
            dtCargoContainer = dataSet.Tables[3];//list
            dtCargoDocument = dataSet.Tables[4];//list
            dtTransportMeans = dataSet.Tables[5];//single
            dtEvents = dataSet.Tables[6];//single
            dtCIM = dataSet.Tables[7];//single
            dtSupportingDocuments = dataSet.Tables[8];//list
            dtFileDetails = dataSet.Tables[9];//single
            dtDigSign = dataSet.Tables[10];//list


            if (dtHeaderField != null)
            {
                foreach (DataRow row in dtHeaderField.Rows)
                {
                    headerField.indicator = Convert.ToString(row["indicator"]);
                    headerField.messageID = Convert.ToString(row["messageID"]);
                    headerField.sequenceOrControlNumber = Convert.ToInt32(row["sequenceOrControlNumber"]);
                    headerField.date = Convert.ToString(row["date"]);
                    headerField.time = Convert.ToString(row["time"]);
                    headerField.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    headerField.senderID = Convert.ToString(row["senderID"]);
                    headerField.receiverID = Convert.ToString(row["receiverID"]);
                    headerField.versionNo = Convert.ToString(row["versionNo"]);
                }
            }
            if (dtDeclaration != null)
            {
                foreach (DataRow row in dtDeclaration.Rows)
                {
                    declaration.messageType = Convert.ToString(row["messageType"]);
                    declaration.portOfReporting = Convert.ToString(row["portOfReporting"]);
                    declaration.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    declaration.jobNo = Convert.ToInt32(row["jobNo"]);
                    declaration.jobDate = Convert.ToString(row["jobDate"]);
                    master.declaration = declaration;
                }
            }
            if (dtLocation != null)
            {
                foreach (DataRow row in dtLocation.Rows)
                {
                    location.reportingPartyType = Convert.ToString(row["reportingPartyType"]);
                    location.reportingPartyCode = Convert.ToString(row["reportingPartyCode"]);
                    location.nextDestinationOfUnlading = Convert.ToString(row["nextDestinationOfUnlading"]);
                    location.reportingLocationCode = Convert.ToString(row["reportingLocationCode"]);
                    location.authorisedPersonPAN = Convert.ToString(row["authorisedPersonPAN"]);
                    location.bondNumber = Convert.ToString(row["bondNumber"]);
                    master.location = location;
                }
            }
            if (dtCargoContainer != null)
            {
                foreach (DataRow row in dtCargoContainer.Rows)
                {

                    DP.CargoContainer cargoContainer = new DP.CargoContainer();

                    cargoContainer.messageType = Convert.ToString(row["messageType"]);
                    cargoContainer.equipmentSequenceNo = Convert.ToInt32(row["equipmentSequenceNo"]);
                    cargoContainer.equipmentID = Convert.ToString(row["equipmentID"]);
                    cargoContainer.equipmentType = Convert.ToString(row["equipmentType"]);
                    cargoContainer.equipmentSize = Convert.ToString(row["equipmentSize"]);
                    cargoContainer.equipmentLoadStatus = Convert.ToString(row["equipmentLoadStatus"]);
                    //cargoContainer.equipmentStatus = Convert.ToString(row["equipmentStatus"]);
                    cargoContainer.equipmentSealType = Convert.ToString(row["equipmentSealType"]);
                    cargoContainer.equipmentSealNumber = Convert.ToString(row["equipmentSealNumber"]);
                    cargoContainer.finalDestinationLocation = Convert.ToString(row["finalDestinationLocation"]);

                    foreach (DataRow cargoRow in dtCargoDocument.Rows)
                    {
                        if (Convert.ToString(row["equipmentID"]) == Convert.ToString(cargoRow["containerID"]))
                        {
                            DP.CargoDocument cargo = new DP.CargoDocument();

                            cargo.messageType = Convert.ToString(cargoRow["messageType"]);
                            cargo.cargoSequenceNo = Convert.ToInt32(cargoRow["cargoSequenceNo"]);
                            cargo.documentType = Convert.ToString(cargoRow["documentType"]);
                            cargo.documentSite = Convert.ToString(cargoRow["documentSite"]);
                            cargo.documentNumber = Convert.ToInt32(cargoRow["documentNumber"]);
                            cargo.documentDate = Convert.ToString(cargoRow["documentDate"]);
                            cargo.shipmentLoadStatus = Convert.ToString(cargoRow["shipmentLoadStatus"]);
                            cargo.packageType = Convert.ToString(cargoRow["packageType"]);
                            cargo.MCINPCIN = Convert.ToString(cargoRow["MCINPCIN"]);

                            cargoContainer.cargoDocument.Add(cargo);
                        }
                    }

                    cargoContainers.Add(cargoContainer);

                }
            }
            master.cargoContainer = cargoContainers;
            if (dtTransportMeans != null)
            {
                foreach (DataRow row in dtTransportMeans.Rows)
                {


                    transportMeansType.transportMeansType = Convert.ToString(row["transportMeansType"]);
                    transportMeansType.transportMeansNumber = Convert.ToString(row["transportMeansNumber"]);
                    transportMeansType.totalEquipments = Convert.ToInt32(row["totalEquipments"]);

                }
            }
            master.transportMeans = transportMeansType;

            if (dtEvents != null)
            {
                foreach (DataRow row in dtEvents.Rows)
                {
                    events.expectedTimeOfDeparture = Convert.ToString(row["expectedTimeOfDeparture"]);
                    ///events.expectedTimeOfArrival = Convert.ToString(row["expectedTimeOfArrival"]);
                }
            }
            master.events = events;

            //if (dtCIM != null)
            //{
            //    foreach (DataRow row in dtCIM.Rows)
            //    {
            //        cIM.CIMNumber = Convert.ToInt32(row["CIMNumber"]);
            //        cIM.CIMDate = Convert.ToString(row["CIMDate"]);
            //    }
            //}

            if (dtFileDetails != null)
            {
                foreach (DataRow row in dtFileDetails.Rows)
                {
                    custodianJson.fileName = Convert.ToString(row["FileName"]);
                    custodianJson.filePath = Convert.ToString(row["FilePath"]);
                    custodianJson.moveFilePath = Convert.ToString(row["MoveFilePath"]);
                    custodianJson.fileNo = Convert.ToInt32(row["FileNo"]);

                }
            }
            //if (dtDigSign != null)
            //{
            //    foreach (DataRow row in dtDigSign.Rows)
            //    {
            //        digSign.startSignature = Convert.ToString(row["startSignature"]);
            //        digSign.startCertificate = Convert.ToString(row["startCertificate"]);
            //        digSign.signerVersion = Convert.ToString(row["signerVersion"]);
            //    }
            //}
            eGM.headerField = headerField;
            eGM.master = master;

            custodianJson.EGM = eGM;
            return custodianJson;
        }

        public DT.CustodianJson GenerateJsonDT(string FileType, string ContainerNo)
        {
            DT.CustodianJson custodianJson = new DT.CustodianJson();
            DT.EGM eGM = new DT.EGM();
            DT.HeaderField headerField = new DT.HeaderField();
            DT.Master master = new DT.Master();
            DT.Declaration declaration = new DT.Declaration();
            DT.Location location = new DT.Location();
            List<DT.CargoContainer> cargoContainers = new List<DT.CargoContainer>();
            List<DT.CargoDocument> cargoDocument = new List<DT.CargoDocument>();
            List<DT.SupportingDocuments> supportingDocuments = new List<DT.SupportingDocuments>();
            DT.DigSign digSign = new DT.DigSign();
            DT.TransportMeansType transportMeansType = new DT.TransportMeansType();
            DT.Events events = new DT.Events();
            DT.CIM CIM = new DT.CIM();


            DataSet dataSet = new DataSet();
            dataSet = CustodianJsonDataLayer.GenerateJsonDT(FileType, ContainerNo);

            DataTable dtHeaderField = new DataTable();
            DataTable dtDeclaration = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCargoContainer = new DataTable();
            DataTable dtCargoDocument = new DataTable();
            DataTable dtTransportMeans = new DataTable();
            DataTable dtEvents = new DataTable();
            DataTable dtCIM = new DataTable();
            DataTable dtSupportingDocuments = new DataTable();
            DataTable dtFileDetails = new DataTable();
            DataTable dtDigSign = new DataTable();

            dtHeaderField = dataSet.Tables[0];//single
            dtDeclaration = dataSet.Tables[1];//single
            dtLocation = dataSet.Tables[2];//single
            dtCargoContainer = dataSet.Tables[3];//list
            dtCargoDocument = dataSet.Tables[4];//list
            dtTransportMeans = dataSet.Tables[5];//single
            dtEvents = dataSet.Tables[6];//single
            dtCIM = dataSet.Tables[7];//single
            dtSupportingDocuments = dataSet.Tables[8];//list
            dtFileDetails = dataSet.Tables[9];//single
            dtDigSign = dataSet.Tables[10];//list


            if (dtHeaderField != null)
            {
                foreach (DataRow row in dtHeaderField.Rows)
                {
                    headerField.indicator = Convert.ToString(row["indicator"]);
                    headerField.messageID = Convert.ToString(row["messageID"]);
                    headerField.sequenceOrControlNumber = Convert.ToInt32(row["sequenceOrControlNumber"]);
                    headerField.date = Convert.ToString(row["date"]);
                    headerField.time = Convert.ToString(row["time"]);
                    headerField.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    headerField.senderID = Convert.ToString(row["senderID"]);
                    headerField.receiverID = Convert.ToString(row["receiverID"]);
                    headerField.versionNo = Convert.ToString(row["versionNo"]);
                }
            }
            if (dtDeclaration != null)
            {
                foreach (DataRow row in dtDeclaration.Rows)
                {
                    declaration.messageType = Convert.ToString(row["messageType"]);
                    declaration.portOfReporting = Convert.ToString(row["portOfReporting"]);
                    declaration.reportingEvent = Convert.ToString(row["reportingEvent"]);
                    declaration.jobNo = Convert.ToInt32(row["jobNo"]);
                    declaration.jobDate = Convert.ToString(row["jobDate"]);
                    master.declaration = declaration;
                }
            }
            if (dtLocation != null)
            {
                foreach (DataRow row in dtLocation.Rows)
                {
                    location.reportingPartyType = Convert.ToString(row["reportingPartyType"]);
                    location.reportingPartyCode = Convert.ToString(row["reportingPartyCode"]);
                    location.nextDestinationOfUnlading = Convert.ToString(row["nextDestinationOfUnlading"]);
                    location.reportingLocationCode = Convert.ToString(row["reportingLocationCode"]);
                    location.reportingLocationName = Convert.ToString(row["reportingLocationName"]);
                    location.authorisedPersonPAN = Convert.ToString(row["authorisedPersonPAN"]);
                    
                    master.location = location;
                }
            }
            if (dtCargoContainer != null)
            {
                foreach (DataRow row in dtCargoContainer.Rows)
                {

                    DT.CargoContainer cargoContainer = new DT.CargoContainer();

                    cargoContainer.messageType = Convert.ToString(row["messageType"]);
                    cargoContainer.equipmentSequenceNo = Convert.ToInt32(row["equipmentSequenceNo"]);
                    cargoContainer.equipmentID = Convert.ToString(row["equipmentID"]);
                    cargoContainer.equipmentType = Convert.ToString(row["equipmentType"]);
                    cargoContainer.equipmentSize = Convert.ToString(row["equipmentSize"]);
                    cargoContainer.equipmentLoadStatus = Convert.ToString(row["equipmentLoadStatus"]);
                    //cargoContainer.equipmentStatus = Convert.ToString(row["equipmentStatus"]);
                    cargoContainer.equipmentSealType = Convert.ToString(row["equipmentSealType"]);
                    cargoContainer.equipmentSealNumber = Convert.ToString(row["equipmentSealNumber"]);
                    cargoContainer.finalDestinationLocation = Convert.ToString(row["finalDestinationLocation"]);

                    foreach (DataRow cargoRow in dtCargoDocument.Rows)
                    {
                        if (Convert.ToString(row["equipmentID"]) == Convert.ToString(cargoRow["containerID"]))
                        {
                            DT.CargoDocument cargo = new DT.CargoDocument();

                            cargo.messageType = Convert.ToString(cargoRow["messageType"]);
                            cargo.cargoSequenceNo = Convert.ToInt32(cargoRow["cargoSequenceNo"]);
                            cargo.documentType = Convert.ToString(cargoRow["documentType"]);
                            cargo.documentSite = Convert.ToString(cargoRow["documentSite"]);
                            cargo.documentNumber = Convert.ToInt32(cargoRow["documentNumber"]);
                            cargo.documentDate = Convert.ToString(cargoRow["documentDate"]);
                            cargo.shipmentLoadStatus = Convert.ToString(cargoRow["shipmentLoadStatus"]);
                            cargo.packageType = Convert.ToString(cargoRow["packageType"]);
                            cargo.MCINPCIN = Convert.ToString(cargoRow["MCINPCIN"]);

                            cargoContainer.cargoDocument.Add(cargo);
                        }
                    }

                    cargoContainers.Add(cargoContainer);

                }
            }
            master.cargoContainer = cargoContainers;
            if (dtTransportMeans != null)
            {
                foreach (DataRow row in dtTransportMeans.Rows)
                {


                    transportMeansType.transportMeansType = Convert.ToString(row["transportMeansType"]);
                    transportMeansType.transportMeansNumber = Convert.ToString(row["transportMeansNumber"]);
                    transportMeansType.totalEquipments = Convert.ToString(row["totalEquipments"]);

                }
            }
            master.transportMeans = transportMeansType;

            if (dtEvents != null)
            {
                foreach (DataRow row in dtEvents.Rows)
                {
                    events.expectedTimeOfDeparture = Convert.ToString(row["expectedTimeOfDeparture"]);
                    ///events.expectedTimeOfArrival = Convert.ToString(row["expectedTimeOfArrival"]);
                }
            }
            master.events = events;

            if (dtCIM != null)
            {
                foreach (DataRow row in dtCIM.Rows)
                {
                    CIM.CIMNumber = Convert.ToInt32(row["CIMNumber"]);
                    CIM.CIMDate = Convert.ToString(row["CIMDate"]);
                }
            }
            master.CIM = CIM;
            if (dtFileDetails != null)
            {
                foreach (DataRow row in dtFileDetails.Rows)
                {
                    custodianJson.fileName = Convert.ToString(row["FileName"]);
                    custodianJson.filePath = Convert.ToString(row["FilePath"]);
                    custodianJson.moveFilePath = Convert.ToString(row["MoveFilePath"]);
                    custodianJson.fileNo = Convert.ToInt32(row["FileNo"]);

                }
            }
            //if (dtDigSign != null)
            //{
            //    foreach (DataRow row in dtDigSign.Rows)
            //    {
            //        digSign.startSignature = Convert.ToString(row["startSignature"]);
            //        digSign.startCertificate = Convert.ToString(row["startCertificate"]);
            //        digSign.signerVersion = Convert.ToString(row["signerVersion"]);
            //    }
            //}
            eGM.headerField = headerField;
            eGM.master = master;

            custodianJson.EGM = eGM;
            return custodianJson;
        }
    }
}
