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
using System.Windows.Media.Effects;
using System.Collections.ObjectModel;


namespace PreOrderWin
{
 
    
    /// <summary>
    /// Логика взаимодействия для MapTov.xaml
    /// </summary>
    public partial class MapTov : Window
    {

        //public static bool OnOffButtonDouwn = true;
        public static string Uri_ImageTov = "";
        public MapTov()
        {
            
          
            InitializeComponent();
             MainWindow.NameMapTov = "MapTov";           
            if (MainWindow.ScreenHeight > 1080) if(MainWindow.OnOffInvalid == 1) this.Top += 200;
 
 
 
            //--------------------------
            // Загрузка изображения выбранного товара

            //ListPriceWin LinkMapTov = ControlWin.LinkMainWindow("ListPriceWin");
            //var pict = (Image)LogicalTreeHelper.FindLogicalNode(LinkMapTov, "ImageTov");

            if (MainWindow.language_Key == 1)
            {
                TitleTov.Text = "MAP PRODUCT";
                //Col.Content = "Salt";
                //Perec.Content = "Pepper";
                //Mayonez.Content = "Mayonnaise";
                Kolichestvo.Text = "Add to basket:";

            }
            else
            {
                TitleTov.Text = "КАРТА ТОВАРУ";
                //Col.Content = "Сіль";
                //Perec.Content = "Перець";
                //Mayonez.Content = "Майонез";
                Kolichestvo.Text = "Додати      до кошика:";

            }
 

            if (ControlWin.ImageFoto.Length > 0)
            { 
                BitmapImage imgsource = new BitmapImage();
                imgsource.BeginInit();
                imgsource.CacheOption = BitmapCacheOption.OnLoad;
                imgsource.StreamSource = new System.IO.MemoryStream(ControlWin.ImageFoto, 0, ControlWin.ImageFoto.Length);
                imgsource.EndInit();
                ImageTov.Source = imgsource; // реальный Image
            }

            

            //ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.ListTov[17], UriKind.Relative)));
            //ImageTov.Background = (System.Windows.Media.Brush)imageBrush;

           
            TitleTov.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[9] : ControlWin.ListTov[8];
            PriceTov.Content = ControlWin.ListTov[10];
            Reclama.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[20] : ControlWin.ListTov[16];
            if (Reclama.Text.Length > 415) Reclama.FontSize = 16;
            Kolvo.Content = "1";
            //  Проверяем наличие товара в заказе
            if (ListPriceWin.MapOrderList == 1)
            { 
                var PrevTovChange = MainWindow.db.OrederPreviews.Where(p => p.Id == ListPriceWin.IdTov).FirstOrDefault();
                if (PrevTovChange != null) Kolvo.Content = Convert.ToString(PrevTovChange.Quantity);
           
            }

        }

        private void ButtonClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            MainWindow.NameSetRelated = "";
            MainWindow.NameMapTov = "";
            this.Close();
             
        }

   
        private void ButtonMinus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindow.TimeClick++;
            Kolvo.Content = Convert.ToInt16(Kolvo.Content) - 1 > 0 ? (Convert.ToInt16(Kolvo.Content) - 1).ToString() : "0";
            Uri Minusjpg = Convert.ToInt16(Kolvo.Content) == 0 ? new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative) : new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
            ImageMinus.Source = new BitmapImage(Minusjpg);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            MainWindow.Kol--;
        }
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
  
            MainWindow.TimeClick++;
   
            MainWindow.Kol = Convert.ToInt16(Kolvo.Content);
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));
            if (MainWindow.Kol > 0)
            {
                ListPriceWin.NameDog = "";
                ControlWin.Addbasket();
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
            MainWindow.NameMapTov = "";
            MainWindow.NameSetRelated = Kolvo.Content.ToString();
            this.Close();

        }

        
        private void ButtonPlus_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            PlusSelect();
        }
        private void ButtonPlus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            PlusSelect();
        }

  
        public void PlusSelect()
        { 
            MainWindow.TimeClick++;
            //Uri Minusjpg = new Uri("/PreOrderWin;component/Images/OffOk.jpg", UriKind.Relative);
            //ImageMinus.Source = new BitmapImage(Minusjpg);
            Uri Plusjpg = new Uri("/PreOrderWin;component/Images/screen60plus.jpg", UriKind.Relative);
            ImagePlus.Source = new BitmapImage(Plusjpg);

            Kolvo.Content = Convert.ToInt16(Kolvo.Content) + 1 >= 99 ? "99" : (Convert.ToInt16(Kolvo.Content) + 1).ToString();
            if (Convert.ToInt16(Kolvo.Content) > 0)
            {
                Uri Minusjpg = new Uri("/PreOrderWin;component/Images/screen60minus.jpg", UriKind.Relative);
                ImageMinus.Source = new BitmapImage(Minusjpg);

            }
            MainWindow.Kol++;
            MainWindow.Cuma = Convert.ToDecimal(Convert.ToInt16(Kolvo.Content) * Convert.ToDecimal(ControlWin.ListTov[10]));

        }

    
    
     
    }
}
