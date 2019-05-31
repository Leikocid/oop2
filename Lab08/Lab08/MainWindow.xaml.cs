using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lab08 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public static string connectionString = ConfigurationManager.ConnectionStrings["ComputersConnection"].ConnectionString;

        private SqlDataAdapter adapterProc;
        private SqlDataAdapter adapterComp;
        private DataTable dataProc;
        private DataTable dataComp;
       
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RefreshGrids();
        }

        private void RefreshGrids() {
            SqlConnection connection = null;
            try {
                connection = new SqlConnection(connectionString);
                connection.Open();

                adapterProc = new SqlDataAdapter(new SqlCommand("SELECT * FROM Processor", connection));  
                dataProc = new DataTable();
                adapterProc.Fill(dataProc);
                dgProcessors.ItemsSource = dataProc.DefaultView;

                adapterComp = new SqlDataAdapter(new SqlCommand("SELECT * FROM Computer", connection));
                dataComp = new DataTable();
                adapterComp.Fill(dataComp);
                dgComputers.ItemsSource = dataComp.DefaultView;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                if (connection != null) {
                    connection.Close();
                }
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e) {
            String sql = txtSql.Text;
            if (sql != "") {
                SqlConnection connection = null;
                try {
                    connection = new SqlConnection(connectionString);
                    connection.Open();

                    SqlTransaction tx = null;
                    try {
                        SqlCommand command = new SqlCommand(sql, connection);
                        tx = connection.BeginTransaction();
                        command.Transaction = tx;

                        SqlDataReader reader = command.ExecuteReader();
                        string str = "";
                        for (int i = 0; i < reader.FieldCount; i++) {
                            str = str + reader.GetName(i) + '\t';
                        }
                        str = str + '\n';
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                for (int i = 0; i < reader.FieldCount; i++) {
                                    str = str + reader.GetValue(i) + '\t';
                                }
                            }
                        }
                        if (str.Length > 0) {
                            MessageBox.Show(str);
                        }
                        reader.Close();

                        tx.Commit();
                        RefreshGrids();
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                        tx.Rollback();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                } finally {
                    if (connection != null) {
                        connection.Close();
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (dgProcessors.SelectedItems != null) {
                for (int i = 0; i < dgProcessors.SelectedItems.Count; i++) {
                    if (dgProcessors.SelectedItems[i] is DataRowView datarowView) {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            if (dgComputers.SelectedItems != null) {
                for (int i = 0; i < dgComputers.SelectedItems.Count; i++) {
                    if (dgComputers.SelectedItems[i] is DataRowView datarowView) {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDatabase();
            RefreshGrids();
        }

        private void UpdateDatabase() {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapterComp);  // автоматич генер-т команды, кот позволяют согласовать изменения, 
            SqlCommandBuilder comandbuilder2 = new SqlCommandBuilder(adapterProc); // вносимые в объект dataset, со связанной бд
            adapterComp.Update(dataComp);
            adapterProc.Update(dataProc);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e) {
            UpdateDatabase();
        }

        private void BtnStored_Click(object sender, RoutedEventArgs e) {
            SqlConnection connection = null;
            try {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("CreateData", connection) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var reader = command.ExecuteReader();
                string str = "";
                for (int i = 0; i < reader.FieldCount; i++) {
                    str = str + reader.GetName(i) + '\t';
                }
                str = str + '\n';
                if (reader.HasRows) {
                    while (reader.Read()) {
                        for (int i = 0; i < reader.FieldCount; i++) {
                            str = str + reader.GetValue(i) + '\t';
                        }
                    }
                }
                if (str.Length > 0) {
                    MessageBox.Show(str);
                }
                RefreshGrids();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                if (connection != null) {
                    connection.Close();
                }
            }
        }
    }
}
