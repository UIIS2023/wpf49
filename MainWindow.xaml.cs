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
using pekaraWPF.Forme;

namespace pekaraWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanaTabela;
        private bool azuriraj;
        private DataRowView red;

        #region Select upiti

        private static string zaposleniSelect = @"SELECT zaposleniID as ID, ime as Ime, prezime as Prezime, JMBG as JMBG, kontakt as Kontakt, adresa as Adresa, grad as Grad, pozicija as Pozicija, plata as Plata FROM Zaposleni";

        private static string kupacSelect = @"SELECT kupacID as ID, ime as Ime, kontakt as Kontakt, adresa as Adresa, grad as Grad FROM Kupac";

        private static string pecivoSelect = @"SELECT pecivoID as ID, ime as 'Naziv peciva', cena as Cena, kolicina as Kolicina, nazivTipa as 'Tip peciva' FROM Pecivo
                                               JOIN tipPeciva ON Pecivo.tipPecivaID = TipPeciva.tipPecivaID";

        private static string piceSelect = @"SELECT piceID as ID, ime as 'Naziv pica', cena as Cena, kolicina as Kolicina, nazivTipa as 'Tip pica' FROM Pice
                                             JOIN tipPica ON Pice.tipPicaID = TipPica.tipPicaID";

        private static string porudzbinaSelect = @"select porudzbinaID as ID, cenaPorudzbine as Cena, vremePorudzbine as Vreme, Kupac.ime as Kupac, Zaposleni.ime as Zaposleni, Pecivo.ime as Pecivo, Pice.ime as Pice, TipPorudzbine.nazivTipa as 'Tip Porudzbine'
                                                    from Porudzbina join Kupac on Porudzbina.kupacID = Kupac.kupacID
				                                    join Zaposleni on Porudzbina.zaposleniID = Zaposleni.zaposleniID
				                                    join Pecivo on Porudzbina.pecivoID = Pecivo.pecivoID
				                                    join Pice on Porudzbina.piceID = Pice.piceID
				                                    join TipPorudzbine on Porudzbina.tipPorudzbineID = TipPorudzbine.tipPorudzbineID";


        #endregion

        #region Select sa uslovom

        private static string selectUslovZaposleni = @"SELECT * FROM Zaposleni WHERE zaposleniID=";

        private static string selectUslovKupac = @"SELECT * FROM Kupac WHERE kupacID=";

        private static string selectUslovPecivo = @"SELECT * FROM Pecivo WHERE pecivoID=";

        private static string selectUslovPice = @"SELECT * FROM Pice WHERE piceID=";

        private static string selectUslovPorudzbina = @"SELECT * FROM Porudzbina WHERE porudzbinaID=";



        #endregion

        #region Delete naredbe

        private static string zaposleniDelete = @"DELETE FROM Zaposleni WHERE zaposleniID=";

        private static string kupacDelete = @"DELETE FROM Kupac WHERE kupacID=";

        private static string pecivoDelete = @"DELETE FROM Pecivo WHERE pecivoID=";

        private static string piceDelete = @"DELETE FROM Pice WHERE piceID=";

        private static string porudzbineDelete = @"DELETE FROM Porudzbina WHERE porudzbinaID=";

        



        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(zaposleniSelect);

        }
        private void UcitajPodatke(string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }

                ucitanaTabela = selectUpit;
                dataAdapter.Dispose();
                dataTable.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Nastala je greska pri ucitavanju tabele", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                prozor = new FrmZaposleni();
                prozor.ShowDialog();
                UcitajPodatke(zaposleniSelect);

            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                prozor = new FrmKupci();
                prozor.ShowDialog();
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                prozor = new FrmPeciva();
                prozor.ShowDialog();
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                prozor = new FrmPica();
                prozor.ShowDialog();
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(porudzbinaSelect))
            {
                prozor = new FrmPorudzbine();
                prozor.ShowDialog();
                UcitajPodatke(porudzbinaSelect);
            }
            
        }
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                PopuniFormu(selectUslovZaposleni);
                UcitajPodatke(zaposleniSelect);

            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                PopuniFormu(selectUslovKupac);
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                PopuniFormu(selectUslovPecivo);
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                PopuniFormu(selectUslovPice);
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(porudzbinaSelect))
            {
                PopuniFormu(selectUslovPorudzbina);
                UcitajPodatke(porudzbinaSelect);
            }
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                ObrisiZapis(zaposleniDelete);
                UcitajPodatke(zaposleniSelect);

            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                ObrisiZapis(kupacDelete);
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                ObrisiZapis(pecivoDelete);
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                ObrisiZapis(piceDelete);
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(porudzbinaSelect))
            {
                ObrisiZapis(porudzbineDelete);
                UcitajPodatke(porudzbinaSelect);
            }
        }
        private void ObrisiZapis(string deleteUslov)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUslov + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(zaposleniSelect);
        }
        private void btnKupci_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupacSelect);
        }
        private void btnPeciva_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(pecivoSelect);
        }
        private void btnPica_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(piceSelect);
        }
        private void btnPorudzbine_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(porudzbinaSelect);
        }
        private void PopuniFormu(object selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(zaposleniSelect))
                    {
                        FrmZaposleni prozorZaposleni = new FrmZaposleni(azuriraj, red);
                        prozorZaposleni.txtIme.Text = citac["ime"].ToString();
                        prozorZaposleni.txtPrez.Text = citac["prezime"].ToString();
                        prozorZaposleni.txtJMBG.Text = citac["JMBG"].ToString();
                        prozorZaposleni.txtKontakt.Text = citac["kontakt"].ToString();
                        prozorZaposleni.txtAdresa.Text = citac["adresa"].ToString();
                        prozorZaposleni.txtGrad.Text = citac["grad"].ToString();
                        prozorZaposleni.txtPoz.Text = citac["pozicija"].ToString();
                        prozorZaposleni.txtPlata.Text = citac["plata"].ToString();
                        prozorZaposleni.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(kupacSelect))
                    {
                        FrmKupci prozorKupci = new FrmKupci(azuriraj, red);
                        prozorKupci.txtIme.Text = citac["ime"].ToString();
                        prozorKupci.txtKontakt.Text = citac["kontakt"].ToString();
                        prozorKupci.txtAdresa.Text = citac["adresa"].ToString();
                        prozorKupci.txtGrad.Text = citac["grad"].ToString();
                        prozorKupci.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(pecivoSelect))
                    {
                        FrmPeciva prozorPeciva = new FrmPeciva(azuriraj, red);
                        prozorPeciva.txtIme.Text = citac["ime"].ToString();
                        prozorPeciva.txtCena.Text = citac["cena"].ToString();
                        prozorPeciva.txtKol.Text = citac["kolicina"].ToString();
                        prozorPeciva.cbTip.SelectedValue = citac["tipPecivaID"].ToString();
                        
                        prozorPeciva.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(piceSelect))
                    {
                        FrmPica prozorPice = new FrmPica(azuriraj, red);
                        prozorPice.txtIme.Text = citac["ime"].ToString();
                        prozorPice.txtCena.Text = citac["cena"].ToString();
                        prozorPice.txtKol.Text = citac["kolicina"].ToString();
                        prozorPice.cbTip.SelectedValue = citac["tipPicaID"].ToString();

                        prozorPice.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(porudzbinaSelect))
                    {
                        FrmPorudzbine prozorPor = new FrmPorudzbine(azuriraj, red);

                        
                        prozorPor.cbZaposleni.SelectedValue = citac["zaposleniID"].ToString();
                        prozorPor.cbKupac.SelectedValue = citac["kupacID"].ToString();
                        prozorPor.cbPecivo.SelectedValue = citac["pecivoID"].ToString();
                        prozorPor.cbPice.SelectedValue = citac["piceID"].ToString();
                        prozorPor.cbTip.SelectedValue = citac["tipPorudzbineID"].ToString();
                        prozorPor.txtCena.Text = citac["cenaPorudzbine"].ToString();

                        prozorPor.txtVreme.Text = citac["vremePorudzbine"].ToString();

                        prozorPor.ShowDialog();
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
