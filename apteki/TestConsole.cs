using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class TestConsole
{
    static void Main(string[] args)
    {
        DB_Logic sqlWrapper = new DB_Logic();

        //Добавляем аптеки
        int intApteka1Id = sqlWrapper.insert_into_apteks("Тестовая аптека 1", "Тестовый адрес 1", "Тестовый телефон 1");
        int intApteka2Id = sqlWrapper.insert_into_apteks("Тестовая аптека 2", "Тестовый адрес 2", "Тестовый телефон 2");

        //Добавляем продукты
        long lngProduct1Id = sqlWrapper.insert_into_products("Тестовый продукт 1");
        long lngProduct2Id = sqlWrapper.insert_into_products("Тестовый продукт 2");
        long lngProduct3Id = sqlWrapper.insert_into_products("Тестовый продукт 3");

        //Добавляем склады
        int intStore1Id = sqlWrapper.insert_into_stores(intApteka1Id, "Тестовый склад 1");
        int intStore2Id = sqlWrapper.insert_into_stores(intApteka1Id, "Тестовый склад 2");
        int intStore3Id = sqlWrapper.insert_into_stores(intApteka2Id, "Тестовый склад 3");

        //Добавляем партии товаров по складам
        int intConsignment1Id = sqlWrapper.insert_into_consignments(lngProduct1Id, intStore1Id, 5);
        int intConsignment2Id = sqlWrapper.insert_into_consignments(lngProduct2Id, intStore1Id, 6);
        int intConsignment3Id = sqlWrapper.insert_into_consignments(lngProduct3Id, intStore1Id, 2);
        int intConsignment4Id = sqlWrapper.insert_into_consignments(lngProduct1Id, intStore2Id, 1);
        int intConsignment5Id = sqlWrapper.insert_into_consignments(lngProduct2Id, intStore2Id, 2);
        int intConsignment6Id = sqlWrapper.insert_into_consignments(lngProduct3Id, intStore2Id, 3);

        //Вывести на экран весь список товаров и его количество в выбранной аптеке(количество товара во всех складах аптеки)
        SqlCommand sqlCommand = null; SqlConnection sqlConnection = null; SqlDataReader rd;
        SqlParameter id_apteka = new SqlParameter("@id_apteka", SqlDbType.Int);
        
        //Задаем, по какой аптеке считаем
        id_apteka.Value = intApteka1Id;

        Console.WriteLine("Вывести на экран весь список товаров и его количество в выбранной аптеке(количество товара во всех складах аптеки)");

        sqlConnection = null;
        sqlCommand = null;
        sqlConnection = new SqlConnection(sqlWrapper.strConn);
        sqlCommand = new SqlCommand("SELECT products.product_name, consignments.id_product, SUM(consignments.product_count) AS SUM_product_count FROM consignments INNER JOIN products ON consignments.id_product = products.id_product INNER JOIN stores ON consignments.id_store = stores.id_store WHERE stores.id_apteka = @id_apteka GROUP BY products.product_name, consignments.id_product ORDER BY products.product_name", sqlConnection);
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
                    Console.WriteLine(rd["product_name"].ToString() + "\t" + rd["SUM_product_count"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString().Trim());
        }
        finally
        {
            if (sqlConnection != null) { sqlConnection.Close(); sqlConnection.Dispose(); }
        }

        //Удаление может быть оргнизовано явно (через запросы) или через каскадное удаление записей в связанных таблицах (схема БД имеется и каскадное удаление включено)

        //Ниже представлено явное удаление
        //Удаляем партию товаров
        sqlWrapper.delete_from_consignments(intConsignment1Id);

        //Удаляем товар
        sqlWrapper.delete_from_products(lngProduct1Id);

        //Удаляем склад
        sqlWrapper.delete_from_stores(intStore3Id);

        //Удаляем аптеку
        sqlWrapper.delete_from_apteks(intApteka1Id);

        Console.ReadKey();
    }
}

