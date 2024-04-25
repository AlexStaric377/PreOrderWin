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

namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для InfoTov.xaml
    /// </summary>
    public partial class InfoTov : Window
    {
        public InfoTov()
        {
            InitializeComponent();
            if (MainWindow.ScreenHeight > 1080)
            { 
                Top = 400;
                if (MainWindow.NameSetWindow == "PriceInvalid") Top = 600;
            }



            // InfoTov ImageMapTov = ControlWin.LinkMainWindow("InfoTov");
            //ImageBrush imageBrush = new ImageBrush((ImageSource)new BitmapImage(new Uri(ControlWin.ListTov[17], UriKind.Relative)));
            //ImageMapTov.SmalImage.Background = (System.Windows.Media.Brush)imageBrush;


            BitmapImage imgsource = new BitmapImage();
            imgsource.BeginInit();
            imgsource.CacheOption = BitmapCacheOption.OnLoad;
            imgsource.StreamSource = new System.IO.MemoryStream(ControlWin.ImageFoto, 0, ControlWin.ImageFoto.Length);
            imgsource.EndInit();
            SmalImage.Source = imgsource; // реальный Image
            TitleTov.Text = MainWindow.language_Key == 1 ? ControlWin.ListTov[9] : ControlWin.ListTov[8];
            PriceTov.Content= ControlWin.ListTov[10];

            if (MainWindow.language_Key == 1)
            {
                Vec.Text = "Finished product weight(g)";
                Sostav.Text = "The composition of the product";
                Alergin.Content = "Allergens";
                Rec.Text = "Recommendations for use";
                Belok.Content = "Protein(g)";
                Gir.Content = "Fats(g)";
                Vuglevod.Content = "Carbohydrates(g)";
                KKAL.Content = "KCalories(KCal)";
            }
            else 
            {
                Vec.Text = "Вага готового виробу (г)";
                Sostav.Text = "Склад виробу";
                Alergin.Content = "Алергени";
                Rec.Text = "Рекомендації щодо вживання";
                Belok.Content = "Білок (г)";
                Gir.Content = "Жири (г)";
                Vuglevod.Content = "Вуглеводи (г)";
                KKAL.Content = "Єнергетична цінність(ккал)";
            }


            var opistov = MainWindow.db.InfoTovs.Where(p => p.Kodtov == Convert.ToInt32(ControlWin.ListTov[4])).FirstOrDefault();

            if (opistov != null)
            {
                if (MainWindow.language_Key == 1)
                {
                    ValueVec.Content = opistov.Weight; // "85г ";
                    ValueSostav.Text = opistov.CompositEn; // "Суміш кондитерська ванільна (молоко сухе знежирене, крохмаль кукурудзяний, розпушувач, емульгатори, загущувач, сіль кухонна харчова, ванілін), цукор, борошно в/г, олія соняшникова, яйця курячі, вода питна, журавлина сушена";
                    ValueAlergin.Text = opistov.AlergenEn; // "Алергени: насіння сезаму";
                    ValueRec.Text = opistov.AlarmEn; // " Не рекомендовано до вживання особам, що мають чутливість до складників виробу";
                    ValueBelok.Content = opistov.Belki ; // "26 г";
                    ValueGir.Content = opistov.Giri ; // "25 г ";
                    ValueVuglevod.Content = opistov.Vuglevody ; // "42 г ";
                    ValueKKAL.Content = opistov.Kkal ; // "503 ккал";                 

                }
                else
                { 
                    ValueVec.Content = opistov.Weight; // "85г ";
                    ValueSostav.Text = opistov.Composition; // "Суміш кондитерська ванільна (молоко сухе знежирене, крохмаль кукурудзяний, розпушувач, емульгатори, загущувач, сіль кухонна харчова, ванілін), цукор, борошно в/г, олія соняшникова, яйця курячі, вода питна, журавлина сушена";
                    ValueAlergin.Text = opistov.Alergen; // "Алергени: насіння сезаму";
                    ValueRec.Text = opistov.Alarm; // " Не рекомендовано до вживання особам, що мають чутливість до складників виробу";
                    ValueBelok.Content = opistov.Belki ; // "26 г";
                    ValueGir.Content = opistov.Giri  ; // "25 г ";
                    ValueVuglevod.Content = opistov.Vuglevody ; // "42 г ";
                    ValueKKAL.Content = opistov.Kkal  ; // "503 ккал";                 
                
                }

         
                   
            }
  
  
        }

        private void CloseInfoTov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            MainWindow.TimeClick++;
            MainWindow.InfoWindow = 0;
            this.Close();
        }

        private void Info_OnMouseDownOrTouchDown(object sender, TouchEventArgs e)
        {
            
            MainWindow.TimeClick++;
            MainWindow.InfoWindow = 0;
            this.Close();
        }
    }
}
