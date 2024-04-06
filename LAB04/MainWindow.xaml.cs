using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string connectionString = "Data Source=LAB1504-11\\SQLEXPRESS; Initial Catalog=NeptunoDB; User Id=Luis; Password=123456";
        public class Producto
        {
            public int IdProducto { get; set; }
            public string NombreProducto { get; set; }
            public string CantidadPorUnidad { get; set; }
            public decimal PrecioUnidad { get; set; }
            public string CategoriaProducto { get; set; }
        }
        public class Categoria
        {
            public int IdCategoria { get; set; }
            public string NombreCategoria{ get; set; }
            public string descripcion { get; set; }
            public bool activo { get; set; }
            public string CodCategoria { get; set; }
        }
        private void Button_PRODUCTO(object sender, RoutedEventArgs e)
        {

            List<Producto> productos = new List<Producto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarProductos", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idProducto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                string nombreProducto = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string cantidadPorUnidad = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                decimal precioUnidad = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                                string categoriaProducto = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

                                productos.Add(new Producto { IdProducto = idProducto, NombreProducto = nombreProducto, CantidadPorUnidad = cantidadPorUnidad, PrecioUnidad = precioUnidad, CategoriaProducto = categoriaProducto });
                            }
                        }
                    }
                }

                dgvDemo.ItemsSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recuperar los productos: " + ex.Message);
            }

        }
           

        private void Button_CATEGORIA(object sender, RoutedEventArgs e)
        {
            List<Categoria> categoria = new List<Categoria>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_Listarcategorias", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idcategoria = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                string nombrecategoria = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string descripcion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                bool activo = !reader.IsDBNull(3) && reader.GetBoolean(3);
                                string CodCategoria = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

                                categoria.Add(new Categoria { IdCategoria = idcategoria, NombreCategoria = nombrecategoria, descripcion = descripcion, activo = activo, CodCategoria = CodCategoria });
                            }
                        }
                    }
                   
                }

                dgvDemo.ItemsSource = categoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recuperar los productos: " + ex.Message);
            }


        }
    }
    }



