using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
/// Многопоточность
using System.Threading;

namespace PreOrderWin
{
    
    
    /// <summary>
    /// Логика взаимодействия для RelatedTov.xaml
    /// </summary>
    public partial class RelatedTov : Window
    {


        public static string Uri_ImageTov = "", StrokaKodTovRelatedTov = "";
        public static int SetBludoList = 0, ValueKodTovRelatedTov = 0,SprTovKodTovRelatedTov = 0, SelectAllRelatedTov=0;
        public static List<SprTovImage> RelatedTovImage = new List<SprTovImage>(1);
        public static List<SprTovImage> RelatedTovImageAdd = new List<SprTovImage>(1);
        public RelatedTov()
        {
            InitializeComponent();
            
            if (MainWindow.ScreenHeight > 1080) Top = 362;
            MainWindow.NameSetRelated = "RelatedTov";
            //--------------------------
            // Загрузка изображения выбранного товара

            //ListPriceWin LinkMapTov = ControlWin.LinkMainWindow("ListPriceWin");
            //var pict = (Image)LogicalTreeHelper.FindLogicalNode(LinkMapTov, "ImageTov");

            //Uri uri = new Uri("pack://application:,,/munch.png");
            //BitmapImage bitmap = new BitmapImage(uri);
            //Image img = new Image();
            //img.Source = bitmap;

            //Uri Filejpg = new Uri(ControlWin.ListTov[11], UriKind.Relative);
            //ImageTov.Source = new BitmapImage(Filejpg);
          
        

            //RelatedTov ImageMapTov = ControlWin.LinkMainWindow("RelatedTov");
            //ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.ListTov[17], UriKind.Relative)));
            //ImageTov.Background = (System.Windows.Media.Brush)imageBrush;
            if (ControlWin.ImageFoto.Length > 0)
            {
                BitmapImage imgsource = new BitmapImage();
                imgsource.BeginInit();
                imgsource.CacheOption = BitmapCacheOption.OnLoad;
                imgsource.StreamSource = new System.IO.MemoryStream(ControlWin.ImageFoto, 0, ControlWin.ImageFoto.Length);
                imgsource.EndInit();
                ImageTov.Source = imgsource; // реальный Image
            }
            if (MainWindow.language_Key == 1)
            {
                Kolichestvo.Text = "Add to basket:";
            }
            else
            {
                Kolichestvo.Text = "Додати      до кошика:";

            }
                   
            TitleTov.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[9] : ControlWin.ListTov[8];
            PriceTov.Content = ControlWin.ListTov[10];
            Reclama.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[20] : ControlWin.ListTov[16];
            Kolvo.Content = "1";
            //  Проверяем наличие товара в заказе
            if (ListPriceWin.MapOrderList == 1)
            {
                var PrevTovChange = MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                if (PrevTovChange != null) Kolvo.Content = Convert.ToString(PrevTovChange.Quantity);

            }

            LoadListRelated1();
            LoadListRelated2();

        }

        public void LoadListRelated1()
        {
            RelatedTovImage.Clear();
            ListRelated1.ItemsSource = null;
            NameRelated1.Text = "";
            string NameTov = "";
            byte[] FotoByte = new byte[0];

            // Группа товаров
            var Strgrup = MainWindow.db.Sprgrups.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0]).FirstOrDefault();
            if (Strgrup != null)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated1.ItemsSource = RelatedTovImage;
                NameRelated1.Text = MainWindow.language_Key == 1 ? Strgrup.NameEN : Strgrup.NameUK;
            }

            // Вид товаров
            var Strvid = MainWindow.db.Sprvids.Where(p => p.Kodvid == ControlWin.ListConectionVid[0]).FirstOrDefault();
            if (Strvid != null && ListRelated1.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodvid == ControlWin.ListConectionVid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated1.ItemsSource = RelatedTovImage;
                NameRelated1.Text = MainWindow.language_Key == 1 ? Strvid.NameEN : Strvid.NameUK;
            }

            // Подвид товаров
            var Strpvid = MainWindow.db.Sprpvids.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0]).FirstOrDefault();
            if (Strpvid != null && ListRelated1.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated1.ItemsSource = RelatedTovImage;
                NameRelated1.Text = MainWindow.language_Key == 1 ? Strpvid.NameEN : Strpvid.NameUK;
            }
            // Один товар
            var Strtov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0]).FirstOrDefault();
            if (Strtov != null && ListRelated1.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated1.ItemsSource = RelatedTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strtov.NameTovEN : Strtov.NameTovUK;
            }
            ListRelated1.Focusable = false;

        }

        public void LoadListRelated2()
        {
            RelatedTovImageAdd.Clear();
            ListRelated2.ItemsSource = null;
            NameRelated2.Text = "";
            string NameTov = "";
            byte[] FotoByte = new byte[0];

            // Группа товаров
            var Strgrup = MainWindow.db.Sprgrups.Where(p => p.KodGrup == ControlWin.ListConectionGrup[1]).FirstOrDefault();
            if (Strgrup != null)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodGrup == ControlWin.ListConectionGrup[1] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImageAdd.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = RelatedTovImageAdd;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strgrup.NameEN : Strgrup.NameUK;
            }

            // Вид товаров
            var Strvid = MainWindow.db.Sprvids.Where(p => p.Kodvid == ControlWin.ListConectionVid[1]).FirstOrDefault();
            if (Strvid != null && ListRelated2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodvid == ControlWin.ListConectionVid[1] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImageAdd.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = RelatedTovImageAdd;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strvid.NameEN : Strvid.NameUK;
            }

            // Подвид товаров
            var Strpvid = MainWindow.db.Sprpvids.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[1]).FirstOrDefault();
            if (Strpvid != null && ListRelated2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[1] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImageAdd.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = RelatedTovImageAdd;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strpvid.NameEN : Strpvid.NameUK;
            }
            // Один товар
            var Strtov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0]).FirstOrDefault();
            if (Strtov != null && ListRelated1.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    RelatedTovImageAdd.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = RelatedTovImageAdd;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strtov.NameTovEN : Strtov.NameTovUK;
            }
            ListRelated2.Focusable = false;
        }





        private void ButtonClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.NameSetRelated = "";
            this.Close();
        }


        private void ButtonPlus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Uri Minusjpg = new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
            ImageMinus.Source = new BitmapImage(Minusjpg);
            Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60plus.jpg", UriKind.Relative);
            ImagePlus.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) + 1 >= 99 ? "99" : (Convert.ToInt16(Kolvo.Content) + 1).ToString();
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
      
        }

 

        private void ButtonMinus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Uri Minusjpg = new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
            Kolvo.Content = Convert.ToInt16(Kolvo.Content) - 1 > 0 ? (Convert.ToInt16(Kolvo.Content) - 1).ToString() : "0";
            if(Convert.ToInt16(Kolvo.Content) == 0) Minusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            ImageMinus.Source = new BitmapImage(Minusjpg);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));

        }

        private void ButtonOk_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            //MessageOk NewOrder = new MessageOk("   ", 2, 0);
            //NewOrder.Show();
            MainWindow.TimeClick++;
            //DialogResult = true;
            //MainWindow.ActivRelated = 0;
            ListPriceWin.NameDog = "";
            MainWindow.Kol = Convert.ToInt16(Kolvo.Content);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            if (MainWindow.Kol > 0)ControlWin.Addbasket();
            else
            {
                var DelCuma = ListPriceWin.MapOrderList == 0 ? MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ControlWin.ListTov[4])).FirstOrDefault() :
                MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                MainWindow.SumZakazBasket -= DelCuma.SumTov;
                MainWindow.SumPaymentBasket -= DelCuma.SumTov;
                MainWindow.SumaOrder -= DelCuma.SumTov;
                MainWindow.db.OrederPreviews.RemoveRange(DelCuma);
                MainWindow.db.SaveChanges();

            }
            MainWindow.NameSetRelated = Kolvo.Content.ToString();
            this.Close();
        }

        
       

        private void InfoTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.TimeClick++;
            MainWindow.NameSetWindow = "RelatedTov";
            MainWindow.InfoWindow = 1;
  
        }


        private void ListRelated1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;

            if (((ListView)e.OriginalSource).Name.Equals("ListRelated1"))
            {
                if (ListRelated1.SelectedItems.Count == 0)
                {
                    ListRelated1.SelectedItem = null;
                    return;
                }

                SprTovImage OpisTov = (SprTovImage)ListRelated1.SelectedItem;
                ListPriceWin.IdTov = 0;
                ListPriceWin.SprTovKodTov = OpisTov.KodTov;
                ListPriceWin.SprPriceTov = OpisTov.PriceTov;
                ListRelated1.SelectedItem = null;
                LoadListRelated1();
                HotDog.KodTovPrice = Convert.ToInt32(ControlWin.ListTov[4]);
                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunSelectTov);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);



            }


        }

        public static void RunSelectTov(object ThreadObj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                RelatedTov Price_lnk = ControlWin.LinkMainWindow("RelatedTov");
                if (ListPriceWin.SprPriceTov != 0)
                {
                    MainWindow.UpdateLoadOpis = false;
                    MainWindow.KodVidTov = 0;
                    MainWindow.KodPvidTov = 0;
                    ControlWin.LoadOpisTov(ListPriceWin.SprTovKodTov, MainWindow.UpdateLoadOpis);
                    if (MainWindow.InfoWindow != 0)
                    {
                        MainWindow.NameSetWindow = "RelatedTov";
                        Price_lnk.Opacity = 0.3;
                        Window WinInfo = new InfoTov();
                        WinInfo.ShowDialog();
                        Price_lnk.Opacity = 1;
                        MainWindow.InfoWindow = 0;
                    }
                    else Price_lnk.SelectWinTov();

                }
                else
                {
                    if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0) MainWindow.KodPvidTov = ListPriceWin.SprTovKodTov;
                    if (MainWindow.KodVidTov == 0) MainWindow.KodVidTov = ListPriceWin.SprTovKodTov;

                    Price_lnk.LoadListRelated1();

                }
            }));

        }


        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }



        private void ListRelated2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            SetBludoList = 2;
            if (((ListView)e.OriginalSource).Name.Equals("ListRelated2"))
            {
                if (ListRelated2.SelectedItems.Count == 0) return;
                SprTovImage CartTov = (SprTovImage)ListRelated2.SelectedItem;
                if (CartTov != null)
                {

                    MainWindow.UpdateLoadOpis = false;
                    MainWindow.KodVidTov = 0;
                    MainWindow.KodPvidTov = 0;
                    HotDog.KodTovPrice = Convert.ToInt32(ControlWin.ListTov[4]);
                    ControlWin.LoadOpisTov(CartTov.KodTov, MainWindow.UpdateLoadOpis);
                    ListRelated2.SelectedItem = null;
                    LoadListRelated2();
                    if (MainWindow.InfoWindow == 0) SelectWinTov();
                    else WinInfoTov();
                }
            }

        }

        public void WinInfoTov()
        {
            this.Opacity = 0.3;
            MainWindow.NameSetWindow = "RelatedTov";
            Window WinInfo = new InfoTov();
            WinInfo.ShowDialog();
            this.Opacity = 1;
            MainWindow.InfoWindow = 0;
        }

        public void SelectWinTov()
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                ListPriceWin InfoTov_Top = ControlWin.LinkMainWindow("ListPriceWin");
                InfoTov_Top.Opacity = 0.3;

                this.Opacity = 0.3;
                Window WinOblako = new MapTov();
                WinOblako.ShowDialog();
                this.Opacity = 1;
                ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
                InfoTov_Top.OrderList.ItemsSource = ListOrderPreview;

                if (MainWindow.db.OrederPreviews.Count() != 0)
                {

                    InfoTov_Top.Text4.Content = MainWindow.SumZakazBasket.ToString();
                    InfoTov_Top.Masege.Visibility = Visibility.Hidden;
                    InfoTov_Top.Gorizont.Visibility = Visibility.Visible;
                    InfoTov_Top.Text3.Visibility = Visibility.Visible;
                    InfoTov_Top.Text4.Visibility = Visibility.Visible;
                    InfoTov_Top.Text5.Visibility = Visibility.Visible;
                    InfoTov_Top.OrderList.Visibility = Visibility.Visible;

                }
                else
                {
                    InfoTov_Top.OrderList.Visibility = Visibility.Hidden;
                    InfoTov_Top.Masege.Visibility = Visibility.Visible;
                    Gorizont.Visibility = Visibility.Hidden;
                    MainWindow.SumZakazBasket = 0;
                    MainWindow.SumPaymentBasket = 0;
                    MainWindow.SumaOrder = 0;
                    InfoTov_Top.Text4.Content = MainWindow.SumZakazBasket.ToString();
                }

                if (MainWindow.db.OrederPreviews.Count() > 4)
                {
                    InfoTov_Top.ScrolLeft.Visibility = Visibility.Visible;
                    InfoTov_Top.ScrolRight.Visibility = Visibility.Visible;
                }


            }));
            MainWindow.UpdateLoadOpis = true;
            ControlWin.LoadOpisTov(HotDog.KodTovPrice);
            Kolvo.Content = "1";
        }





    }
}
