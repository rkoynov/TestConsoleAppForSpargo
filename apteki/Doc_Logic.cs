using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public class Doc_Logic
{
    /*Свойство - строка подключения. Инициализируется при конструировании класса из файла Web.config. Клнструктор берет строку по ее имени. */
    public DataTable getRequest(string strQuery, out string strError, int intTimeout = 30)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd; DB_Logic sqlWrapper = new DB_Logic();   /* Строка поключения по умолчанию в конструкторе */
        int intErrorCount = 0;
        do
        {
            strError = "";
            DataTable dt = new DataTable("result");
            sqlConnection = null;
            sqlCommand = null;
            //Поиск данных в БД
            sqlConnection = new SqlConnection(sqlWrapper.strConn);

            sqlCommand = new SqlCommand(strQuery, sqlConnection);
            sqlCommand.CommandTimeout = intTimeout;
            try
            {
                sqlConnection.Open();
                rd = sqlCommand.ExecuteReader();
                dt.Load(rd);
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                return dt;
            }
            catch (Exception ex)
            {
                intErrorCount++;
                strError = ex.Message;
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
            }
            finally
            {
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
            }
        }
        while (intErrorCount < 5);
        return null;
    }
    public long doInsert(string strQuery, out string strError, int intTimeout = 30)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; DB_Logic sqlWrapper = new DB_Logic(); SqlDataReader rd;  /* Строка поключения по умолчанию в конструкторе */
        int intNumberOfRecords = 0;
        int intErrorCount = 0;
        long lngRecordId = 0;

        intErrorCount = 0;
        do
        {
            sqlConnection = null;
            sqlCommand = null;
            sqlConnection = new SqlConnection(sqlWrapper.strConn);
            strQuery += @"; SELECT @@ROWCOUNT AS Количество_добавленных_записей, @@IDENTITY AS Код_добавленной_записи;";
            sqlCommand = new SqlCommand(strQuery, sqlConnection);
            sqlCommand.CommandTimeout = intTimeout;
            try
            {
                sqlConnection.Open();
                rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd["Количество_добавленных_записей"].ToString()))
                        {
                            intNumberOfRecords = Convert.ToInt32(rd["Количество_добавленных_записей"]);
                        }
                        else
                        {
                            intNumberOfRecords = 0;
                        }
                        if (intNumberOfRecords == 0)
                        {
                            intErrorCount++;
                            if (intErrorCount == 5)
                            {
                                strError = "Ошибка сохранения данных!";
                                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                                return 0;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(rd["Код_добавленной_записи"].ToString()))
                            {
                                strError = "";
                                lngRecordId = Convert.ToInt64(rd["Код_добавленной_записи"]);
                                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                                return lngRecordId;
                            }
                            else
                            {
                                strError = "Ошибка поиска последнего значения кода записи!";
                                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strError = "Ошибка сохранения данных! " + ex.Message;
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                return 0;
            }
            finally
            {
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
            }
        }
        while (intNumberOfRecords == 0 && intErrorCount <= 5);

        strError = "Ошибка сохранения данных!";
        if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        return 0;
    }
    public int doUpdate(string strQuery, out string strError, int intTimeout = 30)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; DB_Logic sqlWrapper = new DB_Logic(); SqlDataReader rd;  /* Строка поключения по умолчанию в конструкторе */
        int intNumberOfRecords = 0;

        do
        {
            sqlConnection = null;
            sqlCommand = null;
            sqlConnection = new SqlConnection(sqlWrapper.strConn);
            strQuery += @"; SELECT @@ROWCOUNT AS Количество_обновленных_записей;";
            sqlCommand = new SqlCommand(strQuery, sqlConnection);
            sqlCommand.CommandTimeout = intTimeout;
            try
            {
                sqlConnection.Open();
                rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd["Количество_обновленных_записей"].ToString()))
                        {
                            intNumberOfRecords = Convert.ToInt32(rd["Количество_обновленных_записей"]);
                        }
                        else
                        {
                            intNumberOfRecords = 0;
                        }

                        strError = "";
                        if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                        return intNumberOfRecords;
                    }
                }
            }
            catch (Exception ex)
            {
                strError = "Ошибка сохранения данных! " + ex.Message;
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                return 0;
            }
            finally
            {
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
            }
        }
        while (intNumberOfRecords == 0);

        strError = "Ошибка сохранения данных!";
        if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        return 0;
    }
    public int doDelete(string strQuery, out string strError, int intTimeout = 30)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; DB_Logic sqlWrapper = new DB_Logic(); SqlDataReader rd;  /* Строка поключения по умолчанию в конструкторе */
        int intNumberOfRecords = 0;

        do
        {
            sqlConnection = null;
            sqlCommand = null;
            sqlConnection = new SqlConnection(sqlWrapper.strConn);
            strQuery += @"; SELECT @@ROWCOUNT AS Количество_удаленных_записей;";
            sqlCommand = new SqlCommand(strQuery, sqlConnection);
            sqlCommand.CommandTimeout = intTimeout;
            try
            {
                sqlConnection.Open();
                rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                        {
                            intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                        }
                        else
                        {
                            intNumberOfRecords = 0;
                        }

                        strError = "";
                        if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                        return intNumberOfRecords;
                    }
                }
            }
            catch (Exception ex)
            {
                strError = "Ошибка удаления данных! " + ex.Message;
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
                return 0;
            }
            finally
            {
                if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
            }
        }
        while (intNumberOfRecords == 0);

        strError = "Ошибка удаления данных!";
        if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        return 0;
    }
}