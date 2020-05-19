using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NV1 : ContentPage
    {
        public int shops = 2; // Количество магазинов
        public static double height = 50;
        string date;
        double Value;
        string choose;
        string chose;
        MainPage Main_Page = new MainPage();        

        public NV1()
        {
            InitializeComponent();
            Refresh();
        }
        void ConnectWithDB()
        {
            // Строка подключения к SQL
            string connectionString = Main_Page.connection_String;
            // Команда-запрос в SQL
            //string sqlExpression = "INSERT INTO Shop_1 (name, value) VALUES ('mobile', 4)"; //Создание
            //string sqlExpression = "UPDATE Shop_1 SET value=5 WHERE id=1003"; // Обновление
            //string sqlExpression = "DELETE FROM Shop_1 WHERE Name='mobile'"; // Удаление
            string sqlExpression = "SELECT * FROM Shop_1"; // Выбор для чтения "*" - всех из "название таблицы"
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        object name = reader["name"]; // название столбца
                        object value = reader["value"]; // название столбца

                        if (name.ToString() == "polki     ") // В SQL тип столбца nchar(10) означает что к значениям будут добавляться пробелы до 10 символов
                        {
                            date = DateTime.Now.ToString("dd.MM.yyyy");                            
                        }

                    }
                }

                reader.Close();
                //command.ExecuteNonQuery();
            }
        }

        

        private void Refresh()
        {            
            // Строка подключения к SQL
            string connectionString = Main_Page.connection_String;
            // Команда-запрос в SQL
            string sqlExpression = "SELECT * FROM Shop_1"; // Выбор для чтения "*" - всех из "название таблицы"
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        object name = reader["name"]; // название столбца
                        object value = reader["value"]; // название столбца
                        switch (name)
                        {
                            case "clean     ":
                                Value = Convert.ToDouble(value);
                                label11.Text = Value.ToString("0.0");
                                break;
                            case "smail     ":
                                Value = Convert.ToDouble(value);
                                label12.Text = Value.ToString("0.0");
                                break;
                            default:
                                break;
                        }
                    }
                }
                reader.Close();                
            }
        }

        private void button_refresh_Clicked(object sender, EventArgs e)
        {
            Refresh();
        }

        private void picker1_SelectedIndexChanged(object sender, EventArgs e)
        {

            choose = picker1.Items[picker1.SelectedIndex];
            switch (choose)
            {
                case "1 балл":
                    chose = "1";
                    break;
                case "2 балла":
                    chose = "2";
                    break;
                case "3 балла":
                    chose = "3";
                    break;
                case "4 балла":
                    chose = "4";
                    break;
                case "5 баллов":
                    chose = "5";
                    break;
            }

            Main_Page.Changing(chose, "clean     ", "1");
            Refresh();
        }

        private void picker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            choose = picker2.Items[picker2.SelectedIndex];
            switch (choose)
            {
                case "1 балл":
                    chose = "1";
                    break;
                case "2 балла":
                    chose = "2";
                    break;
                case "3 балла":
                    chose = "3";
                    break;
                case "4 балла":
                    chose = "4";
                    break;
                case "5 баллов":
                    chose = "5";
                    break;
            }

            Main_Page.Changing(chose, "smail     ", "1");
            Refresh();
        }
    }
}