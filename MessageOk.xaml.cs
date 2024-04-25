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
    /// Логика взаимодействия для MessageOk.xaml
    /// </summary>
    public partial class MessageOk : Window
    {
        public static int AutoCloseTick = 0, SetTimeClose = 0;
        System.Windows.Threading.DispatcherTimer CloseAuto = new System.Windows.Threading.DispatcherTimer();
        public MessageOk(string TextWindows = null, int AutoClose = 0, int TimeClose = 0)
        {
            InitializeComponent();
            if (MainWindow.language_Key == 1)
            {
                TextConect.Text = "Confirm selection";
                BackTake.Content = "YES";
                Close_But.Content = "NO";
            }
            else
            {
                TextConect.Text = "Підтвердити вибір";
                BackTake.Content = "ТАК";
                Close_But.Content = "НІ";
            }    

            



            if (AutoClose == 1 || AutoClose == 2)
            {
                // Автозакрытие окна
                SetTimeClose = TimeClose;
                AutoCloseTick = AutoClose;
                CloseAuto.Tick += CloseAutoTick;
                CloseAuto.Interval = TimeSpan.FromSeconds(1);
                CloseAuto.Start();
            }

            if (TextWindows != null)
            {
                // Определить высоту окна по количеству \n в многострочном тексте
                var TextWindows_a = TextWindows.Split('\n');

                //this.MessageText.Text = TextWindows != null ? TextWindows : "Сообщение отсутствует!";
                //}
            }
        }

        private void ButtonTak_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            HotDog.OkImage++;
            this.Close();
        }

        private void ButtonNi_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            HotDog.OkImage = 0;
            this.Close();
        }

        private void ButtonNi_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            HotDog.OkImage = 0;
            this.Close();
        }

        private void ButtonTak_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            HotDog.OkImage++;
            this.Close();
        }

        private void CloseAutoTick(object sender, EventArgs e)
        {
            --SetTimeClose;
            if (SetTimeClose < 0)
            {
                CloseAuto.Stop();
                if (AutoCloseTick == 2)
                {
                    this.Close();
                }
                if (AutoCloseTick == 1)
                {
                    this.Close();
                    Environment.Exit(0);
                }

            }

        }

    }
}
