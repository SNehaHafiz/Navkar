using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DB = TrackerMVCDBConnector;

namespace TrackerMVCDataLayer.TicketSystem
{
    public class TicketSystemDataLayer
    {
        public DataTable AjaxAddOrEditTicketData(string MailID, int TktType, string Subject, string Description,
            int AddedBy, string AddedOn, DataTable TktAttachment)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_AddOrEditRaisedTktData";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@MailID", MailID);
                    objCommand.Parameters.AddWithValue("@TktType", TktType);
                    objCommand.Parameters.AddWithValue("@Subject", Subject);
                    objCommand.Parameters.AddWithValue("@Description", Description);
                    objCommand.Parameters.AddWithValue("@AddedBy", AddedBy);
                    objCommand.Parameters.AddWithValue("@AddedOn", AddedOn);

                    if (TktAttachment != null)
                    {
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "@PT_RaisedTktAttachments";
                        param1.Value = TktAttachment;
                        param1.TypeName = "PT_RaisedTktAttachments";
                        param1.SqlDbType = SqlDbType.Structured;
                        objCommand.Parameters.Add(param1);

                    }
                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxGetMailSettingDetail(string Issues)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "Usp_Mail_Settings_Date_Fill";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@Issues", Issues);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxDeleteTicketData(string ID, string UserID, string Reason)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_TicketClosedDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ID", ID);
                    objCommand.Parameters.AddWithValue("@UserID", UserID);
                    objCommand.Parameters.AddWithValue("@Reason", Reason);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxGetOpenTicketData(string UserID)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_OPEN_TICKET";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@UserID", UserID);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxGetClosedTicketData(string FromDate, string ToDate)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_Closed_TICKET";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@FromDate", FromDate);
                    objCommand.Parameters.AddWithValue("@ToDate", ToDate);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxApproveTicketData(string ID, string UserID)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_TicketApprovedDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ID", ID);
                    objCommand.Parameters.AddWithValue("@UserID", UserID);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxUpdateTicketTypeDetails(string ID, string UserID, int TktType)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_UpdateTicketTypeDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ID", ID);
                    objCommand.Parameters.AddWithValue("@UserID", UserID);
                    objCommand.Parameters.AddWithValue("@TktType", TktType);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxGetDashboardTicketData(int UserID)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetTotalDashboardCount";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@UserID", UserID);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataSet GetDetailsForTicket(int ID)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetTicketDetails";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ID", ID);

                    DataSet dtLoginDetails = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxDashboardListAgainstType(string FromDate, string ToDate, string Type)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetDashboardTicketTypeWiseSummary";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@FromDate", FromDate);
                    objCommand.Parameters.AddWithValue("@ToDate", ToDate);
                    objCommand.Parameters.AddWithValue("@Type", Type);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }
        public DataTable AjaxTicketListAgainstKeyword(string FromDate, string ToDate, string searchText)
        {
            DB.DBConnection objConn = new DB.DBConnection();
            string sqlCommandString;

            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetKeywordWiseTicketSummary";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@FromDate", FromDate);
                    objCommand.Parameters.AddWithValue("@ToDate", ToDate);
                    objCommand.Parameters.AddWithValue("@SearchText", searchText);

                    DataTable dtLoginDetails = new DataTable();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;
                        _Data.Fill(dtLoginDetails);
                    }
                    connection.Close();
                    return dtLoginDetails;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw ex;
                }
            }

        }

    }   
}
