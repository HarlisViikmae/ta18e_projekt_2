﻿using System;
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
using System.Threading;

namespace Snake_game_t118e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool car = false;
        int length = 0, gridsize = 18,dir = 0;
        double x, y, fx = 0, fy = 0;


        List<Rectangle> recList = new List<Rectangle>();

        public void Timer_Tick(object sender, EventArgs e)//tick event *main
        {
            mov(dir);
            if (checkfood())
            {
                growth();
            }
            length = recList.Count;
            score_box.Text = $"Score: {recList.Count}";
        }

        private DispatcherTimer timer;
        public MainWindow()//Setup
        {
            recList.Add(new Rectangle { Width = 50, Height = 50, Fill = Brushes.Red, StrokeThickness = 3, Stroke = Brushes.Black });// kuidas reclist[i] asukohta muuta?
            Panel.SetZIndex(recList[0], 5);
            Canvas.SetBottom(recList[0], 250);
            Canvas.SetLeft(recList[0], 250);
            SolidColorBrush gridder = new SolidColorBrush();
            gridder.Color = Color.FromArgb(255, 10, 200, 10);
            InitializeComponent();
            for (int j = 0; j < gridsize; j++)//creating backround grid
            {
                for (int i = 0; i < 9; i++)
                {
                    Rectangle e = new Rectangle();
                    e.Height = 25;
                    e.Width = 25;
                    e.Fill = gridder;

                    maal.Children.Add(e);
                    y += 10;
                    if (j % 2 == 1)
                        Canvas.SetLeft(e, i * 50);
                    else
                        Canvas.SetLeft(e, i * 50 + 25);
                    Canvas.SetTop(e, j * 25);
                }
                //Creating timer
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(.2);
                timer.Tick += Timer_Tick;
            }

        }

        private void btn_cartgl(object sender, RoutedEventArgs e)
        {
            car = !car;
            if (car) dir = dir % 4 + 10000;
        }
        bool rdy = true;

        private void btn_startsreset(object sender, RoutedEventArgs e)
        {
            if (rdy)
            {
                timer.Start();
                yeet.Content = "Pause"; // sshoud = start rn
                new_food();
                dir = 0;
                rdy = false;
            }
            else
            {
                timer.Stop();
                yeet.Content = "Resume";
                rdy = true;
            }
            /*    List<Rectangle> recList = new List<Rectangle>();
                for (var ix = 0; ix < 100; ix++)
                    recList.Add(new Rectangle { Width = 10, Height = 10, Fill = Brushes.Black });
            */
        }




        private void growth()
        {
            recList.Add(new Rectangle { Width = 25, Height = 25, Fill = Brushes.Black, StrokeThickness = 3, Stroke = Brushes.Red,  Visibility = Visibility.Visible});// kuidas reclist[i] asukohta muuta?
            maal.Children.Add(recList[recList.Count - 1]);
        }




        private bool tailf()
        {
            return false;
        }

        
        //utility functions
        private void end()
        {
            timer.Stop();
            timer.Stop();
            Canvas.SetLeft(Snek, 50);
            Canvas.SetBottom(Snek, 350);
            //new_food();
            length = 0;
            
            rdy = true;
            yeet.Content = "Start";

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (!rdy)
            {
                return;
            }*/

            switch (e.Key)
            {
                case Key.Up:
                    dir = 1;
                    break;
                case Key.Down:
                    dir = 3;
                    break;
                case Key.Left:
                    if (!car)
                        dir = 2;
                    else
                        dir++;
                    break;
                case Key.Right:
                    if (!car)
                        dir = 0;
                    else
                        dir--;
                    break;
                default:
                    return;
            }
            Keyboard.ClearFocus();

        }

        //Onsreen direction keys:
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


        //Tootavad functsioonid.

        private bool checkfood()//did i eat something?
        {
            if (Canvas.GetBottom(Snek) == Canvas.GetBottom(Food) && Canvas.GetLeft(Snek) == Canvas.GetLeft(Food))
                return true;
            else
                return false;
        }



        private void new_food()
        {
            Random rnd = new Random();
            fx = rnd.Next(gridsize-1) * 450 / gridsize;
            fy = rnd.Next(gridsize-1) * 450 / gridsize;
            Food.Visibility = Visibility.Visible;
            Canvas.SetLeft(Food, fx);
            Canvas.SetBottom(Food, fy);
        }

        //Void tick
        private void mov(int dire)
        {
            y = Canvas.GetBottom(Snek);
            x = Canvas.GetLeft(Snek);
            switch (dir % 4)
            {
                case 0:
                    x += 25;
                    break;
                case 1:
                    y += 25;
                    break;
                case 2:
                    x -= 25;
                    break;
                case 3:
                    y -= 25;
                    break;
            }
            if (y == 450 || y == -25 || x == 450 || x == -25 || tailf()) end();
            if (recList.Count > 1)
                for(int i = recList.Count-1; i > 0; i--)
                {
                    //Canvas.SetLeft(recList[i], Canvas.GetLeft(recList[i - 1]));
                    //Canvas.SetBottom(recList[i], Canvas.GetBottom(recList[i-1]));
                    Canvas.SetLeft(recList[0], 250);
                    Canvas.SetBottom(recList[0], 250);

                }
            if(recList.Count > 0){
                Canvas.SetLeft(recList[0], Canvas.GetLeft(Snek));
                Canvas.SetBottom(recList[0], Canvas.GetBottom(Snek));
            }
            if (y == 450 || y == -25 || x == 450 || x == -25 || tailf()) ;
            else
            {
                Canvas.SetLeft(Snek, x);
                Canvas.SetBottom(Snek, y);
            }
        }

    }
}