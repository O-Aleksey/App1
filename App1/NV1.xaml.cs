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
        public static double height = 50; //Высота строк
        string date;
        double Value;
        string choose; // Для хранения выбраного значения picker
        string chose; // Для передачи значения picker в удобном виде в следующий метод
        MainPage Main_Page = new MainPage();        

        public NV1()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh() // Метод обновления значений
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
                        object name = reader["name"]; // название столбца с именами строк
                        object value = reader["value"]; // название столбца с хранимыми значениями
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

        private void picker1_SelectedIndexChanged(object sender, EventArgs e) // Метод обработки выставления оценки
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

            Main_Page.Changing(chose, "clean     ", "1"); // Передаём название строки и номер магазина
            Refresh();
        }

        private void picker2_SelectedIndexChanged(object sender, EventArgs e) // Метод обработки выставления оценки
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

            Main_Page.Changing(chose, "smail     ", "1"); // Передаём название строки и номер магазина
            Refresh();
        }
    }
}