﻿using System;
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
using System.Windows.Shapes;

namespace pekaraWPF.Forme
{
    /// <summary>
    /// Interaction logic for FrmPeciva.xaml
    /// </summary>
    public partial class FrmPeciva : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public FrmPeciva()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public FrmPeciva(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
            PopuniPadajuceListe();
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiTip = @"SELECT nazivTipa, tipPecivaID FROM tipPeciva";
                SqlDataAdapter daTip = new SqlDataAdapter(vratiTip, konekcija);
                DataTable dtTip = new DataTable();
                daTip.Fill(dtTip);
                cbTip.ItemsSource = dtTip.DefaultView;
                daTip.Dispose();
                dtTip.Dispose();


              

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@cena", SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@kolicina", SqlDbType.Int).Value = txtKol.Text;
                cmd.Parameters.Add("@tipPecivaID", SqlDbType.Int).Value = cbTip.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Pecivo SET ime=@ime,cena=@cena,kolicina=@kolicina,tipPecivaID=@tipPecivaID WHERE pecivoID=@id";

                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Pecivo(ime,cena,kolicina,tipPecivaID)
                                    VALUES (@ime,@cena,@kolicina,@tipPecivaID)";
                }


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
