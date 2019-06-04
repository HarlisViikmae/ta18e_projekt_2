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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;

namespace Snake_game_t118e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int dir = 0;
        bool car = false;
        double x;
        double y;
        Rectangle[] recArray = new Rectangle[100];
        List<Rectangle> recList = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_cartgl(object sender, RoutedEventArgs e)
        {
            car = !car;
        }

        private void btn_startsreset(object sender, RoutedEventArgs e)
        {
            tick(dir);
            List<Rectangle> recList = new List<Rectangle>();
            for (var ix = 0; ix < 100; ix++)
                recList.Add(new Rectangle { Width = 10, Height = 10, Fill = Brushes.Black });
        }

        private void tick(int dire)
        {
            y = Canvas.GetBottom(Snek);
            x = Canvas.GetLeft(Snek);
            if (dire%4 == 0) x += 25;
            else if (dire%4 == 1) y += 25;
            else if (dire%4 == 2) x -= 25;
            else if (dire%4 == 3) y -= 25;
            //if (x < 0 || x > 450||y < 0|| y > 450) end();
            Canvas.SetLeft(Snek, x);

            Canvas.SetBottom(Snek, y);
        }

        private void btn_right(object sender, RoutedEventArgs e)
        {
            if (!car)
                dir = 0;
            else
                dir--;
        }

        private void btn_up(object sender, RoutedEventArgs e)
        {
            if (!car)
                dir = 1;
        }

        private void btn_left(object sender, RoutedEventArgs e)
        {
            if (!car)
                dir = 2;
            else
                dir++;
        }

        private void btn_down(object sender, RoutedEventArgs e)
        {
            if (!car)
                dir = 3;
        }

        private void end()
        {

        }
    }
}
