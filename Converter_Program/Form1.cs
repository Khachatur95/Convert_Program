using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Converter_Program
{
    public partial class Form1 : Form
    {
        static string connString = @"Provider=vfpoledb;Data Source=D:\Server\For_Test;Collating Sequence=machine;";


        static string connectionString = @"Data Source = DBSERVER; Initial Catalog = test_; User ID = sa; Password=sa123456";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transfer();
        }
        private void Transfer()
        {
            string svo_dirDBFiles = "D:\\Server\\DB_Files", strSelect = "";
            string svo_dirDB = "D:\\Server\\DB_Files\\Svodka_Backup.mdb";
            OdbcCommand odbc_Cmnd, odbc_FigurCmnd;
            SqlCommand sql_Cmnd, sql_FigurCmnd;
            OdbcDataReader rdOdbc = null;

            svo_ProgressBar.Minimum = 0;
            svo_ProgressBar.Visible = true;
            svo_ProgressBar.Visible = false;


            OdbcConnection odbc_DBConn = new OdbcConnection();
            odbc_DBConn.ConnectionString = "Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + svo_dirDBFiles + ";Exclusive=Yes;Collate=Machine;NULL=NO;DELETED=YES;BACKGROUNDFETCH=NO;";

            string OleConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + svo_dirDB;
            OleDbConnection oleDBConn = new OleDbConnection(OleConnString);

            if (odbc_DBConn.State == ConnectionState.Closed)
                odbc_DBConn.Open();
            odbc_Cmnd = new OdbcCommand("Set Exclusive On", odbc_DBConn);
            odbc_Cmnd.ExecuteNonQuery();

            strSelect = "SELECT n_zadan FROM stos_day WHERE DTOS(data_p)='" + svo_dtPicker.Value.ToString("yyyyMMdd") + "'";
            odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
            rdOdbc = odbc_Cmnd.ExecuteReader();
            if (rdOdbc.Read())
            {
                odbc_Cmnd = new OdbcCommand("Set Exclusive On", odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                strSelect = "DELETE FROM Figur_os_day where Figur_os_day.stos_id in (SELECT stos_day.number_id FROM stos_day where DTOS(stos_day.data_p)='" + svo_dtPicker.Value.ToString("yyyyMMdd") + "')";
                odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                odbc_DBConn.Close();
                odbc_DBConn.Open();

                odbc_Cmnd = new OdbcCommand("Set Exclusive On", odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                strSelect = "SELECT * FROM Figur_os_day";
                odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                odbc_Cmnd = new OdbcCommand("PACK", odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                strSelect = "DELETE FROM stos_day WHERE DTOS(data_p)='" + svo_dtPicker.Value.ToString("yyyyMMdd") + "'";
                odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();
                odbc_DBConn.Close();
                odbc_DBConn.Open();

                odbc_Cmnd = new OdbcCommand("Set Exclusive On", odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                strSelect = "SELECT * FROM stos_day";
                odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();

                odbc_Cmnd = new OdbcCommand("PACK", odbc_DBConn);
                odbc_Cmnd.ExecuteNonQuery();
            }

            int number_id = 0;
            strSelect = "SELECT Max(number_Id) as max_Num from stos_day ";
            odbc_Cmnd = new OdbcCommand(strSelect, odbc_DBConn);
            rdOdbc = odbc_Cmnd.ExecuteReader();

            if (rdOdbc.Read())
            {
                number_id = Convert.ToInt32(rdOdbc["max_Num"].ToString().Trim()) + 1;
            }
            else
            {
                number_id = 1;
            }

            SqlConnection sql_DBConn = new SqlConnection(connectionString);
            if (sql_DBConn.State == ConnectionState.Closed)
                sql_DBConn.Open();
            sql_Cmnd = new SqlCommand("INSERT INTO stos_day(number_id, n_zadan,vid_prest,dou,n_dou,n_ud, movd,rovd,data_p,tip_prest, prim) VALUES (@number_id,@n_zadan,@vid_prest,@dou,@n_dou,@n_ud,@movd,@rovd,@data_p,@tip_prest,@prim)", sql_DBConn);
            sql_Cmnd.Parameters.Add("number_id", SqlDbType.Int);
            sql_Cmnd.Parameters.Add("n_zadan", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("vid_prest", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("dou", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("n_dou", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("n_ud", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("movd", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("rovd", SqlDbType.NVarChar);
            sql_Cmnd.Parameters.Add("data_p", SqlDbType.Date);
            sql_Cmnd.Parameters.Add("tip_prest", SqlDbType.Int);
            sql_Cmnd.Parameters.Add("prim", SqlDbType.Text);
            sql_Cmnd.Parameters["data_p"].Value = svo_dtPicker.Value;
            

            sql_FigurCmnd = new SqlCommand("INSERT INTO figur_os_day(stos_id, n_zadan, kateg, fam, im, otch, god_r, ser, marz, gorod, rajon, m_rajon, ul, dom, kv, rab, dolj, notes, klassif, n_avto, m_avto, cv_avto, zov, st1, mas1, punkt1, prim1, st2, mas2, punkt2, prim2, st3, mas3, punkt3, prim3, st4, mas4, punkt4, prim4, umer, zader) VALUES(@stos_id,@n_zadan,@kateg,@fam,@im,@otch,@god_r,@ser,@marz,@gorod,@rajon,@m_rajon,@ul,@dom,@kv,@rab,@dolj,@notes,@klassif,@n_avto,@m_avto,@cv_avto,@zov,@st1,@mas1,@punkt1,@prim1,@st2,@mas2,@punkt2,@prim2,@st3,@mas3,@punkt3,@prim3,@st4,@mas4,@punkt4,@prim4,@umer,@zader)", sql_DBConn);
            sql_FigurCmnd.Parameters.Add("stos_id", SqlDbType.Int);
            sql_FigurCmnd.Parameters.Add("n_zadan", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("kateg", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("fam", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("im", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("otch", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("god_r", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("ser", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("marz", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("gorod", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("rajon", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("m_rajon", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("ul", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("dom", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("kv", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("rab", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("dolj", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("notes", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("klassif", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("n_avto", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("m_avto", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("cv_avto", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("zov", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("st1", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("mas1", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("punkt1", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("prim1", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("st2", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("mas2", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("punkt2", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("prim2", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("st3", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("mas3", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("punkt3", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("prim3", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("st4", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("mas4", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("punkt4", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("prim4", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("umer", SqlDbType.NVarChar);
            sql_FigurCmnd.Parameters.Add("zader", SqlDbType.NVarChar);

            string strCommStosDay = "SELECT number_id, n_zadan, vid_prest, tip_prest, dou, n_dou, n_ud, movd, rovd, data_p, prim  FROM stos_day WHERE data_p = #" + svo_dtPicker.Value.ToString("M/d/yyyy") + "# ORDER BY number_id";
            DataTable svo_tableStosDay = new DataTable();
            OleDbDataAdapter oleDBStosDayda = new OleDbDataAdapter(strCommStosDay, oleDBConn);
            oleDBStosDayda.Fill(svo_tableStosDay);

            string strCommFigurDay = "Select * From Figur_os_day";
            DataTable svo_tableFigurDay = new DataTable();
            OleDbDataAdapter oleDBFigurDayda = new OleDbDataAdapter(strCommFigurDay, oleDBConn);
            oleDBFigurDayda.Fill(svo_tableFigurDay);


            int cnt = svo_tableStosDay.Rows.Count;
            svo_ProgressBar.Maximum = cnt;
            svo_ProgressBar.Step = 1;
            svo_ProgressBar.Value = 0;
            foreach (DataRow svo_drStosDay in svo_tableStosDay.Rows)
            {
                sql_Cmnd.Parameters["number_id"].Value = number_id;
                sql_Cmnd.Parameters["n_zadan"].Value = "";
                sql_Cmnd.Parameters["vid_prest"].Value = svo_drStosDay["vid_prest"].ToString();
                sql_Cmnd.Parameters["dou"].Value = svo_drStosDay["dou"].ToString();
                sql_Cmnd.Parameters["n_dou"].Value = svo_drStosDay["n_dou"].ToString();
                sql_Cmnd.Parameters["n_ud"].Value = svo_drStosDay["n_ud"].ToString();
                sql_Cmnd.Parameters["movd"].Value = svo_drStosDay["movd"].ToString();
                sql_Cmnd.Parameters["rovd"].Value = svo_drStosDay["rovd"].ToString();
                sql_Cmnd.Parameters["data_p"].Value = svo_drStosDay["data_p"];
                sql_Cmnd.Parameters["tip_prest"].Value = Convert.ToInt16(svo_drStosDay["tip_prest"].ToString());
                sql_Cmnd.Parameters["prim"].Value = svo_drStosDay["prim"].ToString();
                sql_Cmnd.ExecuteNonQuery();

                string strQuery = "stos_id = " + svo_drStosDay["number_id"].ToString();
                DataRow[] svo_drsFigurDay;

                svo_drsFigurDay = svo_tableFigurDay.Select(strQuery);
                foreach (DataRow svo_drFigurDay in svo_drsFigurDay)
                {
                    sql_FigurCmnd.Parameters["stos_id"].Value = number_id;
                    sql_FigurCmnd.Parameters["n_zadan"].Value = "";
                    sql_FigurCmnd.Parameters["kateg"].Value = svo_drFigurDay["kateg"].ToString();
                    sql_FigurCmnd.Parameters["fam"].Value = svo_drFigurDay["fam"].ToString();
                    sql_FigurCmnd.Parameters["im"].Value = svo_drFigurDay["im"].ToString();
                    sql_FigurCmnd.Parameters["otch"].Value = svo_drFigurDay["otch"].ToString();
                    sql_FigurCmnd.Parameters["god_r"].Value = svo_drFigurDay["god_r"].ToString();
                    sql_FigurCmnd.Parameters["ser"].Value = svo_drFigurDay["ser"].ToString();
                    sql_FigurCmnd.Parameters["marz"].Value = svo_drFigurDay["marz"].ToString();
                    sql_FigurCmnd.Parameters["gorod"].Value = svo_drFigurDay["gorod"].ToString();
                    sql_FigurCmnd.Parameters["rajon"].Value = svo_drFigurDay["rajon"].ToString();
                    sql_FigurCmnd.Parameters["m_rajon"].Value = svo_drFigurDay["m_rajon"].ToString();
                    sql_FigurCmnd.Parameters["ul"].Value = svo_drFigurDay["ul"].ToString();
                    sql_FigurCmnd.Parameters["dom"].Value = svo_drFigurDay["dom"].ToString();
                    sql_FigurCmnd.Parameters["kv"].Value = svo_drFigurDay["kv"].ToString();
                    sql_FigurCmnd.Parameters["rab"].Value = svo_drFigurDay["rab"].ToString();
                    sql_FigurCmnd.Parameters["dolj"].Value = svo_drFigurDay["dolj"].ToString();
                    sql_FigurCmnd.Parameters["notes"].Value = svo_drFigurDay["notes"].ToString();
                    sql_FigurCmnd.Parameters["klassif"].Value = svo_drFigurDay["klassif"].ToString();
                    sql_FigurCmnd.Parameters["n_avto"].Value = svo_drFigurDay["n_avto"].ToString().Replace(" ", "").ToUpper();
                    sql_FigurCmnd.Parameters["m_avto"].Value = svo_drFigurDay["m_avto"].ToString();
                    sql_FigurCmnd.Parameters["cv_avto"].Value = svo_drFigurDay["cv_avto"].ToString();
                    sql_FigurCmnd.Parameters["zov"].Value = "";
                    sql_FigurCmnd.Parameters["st1"].Value = "";
                    sql_FigurCmnd.Parameters["mas1"].Value = "";
                    sql_FigurCmnd.Parameters["punkt1"].Value = "";
                    sql_FigurCmnd.Parameters["prim1"].Value = "";
                    sql_FigurCmnd.Parameters["st2"].Value = "";
                    sql_FigurCmnd.Parameters["mas2"].Value = "";
                    sql_FigurCmnd.Parameters["punkt2"].Value = "";
                    sql_FigurCmnd.Parameters["prim2"].Value = "";
                    sql_FigurCmnd.Parameters["st3"].Value = "";
                    sql_FigurCmnd.Parameters["mas3"].Value = "";
                    sql_FigurCmnd.Parameters["punkt3"].Value = "";
                    sql_FigurCmnd.Parameters["prim3"].Value = "";
                    sql_FigurCmnd.Parameters["st4"].Value = "";
                    sql_FigurCmnd.Parameters["mas4"].Value = "";
                    sql_FigurCmnd.Parameters["punkt4"].Value = "";
                    sql_FigurCmnd.Parameters["prim4"].Value = "";
                    sql_FigurCmnd.Parameters["umer"].Value = "";
                    sql_FigurCmnd.Parameters["zader"].Value = "";

                    sql_FigurCmnd.ExecuteNonQuery();

                }
                number_id++;
      
                svo_ProgressBar.Value++;
            }

            if (odbc_DBConn.State == ConnectionState.Open)
                odbc_DBConn.Close();

            svo_ProgressBar.Value = 0;
            svo_ProgressBar.Visible = false;
            svo_ProgressBar.Text = GlobalVariables.GlobVariables.strTransToDayVFPDB;
            svo_ProgressBar.Visible = true;
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "All Files | *.DBF" };
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)

            {

                string[] lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                foreach (string line in lines)
                    svo_ListBox.Items.Add(line);
                this.svo_ListBox.Click += new EventHandler(svo_ListBox_Click);

            }
            openFileDialog.ShowDialog();



        }

        private void svo_DtGridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //svo_DtGridview.DataSource = 
        }

        private void svo_ListBox_Click(object sender, EventArgs e)
        {
            string line = svo_ListBox.Items[svo_ListBox.SelectedIndex].ToString();
            MessageBox.Show(line);
            Clipboard.SetDataObject(line, true);
        }

    }
}
