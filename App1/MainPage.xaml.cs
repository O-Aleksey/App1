using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int shops = 2; // Количество магазинов
        public static double height = 50;
        string date;
        Label[] labels = new Label[3];
        public string connection_String = @"Data Source = 192.168.1.10, 1433; Initial Catalog = testbd; Persist Security Info=True;User ID = sa; Password=321"; // Строка подключения к SQL
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        public MainPage()
        {
            InitializeComponent();
            
            labels[1] = label1;
            labels[2] = label2;
            Refresh();
            nfi.NumberDecimalSeparator = ".";
        }

        public void Changing(string inputValue, string nameFieldSQL, string shopNumber)
        {
            int sum_;            
            int number_;
            int input_;
            double value;
            string commandString;

            input_ = Convert.ToInt32(inputValue);
            // Строка подключения к SQL
            string connectionString = connection_String;
            // Команда-запрос в SQL
            commandString = ("SELECT * FROM Shop_" + shopNumber); // Выбор для чтения "*" - всех из "название таблицы"
            string sqlExpression = commandString;
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
                        object sum = reader["sum"]; // название столбца
                        object number = reader["number"]; // название столбца
                        sum_ = Convert.ToInt32(sum);
                        number_ = Convert.ToInt32(number);

                        if (nameFieldSQL == name.ToString())
                        {
                            number_++;
                            sum_ = sum_ + input_;
                            value = (double)sum_ / (double)number_;
                            reader.Close();
                            Write(shopNumber, nameFieldSQL, value, input_, number_, sum_);
                            break;
                        }
                    }                    
                }
                reader.Close();
            }
        }

        public void Write(string shopNumber, string nameFieldSQL, double value, int l_value, int number, int sum)
        {
            String value_;
            value_ = value.ToString("G3", nfi);
            string commandString;
            // Строка подключения к SQL
            string connectionString = connection_String;
            // Команда-запрос в SQL
            commandString = ("UPDATE Shop_" + shopNumber + " SET value=" + value_ + " WHERE name='" + nameFieldSQL + "'");
            string sqlExpression1 = commandString;
            commandString = ("UPDATE Shop_" + shopNumber + " SET l_value=" + l_value + " WHERE name='" + nameFieldSQL + "'");
            string sqlExpression2 = commandString;
            commandString = ("UPDATE Shop_" + shopNumber + " SET number=" + number + " WHERE name='" + nameFieldSQL + "'");
            string sqlExpression3 = commandString;
            commandString = ("UPDATE Shop_" + shopNumber + " SET sum=" + sum + " WHERE name='" + nameFieldSQL + "'");
            string sqlExpression4 = commandString;
            //string sqlExpression = "DELETE FROM Shop_1 WHERE Name='mobile'"; // Удаление

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                command1.ExecuteNonQuery();
                SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                command2.ExecuteNonQuery();
                SqlCommand command3 = new SqlCommand(sqlExpression3, connection);
                command3.ExecuteNonQuery();
                SqlCommand command4 = new SqlCommand(sqlExpression4, connection);
                command4.ExecuteNonQuery();
            }
        }

        // Метод обновления значений на главном экране
        public void Refresh()
        {
            string nameTable; // Для хранения строки SQL запроса
            
            double middleScore = 0; // переменная среднего балла
            double Value;
            int numberOfRows;

            // Строка подключения к SQL
            string connectionString = connection_String;
            for (int i = 1; i <= shops; i++) // цикл перебора названий таблиц
            {
                middleScore = 0;
                numberOfRows = 0;
                nameTable = ("SELECT * FROM Shop_" + i); // Выбор для чтения "*" - всех из "название таблицы"
                string sqlExpression = nameTable;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Открываем подключение к БД
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {                            
                            object value = reader["value"]; // название столбца
                            numberOfRows++; // Считаем строки
                            Value = Convert.ToDouble(value);
                            middleScore = middleScore + Value; // Получаем сумму всех баллов
                        }
                    }
                    reader.Close();
                    middleScore = middleScore / numberOfRows; // Средний балл = сумма всех баллов / количество строк
                    labels[i].Text = middleScore.ToString("0.0"); // Обновляем значения баллов на главной странице
                }
            }
        }

        public void ConnectWithDB() // Не задействованый метод для примера работы с SQL
        {
            // Строка подключения к SQL
            string connectionString = connection_String;
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
                            button1.Text = date;                            
                        }
                        
                    }
                }
                
                reader.Close();
                //command.ExecuteNonQuery();
            }
        }

        // Обработчики нажатия на кнопки главного меню
        public async void Button1_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NV1());
        }

        private void button2_Clicked(object sender, EventArgs e)
        {

        }

        private async void button_login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login_Page());
        }

        private void button_refresh_Clicked(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
