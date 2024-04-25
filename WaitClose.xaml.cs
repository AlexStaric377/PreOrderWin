using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using PreOrderWin;

namespace PreOrderWin
{

    public  class AsyncDataSource
    {
        private string time29;
        private string time28;
        private string time27;

        public AsyncDataSource()
        {
        }

        public string Second29
        {

            get
            {
                Thread.Sleep(1000);
                return time29;
            }
            set { time29 = value; }
        }

        public string Second28
        {
            get
            {
                Thread.Sleep(2000);
                return time28;
            }
            set { time28 = value; }
        }

        public string Second27
        {
            get
            {

                Thread.Sleep(2000);
                return time27;
            }
            set { time27 = value; }
        }
    }
    /// <summary>
    /// Логика взаимодействия для WaitClose.xaml
    /// </summary>
    public partial class WaitClose : Window
    {
        public static int TimeSet = 29;
        public static bool ClickTak = true, ClickNi = true;

        DispatcherTimer timer = new DispatcherTimer();

                 
        
        public WaitClose()
        {
            InitializeComponent();

            if (MainWindow.language_Key == 0)
            {
                LabSecond.Content = "СЕКУНД";
                Continue.Content = "БАЖАЄТЕ ПРОДОВЖИТИ?";
                Backm.Content = "ТАК";
                Backni.Content = "НІ";

            }
            else 
            {
                LabSecond.Content = "SECONDS";
                Continue.Content = "WANT TO CONTINUE?";
                Backm.Content = "YES";
                Backni.Content = "NO";
            }
            MainWindow.TimeClick = 0; MainWindow.SetClick = 0;
            TimeSet = 29;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {

            //string timeNow= DateTime.Now.ToString("HH:mm:ss").Substring(6,2);
            Second.Content = TimeSet.ToString();
            --TimeSet;
            if (TimeSet < 0)
            {
                WaitStop();
            }
        }

 
        private void ButtonNi_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            WaitStop();
        } 

        private void ButtonNi_TouchDown(object sender, TouchEventArgs e)
        {
 
            e.Handled = true;
            ClickNi = false;
            WaitStop();
        }

        private void ButtonTak_TouchDown(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            timer.Stop();
            MainWindow.TimeClick = 0; MainWindow.SetClick = 0;
            MainWindow.TimeOut = 0;
            MainWindow.TimeClick++;
            ControlWin.RunTimeOut();
            this.Close();
        }

        private void ButtonTak_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            timer.Stop();
            MainWindow.TimeClick = 0; MainWindow.SetClick = 0;
            MainWindow.TimeOut = 0;
            MainWindow.TimeClick++;
            ControlWin.RunTimeOut();
            this.Close();
        }

        public void WaitStop()
        {
            int indwin = 0;
            timer.Stop();
            // Удаление заказ из очереди отложеных чеков кассы
            if (MainWindow.ApiOrdersAdd == true && MainWindow.RunServer == true) ControlWin.ReversOrderTurnCheck(); 
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                if (MainWindow.NameError != "")
            {
                Window WinError = ControlWin.LinkMainWindow(MainWindow.NameError);
                if (WinError != null)
                {
                    indwin++;
                    PaymentBankWin.ErrorKodVoid = 3;
                    ControlWin.DeleteOrderPreview();
                    MainWindow.NameError = "";
                    WinError.Close();
                } 

            }
            if (MainWindow.NameMapTov != "")
            {
                Window WinMapTov = ControlWin.LinkMainWindow(MainWindow.NameMapTov);
                if (WinMapTov != null)
                {
                    indwin++;
                    MainWindow.NameMapTov = "";
                    WinMapTov.Close();
                } 
            }
            if (MainWindow.NameSetRelated != "")
            {
                Window WinRelated = ControlWin.LinkMainWindow(MainWindow.NameSetRelated);
                if (WinRelated != null)
                {
                    indwin++;
                    MainWindow.NameSetRelated = "";
                    WinRelated.Close();
                } 
            }

            if (MainWindow.NameSetWindow != "")
            {
                indwin++;
                Window WinSet = ControlWin.LinkMainWindow(MainWindow.NameSetWindow);
                if (WinSet != null) WinSet.Close();
            }

            if (indwin > 0)
            { 
                Window WinMain = ControlWin.LinkMainWindow("MainWindow");
                if (WinMain != null) WinMain.Show();          
            }
            }));
            MainWindow.TimeOut = 0;
            MainWindow.TimeOutRun = 0;
            this.Close();
        }
    }
}
