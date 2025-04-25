using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace WpfApp1
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();
               LoadPartners();
          }
          private void LoadPartners()
          {
               string connectionString = ConfigurationManager.ConnectionStrings["PartnerDB"].ConnectionString;
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Partners", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataGrid1.ItemsSource = dt.DefaultView;
                    // Для WPF: dataGrid.ItemsSource = dt.DefaultView;
               }
          }
          private void btnAddPartner_Click(object sender, RoutedEventArgs e)
          {
               AddPartnerForm form = new AddPartnerForm();
               form.ShowDialog();
               LoadPartners();  // Обновить таблицу после добавления
          }
     }
}
