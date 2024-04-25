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
    /// Логика взаимодействия для PriceInvalid.xaml
    /// </summary>
    public partial class PriceInvalid : Window
    {
        public static string NameTovPrice = "", NameDog = "", StrokaKodTov = "", StrokaIdTov = "", StrokaIndex = "";
        public static int IdCartTov = 0, IdCartTovInv = 0, BludoListSet = 0, LoadBludoList = 0, ValueKodTov=0, SelectAll = 0;
        public static bool InvalidTouchButton = true, TovButton = false;

        public PriceInvalid()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "PriceInvalid";
            if (MainWindow.language_Key == 1)
            {
          

                Masege.Content = "You have nothing ordered";
                DeletOrder.Content = "CANCEL ORDERS ";
                Ok.Content = "FINISH";
                BackTake.Content = "BACK";
                Text1.Content = "MY ORDER -";
                if (MainWindow.RestHier == 0) Text2.Content = "HERE";
                else Text2.Content = "WITH YOURSELF";
                Text3.Content = "TOTAL:";
                TitleGrup.Content = "";
            }
            else
            {

             
                Masege.Content = "Ви ще нічого не замовили";
                DeletOrder.Content = "СКАСУВАТИ ЗАМОВЛЕННЯ";
                Ok.Content = "ГОТОВО";
                BackTake.Content = "НАЗАД";
                Text1.Content = "МОЄ ЗАМОВЛЕННЯ - ";

                if (MainWindow.RestHier == 0) Text2.Content = "У ЗАКЛАДІ";
                else Text2.Content = "З СОБОЮ";
                Text3.Content = "РАЗОМ:";
                TitleGrup.Content = "";
            }
            SelectAll=0;
   
            LoadGrupTovs();
            LoadListPrice();
            //------------------------------------
            ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
            OrderList.ItemsSource = ListOrderPreview;

            if (MainWindow.db.OrederPreviews.Count() == 0)
            {

                Text3.Visibility = Visibility.Hidden;
                Text4.Visibility = Visibility.Hidden;
                Text5.Visibility = Visibility.Hidden;
                OrderList.Visibility = Visibility.Hidden;
                Masege.Visibility = Visibility.Visible;
                Gorizont.Visibility = Visibility.Hidden;
            }
            else
            {
                Text4.Content = MainWindow.SumZakazBasket.ToString();
                Masege.Visibility = Visibility.Hidden;
                Gorizont.Visibility = Visibility.Visible;

            }
            ScrolLeft.Visibility = Visibility.Hidden;
            ScrolRight.Visibility = Visibility.Hidden;
            if (MainWindow.db.OrederPreviews.Count() > 4)
            {
                ScrolLeft.Visibility = Visibility.Visible;
                ScrolRight.Visibility = Visibility.Visible;
            }
            Text4.Content = MainWindow.SumZakazBasket.ToString();
 
        }

        private void GrupTovList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            MainWindow.KodPvidTov = 0;
            MainWindow.KodVidTov = 0;
            MainWindow.TimeClick++;
            if (ListGrupTovs.SelectedItems.Count == 0) return;
            SprGrupImage p = (SprGrupImage)ListGrupTovs.SelectedItem;

            TitleGrup.Content = p.Name;
            MainWindow.IdGrupTov = p.Id;
            ListGrupTovs.SelectedItem = null;
 
            LoadGrupTovs();
            LoadListPrice();
        }


        public void LoadGrupTovs()
        {
            string NameGrup = "";
            ListPriceWin.ListGrupImage.Clear();
            ListGrupTovs.ItemsSource = null;
 
            var GrupTovs = MainWindow.db.Sprgrups.OrderBy(p => p.KodGrup).ToList<Sprgrup>();
            foreach (Sprgrup pole in GrupTovs)
            {
                if (MainWindow.language_Key == 1) NameGrup = pole.NameEN;
                else NameGrup = pole.NameUK;
                int CountFoto = MainWindow.db.ImagesTovs.Where(p => p.Id == pole.IDFotoGrup).Count();
                if (CountFoto != 0)
                {
                    var Foto = MainWindow.db.ImagesTovs.Where(p => p.Id == pole.IDFotoGrup).FirstOrDefault();
                    byte[] FotoByte = Foto.FotoGruopTov;
                    ListPriceWin.ListGrupImage.Add(new SprGrupImage(pole.Id, NameGrup, FotoByte));
                }
            }

            ListGrupTovs.ItemsSource = ListPriceWin.ListGrupImage;
        }


        public void LoadListPrice()
        {


            if (MainWindow.IdGrupTov == 0)
            {

                var FirstGrup = MainWindow.db.Sprgrups.ToList().OrderBy(p => p.KodGrup);
                foreach (Sprgrup pole in FirstGrup)
                {
                    MainWindow.IdGrupTov = pole.KodGrup;
                    TitleGrup.Content = MainWindow.language_Key == 1 ? pole.NameEN : pole.NameUK;
                    break;
                }

            }
            else
            {
                var FirstGrup = MainWindow.db.Sprgrups.Where(p => p.KodGrup == MainWindow.IdGrupTov).FirstOrDefault();
                MainWindow.IdGrupTov = FirstGrup.KodGrup;
                TitleGrup.Content = MainWindow.language_Key == 1 ? FirstGrup.NameEN : FirstGrup.NameUK;
            }


            //List<SprTovImage> ListTovImage = new List<SprTovImage>(1);
            ListPriceWin.ListTovImage.Clear();
            Listtov.ItemsSource = null;
            ListVid.ItemsSource = null;
            string NameTov = "";
            bool KodVidTov = false, KodPvidTov = false;
            byte[] FotoByte = new byte[0];
            if (MainWindow.KodVidTov == 0)
            {
                var Tovar = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov).FirstOrDefault();
                if (Tovar != null)
                {
                    if (Tovar.Kodvid == 0)
                    {
                        var ListSprtov = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov);
                        foreach (Sprtov pole in ListSprtov)
                        {
                            if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                            else NameTov = pole.NameTovUK;
                            ListPriceWin.ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                        }

                    }

                    if (Tovar.Kodvid != 0)
                    {
                        var VidTov = MainWindow.db.Sprvids.ToList().Where(p => p.KodGrup == MainWindow.IdGrupTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid); //&& p.Kodvid == Tovar.Kodvid
                        foreach (Sprvid zapvid in VidTov)
                        {
                            if (MainWindow.language_Key == 1) NameTov = zapvid.NameEN;
                            else NameTov = zapvid.NameUK;
                            int CountFoto = MainWindow.db.ImagesTovs.Where(p => p.Id == zapvid.IDFotovid).Count();
                            if (CountFoto != 0)
                            {
                                var Foto = MainWindow.db.ImagesTovs.Where(p => p.Id == zapvid.IDFotovid).FirstOrDefault();
                                FotoByte = Foto.FotoGruopTov;

                            }
                            ListPriceWin.ListTovImage.Add(new SprTovImage(zapvid.Id, zapvid.Kodvid, 0, NameTov, FotoByte));
                        }
                        KodVidTov = ListPriceWin.ListTovImage.Count > 0 ? true : false;
                    }
                }
                else ListPriceWin.ListTovImage = new List<SprTovImage>(1);
            }
            if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0)
            {
                var Tovar = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov).FirstOrDefault();
                if (Tovar != null)
                {
                    if (Tovar.Kodpvid == 0)
                    {
                        var ListSprtov = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov);
                        foreach (Sprtov pole in ListSprtov)
                        {
                            if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                            else NameTov = pole.NameTovUK;
                            ListPriceWin.ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                        }
                    }

                    if (Tovar.Kodpvid != 0)
                    {
                        var VidTov = MainWindow.db.Sprpvids.ToList().Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid);
                        foreach (Sprpvid zapvid in VidTov)
                        {
                            if (MainWindow.language_Key == 1) NameTov = zapvid.NameEN;
                            else NameTov = zapvid.NameUK;
                            int CountFoto = MainWindow.db.ImagesTovs.Where(p => p.Id == zapvid.IDFotovid).Count();
                            if (CountFoto != 0)
                            {
                                var Foto = MainWindow.db.ImagesTovs.Where(p => p.Id == zapvid.IDFotovid).FirstOrDefault();
                                FotoByte = Foto.FotoGruopTov;
                            }
                            ListPriceWin.ListTovImage.Add(new SprTovImage(zapvid.Id, zapvid.Kodpvid, 0, NameTov, FotoByte));
                        }
                        KodPvidTov = ListPriceWin.ListTovImage.Count > 0 ? true : false;
                    }
                }
                else
                {
                    ListPriceWin.ListTovImage = new List<SprTovImage>(1);
                    MainWindow.KodPvidTov = 0;
                    MainWindow.KodVidTov = 0;
                }
            }
            if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov != 0)
            {
                var Tovar = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov && p.Kodpvid == MainWindow.KodPvidTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov).FirstOrDefault();
                if (Tovar != null)
                {
                    var ListSprtov = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov && p.Kodpvid == MainWindow.KodPvidTov && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov);
                    foreach (Sprtov pole in ListSprtov)
                    {
                        if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                        else NameTov = pole.NameTovUK;
                        ListPriceWin.ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                    }
                }
                else
                {
                    ListPriceWin.ListTovImage = new List<SprTovImage>(1);
                    MainWindow.KodPvidTov = 0;
                    MainWindow.KodVidTov = 0;
                }
            }
            Listtov.ItemsSource = ListPriceWin.ListTovImage;
            ListVid.ItemsSource = ListPriceWin.ListTovImage;
            Listtov.Visibility = (KodPvidTov == true || KodVidTov == true) ? Visibility.Hidden : Visibility;
            Listtov.Focusable = false;
        }
        private void ListTov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.TimeClick++;
            e.Handled = true;
            
            if (((ListView)e.OriginalSource).Name.Equals("Listtov"))
            {
                if (Listtov.SelectedItems.Count == 0 || IdCartTovInv == 1) //|| TovButton == true || ListPriceWin.TouchButtonTov == true
                {
                    Listtov.SelectedItem = null;
                    return;
                }
                SprTovImage OpisTov = (SprTovImage)Listtov.SelectedItem;

                if (Listtov.SelectedIndex == 5 && ListPriceWin.TouchButtonTov == true) // || PriceInvalid.TovButton == true
                {

                    if (MainWindow.db.OrederPreviews.Where(p => p.Idtov == OpisTov.KodTov).FirstOrDefault() != null)
                    {
                        Listtov.SelectedItem = null;
                        return;
                    }
                    else ListPriceWin.TouchButtonTov = false;

                }
                if (Listtov.SelectedIndex == 5 && ListPriceWin.TouchButtonTov == false) ListPriceWin.TouchButtonTov = true;
                ListPriceWin.SprTovKodTov = OpisTov.KodTov;
                ListPriceWin.SprPriceTov = OpisTov.PriceTov;
                Listtov.SelectedItem = null;
                Listtov.Focusable = false;             
                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunInvSelectTov);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);
 
 

            }
            if (((ListView)e.OriginalSource).Name.Equals("ListVid"))
            {

                if (ListVid.SelectedItems.Count == 0 || IdCartTov == 1) //|| TouchButtonTov == true || PriceInvalid.TovButton == true
                {
                    ListVid.SelectedItem = null;
                    return;
                }
                SprTovImage OpisTov = (SprTovImage)ListVid.SelectedItem;


                if (ListVid.SelectedIndex == 5 && ListPriceWin.TouchButtonTov == true) // || PriceInvalid.TovButton == true
                {

                    if (MainWindow.db.OrederPreviews.Where(p => p.Idtov == OpisTov.KodTov).FirstOrDefault() != null)
                    {
                        ListVid.SelectedItem = null;
                        return;
                    }
                    else ListPriceWin.TouchButtonTov = false;

                }
                if (ListVid.SelectedIndex == 5 && ListPriceWin.TouchButtonTov == false) ListPriceWin.TouchButtonTov = true;
                ListPriceWin.SprTovKodTov = OpisTov.KodTov;
                ListPriceWin.SprPriceTov = OpisTov.PriceTov;
                ListVid.SelectedItem = null;
                ListVid.Focusable = false;               

                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunInvSelectTov);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);
 

            }

        }

        public static void RunInvSelectTov(object ThreadObj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                PriceInvalid Price_lnk = ControlWin.LinkMainWindow("PriceInvalid");
   
                if (ListPriceWin.SprPriceTov != 0)
                {
                    MainWindow.KodVidTov = 0;
                    MainWindow.KodPvidTov = 0;
                    ControlWin.LoadOpisTov(ListPriceWin.SprTovKodTov);
                    if (MainWindow.InfoWindow != 0)
                    {
                        Window WinInfo = new InfoTov();
                        Price_lnk.Opacity = 0.3;
                        //WinInfo.Owner = this;
                        WinInfo.ShowDialog();
                        Price_lnk.Opacity = 1;
                        MainWindow.InfoWindow = 0;
                    }
                    else Price_lnk.SelectCartTov();

                }
                else
                {
                    if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0) MainWindow.KodPvidTov = ListPriceWin.SprTovKodTov;
                    if (MainWindow.KodVidTov == 0) MainWindow.KodVidTov = ListPriceWin.SprTovKodTov;

                    Price_lnk.LoadListPrice();
                }
            }));
        }

      
        public void SelectCartTov()
        {
            ListPriceWin.SelectHotdog = 0;
            this.Opacity = 0.3;
            Listtov.ItemsSource = null;

            if (ControlWin.OnOffConectionGrup == 0 && ControlWin.CompositionTovIdtovBaz == 0)
            {

    
                Window WinOblako = new MapTov();
                WinOblako.Owner = this;
                WinOblako.ShowDialog();
                RefreshResultSelectTov();
                return;
            }
            if (ControlWin.OnOffConectionGrup != 0 && ControlWin.CompositionTovIdtovBaz == 0)
            {

   
                Window WinOblako = new RelatedInvalid();
                WinOblako.Owner = this;
                WinOblako.ShowDialog();
                RefreshResultSelectTov();
                //Nullable<bool> dialogResult = WinOblako.ShowDialog();
                //if (WinOblako.DialogResult.HasValue && WinOblako.DialogResult.Value) RefreshResultSelectTov();
                return;
            }
            if (ControlWin.CompositionTovIdtovBaz != 0)   //ControlWin.OnOffConectionGrup != 0 &&
            {

 
                Window WinOblako = new HotDogInvalid();
                WinOblako.Owner = this;
                WinOblako.ShowDialog();
                RefreshResultSelectTov();
                return;
            }

        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        public void RefreshResultSelectTov()
        {
            this.Opacity = 1;
            Listtov.ItemsSource = ListPriceWin.ListTovImage;
            ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
            OrderList.ItemsSource = ListOrderPreview;

            if (MainWindow.db.OrederPreviews.Count() != 0)
            {
                Text4.Content = MainWindow.SumZakazBasket.ToString();
                Masege.Visibility = Visibility.Hidden;
                Gorizont.Visibility = Visibility.Visible;
                Text3.Visibility = Visibility.Visible;
                Text4.Visibility = Visibility.Visible;
                Text5.Visibility = Visibility.Visible;
                OrderList.Visibility = Visibility.Visible;

            }
            else
            {
                OrderList.Visibility = Visibility.Hidden;
                Masege.Visibility = Visibility.Visible;
                Gorizont.Visibility = Visibility.Hidden;
                MainWindow.SumZakazBasket = 0;
                MainWindow.SumPaymentBasket = 0;
                MainWindow.SumaOrder = 0;
                Text4.Content = MainWindow.SumZakazBasket.ToString();
            }
            if (MainWindow.db.OrederPreviews.Count() > 4)
            {
                ScrolLeft.Visibility = Visibility.Visible;
                ScrolRight.Visibility = Visibility.Visible;
            }
            FocusManager.SetFocusedElement(this, ListGrupTovs);
        }

       
        private void ButtonBackPriceInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            TakeInvalid WinTake = new TakeInvalid();
            WinTake.Show();
            this.Close();
        }
        private void ButtonDeleteOrderInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Gorizont.Visibility = Visibility.Hidden;
            ControlWin.DeleteOrderPreview();
            ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
            OrderList.ItemsSource = ListOrderPreview;
            MainWindow.SumZakazBasket = 0;
            MainWindow.SumaOrder = 0;
            MainWindow.SumDiscountBasket = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.TextContent = "";
            MainWindow.Kol = 0;
            this.Close();
        }

      
        private void ButtonFinishPriceInvalid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Listtov.SelectedItem = null;
            if (MainWindow.db.OrederPreviews.Count() == 0) return;
            if (MainWindow.ChangeOrder == 0 && MainWindow.CmdStrokaServer.Length == 0)
            {
                DiscountWin WinPayment = new DiscountWin();
                WinPayment.Show();
            }
            else
            {

                if (MainWindow.ResultPayment == true)
                {
                    MainWindow.ChangeOrder = 0;
                    ListZakazWin WinPayment = new ListZakazWin();
                    WinPayment.Show();
                }
                else
                {
                    MainWindow.ResultPayment = true;
                    DiscountWin Discount = new DiscountWin();
                    Discount.Show();
                }
            }
            this.Close();

        }

     

       

   
        private void ButtonPriceInvalidOf_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.OnOffInvalid = 0;
            MainWindow.TimeClick++;
            ListPriceWin WinTake = new ListPriceWin();
            WinTake.Show();
            this.Close();
 
        }
        // Нажатие на окно с товаром выводит окно с его описанием и кооретировки количества 
        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;

            if (OrderList.SelectedItems.Count == 0)
            {
                OrderList.SelectedItem = null;
                return;
            }
            OrederPreview OpisTov = (OrederPreview)OrderList.SelectedItem;
            ListPriceWin.IdTov = OpisTov.Id;
            ListPriceWin.SprTovKodTov = OpisTov.Idtov;
            ListPriceWin.OrderSumTov = OpisTov.SumTov;
            ListPriceWin.OrderNameTov = OpisTov.NameTov;
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(RunSelectOrderInvalid);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);
            OrderList.SelectedItem = null;
            //OrderList.SelectedIndex = -1;
        }
        public static void RunSelectOrderInvalid(object ThreadObj)
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                PriceInvalid Price_lnk = ControlWin.LinkMainWindow("PriceInvalid");
                if (IdCartTovInv == 0)
                {
                    ListPriceWin.MapOrderList = 1;
                    ControlWin.LoadOpisTov(ListPriceWin.SprTovKodTov);
                    Price_lnk.SelectCartTov();
                    ListPriceWin.MapOrderList = 0;
                }
                else
                {
                    IdCartTovInv = 0;
                    MainWindow.Kol = 0;
                    MainWindow.Cuma = 0;
                    MainWindow.SumZakazBasket -= ListPriceWin.OrderSumTov;
                    MainWindow.SumPaymentBasket -= ListPriceWin.OrderSumTov;
                    MainWindow.SumaOrder -= ListPriceWin.OrderSumTov;
                    Price_lnk.Text4.Content = MainWindow.SumZakazBasket.ToString();

                    MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Idtov == ListPriceWin.SprTovKodTov));
                    MainWindow.db.SaveChanges();
                    ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
                    Price_lnk.OrderList.ItemsSource = ListOrderPreview;

                    if (MainWindow.db.OrederPreviews.Count() == 0)
                    {
                        Price_lnk.OrderList.Visibility = Visibility.Hidden;
                        Price_lnk.Masege.Visibility = Visibility.Visible;
                        Price_lnk.Gorizont.Visibility = Visibility.Hidden;
                        MainWindow.SumZakazBasket = 0;
                        MainWindow.SumPaymentBasket = 0;
                        MainWindow.SumaOrder = 0;
                        Price_lnk.Text4.Content = MainWindow.SumZakazBasket.ToString();
  
                    }

                    if (MainWindow.db.OrederPreviews.Count() > 4)
                    {
                        Price_lnk.ScrolLeft.Visibility = Visibility.Visible;
                        Price_lnk.ScrolRight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Price_lnk.ScrolLeft.Visibility = Visibility.Hidden;
                        Price_lnk.ScrolRight.Visibility = Visibility.Hidden;
                    }
  
                }

            }));

        }
        // Удаление строки товара из заказа 
        private void ButtonDelTovPriceInvalid_PreviewTouchDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow.TimeClick++;
            IdCartTovInv = 1;
      
        }
        private void ButtonInfoInvalid_TouchDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow.TimeClick++;
            MainWindow.InfoWindow = 1;
        }

        private void DelTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.TimeClick++;
            IdCartTovInv = 1;
        }

    }
}