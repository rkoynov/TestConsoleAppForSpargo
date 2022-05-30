using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Data;
using System.Configuration;
/// <summary>
/// Сводное описание для DB_Logic
/// </summary>
public class DB_Logic
{
    /*Свойство - строка подключения. Инициализируется при конструировании класса из файла App.config. Клнструктор берет строку по ее имени. */
    public string strConn { get; private set; }

    /* Конструктор. Берем из файла App.Config  */

    public DB_Logic()
    {
        strConn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    }
    public int insert_into_stores(int intAptekaId, string strStoreName)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_apteka = new SqlParameter("@id_apteka", SqlDbType.Int);
        SqlParameter store_name = new SqlParameter("@store_name", SqlDbType.NVarChar);
        id_apteka.Value = intAptekaId;
        store_name.Value = strStoreName;
        int intNumberOfRecords = 0;
        int intRecordId = 0;

        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("INSERT INTO [stores] ([id_apteka], [store_name]) VALUES (@id_apteka, @store_name); SELECT @@ROWCOUNT AS Количество_добавленных_записей, @@IDENTITY AS Код_добавленной_записи;", sqlConnection);
        sqlCommand.Parameters.Add(id_apteka);
        sqlCommand.Parameters.Add(store_name);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_добавленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_добавленных_записей"]);
                    }
                    if (!string.IsNullOrEmpty(rd["Код_добавленной_записи"].ToString()))
                    {
                        intRecordId = Convert.ToInt32(rd["Код_добавленной_записи"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод insert_into_stores: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intRecordId;
    }
    public int insert_into_consignments(long lngProductId, int intStoreId, int intProductCount)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_product = new SqlParameter("@id_product", SqlDbType.BigInt);
        SqlParameter id_store = new SqlParameter("@id_store", SqlDbType.Int);
        SqlParameter product_count = new SqlParameter("@product_count", SqlDbType.Int);
        id_product.Value = lngProductId;
        id_store.Value = intStoreId;
        product_count.Value = intProductCount;
        int intNumberOfRecords = 0;
        int intRecordId = 0;

        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("INSERT INTO [consignments] ([id_product], [id_store], [product_count]) VALUES (@id_product, @id_store, @product_count); SELECT @@ROWCOUNT AS Количество_добавленных_записей, @@IDENTITY AS Код_добавленной_записи;", sqlConnection);
        sqlCommand.Parameters.Add(id_product);
        sqlCommand.Parameters.Add(id_store);
        sqlCommand.Parameters.Add(product_count);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_добавленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_добавленных_записей"]);
                    }
                    if (!string.IsNullOrEmpty(rd["Код_добавленной_записи"].ToString()))
                    {
                        intRecordId = Convert.ToInt32(rd["Код_добавленной_записи"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод insert_into_products_by_consignments: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intRecordId;
    }
    public long insert_into_products(string strProductName)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.NVarChar);
        product_name.Value = strProductName;
        int intNumberOfRecords = 0;
        long lngRecordId = 0;

        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("INSERT INTO [products] ([product_name]) VALUES (@product_name); SELECT @@ROWCOUNT AS Количество_добавленных_записей, @@IDENTITY AS Код_добавленной_записи;", sqlConnection);
        sqlCommand.Parameters.Add(product_name);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_добавленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_добавленных_записей"]);
                    }
                    if (!string.IsNullOrEmpty(rd["Код_добавленной_записи"].ToString()))
                    {
                        lngRecordId = Convert.ToInt64(rd["Код_добавленной_записи"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод insert_into_products: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return lngRecordId;
    }
    public int insert_into_apteks(string strAptekaName, string strAptekaAddress, string strAptekaTelephone)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter apteka_name = new SqlParameter("@apteka_name", SqlDbType.NVarChar);
        SqlParameter apteka_address = new SqlParameter("@apteka_address", SqlDbType.NVarChar);
        SqlParameter apteka_telephone = new SqlParameter("@apteka_telephone", SqlDbType.NVarChar);
        apteka_name.Value = strAptekaName;
        apteka_address.Value = strAptekaAddress;
        apteka_telephone.Value = strAptekaTelephone;
        int intNumberOfRecords = 0;
        int intRecordId = 0;

        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("INSERT INTO [apteks] ([apteka_name], [apteka_address], [apteka_telephone]) VALUES (@apteka_name, @apteka_address, @apteka_telephone); SELECT @@ROWCOUNT AS Количество_добавленных_записей, @@IDENTITY AS Код_добавленной_записи;", sqlConnection);
        sqlCommand.Parameters.Add(apteka_name);
        sqlCommand.Parameters.Add(apteka_address);
        sqlCommand.Parameters.Add(apteka_telephone);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_добавленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_добавленных_записей"]);
                    }
                    if (!string.IsNullOrEmpty(rd["Код_добавленной_записи"].ToString()))
                    {
                        intRecordId = Convert.ToInt32(rd["Код_добавленной_записи"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод insert_into_apteks: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intRecordId;
    }

    public int delete_from_apteks(int intAptekaId)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_apteka = new SqlParameter("@id_apteka", SqlDbType.Int);
        id_apteka.Value = intAptekaId;
        int intNumberOfRecords = 0;

        //Удаляем партии в складах
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [consignments] WHERE id_store IN (SELECT id_store FROM stores WHERE id_apteka = @id_apteka); SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_apteka);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_apteks: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        //Удаляем склады
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM stores WHERE id_apteka = @id_apteka; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_apteka);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_apteks: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        //Удаляем аптеку
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [apteks] WHERE id_apteka = @id_apteka; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_apteka);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_apteks: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intNumberOfRecords;
    }
    public int delete_from_products(long lngProductId)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_product = new SqlParameter("@id_product", SqlDbType.BigInt);
        id_product.Value = lngProductId;
        int intNumberOfRecords = 0;

        //Удаляем партии в складах
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [consignments] WHERE id_product = @id_product; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_product);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_products: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        //Удаляем товар
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [products] WHERE id_product = @id_product; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_product);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_products: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intNumberOfRecords;
    }
    public int delete_from_stores(int intStoreId)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_store = new SqlParameter("@id_store", SqlDbType.Int);
        id_store.Value = intStoreId;
        int intNumberOfRecords = 0;

        //Удаляем партии в складах
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [consignments] WHERE id_store = @id_store; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_store);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_stores: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        //Удаляем товар
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [stores] WHERE id_store = @id_store; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_store);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_stores: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intNumberOfRecords;
    }
    public int delete_from_consignments(int intConsignmentId)
    {
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_consignment = new SqlParameter("@id_consignment", SqlDbType.Int);
        id_consignment.Value = intConsignmentId;
        int intNumberOfRecords = 0;

        //Удаляем партию
        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(this.strConn);
        sqlCommand = new SqlCommand("DELETE FROM [consignments] WHERE id_consignment = @id_consignment; SELECT @@ROWCOUNT AS Количество_удаленных_записей;", sqlConnection);
        sqlCommand.Parameters.Add(id_consignment);
        try
        {
            sqlConnection.Open();
            rd = sqlCommand.ExecuteReader();
            sqlCommand.Parameters.Clear();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (!string.IsNullOrEmpty(rd["Количество_удаленных_записей"].ToString()))
                    {
                        intNumberOfRecords = Convert.ToInt32(rd["Количество_удаленных_записей"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Метод delete_from_consignments: " + ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        return intNumberOfRecords;
    }
}