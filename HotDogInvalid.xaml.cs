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
    /// Логика взаимодействия для HotDogInvalid.xaml
    /// </summary>
    public partial class HotDogInvalid : Window
    {
        public string  NameBunBlack= "темна булочка" , NameBunWait = "світла булочка";
        public static int PointBunWait = 0, PointBunBlack = 0, BludoList = 0;
        public static bool OnOffButtonDouwn = true, HotDogTouchButtonTov = false;
        public HotDogInvalid()
        {
            InitializeComponent();
            if (MainWindow.ScreenHeight > 1080) Top = 900;
            MainWindow.NameSetRelated = "HotDogInvalid";
            
            //--------------------------
            // Загрузка изображения выбранного товара

            //ListPriceWin LinkMapTov = ControlWin.LinkMainWindow("ListPriceWin");
            //var pict = (Image)LogicalTreeHelper.FindLogicalNode(LinkMapTov, "ImageTov");

            //Uri uri = new Uri("pack://application:,,/munch.png");
            //BitmapImage bitmap = new BitmapImage(uri);
            //Image img = new Image();
            //img.Source = bitmap;

            
            //HotDog ImageMapTov = ControlWin.LinkMainWindow("HotDog");
            //ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.ListTov[17], UriKind.Relative)));
            //ImageTov.Background = (System.Windows.Media.Brush)imageBrush;
            MainWindow.ColOnOff = 1; MainWindow.PapricOnOff = 1; MainWindow.MayonezOnOff = 1;
            Opacity = 1;
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
                BunBlack.Text = HotDog.NameBunWait; //"LIGHT BUN";
                BunWait.Text = HotDog.NameBunBlack;  //"DARK BUN";
                Col.Content = "Ketchup";
                Perec.Content = "Mayonnaise";
                Mayonez.Content = "Mustard";
                SetAdd.Content = "CHOOSE ADDITIONALLY";
            }
            else
            {
                Kolichestvo.Text = "Додати      до кошика:";
                BunBlack.Text = HotDog.NameBunWait; // "СВІТЛА БУЛОЧКА";
                BunWait.Text = HotDog.NameBunBlack; // "ТЕМНА БУЛОЧКА";
                Col.Content = "Кетчуп";
                Perec.Content = "Майонез";
                Mayonez.Content = "Горчиця";
                SetAdd.Content = "ОБЕРІТЬ ДОДАТКОВО";
            }
            string MemOpcii = "";
            int OpciyaCol = 0, OpciyaPaper = 0, OpciyaMayonez = 0;
            PointBunWait = 0;
            TitleTov.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[9] : ControlWin.ListTov[8];
            PriceTov.Content = ControlWin.ListTov[10];
            Reclama.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[20] : ControlWin.ListTov[16];
            Kolvo.Content = "1";
            BludoList = 0;
            ListPriceWin.SelectHotdog = 1;
            Col.Visibility = Visibility.Hidden;
            Perec.Visibility = Visibility.Hidden;
            Mayonez.Visibility = Visibility.Hidden;
            ImageKetchup.Visibility = Visibility.Hidden;
            ImagePaprik.Visibility = Visibility.Hidden;
            ImageMustard.Visibility = Visibility.Hidden;

            var OptionsStr = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ControlWin.ListTov[4])).FirstOrDefault();
            if (OptionsStr != null && ListPriceWin.MapOrderList == 0)  //Convert.ToInt32(ControlWin.ListTov[14]) != 0
            {
                var OptionsZap = MainWindow.db.OptionsTovs.Where(p => p.Kodtov == Convert.ToInt32(ControlWin.ListTov[4]));
                foreach (OptionsTov stroka in OptionsZap)
                {
                    if (stroka.KodOpcii == 1)
                    {
                        Col.Content = stroka.NameOptionsUk;
                        if (MainWindow.language_Key == 1) Col.Content = stroka.NameOptionsEn;
                        Col.Visibility = Visibility.Visible;
                        ImageKetchup.Visibility = Visibility.Visible;
                        MainWindow.ColOnOff = 1;
                        MainWindow.IdCol = stroka.Id;
                    }
                    if (stroka.KodOpcii == 2)
                    {
                        Perec.Content = stroka.NameOptionsUk;
                        if (MainWindow.language_Key == 1) Perec.Content = stroka.NameOptionsEn;
                        Perec.Visibility = Visibility.Visible;
                        ImagePaprik.Visibility = Visibility.Visible;
                        MainWindow.PapricOnOff = 1;
                        MainWindow.IdPaper = stroka.Id;
                    }
                    if (stroka.KodOpcii == 3)
                    {
                        Mayonez.Content = stroka.NameOptionsUk;
                        if (MainWindow.language_Key == 1) Mayonez.Content = stroka.NameOptionsEn;
                        Mayonez.Visibility = Visibility.Visible;
                        ImageMustard.Visibility = Visibility.Visible;
                        MainWindow.MayonezOnOff = 1;
                        MainWindow.IdMayonez = stroka.Id;
                    }

                }


            }

            //  Проверяем наличие товара в заказе
            if (ListPriceWin.MapOrderList == 1)
            {
                var PrevTovChange = MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                if (PrevTovChange != null) Kolvo.Content = Convert.ToString(PrevTovChange.Quantity);
                if (ListPriceWin.OrderNameTov.Contains(HotDog.NameBunBlack) == true)
                {
                    ListPriceWin.SelectHotdog = 2;
                    HotDog.ImageElow = "/PreOrderWin;component/Images/PointYelow.jpg";
                    HotDog.ImageBlack = "/PreOrderWin;component/Images/PointBlack.jpg";
                    Uri BunWaitjpg = new Uri(HotDog.ImageElow, UriKind.Relative);
                    ImageBunWait.Source = new BitmapImage(BunWaitjpg);
                    Uri BunBlackjpg = new Uri(HotDog.ImageBlack, UriKind.Relative);
                    ImageBunBlack.Source = new BitmapImage(BunBlackjpg);
                }
                MemOpcii = PrevTovChange.Opcii;
                MainWindow.MayonezOnOff = MainWindow.PapricOnOff = MainWindow.ColOnOff = 0;
                OpciyaCol = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                ApplicationContext dbOpcii = new ApplicationContext();
                if (OpciyaCol != 0)
                {
                    int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaCol).Count();
                    if (CountOpcii != 0)
                    {
                        Col.Visibility = Visibility.Visible;
                        ImageKetchup.Visibility = Visibility.Visible;
                        MainWindow.ColOnOff = 1;
                    }


                }
               
                MemOpcii = MemOpcii.Substring(MemOpcii.IndexOf(";") + 1, MemOpcii.Length - (MemOpcii.IndexOf(";") + 1));
                OpciyaPaper = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                if (OpciyaPaper != 0)
                {
                    int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaPaper).Count();
                    if (CountOpcii != 0)
                    {
                        Perec.Visibility = Visibility.Visible;
                        ImagePaprik.Visibility = Visibility.Visible;
                        MainWindow.PapricOnOff  = 1;
                    }

                }
                
                MemOpcii = MemOpcii.Substring(MemOpcii.IndexOf(";") + 1, MemOpcii.Length - (MemOpcii.IndexOf(";") + 1));
                OpciyaMayonez = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                if (OpciyaMayonez != 0)
                {

                    int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaMayonez).Count();
                    if (CountOpcii != 0)
                    {
                        Mayonez.Visibility = Visibility.Visible;
                        ImageMustard.Visibility = Visibility.Visible;
                        MainWindow.MayonezOnOff = 1;
                    }


                }
               


            }
            LoadRelatedInvalid();
            ImageOkInvalid.Focusable = false;
        }

        public void LoadRelatedInvalid()
        {


            NameRelated2.Text = "";
            string NameTov = "";
            byte[] FotoByte = new byte[0];
            HotDog.HotDogTovImage.Clear();
            ListRelatedInvalid2.ItemsSource = null;

            // Группа товаров
            var Strgrup = MainWindow.db.Sprgrups.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0]).FirstOrDefault();
            if (Strgrup != null)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDog.HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelatedInvalid2.ItemsSource = HotDog.HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strgrup.NameEN : Strgrup.NameUK;
            }

            // Вид товаров
            var Strvid = MainWindow.db.Sprvids.Where(p => p.Kodvid == ControlWin.ListConectionVid[0]).FirstOrDefault();
            if (Strvid != null && ListRelatedInvalid2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodvid == ControlWin.ListConectionVid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDog.HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelatedInvalid2.ItemsSource = HotDog.HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strvid.NameEN : Strvid.NameUK;
            }

            // Подвид товаров
            var Strpvid = MainWindow.db.Sprpvids.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0]).FirstOrDefault();
            if (Strpvid != null && ListRelatedInvalid2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDog.HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelatedInvalid2.ItemsSource = HotDog.HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strpvid.NameEN : Strpvid.NameUK;
            }
            // Один товар
            var Strtov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0]).FirstOrDefault();
            if (Strtov != null && ListRelatedInvalid2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDog.HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelatedInvalid2.ItemsSource = HotDog.HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strtov.NameTovEN : Strtov.NameTovUK;
            }
            if (ListRelatedInvalid2.Items.Count == 0) SetAdd.Content = "";
            //ListRelatedInvalid2.Focusable = false;
        }

        private void ButtonClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            CloseMapTov();
        }

        private void ButtonClose_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            CloseMapTov();
        }


        public void CloseMapTov()
        {
            MainWindow.TimeClick++;
            MainWindow.NameSetRelated = "";
            this.Close();
        }



        private void ButtonPlus_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            PlusSelect();
        }

        public void PlusSelect()
        {

            MainWindow.TimeClick++;
            Uri Minusjpg = new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
            ImageMinus.Source = new BitmapImage(Minusjpg);
            Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60plus.jpg", UriKind.Relative);
            ImagePlus.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) + 1 >= 99 ? "99" : (Convert.ToInt16(Kolvo.Content) + 1).ToString();
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            MainWindow.TimeClick++;
        }
    

   
        public void MinusSelect()
        {
            MainWindow.TimeClick++;
            //Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            //ImagePlus.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) - 1 > 0 ? (Convert.ToInt16(Kolvo.Content) - 1).ToString() : "0";
            Uri Minusjpg = Convert.ToInt16(Kolvo.Content) > 0 ? new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative) : new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            ImageMinus.Source = new BitmapImage(Minusjpg);

            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            MainWindow.TimeClick++;
        }

        private void OkInvalidButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
     
            MainWindow.TimeClick++;
            MainWindow.Kol = Convert.ToInt16(Kolvo.Content);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            if (MainWindow.Kol > 0)
            {

                if (OnOffButtonDouwn == true || HotDog.KetchupBool == true || HotDog.Mayonezbool == true || HotDog.MustardButtonDouwn == true)
                {

                    MessageOk NewOrder = new MessageOk(" ", 0, 0);
                    NewOrder.ShowDialog();
                }

                if (HotDog.OkImage != 0)
                {
                    ListPriceWin.NameDog = ListPriceWin.SelectHotdog == 1 ? HotDog.NameBunWait : HotDog.NameBunBlack; // 1-белая 2- темная
                    ControlWin.Addbasket();
               
                }


            }
            else
            {
                if (MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault() != null)
                {
                    var DelCuma = ListPriceWin.MapOrderList == 0 ? MainWindow.db.OrederPreviews.Where(p => p.Idtov == Convert.ToInt32(ControlWin.ListTov[4])).FirstOrDefault() :
                    MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                    MainWindow.SumZakazBasket -= DelCuma.SumTov;
                    MainWindow.SumPaymentBasket -= DelCuma.SumTov;
                    MainWindow.SumaOrder -= DelCuma.SumTov;
                    MainWindow.db.OrederPreviews.RemoveRange(DelCuma);
                    MainWindow.db.SaveChanges();
                }

            }
            MainWindow.NameSetRelated = Kolvo.Content.ToString();
            ListPriceWin.SprTovKodTov = 0;
            this.Close();

        }
          

        private void InfoTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.NameSetWindow = "HotDogInvalid";
            MainWindow.InfoWindow = 1;

        }

        private void BludoListInvalid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.TimeClick++;
            e.Handled = true;

            if (((ListView)e.OriginalSource).Name.Equals("ListRelatedInvalid2"))
            {

                if (ListRelatedInvalid2.SelectedItems.Count == 0)
                {
                    ListRelatedInvalid2.SelectedItem = null;
                    return;
                }
                SprTovImage OpisTov = (SprTovImage)ListRelatedInvalid2.SelectedItem;
                ListPriceWin.IdTov = OpisTov.Id;
                ListPriceWin.SprTovKodTov = OpisTov.KodTov;
                ListPriceWin.SprPriceTov = OpisTov.PriceTov;
                ListRelatedInvalid2.SelectedItem = null;
                 LoadRelatedInvalid();
              
                HotDog.KodTovPrice = Convert.ToInt32(ControlWin.ListTov[4]);
                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunSelectTov);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);

            }
        }
        public  void RunSelectTov(object ThreadObj)
        {
          
            ControlWin.LoadOpisTov(ListPriceWin.SprTovKodTov);
            if (MainWindow.InfoWindow == 0) SelectWinTov();
            else
            {
                    MainWindow.NameSetWindow = "HotDogInvalid";
                    this.Opacity = 0.3;
                    Window WinInfo = new InfoTov();
                    WinInfo.ShowDialog();
                    this.Opacity = 1;
                    MainWindow.InfoWindow = 0;
            }
                   
        }

        public void SelectWinTov()
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                PriceInvalid InfoTov_Top = ControlWin.LinkMainWindow("PriceInvalid");
                InfoTov_Top.Opacity = 0.3;
                this.Opacity = 0.3;

                Window WinOblako = new MapTov();
                //WinOblako.Owner = this;
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
                ControlWin.LoadOpisTov(HotDog.KodTovPrice);
                Kolvo.Content = "1";
            }));
 
 


        }

        private void ButtonKetchup_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            HotDog.KetchupBool = true;
            MainWindow.TimeClick++;
            if (MainWindow.ColOnOff == 0)
            {
                Col.Visibility = Visibility.Visible;
                OffCol.Visibility = Visibility.Hidden;
                ImageKetchup.Visibility = Visibility.Visible;
                MainWindow.ColOnOff = 1;
            }
            else
            {
                Col.Visibility = Visibility.Hidden;
                OffCol.Visibility = Visibility.Visible;
                ImageKetchup.Visibility = Visibility.Hidden;
                MainWindow.ColOnOff = 0;
            }
            //if (MainWindow.ColOnOff == 1)
            //{
            //   Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);

            //    ImageKetchup.Source = new BitmapImage(Plusjpg);
            //    MainWindow.ColOnOff = 0;
            //}
            //else
            //{
            //   Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
            //    ImageKetchup.Source = new BitmapImage(Plusjpg);
            //    MainWindow.ColOnOff = 1;
            //}

        }
    
        private void ButtonPaprik_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            HotDog.Mayonezbool = true;
            MainWindow.TimeClick++;
            if (MainWindow.PapricOnOff == 0)
            {

                Perec.Visibility = Visibility.Visible;

                OffPaprik.Visibility = Visibility.Hidden;
                ImagePaprik.Visibility = Visibility.Visible;
                MainWindow.PapricOnOff = 1;
            }
            else
            {
                Perec.Visibility = Visibility.Hidden;
                OffPaprik.Visibility = Visibility.Visible;
                ImagePaprik.Visibility = Visibility.Hidden;
                MainWindow.PapricOnOff = 0;
            }
            //if (MainWindow.PapricOnOff == 1)
            //{
            //   Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
 
            //    ImagePaprik.Source = new BitmapImage(Plusjpg);
            //    MainWindow.PapricOnOff = 0;
            //}
            //else
            //{
            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
            //    ImagePaprik.Source = new BitmapImage(Plusjpg);
            //    MainWindow.PapricOnOff = 1;
            //}

        }

   
  
        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        private void ButtonMustard_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            HotDog.MustardButtonDouwn = true;
            if (MainWindow.MayonezOnOff == 0)
            {

                Mayonez.Visibility = Visibility.Visible;
                OffMayonez.Visibility = Visibility.Hidden;
                ImageMustard.Visibility = Visibility.Visible;
                MainWindow.MayonezOnOff = 1;
            }
            else
            {
                Mayonez.Visibility = Visibility.Hidden;
                OffMayonez.Visibility = Visibility.Visible;
                ImageMustard.Visibility = Visibility.Hidden;
                MainWindow.MayonezOnOff = 0;
            }
            //if (MainWindow.MayonezOnOff == 1)
            //{
            // Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);

            //    ImageMustard.Source = new BitmapImage(Plusjpg);
            //    MainWindow.MayonezOnOff = 0;
            //}
            //else
            //{
            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
            //    ImageMustard.Source = new BitmapImage(Plusjpg);
            //    MainWindow.MayonezOnOff = 1;
            //}
        }

        private void ButtonBunBlack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            WaitSelect();
        }

        private void ButtonBunWait_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            WaitSelect();
        }

       

     private void ButtonKetchup_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.ColOnOff == 0)
            {
                Col.Visibility = Visibility.Visible;
                OffCol.Visibility = Visibility.Hidden;
                ImageKetchup.Visibility = Visibility.Visible;
                MainWindow.ColOnOff = 1;
            }
            else
            {

                Col.Visibility = Visibility.Hidden;
                OffCol.Visibility = Visibility.Visible;
                ImageKetchup.Visibility = Visibility.Hidden;
                MainWindow.ColOnOff = 0;
            }
        }  

        private void TakeColOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.ColOnOff == 0)
            {
                Col.Visibility = Visibility.Visible;
                OffCol.Visibility = Visibility.Hidden;
                ImageKetchup.Visibility = Visibility.Visible;
                MainWindow.ColOnOff = 1;
            }
            else
            {
                
                Col.Visibility = Visibility.Hidden;
                OffCol.Visibility = Visibility.Visible;
                ImageKetchup.Visibility = Visibility.Hidden;
                MainWindow.ColOnOff = 0;
            }
        }

        private void TakePaprikOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.PapricOnOff == 0)
            {
                Perec.Visibility = Visibility.Visible;
                OffPaprik.Visibility = Visibility.Hidden;
                ImagePaprik.Visibility = Visibility.Visible;
                MainWindow.PapricOnOff = 1;
            }
            else
            {
                Perec.Visibility = Visibility.Hidden;
                OffPaprik.Visibility = Visibility.Visible;
                ImagePaprik.Visibility = Visibility.Hidden;
                MainWindow.PapricOnOff = 0;
            }
        }

        private void ButtonPaprik_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.PapricOnOff == 0)
            {
                Perec.Visibility = Visibility.Visible;
                OffPaprik.Visibility = Visibility.Hidden;
                ImagePaprik.Visibility = Visibility.Visible;
                MainWindow.PapricOnOff = 1;
            }
            else
            {
                Perec.Visibility = Visibility.Hidden;
                OffPaprik.Visibility = Visibility.Visible;
                ImagePaprik.Visibility = Visibility.Hidden;
                MainWindow.PapricOnOff = 0;
            }
        }

        private void TakeMayonezOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.MayonezOnOff == 0)
            {
                Mayonez.Visibility = Visibility.Visible;
                OffMayonez.Visibility = Visibility.Hidden;
                ImageMustard.Visibility = Visibility.Visible;
                MainWindow.MayonezOnOff = 1;
            }
            else
            {
                Mayonez.Visibility = Visibility.Hidden;
                OffMayonez.Visibility = Visibility.Visible;
                ImageMustard.Visibility = Visibility.Hidden;
                MainWindow.MayonezOnOff = 0;
            }
        }

        

        private void ButtonPlus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            PlusSelect();
        }

        private void ButtonMinus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MinusSelect();
        }

   

        public void WaitSelect()
        {
            MainWindow.TimeClick++;
            string ImageElow = "/PreOrderWin;component/Images/PointYelow.jpg", ImageBlack = "/PreOrderWin;component/Images/PointBlack.jpg";
            if (ListPriceWin.SelectHotdog == 1)
            {
                ImageElow = "/PreOrderWin;component/Images/PointYelow.jpg";
                ImageBlack = "/PreOrderWin;component/Images/PointBlack.jpg";
                ListPriceWin.SelectHotdog = 2;
            }
            else
            {
                ImageElow = "/PreOrderWin;component/Images/PointBlack.jpg";
                ImageBlack = "/PreOrderWin;component/Images/PointYelow.jpg";
                ListPriceWin.SelectHotdog = 1;
            }

            Uri BunWaitjpg = new Uri(ImageElow, UriKind.Relative);
            ImageBunWait.Source = new BitmapImage(BunWaitjpg);
            Uri BunBlackjpg = new Uri(ImageBlack, UriKind.Relative);
            ImageBunBlack.Source = new BitmapImage(BunBlackjpg);
        }

   
       
    }
}
