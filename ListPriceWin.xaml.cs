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
using ECRCommXLib;
/// Многопоточность
using System.Threading;

namespace PreOrderWin
{

    //public class GrupTov
    //{
    //    public int Id { get; set; }
    //    public string NameUK { get; set; }
    //    public string ImagePath { get; set; } // путь к изображению

    //}


    //public class Bludo
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string ImagePath { get; set; } // путь к изображению
    //    public string Inform { get; set; }
    //    public string Price { get; set; }
    //    public string Grn { get; set; }
    //}




    /// <summary>
    /// Логика взаимодействия для ListPriceWin.xaml
    /// </summary>
    public partial class ListPriceWin : Window
    {
        public static bool mouseClicked = false, TouchButtonTov = false, TouchButtonDelete =false, TouchButtonFinish = false, TouchButtonBack = true, TouchButtonInvalid = true, OrderDleteTov = true;
        public static string NameTovPrice = "", NameDog = "", StrokaKodTov= "", StrokaIdTov="", StrokaIndex="", OrderNameTov ="";
        public static int IdCartTov = 0, MapOrderList = 0, SelectHotdog = 0, BludoListSet = 0, LoadBludoList = 0, ValueKodTov = 0, SprTovKodTov=0, SelectAll =0, IdTov =0;
        public static decimal SprPriceTov = 0, OrderSumTov =0;
        public static List<SprTovImage> ListTovImage = new List<SprTovImage>(1);
        public static List<SprGrupImage> ListGrupImage = new List<SprGrupImage>(1);
        public static ObservableCollection<SprTovSelect> TovList = new ObservableCollection<SprTovSelect>(MainWindow.db.SprTovSelects.ToList<SprTovSelect>());
        public ListPriceWin()
        {
            InitializeComponent();
            MainWindow.NameSetWindow = "ListPriceWin";
            SelectAll = 0; StrokaKodTov = "";
            TouchButtonDelete = false; TouchButtonFinish = false; TouchButtonBack = true; TouchButtonInvalid = true;
            if (MainWindow.language_Key == 1)
            {

                this.Masege.Content = "You have nothing ordered";
                DeletOrder.Content = "CANCEL ORDERS ";
                Ok.Content = "FINISH";
                BackTake.Content = "BACK";
                Text1.Content = "MY ORDER -";
                if (MainWindow.RestHier == 1) Text2.Content = "HERE";
                else Text2.Content = "WITH YOURSELF";
                Text3.Content = "TOTAL:";
                TitleGrup.Content = "";
            }
            else
            {

                this.Masege.Content = "Ви ще нічого не замовили";
                DeletOrder.Content = "СКАСУВАТИ ЗАМОВЛЕННЯ";
                Ok.Content = "ГОТОВО";
                BackTake.Content = "НАЗАД";
                Text1.Content = "МОЄ ЗАМОВЛЕННЯ - ";

                if (MainWindow.RestHier == 1) Text2.Content = "У ЗАКЛАДІ";
                else Text2.Content = "З СОБОЮ";
                Text3.Content = "РАЗОМ:";
                TitleGrup.Content = "";
            }
            LoadGrupTovs();
            LoadListPrice();
            //ListGrupTovs.SelectedItem = 1;
            //FocusManager.SetFocusedElement(this, ListGrupTovs);

            

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
                Text3.Visibility = Visibility.Visible;
                Text4.Visibility = Visibility.Visible;
                Text5.Visibility = Visibility.Visible;
                OrderList.Visibility = Visibility.Visible;

            }
            ScrolLeft.Visibility = Visibility.Hidden;
            ScrolRight.Visibility = Visibility.Hidden;
            if (MainWindow.db.OrederPreviews.Count() > 4)
            {
                ScrolLeft.Visibility = Visibility.Visible;
                ScrolRight.Visibility = Visibility.Visible;
            }

            Text4.Content = MainWindow.SumZakazBasket.ToString();
            ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
            OrderList.ItemsSource = ListOrderPreview;
 
          
        }

   
        public BitmapImage ConvertByteArrayToImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new System.IO.MemoryStream(array, 0, array.Length))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            return null;

            //{
            //    byte[] imageBytes = (byte[])reader_image[0];
            //    MemoryStream ms = new MemoryStream();
            //    ms.Write(imageBytes, 0, imageBytes.Length);
            //    BitmapImage bmp = new BitmapImage();
            //    bmp.BeginInit();
            //    bmp.StreamSource = ms;
            //    bmp.EndInit();
            //    Photo.Source = bmp;
            //}

        }
        //public void c()
        //{

        //    Image image = new Image();
        //    BitmapImage imgsource = new BitmapImage();
        //    imgsource.BeginInit();
        //    imgsource.CacheOption = BitmapCacheOption.OnLoad;
        //    imgsource.StreamSource = new System.IO.MemoryStream(FotoByte, 0, FotoByte.Length);
        //    imgsource.EndInit();
        //    image.Source = imgsource; // реальный Image
        //                              //images.Add(image);


        //}


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
            //List<SprGrupImage> ListGrupImage = new List<SprGrupImage>(1);
            ListGrupImage.Clear();
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
                        ListGrupImage.Add(new SprGrupImage(pole.Id, NameGrup, FotoByte));
                    }
                }
                ListGrupTovs.ItemsSource = ListGrupImage;

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
            ListTovImage.Clear();
            BludoList.ItemsSource = null;
            ListVid.ItemsSource = null;
            DeleteAllSprTovSlect();
            bool KodVidTov = false, KodPvidTov = false;
            string NameTov = "";
            byte[] FotoByte =  new byte[0];
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
                            AddSprTovSelect(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov);
                            //ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                        }

                    }
 
                    if (Tovar.Kodvid != 0)
                    {
                        var VidTov = MainWindow.db.Sprvids.ToList().Where(p => p.KodGrup == MainWindow.IdGrupTov ).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid); //&& p.Kodvid == Tovar.Kodvid
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
                            AddSprTovSelect(zapvid.Id, zapvid.Kodvid, 0, NameTov, FotoByte);
                            KodVidTov = true;
                            //ListTovImage.Add(new SprTovImage(zapvid.Id, zapvid.Kodvid, 0, NameTov, FotoByte));
                        }
                        
                    }
                }
                else ListTovImage.Clear();
            }
            if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0)
            {
                var Tovar = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov &&  p.Kodvid == MainWindow.KodVidTov).OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov).FirstOrDefault();
                if (Tovar != null)
                {
                    if (Tovar.Kodpvid == 0)
                    {
                        var ListSprtov = MainWindow.db.Sprtovs.Where(p => p.KodGrup == MainWindow.IdGrupTov && p.Kodvid == MainWindow.KodVidTov && p.SaleStop == false && p.PartCalc == false).ToList().OrderBy(p => p.KodGrup).OrderBy(p => p.Kodvid).OrderBy(p => p.Kodpvid).OrderBy(p => p.KodTov);
                        foreach (Sprtov pole in ListSprtov)
                        {
                            if (MainWindow.language_Key == 1) NameTov = pole.NameTovEN;
                            else NameTov = pole.NameTovUK;
                            //ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                            AddSprTovSelect(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov);
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
                            //ListTovImage.Add(new SprTovImage(zapvid.Id, zapvid.Kodpvid, 0, NameTov, FotoByte));
                            AddSprTovSelect(zapvid.Id, zapvid.Kodvid, 0, NameTov, FotoByte);
                            KodPvidTov = true;
                        }
                        
                    }
                }
                else
                {
                    ListTovImage.Clear();

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
                        //ListTovImage.Add(new SprTovImage(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov));
                        AddSprTovSelect(pole.Id, pole.KodTov, pole.PriceTov, NameTov, pole.IDFotoTov);
                    }
                }
                else
                {
                    ListTovImage.Clear();
                    MainWindow.KodPvidTov = 0;
                    MainWindow.KodVidTov = 0;
                }
            }
 
 
            //BludoList.ItemsSource = ListTovImage;
 

            TovList = new ObservableCollection<SprTovSelect>(MainWindow.db.SprTovSelects.ToList<SprTovSelect>());
            if (KodPvidTov == true || KodVidTov == true)
            { 
                ListVid.ItemsSource = TovList;
                BludoList.Visibility = Visibility.Hidden;
                ListVid.Visibility = Visibility.Visible;
            }

            else
            { 
                BludoList.ItemsSource = TovList;
                ListVid.Visibility = Visibility.Hidden;
                BludoList.Visibility = Visibility.Visible;


            }
  
            BludoList.Focusable = false;

        }

        private void AddSprTovSelect( int Id, int KodTov, decimal PriceTov, string NameTov, byte[] IDFotoTov)
        {
            try 
            { 
                SprTovSelect StrTovAdd = new SprTovSelect();
                StrTovAdd.Id = Id;
                StrTovAdd.KodTov = KodTov;
                StrTovAdd.PriceTov = PriceTov;
                StrTovAdd.Name = NameTov;
                StrTovAdd.ImagePath = IDFotoTov;

                MainWindow.db.SprTovSelects.Add(StrTovAdd);
                MainWindow.db.SaveChanges();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainWindow.ExMessage = ex.Message;
                PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);

            }
        }

        public static void DeleteAllSprTovSlect()
        {
            PreOrderConection.ErorDebag("RemoveRange", 3);
            //MessageBox.Show("RemoveRange");
            // Удаление тела заказа
            try
            {
                MainWindow.db.SprTovSelects.RemoveRange(MainWindow.db.SprTovSelects.Where(c => c.Id != 0));
                //  Сохранить изменения
                MainWindow.db.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainWindow.ExMessage = ex.Message;
                PreOrderConection.ErorDebag("Ошибка: " + MainWindow.ExMessage, 2);

            }

        }


        private void ButtonInfoPrice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.TimeClick++;
            MainWindow.InfoWindow = 1;
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

     

        private void ButtonInfoPrice_TouchDown(object sender, TouchEventArgs e)
        {
            MainWindow.TimeClick++;
            MainWindow.InfoWindow = 1;
            
        }


        /// <summary>
        /// Выбор товара из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BludoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            //MessageBox.Show(IdCartTov.ToString() + "  " + BludoList.SelectedItems.Count.ToString());
            MainWindow.TimeClick++;
           

            if (((ListView)e.OriginalSource).Name.Equals("BludoList"))
            {
                
                if (BludoList.SelectedItems.Count == 0 || IdCartTov == 1 ) // || PriceInvalid.TovButton == true
                {
                    BludoList.SelectedItem = null;
                    return;
                }
                
                SprTovSelect OpisTov = (SprTovSelect)BludoList.SelectedItem;
                //int index = BludoList.SelectedIndex;
                //int ListCount = BludoList.Items.Count;
                

                if (BludoList.SelectedIndex == 5)
                {

                    if (TouchButtonTov == false)
                    {
                        TouchButtonTov = true;
                        BludoList.SelectedItem = null;
                        return;

                    }

                    if (BludoList.SelectedIndex == 5 && TouchButtonDelete == true)
                    {
                        TouchButtonDelete = false;
                        TouchButtonTov = true;
                        BludoList.SelectedItem = null;
                        return;

                    }
                    if (MainWindow.db.OrederPreviews.Where(p => p.Idtov == OpisTov.KodTov).FirstOrDefault() == null && TouchButtonTov == true) TouchButtonTov = false;

                    else
                    {
                        BludoList.SelectedItem = null;
                        return;

                    }
                }



                //if (BludoList.SelectedIndex == 5 && TouchButtonTov == false) TouchButtonTov = true;


                //SprTovImage OpisTov = (SprTovImage)BludoList.SelectedItem;
                SprTovKodTov = OpisTov.KodTov;
                SprPriceTov = OpisTov.PriceTov;

               

  
                //LoadListPrice();
                IdTov = 0;
                MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
                Thread thread = new Thread(RunSelectTov);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true; // Фоновый поток
                thread.Start(Arguments01);
                //LoadBludoList++;



            }

            if (((ListView)e.OriginalSource).Name.Equals("ListVid"))
            {

                if (ListVid.SelectedItems.Count == 0 || IdCartTov == 1) //|| TouchButtonTov == true || PriceInvalid.TovButton == true
                {
                    ListVid.SelectedItem = null;
                    return;
                }
                SprTovSelect OpisTov = (SprTovSelect)ListVid.SelectedItem;


                if (BludoList.SelectedIndex == 5)
                {

                    if (TouchButtonTov == false)
                    {
                        TouchButtonTov = true;
                        BludoList.SelectedItem = null;
                        return;

                    }

                    if (BludoList.SelectedIndex == 5 && TouchButtonDelete == true)
                    {
                        TouchButtonDelete = false;
                        TouchButtonTov = true;
                        BludoList.SelectedItem = null;
                        return;

                    }
                    if (MainWindow.db.OrederPreviews.Where(p => p.Idtov == OpisTov.KodTov).FirstOrDefault() == null && TouchButtonTov == true) TouchButtonTov = false;

                    else
                    {
                        BludoList.SelectedItem = null;
                        return;

                    }
                }
                ListPriceWin.SprTovKodTov = OpisTov.KodTov;
                ListPriceWin.SprPriceTov = OpisTov.PriceTov;
                ListVid.SelectedItem = null;
                ListVid.Focusable = false;

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
                ListPriceWin Price_lnk = ControlWin.LinkMainWindow("ListPriceWin");
     
                if (SprPriceTov != 0)
                {
                    MainWindow.KodVidTov = 0;
                    MainWindow.KodPvidTov = 0;
                    ControlWin.LoadOpisTov(SprTovKodTov);
                    if (MainWindow.InfoWindow != 0)
                    {
                        Window WinInfo = new InfoTov();
                        Price_lnk.Opacity = 0.3;
                        WinInfo.ShowDialog();
                        Price_lnk.Opacity = 1;
                        MainWindow.InfoWindow = 0;
                    }
                    else Price_lnk.SelectCartTov();

                }
                else
                {
                    if (MainWindow.KodVidTov != 0 && MainWindow.KodPvidTov == 0) MainWindow.KodPvidTov = SprTovKodTov;
                    if (MainWindow.KodVidTov == 0) MainWindow.KodVidTov = SprTovKodTov;

                    Price_lnk.LoadListPrice();
                }
            }));
            
        }

        public void SelectCartTov()
        {
            ListPriceWin.SelectHotdog = 0;
            this.Opacity = 0.3;
            BludoList.ItemsSource = null;

            if (ControlWin.OnOffConectionGrup == 0 && ControlWin.CompositionTovIdtovBaz == 0)
            {

                Window WinOblako = new MapTov();
                //WinOblako.Owner = this;
                WinOblako.ShowDialog();
                RefreshResultSelectTov();
                return;
            }
 
            if (ControlWin.CompositionTovIdtovBaz != 0)   //ControlWin.OnOffConectionGrup != 0 &&
            {

                Window WinOblako = new HotDog();
                //WinOblako.Owner = this;
                WinOblako.ShowDialog();
                RefreshResultSelectTov();
               
                return;
            }
            if (ControlWin.OnOffConectionGrup != 0 && ControlWin.CompositionTovIdtovBaz == 0)
            {

                Window WinOther = new RelatedTov();
                //WinOther.Owner = this;
                WinOther.ShowDialog();
                RefreshResultSelectTov();

                return;
                
            }

        }

        public void RefreshResultSelectTov()
        {
            this.Opacity = 1;
            BludoList.ItemsSource = TovList;
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

            //FocusManager.SetFocusedElement(this, ListGrupTovs);
            //FocusManager.SetFocusedElement(this, BludoList);


        }


        private void ButtonBackPrice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            TakeWin WinTake = new TakeWin();
            WinTake.Show();
            this.Close();

        }

     
 


        private void ButtonDeletePrice_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            DeletePrice();
        }
        private void ButtonDeletePrice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            e.Handled = true;
            DeletePrice();
        }

        public void DeletePrice()
        {
 
            MainWindow.TimeClick++;
            MainWindow.ClickButtonOnOff = true;
            MainWindow.ResultPayment = false;
            Gorizont.Visibility = Visibility.Hidden;
            ControlWin.DeleteOrderPreview();
            ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
            OrderList.ItemsSource = ListOrderPreview;
            MainWindow.CountOrder = 0;
            MainWindow.SumZakazBasket = 0;
            MainWindow.SumaOrder = 0;
            MainWindow.SumDiscountBasket = 0;
            MainWindow.NumberHdZakaz = 0;
            MainWindow.TextContent = "";
            MainWindow.Kol = 0;
            MainWindow.TimeOut = 1;
            this.Close();
        }

        private void ButtonFinishPrice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            FinishSelectOrder();
        }

        private void ButtonFinishPrice_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            FinishSelectOrder();
        }

        public void FinishSelectOrder()
        {
            MainWindow.TimeClick++;
            DiscountWin.DiscountBarCod = MainWindow.CodTexnologLoyalnost;
            //BludoList.SelectedItem = null;
            //BludoList.SelectedIndex = -1;
            if (MainWindow.db.OrederPreviews.Count() == 0) return;
            if (MainWindow.ChangeOrder == 0 && MainWindow.CmdStrokaServer.Length == 0)
            {
                // Тестовый запрос
                //CuponWin.LoadND2017();
                //ListZakazWin WinPayment = new ListZakazWin();
                //CuponWin WinPayment = new CuponWin();
                //DiscountWin.DiscountBarCod = "9010016990203";
                //PaymentOrderWin.PrintOrder();
                //ControlWin.AddOreder();
                //ControlWin.RunBankTerminal();
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


        private void ButtonInvalidPriceOn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.OnOffInvalid = 1;
            MainWindow.TimeClick++;
            if (MainWindow.db.Sprtovs.Count() == 0)
            {
                PreOrderConection.ErorDebag("Справочник товаров не загружен " + Environment.NewLine + "Загрузите данные из БД 1С ", 2, 0, (PreOrderConection.StruErrorDebag)null);
                string TextWindows = "Справочник товаров не загружен " + Environment.NewLine + "Загрузите данные из БД 1С ";
                MessageError NewOrder = new MessageError(TextWindows, 2);
                NewOrder.Show();
                Thread.Sleep(8000);
                this.Close();
            }
            else
            {
                MainWindow.TimeClick++;
                PriceInvalid WinPayment = new PriceInvalid();
                WinPayment.Show();
                this.Close();
            }

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
            IdTov = OpisTov.Id;
            SprTovKodTov = OpisTov.Idtov;
            OrderNameTov = OpisTov.NameTov;
            OrderSumTov = OpisTov.SumTov;
            OrderList.SelectedItem = null;
            OrderList.Focusable = false;
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(RunSelectOrder);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);

        }
        public static void RunSelectOrder(object ThreadObj)
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ListPriceWin Price_lnk = ControlWin.LinkMainWindow("ListPriceWin");


                if (IdCartTov == 0)
                {
                    MapOrderList = 1;
                    ControlWin.LoadOpisTov(SprTovKodTov);
                    Price_lnk.SelectCartTov();
                    MapOrderList = 0;
 
                }
                else
                {
 
                    IdCartTov = 0;
                    MainWindow.Kol = 0;
                    MainWindow.Cuma = 0;
                    MainWindow.SumZakazBasket -= OrderSumTov;
                    MainWindow.SumPaymentBasket -= OrderSumTov;
                    MainWindow.SumaOrder -= OrderSumTov;
                    Price_lnk.Text4.Content = MainWindow.SumZakazBasket.ToString();

                    MainWindow.db.OrederPreviews.RemoveRange(MainWindow.db.OrederPreviews.Where(c => c.Id == IdTov));
                    MainWindow.db.SaveChanges();
                    ObservableCollection<OrederPreview> ListOrderPreview = new ObservableCollection<OrederPreview>(MainWindow.db.OrederPreviews.ToList<OrederPreview>());
                    Price_lnk.OrderList.ItemsSource = ListOrderPreview;
                    TouchButtonDelete = true;
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
        private void DelTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.TimeClick++;
            IdCartTov = 1;
           

        }
        private void ButtonDelTovPrice_PreviewTouchDown(object sender, TouchEventArgs e)
        {
           
            MainWindow.TimeClick++;
            IdCartTov = 1;
          
        }

    }  
}
