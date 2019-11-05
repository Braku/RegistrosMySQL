using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
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
using MySql.Data.MySqlClient;

namespace MySQL
    {
        /// <summary>
        /// Lógica de interacción para Registros.xaml
        /// </summary>
        public partial class Registros : Window
        {
            public Registros()
            {
                InitializeComponent();
                Mostrar();
            }

            private void Mostrar()
            {
                string connectionString = "datasource=127.0.0.1;username=root;password=Brambila1402;database=reg_sechma;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM registros", connection);
                connection.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                connection.Close();
                dgrid.DataContext = dt;
            }

            private void Visibilidad()
            {
                BUpdate.Visibility = System.Windows.Visibility.Collapsed;
                BCan.Visibility = System.Windows.Visibility.Collapsed;
                BEdit.Visibility = System.Windows.Visibility.Visible;
                BDel.Visibility = System.Windows.Visibility.Visible;
                SPEdit.Visibility = System.Windows.Visibility.Collapsed;
                dgrid.Visibility = System.Windows.Visibility.Visible;
                Mostrar();
            }

            private void Limpiar()
            {
                Nom.Text = "";
                Correo.Text = "";
                Telefono.Text = "";
            }

            private void BUpdate_Click(object sender, RoutedEventArgs e)
            {
                string connectionString = "datasource=127.0.0.1;username=root;password=Brambila1402;database=reg_sechma;";
                string querty = "UPDATE registros SET Nombre = '" + Nom.Text + "' , Correo = '" + Correo.Text + "', Telefono = " + Telefono.Text + " WHERE id = " + id.Text + "";

                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(querty, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                Limpiar();
                Visibilidad();
            }

            private void BEdit_Click(object sender, RoutedEventArgs e)
            {
                if (dgrid.SelectedItems.Count > 0)
                {
                    SPEdit.Visibility = System.Windows.Visibility.Visible;
                    dgrid.Visibility = System.Windows.Visibility.Hidden;
                    DataRowView row = (DataRowView)dgrid.SelectedItems[0];
                    id.Text = row["id"].ToString();
                    Nom.Text = row["Nombre"].ToString();
                    Correo.Text = row["Correo"].ToString();
                    Telefono.Text = row["Telefono"].ToString();
                    id.IsEnabled = false;
                    BUpdate.Visibility = System.Windows.Visibility.Visible;
                    BCan.Visibility = System.Windows.Visibility.Visible;
                    BEdit.Visibility = System.Windows.Visibility.Collapsed;
                    BDel.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Seleccione un registro de la lista");
                }
            }

            private void BCan_Click(object sender, RoutedEventArgs e)
            {
                Limpiar();
                Visibilidad();
            }

            private void BDel_Click(object sender, RoutedEventArgs e)
            {
                string connectionString = "datasource=127.0.0.1;username=root;password=Brambila1402;database=reg_sechma;";
                if (dgrid.SelectedItems.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Seguro que quieres eliminar este registro?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DataRowView row = (DataRowView)dgrid.SelectedItems[0];
                        string querty = "DELETE FROM registros WHERE id =" + row["id"].ToString();

                        MySqlConnection connection = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(querty, connection);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        Limpiar();
                        Visibilidad();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un registro de la lista");
                }
            }
        }
    }
}
