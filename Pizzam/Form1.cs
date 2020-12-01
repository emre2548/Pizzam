using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pizzam
{
    public partial class Form1 : Form
    {

        // connect DB
        Class.c_db sqlConnect = new Class.c_db();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            fillCombobox();
            list();
        }

        private void fillCombobox()
        {
            SqlCommand dbGetDataDrink = new SqlCommand("select * from Drink",sqlConnect.db_connect());
            SqlDataReader readSqlDataDrink = dbGetDataDrink.ExecuteReader();

            while (readSqlDataDrink.Read())
            {
                cmbDrink.Items.Add(readSqlDataDrink["Drink"]);
            }

            sqlConnect.db_connect().Close();

            SqlCommand dbGetDataSize = new SqlCommand("select * from Pizza_Size", sqlConnect.db_connect());
            SqlDataReader readSqlDataSize = dbGetDataSize.ExecuteReader();

            while (readSqlDataSize.Read())
            {
                cmbPizzaSize.Items.Add(readSqlDataSize["Size"]);
            }

            sqlConnect.db_connect().Close();
        }

        private void list()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter("select Name_Surname as 'Adı Soyadı', Phone_Num as 'Telefon Numarası', Pizza_Content as 'Pizza İçeriği', Drink as 'İçecek',Ordered_Time as 'Sipariş Tarihi', Address as 'Adres' from Customer",sqlConnect.db_connect());
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e) // Sipariş Al
        {
            string[] content = new string[16];
            string contentAll;

            if (cbBroccoli.Checked)
                content[0] = cbBroccoli.Text;
            if (cbCheese.Checked)
                content[1] = cbCheese.Text;
            if (cbCorn.Checked)
                content[2] = cbCorn.Text;
            if (cbMushroom.Checked)
                content[3] = cbMushroom.Text;
            if (cbOlive.Checked)
                content[4] = cbOlive.Text;
            if (cbRoasting.Checked)
                content[5] = cbRoasting.Text;
            if (cbSalami.Checked)
                content[6] = cbSalami.Text;
            if (cbSausage.Checked)
                content[7] = cbSausage.Text;
            if (cbSujuk.Checked)
                content[8] = cbSujuk.Text;

            contentAll = content[0] + " " + content[1] + " " + content[2] + " " +
                         content[3] + " " + content[4] + " " + content[5] + " " +
                         content[6] + " " + content[7] + " " + content[8] + " ";


            try
            {
                //SqlCommand add = new SqlCommand("insert into Customer(Name_Surname=@p1,Phone_Num=@p2,Address=@p3,Pizza_Content=@p4,Ordered_Time=@p5)", sqlConnect.db_connect());
                SqlCommand add = new SqlCommand("insert into Customer(Name_Surname,Phone_Num,Address,Pizza_Content,Ordered_Time,Pizza_Size,Drink) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", sqlConnect.db_connect());
                add.Parameters.AddWithValue("@p1",tbNameSurname.Text);
                add.Parameters.AddWithValue("@p2",mtbPhoneNum.Text);
                add.Parameters.AddWithValue("@p3",tbAddress.Text);
                add.Parameters.AddWithValue("@p4",contentAll.ToString());
                add.Parameters.AddWithValue("@p5",DateTime.Now);
                add.Parameters.AddWithValue("@p6",cmbPizzaSize.Text);
                add.Parameters.AddWithValue("@p7",cmbDrink.Text);

                add.ExecuteNonQuery();
                sqlConnect.db_connect().Close();
                MessageBox.Show("Spariş Alındı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                list();

            }
            catch (Exception)
            {
                MessageBox.Show("Birşeyler Ters Gitti !!!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }


    }
}
