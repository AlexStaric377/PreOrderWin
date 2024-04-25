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
    /// Логика взаимодействия для HotDog.xaml
    /// </summary>
    public partial class HotDog : Window
    {

        public static string Uri_ImageTov = "", NameBunBlack= "темна булочка" , NameBunWait = "світла булочка";
        public static string ImageElow = "/PreOrderWin;component/Images/PointYelow.jpg", ImageBlack = "/PreOrderWin;component/Images/PointBlack.jpg";
        public static int PointBunWait = 0, PointBunBlack = 0, BludoList = 0, KodTovPrice = 0, OkImage = 1;
        public static bool OnOffButtonDouwn = false, ButtonPlusPreview = false, ButtonClose= false, KetchupBool = false, Mayonezbool= false, MustardButtonDouwn= false;
        public static List<SprTovImage> HotDogTovImage = new List<SprTovImage>(1);
        public HotDog()
        {
            
    
            InitializeComponent();
            
            if (MainWindow.ScreenHeight > 1080) Top = 362;
            MainWindow.NameSetRelated = "HotDog";

            //--------------------------
            // Загрузка изображения выбранного товара

            //ListPriceWin LinkMapTov = ControlWin.LinkMainWindow("ListPriceWin");
            //var pict = (Image)LogicalTreeHelper.FindLogicalNode(LinkMapTov, "ImageTov");

            //Uri uri = new Uri("pack://application:,,/munch.png");
            //BitmapImage bitmap = new BitmapImage(uri);
            //Image img = new Image();
            //img.Source = bitmap;

           
            MainWindow.ColOnOff = 1; MainWindow.PapricOnOff = 1; MainWindow.MayonezOnOff = 1;
            Opacity = 1;
            //HotDog ImageMapTov = ControlWin.LinkMainWindow("HotDog");
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
            OnOffButtonDouwn = false; ButtonPlusPreview = false; ButtonClose = false; KetchupBool = false; Mayonezbool = false; MustardButtonDouwn = false;
            int OpciyaCol =  0, OpciyaPaper = 0, OpciyaMayonez =0;
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
            ImageCol.Visibility = Visibility.Hidden;
            ImagePaprik.Visibility = Visibility.Hidden;
            ImageMayonez.Visibility = Visibility.Hidden;
            MainWindow.MayonezOnOff = MainWindow.PapricOnOff = MainWindow.ColOnOff = 0;
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
                        ImageCol.Visibility = Visibility.Visible;
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
                        ImageMayonez.Visibility = Visibility.Visible;
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
                    ImageElow = "/PreOrderWin;component/Images/PointYelow.jpg";
                    ImageBlack = "/PreOrderWin;component/Images/PointBlack.jpg";
                    Uri BunWaitjpg = new Uri(HotDog.ImageElow, UriKind.Relative);
                    ImageBunWait.Source = new BitmapImage(BunWaitjpg);
                    Uri BunBlackjpg = new Uri(HotDog.ImageBlack, UriKind.Relative);
                    ImageBunBlack.Source = new BitmapImage(BunBlackjpg);
                }
                //MainWindow.MayonezOnOff = MainWindow.PapricOnOff = MainWindow.ColOnOff = 0;
                MemOpcii = PrevTovChange.Opcii;

                OpciyaCol = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                ApplicationContext dbOpcii = new ApplicationContext();
                if (OpciyaCol != 0)
                {
                    int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaCol).Count();
                    if (CountOpcii != 0)
                    {
                        Col.Visibility = Visibility.Visible;
                        ImageCol.Visibility = Visibility.Visible;
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
                        MainWindow.PapricOnOff = 1;
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
                        ImageMayonez.Visibility = Visibility.Visible;
                        MainWindow.MayonezOnOff = 1;
                    }


                }
             

            }
            LoadRelated();
            //ImageOkHotdog.Focusable = false;
        }

    
        public void LoadRelated()
        {
            NameRelated2.Text = "";
            string NameTov = "";
            byte[] FotoByte = new byte[0];
            HotDogTovImage.Clear();
            ListRelated2.ItemsSource = null;
 
            // Группа товаров
            var Strgrup = MainWindow.db.Sprgrups.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0]).FirstOrDefault();
            if (Strgrup != null)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodGrup == ControlWin.ListConectionGrup[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }
 
                ListRelated2.ItemsSource = HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strgrup.NameEN : Strgrup.NameUK;
            }
 
            // Вид товаров
            var Strvid = MainWindow.db.Sprvids.Where(p => p.Kodvid == ControlWin.ListConectionVid[0]).FirstOrDefault();
            if (Strvid != null && ListRelated2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodvid == ControlWin.ListConectionVid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strvid.NameEN : Strvid.NameUK;
            }

            // Подвид товаров
            var Strpvid = MainWindow.db.Sprpvids.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0]).FirstOrDefault();
            if (Strpvid != null && ListRelated2.Items.Count ==0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.Kodpvid == ControlWin.ListConectionPvid[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strpvid.NameEN : Strpvid.NameUK;
            }
            // Один товар
            var Strtov = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0]).FirstOrDefault();
            if (Strtov != null && ListRelated2.Items.Count == 0)
            {
                var ListBludo = MainWindow.db.Sprtovs.Where(p => p.KodTov == ControlWin.ListConectionTov[0] && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(row => row.KodTov);
                foreach (Sprtov pole in ListBludo)
                {
                    NameTov = pole.NameTovUK;
                    if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                    HotDogTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                }

                ListRelated2.ItemsSource = HotDogTovImage;
                NameRelated2.Text = MainWindow.language_Key == 1 ? Strtov.NameTovEN : Strtov.NameTovUK;
            }
            if (ListRelated2.Items.Count == 0) SetAdd.Content = "";
            ListRelated2.SelectedItem = null;
 
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
            ButtonPlusPreview = true;
            MainWindow.TimeClick++;
            Uri Minusjpg = new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
            ImageMinusHotdog.Source = new BitmapImage(Minusjpg);
            Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60plus.jpg", UriKind.Relative);
            ImagePlusHotdog.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) + 1 >= 99 ? "99" : (Convert.ToInt16(Kolvo.Content) + 1).ToString();
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            
        }

        private void ButtonMinusHotdog_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            
            //Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            //ImagePlus.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) - 1 > 0 ? (Convert.ToInt16(Kolvo.Content) - 1).ToString() : "0";
            Uri Minusjpg = Convert.ToInt16(Kolvo.Content) > 0 ? new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative) : new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            ImageMinusHotdog.Source = new BitmapImage(Minusjpg);

            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            
        }


        private void ButtonOkHotdog_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
 

          
            //Thread.Sleep(100);
            
 
            MainWindow.TimeClick++;
            MainWindow.Kol = Convert.ToInt16(Kolvo.Content);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10])); 
            if (MainWindow.Kol > 0)
            {
                //MessageBox.Show("ButtonOkHotdog");|| ButtonPlusPreview == true || ButtonClose == true 
                if (OnOffButtonDouwn == true || KetchupBool == true || Mayonezbool == true || MustardButtonDouwn == true)
                { 

                    MessageOk NewOrder = new MessageOk(" ", 0, 0);
                    NewOrder.ShowDialog();
                }

                if (OkImage != 0)
                { 
                    ListPriceWin.NameDog = ListPriceWin.SelectHotdog == 1 ? HotDog.NameBunWait : HotDog.NameBunBlack; // 1-белая 2- темная
                    ControlWin.Addbasket();               
                    this.Close();
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
                this.Close();

            }
            
 

        }

            

        private void InfoTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.TimeClick++;
            MainWindow.NameSetWindow = "HotDog";
            MainWindow.InfoWindow = 1;
   
        }

        private void TakeColOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.ColOnOff == 0)
            {
                Col.Visibility = Visibility.Visible;
                OffCol.Visibility = Visibility.Hidden;
                ImageCol.Visibility = Visibility.Visible;
                MainWindow.ColOnOff = 1;
            }
            else
            {
                Col.Visibility = Visibility.Hidden;
                OffCol.Visibility = Visibility.Visible;
                ImageCol.Visibility = Visibility.Hidden;
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

        private void TakeMayonezOnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (MainWindow.MayonezOnOff == 0)
            {
 
                Mayonez.Visibility = Visibility.Visible;
                OffMayonez.Visibility = Visibility.Hidden;
                ImageMayonez.Visibility = Visibility.Visible;
                MainWindow.MayonezOnOff  = 1;
            }
            else
            {
                Mayonez.Visibility = Visibility.Hidden;
                OffMayonez.Visibility = Visibility.Visible;
                ImageMayonez.Visibility = Visibility.Hidden;
                MainWindow.MayonezOnOff = 0;
            }
        }

        private void BludoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             e.Handled = true;
            MainWindow.TimeClick++;

            if (ListRelated2.SelectedItems.Count == 0) 
            {
                ListRelated2.SelectedItem = null;
                return;
            }
 
            SprTovImage OpisTov = (SprTovImage)ListRelated2.SelectedItem;
            ListPriceWin.IdTov = 0;
            ListPriceWin.SprTovKodTov = OpisTov.KodTov;
            ListPriceWin.SprPriceTov = OpisTov.PriceTov;

            LoadRelated();
            HotDog.KodTovPrice = Convert.ToInt32(ControlWin.ListTov[4]);
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(RunSelectTov);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);
  

        }

        public static void RunSelectTov(object ThreadObj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                HotDog Price_lnk = ControlWin.LinkMainWindow("HotDog");
                if (ListPriceWin.SprPriceTov != 0)
                {
                    MainWindow.UpdateLoadOpis = false;
                    MainWindow.KodVidTov = 0;
                    MainWindow.KodPvidTov = 0;
                    ControlWin.LoadOpisTov(ListPriceWin.SprTovKodTov, MainWindow.UpdateLoadOpis);
                    if (MainWindow.InfoWindow != 0)
                    {
                        MainWindow.NameSetWindow = "HotDog";
                        Price_lnk.Opacity = 0.3;
                        Window WinInfo = new InfoTov();
                        WinInfo.ShowDialog();
                        Price_lnk.Opacity = 1;
                        MainWindow.InfoWindow = 0;
                    }
                    else
                    {
                                       
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {

                            ListPriceWin InfoTov_Top = ControlWin.LinkMainWindow("ListPriceWin");
                            InfoTov_Top.Opacity = 0.3;
                            Price_lnk.Opacity = 0.3;
                            Window WinOblako = new MapTov();
                            WinOblako.ShowDialog();

                            Price_lnk.Opacity = 1;
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
                                Price_lnk.Gorizont.Visibility = Visibility.Hidden;
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
                        ControlWin.LoadOpisTov(KodTovPrice);
                        Price_lnk.Kolvo.Content = "1";
                    }


                }
                else
                {
                    if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0) MainWindow.KodPvidTov = ListPriceWin.SprTovKodTov;
                    if (MainWindow.KodVidTov == 0) MainWindow.KodVidTov = ListPriceWin.SprTovKodTov;

                    Price_lnk.LoadRelated();
                }
            }));

        }

  

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        

        //public void KetchupSelect()
        //{ 
        //    MainWindow.TimeClick++;
        //    if (MainWindow.ColOnOff == 1)
        //    {
        //        Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
        //        ImageCol.Source = new BitmapImage(Plusjpg);
        //        MainWindow.ColOnOff = 0;
        //    }
        //    else
        //    {
 
        //       Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
        //        ImageCol.Source = new BitmapImage(Plusjpg);
        //        MainWindow.ColOnOff = 1;
        //    }
        
          
        //}

        private void Ketchup_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            KetchupBool = true;
            MainWindow.TimeClick++;

            if (MainWindow.ColOnOff == 0)
            {
                Col.Visibility = Visibility.Visible;
                OffCol.Visibility = Visibility.Hidden;
                ImageCol.Visibility = Visibility.Visible;
                MainWindow.ColOnOff = 1;
            }
            else
            {

                Col.Visibility = Visibility.Hidden;
                OffCol.Visibility = Visibility.Visible;
                ImageCol.Visibility = Visibility.Hidden;
                MainWindow.ColOnOff = 0;
            }

            //if (MainWindow.ColOnOff == 1)
            //{
            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            //    ImageCol.Source = new BitmapImage(Plusjpg);
            //    MainWindow.ColOnOff = 0;
            //}
            //else
            //{

            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
            //    ImageCol.Source = new BitmapImage(Plusjpg);
            //    MainWindow.ColOnOff = 1;
            //}
        }

        private void Paprik_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Mayonezbool = true;
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

            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
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

   

        //public void MayonezSelect()
        //{ 
        //    MainWindow.TimeClick++;
        //    if (MainWindow.PapricOnOff == 1)
        //    {

        //       Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
        //        ImagePaprik.Source = new BitmapImage(Plusjpg);
        //        MainWindow.PapricOnOff = 0;
        //    }
        //    else
        //    {
        //         Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
        //        ImagePaprik.Source = new BitmapImage(Plusjpg);
        //        MainWindow.PapricOnOff = 1;
        //    }
    
        //}

        private void Mustard_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MustardButtonDouwn = true;
            MainWindow.TimeClick++;
            if (MainWindow.MayonezOnOff == 0)
            {
                Mayonez.Visibility = Visibility.Visible;
                OffMayonez.Visibility = Visibility.Hidden;
                ImageMayonez.Visibility = Visibility.Visible;
                MainWindow.MayonezOnOff = 1;
            }
            else
            {
                Mayonez.Visibility = Visibility.Hidden;
                OffMayonez.Visibility = Visibility.Visible;
                ImageMayonez.Visibility = Visibility.Hidden;
                MainWindow.MayonezOnOff = 0;
            }

            //if (MainWindow.MayonezOnOff == 1)
            //{

            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            //    ImageMayonez.Source = new BitmapImage(Plusjpg);
            //    MainWindow.MayonezOnOff = 0;
            //}
            //else
            //{
            //    Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
            //    ImageMayonez.Source = new BitmapImage(Plusjpg);
            //    MainWindow.MayonezOnOff = 1;
            //}
        }
   

        //public void MustardSelect()
        //{ 
        //    MainWindow.TimeClick++;
        //    if (MainWindow.MayonezOnOff == 1)
        //    {

        //       Uri Plusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
        //        ImageMayonez.Source = new BitmapImage(Plusjpg);
        //        MainWindow.MayonezOnOff = 0;
        //    }
        //    else
        //    {
        //         Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60Select.jpg", UriKind.Relative);
        //        ImageMayonez.Source = new BitmapImage(Plusjpg);
        //        MainWindow.MayonezOnOff = 1;
        //    }
    
        //}

   

        private void ButtonBunWait_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            WaitSelect();
        }


        private void ButtonBunBlack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            
            WaitSelect();
        }


        public void WaitSelect()
        { 
            MainWindow.TimeClick++;
            OnOffButtonDouwn = true;
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
