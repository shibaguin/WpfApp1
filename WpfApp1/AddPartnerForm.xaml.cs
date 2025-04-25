using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace WpfApp1
{
                         /// <summary>
                         /// Interaction logic for Window1.xaml
                         /// </summary>
                         public partial class AddPartnerForm : Window
                         {
                                                  public AddPartnerForm()
                                                  {
                                                                           InitializeComponent();
                                                  }
                                                  private void btnSave_Click(object sender, RoutedEventArgs e)  // Для WPF: BtnSave_Click
                                                  {
                                                                           try
                                                                           {
                                                                                                    string PartnerName = txtName.Text;
                                                                                                    string Type = txtType.Text;
                                                                                                    int Rating = int.Parse(txtRating.Text);

                                                                                                    if (string.IsNullOrEmpty(PartnerName))
                                                                                                                             throw new Exception("Имя не может быть пустым");

                                                                                                    string connectionString = ConfigurationManager.ConnectionStrings["PartnerDB"].ConnectionString;
                                                                                                    using (SqlConnection connection = new SqlConnection(connectionString))
                                                                                                    {
                                                                                                                             connection.Open();
                                                                                                                             // Получаем новый PartnerID
                                                                                                                             SqlCommand idCmd = new SqlCommand("SELECT ISNULL(MAX(PartnerID), 0) + 1 FROM Partners", connection);
                                                                                                                             int newID = (int)idCmd.ExecuteScalar();
                                                                                                                             // Вставляем запись с явным указанием PartnerID
                                                                                                                             string query = "INSERT INTO Partners (PartnerID, PartnerName, Type, Rating) VALUES (@PartnerID, @PartnerName, @Type, @Rating)";
                                                                                                                             SqlCommand command = new SqlCommand(query, connection);
                                                                                                                             command.Parameters.AddWithValue("@PartnerID", newID);
                                                                                                                             command.Parameters.AddWithValue("@PartnerName", PartnerName);
                                                                                                                             command.Parameters.AddWithValue("@Type", Type);
                                                                                                                             command.Parameters.AddWithValue("@Rating", Rating);
                                                                                                                             command.ExecuteNonQuery();
                                                                                                    }
                                                                                                    this.Close();
                                                                           }
                                                                           catch (Exception ex)
                                                                           {
                                                                                                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                           }
                                                  }
                         }
}
