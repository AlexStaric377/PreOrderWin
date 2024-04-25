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
/// Многопоточность
using System.Threading;


namespace PreOrderWin
{
    /// <summary>
    /// Логика взаимодействия для Dykuy.xaml
    /// </summary>
    public partial class Dykuy : Window
    {
        public Dykuy()
        {
            InitializeComponent();

            if (MainWindow.language_Key == 0)
            {
                Text1.Content = "ДЯКУЄМО ЗА ЗАМОВЛЕННЯ!";
            }
            else
            {
                Text1.Content = "THANK YOU FOR THE ORDER!";
            }

        }

    }
}
