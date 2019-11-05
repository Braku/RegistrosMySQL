using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace MySQL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            Nom.Text = "";
            Correo.Text = "";
            Telefono.Text = "";
        }

        private void BtnN_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=Brambila1402;database=reg_sechma;";
            if (Nom.Text != "")
            {
                if (Correo.Text != "")
                {
                    if (Telefono.Text != "")
                    {
                        string querty = "INSERT INTO registros(id,Nombre,Correo,Telefono) " + "VALUES(NULL,'" + Nom.Text + "' , '" + Correo.Text + "', " + Telefono.Text + ")";

                        MySqlConnection connection = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(querty, connection);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro exitoso.");
                        connection.Close();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Favor introduce tu telefono", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Favor introduce tu correo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Favor introduce tu nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BRegs_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=Brambila1402;database=reg_sechma;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM registros", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            if (dt.Rows.Count > 0)
            {
                Registros RG = new Registros();
                RG.Show();
            }
            else
            {
                MessageBox.Show("No se encontraron registros");
            }
            connection.Close();
        }
    }
}