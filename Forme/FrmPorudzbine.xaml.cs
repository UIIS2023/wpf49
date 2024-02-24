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
using System.Windows.Shapes;

namespace pekaraWPF.Forme
{
    /// <summary>
    /// Interaction logic for FrmPorudzbine.xaml
    /// </summary>
    public partial class FrmPorudzbine : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public FrmPorudzbine()
        {
            InitializeComponent();
            txtCena.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public FrmPorudzbine(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtCena.Focus();
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

                string vratiTip = @"SELECT nazivTipa, tipPorudzbineID FROM tipPorudzbine";
                SqlDataAdapter daTip = new SqlDataAdapter(vratiTip, konekcija);
                DataTable dtTip = new DataTable();
                daTip.Fill(dtTip);
                cbTip.ItemsSource = dtTip.DefaultView;
                daTip.Dispose();
                dtTip.Dispose();

                string vratiZaposlenog = @"SELECT zaposleniID, ime FROM Zaposleni";
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposlenog, konekcija);
                DataTable dtZaposleni = new DataTable();
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                daZaposleni.Dispose();
                dtZaposleni.Dispose();


                string vratiKupca = @"SELECT kupacID, ime FROM Kupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupca, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

                string vratiPecivo = @"SELECT pecivoID, ime FROM Pecivo";
                SqlDataAdapter daPecivo = new SqlDataAdapter(vratiPecivo, konekcija);
                DataTable dtPecivo = new DataTable();
                daPecivo.Fill(dtPecivo);
                cbPecivo.ItemsSource = dtPecivo.DefaultView;
                daPecivo.Dispose();
                dtPecivo.Dispose();


                string vratiPice = @"SELECT piceID, ime FROM Pice";
                SqlDataAdapter daPice = new SqlDataAdapter(vratiPice, konekcija);
                DataTable dtPice = new DataTable();
                daPice.Fill(dtPice);
                cbPice.ItemsSource = dtPice.DefaultView;
                daPice.Dispose();
                dtPice.Dispose();


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

                cmd.Parameters.Add("@cenaPorudzbine", SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@vremePorudzbine", SqlDbType.Date).Value = txtVreme.Text;
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@kupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@pecivoID", SqlDbType.Int).Value = cbPecivo.SelectedValue;
                cmd.Parameters.Add("@piceID", SqlDbType.Int).Value = cbPice.SelectedValue;
                cmd.Parameters.Add("@tipPorudzbineID", SqlDbType.Int).Value = cbTip.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Porudzbina SET cenaPorudzbine=@cenaPorudzbine,vremePorudzbine=@vremePorudzbine,zaposleniID=@zaposleniID,kupacID=@kupacID,pecivoID=@pecivoID,piceID=@piceID,tipPorudzbineID=@tipPorudzbineID WHERE porudzbinaID=@id";

                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Porudzbina(cenaPorudzbine,vremePorudzbine,zaposleniID,kupacID,pecivoID,piceID,tipPorudzbineID)
                                    VALUES (@cenaPorudzbine,@vremePorudzbine,@zaposleniID,@kupacID,@pecivoID,@piceID,@tipPorudzbineID)";
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

