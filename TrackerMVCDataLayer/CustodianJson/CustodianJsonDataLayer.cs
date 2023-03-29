using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BombayToolsDataLayer.CustodianJson
{
    public class CustodianJsonDataLayer
    {
        string sqlCommandString;
        public DataSet GenerateJsonSF(string FileType, string ContainerNo)
        {
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCustodianJsonEGMSF";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FileType", FileType);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);

                    DataSet dtItemList = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtItemList);
                    }
                    connection.Close();
                    return dtItemList;

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
        public DataSet GenerateJsonASR(string FileType, string ContainerNo, string FilterType)
        {
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCustodianJsonEGMASR";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FileType", FileType);
                    objCommand.Parameters.AddWithValue("@SearchNo", ContainerNo);
                    objCommand.Parameters.AddWithValue("@FilterType", FilterType);

                    DataSet dtItemList = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtItemList);
                    }
                    connection.Close();
                    return dtItemList;

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

        public DataSet GenerateJsonAR(string FileType, string ContainerNo)
        {
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCustodianJsonEGMAR";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FileType", FileType);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);

                    DataSet dtItemList = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtItemList);
                    }
                    connection.Close();
                    return dtItemList;

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

        public DataSet GenerateJsonDP(string FileType, string ContainerNo)
        {
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCustodianJsonEGMDP";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FileType", FileType);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);

                    DataSet dtItemList = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtItemList);
                    }
                    connection.Close();
                    return dtItemList;

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

        public DataSet GenerateJsonDT(string FileType, string ContainerNo)
        {
            TrackerMVCDBConnector.DBConnection objConn = new TrackerMVCDBConnector.DBConnection();
            using (SqlConnection connection = objConn.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    sqlCommandString = "USP_GetCustodianJsonEGMDT";
                    SqlCommand objCommand = new SqlCommand(sqlCommandString, connection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@FileType", FileType);
                    objCommand.Parameters.AddWithValue("@ContainerNo", ContainerNo);

                    DataSet dtItemList = new DataSet();

                    using (SqlDataAdapter _Data = new SqlDataAdapter())
                    {
                        _Data.SelectCommand = objCommand;

                        _Data.Fill(dtItemList);
                    }
                    connection.Close();
                    return dtItemList;

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
