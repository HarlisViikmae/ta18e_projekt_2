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

        public void Timer_Tick(object sender, EventArgs e)
        {
            tick(dir);
            
        }

        private DispatcherTimer timer;
        public MainWindow()//Setup
        {
            SolidColorBrush gridder = new SolidColorBrush();
            gridder.Color = Color.FromArgb(255, 10, 200, 10);
            InitializeComponent();
            for (int j = 0; j < 18; j++)//creating backround grid
            {
                for (int i = 0; i < 9; i++)
                {
                    Rectangle e = new Rectangle();
                    e.Height = 25;
                    e.Width = 25;
                    e.Fill = gridder;

                    maal.Children.Add(e);
                    y += 10;
                    if(j%2==1)
                    Canvas.SetLeft(e, i * 50);
                    else
                        Canvas.SetLeft(e, i * 50+25);
                    Canvas.SetTop(e, j * 25);
                }
                //Creating timer
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(.5);
                timer.Tick += Timer_Tick;
                timer.Tick += Timer_Tick1;
            }
        }

        private void Timer_Tick1(object sender, EventArgs e)
        {
            
        }

        private void btn_cartgl(object sender, RoutedEventArgs e)
        {
            car = !car;
            if (car) dir = dir % 4 + 10000;
        }

        private void btn_startsreset(object sender, RoutedEventArgs e)
        {
            timer.Start();
            /*    List<Rectangle> recList = new List<Rectangle>();
                for (var ix = 0; ix < 100; ix++)
                    recList.Add(new Rectangle { Width = 10, Height = 10, Fill = Brushes.Black });
            */
        }
        //Void tick
        private void tick(int dire)
        {
            y = Canvas.GetBottom(Snek);
            x = Canvas.GetLeft(Snek);
            if (dire % 4 == 0) x += 25;
            else if (dire % 4 == 1) y += 25;
            else if (dire % 4 == 2) x -= 25;
            else if (dire % 4 == 3) y -= 25;
            if (y > 450 || y < 0 || x > 450 || x < 0) end();
                Canvas.SetLeft(Snek, x);
                Canvas.SetBottom(Snek, y);
        }
        //Direction keys:
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
        //utility functions
        private void end()
        {
            yeet.Content = "Reset";
            while (true) ;
        }
        static void new_food()
        {

        }
    }
}
