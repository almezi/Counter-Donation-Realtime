using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Counting_Bisa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string MyConnection2 = "datasource=assembly.alimenji.com;database=db_assembly;username=team-entry;password=aplikasi-entry-sukses";
   
        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
            Total();
            SetTimer();
        }
        private void UpdateUI()
        {
           

                    MySqlConnection myConn = new MySqlConnection(MyConnection2);
                    MySqlCommand command = myConn.CreateCommand();

                    command.CommandText = "Select * from donasi";
                    //command.Parameters.Add(new MySqlParameter("@userid", userId));
                    MySqlDataReader myReader;

                    try
                    {
                        myConn.Open();
                        myReader = command.ExecuteReader();

                        while (myReader.Read())
                        {
                    // Show face marker


                   
                    lbl_nama.Content = myReader[1].ToString();
                            lbl_kelas.Content = myReader[2].ToString();
                            lbl_nominal.Content = myReader[3].ToString();

                }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    myConn.Close();
            
                }
        private void Total()
        {


            MySqlConnection myConn = new MySqlConnection(MyConnection2);
            MySqlCommand command = myConn.CreateCommand();

            command.CommandText = "Select sum(nominal) from donasi";
            //command.Parameters.Add(new MySqlParameter("@userid", userId));
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = command.ExecuteReader();

                while (myReader.Read())
                {
                    // Show face marker


                    lbl_total.Content = myReader[0].ToString();
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myConn.Close();

        }
        protected void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
            Total();
        }
        private void update_Data_Grid()
        {
            MySqlConnection connection = new MySqlConnection(MyConnection2);
            MySqlCommand cmd = new MySqlCommand("Select * from donasi");
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
        
        }


        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            SetTimer();
        }
    }
}

