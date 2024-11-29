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
using System.Drawing;
using Brush = System.Drawing.Brush;
using Pen = System.Drawing.Pen;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using static System.Drawing.Graphics;
using System.Windows.Shapes;

namespace GUI_GEO2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Line myLine;
        private Line myLine2;
        private Line myLine3;
        private Line myLine4;
        private Line myLine5;
        private Line myLine6;
        private Line myLine7;

        public MainWindow()
        {
            InitializeComponent();
        }

        int st_tock = 0;
        bool pointsTrue = false;
        public class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        List<Point> points = new List<Point>();

        public static int orientation(Point p, Point q, Point r)
        {
            int val = (q.y - p.y) * (r.x - q.x) -
                    (q.x - p.x) * (r.y - q.y);

            if (val == 0) return 0; // collinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
            
        public void Jarvis(Point[] tocka, int stevilo_tock)
        {
            List<Point> shell = new List<Point>();

            int skrajno_levo = 0;
            for (int i = 1; i < stevilo_tock; i++)
            {
                if (tocka[i].x < tocka[skrajno_levo].x)
                {
                    skrajno_levo = i;
                }
            }
            int position = skrajno_levo, next;

            do
            {
                shell.Add(tocka[position]);
                next = (position + 1) % stevilo_tock;
                for (int i = 0; i < stevilo_tock; i++)
                {
                    if (orientation(tocka[position], tocka[i], tocka[next]) == 2)
                    {

                        next = i;
                    }


                }


                position = next;
            } while (position != skrajno_levo);

            for (int i = 0; i < shell.Count; i++)
            {
                if (i != shell.Count - 1)
                {
                    myLine3 = new Line();
                    myLine3.Stroke = System.Windows.Media.Brushes.Black;
                    myLine3.StrokeThickness = 2;
                    myLine3.X1 = shell[i].x;
                    myLine3.Y1 = shell[i].y;
                    myLine3.X2 = shell[i + 1].x;
                    myLine3.Y2 = shell[i + 1].y;

                    myGrid.Children.Add(myLine3);
                }
                else
                {
                    myLine4 = new Line();
                    myLine4.Stroke = System.Windows.Media.Brushes.Black;
                    myLine4.StrokeThickness = 2;
                    myLine4.X1 = shell[i].x;
                    myLine4.Y1 = shell[i].y;
                    myLine4.X2 = shell[0].x;
                    myLine4.Y2 = shell[0].y;

                    myGrid.Children.Add(myLine4);
                }
            }
        }

        private void RB1_Checked(object sender, RoutedEventArgs e)
        {
            if (pointsTrue)
            {
                Point[] tocka = new Point[st_tock];
                for (int i = 0; i < st_tock; i++)
                {
                    tocka[i] = new Point(points[i].x, points[i].y);
                }
                Jarvis(tocka, st_tock);
                points.Clear();
            }
            else
            {
                string msg = "Znova generirajte naključne točke";
                MessageBox.Show(msg);
            }
        }
        private void RB2_Checked(object sender, RoutedEventArgs e)
        {
            if (pointsTrue)
            {
                Point[] tocka = new Point[st_tock];
                for (int i = 0; i < st_tock; i++)
                {
                    tocka[i] = new Point(points[i].x, points[i].y);
                }
                FindHull(tocka, st_tock);
                points.Clear();
            }
            else
            {
                string msg = "Znova generirajte naključne točke";
                MessageBox.Show(msg);
            }
        }
        List<Point> shell = new List<Point>();
        private void FindHull(Point[] tocka, int st_tock)
        {

            int skrajno_levo = 0, skrajno_desno = 0;

            for (int i = 0; i < st_tock; i++)
            {
                if (tocka[i].x < tocka[skrajno_levo].x)
                {
                    skrajno_levo = i;
                }
                if (tocka[i].x > tocka[skrajno_desno].x)
                {
                    skrajno_desno = i;
                }
            }

            myLine5 = new Line();
            myLine5.Stroke = System.Windows.Media.Brushes.Black;
            myLine5.StrokeThickness = 2;
            myLine5.X1 = points[skrajno_levo].x;
            myLine5.Y1 = points[skrajno_levo].y;
            myLine5.X2 = points[skrajno_desno].x;
            myLine5.Y2 = points[skrajno_desno].y;


           // myGrid.Children.Add(myLine5);


            QuickHull(tocka[skrajno_levo], tocka[skrajno_desno], 1);
            QuickHull(tocka[skrajno_levo], tocka[skrajno_desno], -1);

        }

        int Stran(Point point1, Point point2, Point point3)
        {
            int val = (point3.y - point1.y) * (point2.x - point1.x) - (point2.y - point1.y) * (point3.x - point1.x);

            if (val > 0) { return 1; }
            if (val < 0) { return -1; }
            return 0;
        }

        int distance(Point point1, Point point2, Point point3)
        {
            return Math.Abs((point3.y - point1.y) * (point2.x - point1.x) - (point2.y - point1.y) * (point3.x - point1.x));
        }
        private void QuickHull(Point point1, Point point2, int v)
        {
            int pos = -1;
            int MAXdistance = 0;
            for (int i = 0; i < st_tock; i++)
            {

                int distanceATM = distance(point1, point2, points[i]);

                if (Stran(point1, point2, points[i]) == v && distanceATM > MAXdistance)
                {
                    pos = i;
                    MAXdistance = distanceATM;
                    i++;
                }
            }

            if (pos > 0)
            {
                myLine6 = new Line();
                myLine6.Stroke = System.Windows.Media.Brushes.Black;
                myLine6.StrokeThickness = 2;
                myLine6.X1 = points[pos].x;
                myLine6.Y1 = points[pos].y;
                myLine6.X2 = point2.x;
                myLine6.Y2 = point2.y;

                myLine7 = new Line();
                myLine7.Stroke = System.Windows.Media.Brushes.Black;
                myLine7.StrokeThickness = 2;
                myLine7.X1 = points[pos].x;
                myLine7.Y1 = points[pos].y;
                myLine7.X2 = point1.x;
                myLine7.Y2 = point1.y;

                myGrid.Children.Add(myLine6);
                myGrid.Children.Add(myLine7);
            }

            

            if (pos == -1)
            {
                shell.Add(point1);
                shell.Add(point2);
                return;
            }

            QuickHull(points[pos], point1 , -Stran(points[pos], point1, point2));
            QuickHull(points[pos], point2, -Stran(points[pos], point2, point1));

        }

        private void RB3_Checked(object sender, RoutedEventArgs e)
        {
            myGrid.Children.Clear();

            if (String.IsNullOrEmpty(sttock.Text))
            {
                string msg = "Vnesite zeljeno stevilo tock v odprto polje";
                MessageBox.Show(msg);
            }
            else
            {
                int tocke = int.Parse(sttock.Text);
                st_tock = tocke;
            }

            Random rnd = new Random();
            int x = 0, y = 0;
            for (int i = 0; i < st_tock; i++)
            {
                x = rnd.Next(0, 608); 
                y = rnd.Next(0, 560);
                myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.Black;
                myLine.StrokeThickness = 2;
                myLine.X1 = x;
                myLine.Y1 = y;
                myLine.X2 = x + 1;
                myLine.Y2 = y + 1;

                myGrid.Children.Add(myLine);
                points.Add(new Point(x, y));
            }
        }

        private void RB4_Checked(object sender, RoutedEventArgs e)
        {
            myGrid.Children.Clear();

            if (String.IsNullOrEmpty(sttock.Text))
            {
                string msg = "Vnesite zeljeno stevilo tock v odprto polje";
                MessageBox.Show(msg);
            }
            else
            {
                int tocke = int.Parse(sttock.Text);
                st_tock = tocke;
            }

            int mean1 = 250;
            int mean2 = 232;
            int stdDev = 50;

            Random rand = new Random();
            for (int i = 0; i < st_tock; i++)
            {
                double x1 = 1 - rand.NextDouble();
                double x2 = 1 - rand.NextDouble();
                double randNormX = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Sin(2.0 * Math.PI * x2);
                double y1 = 1 - rand.NextDouble();
                double y2 = 1 - rand.NextDouble();
                double randNormY = Math.Sqrt(-2.0 * Math.Log(y1)) * Math.Sin(2.0 * Math.PI * y2);

                int x = (int)(mean1 + stdDev * randNormX) + 50;
                int y = (int)(mean2 + stdDev * randNormY) + 50;

                myLine2 = new Line();
                myLine2.Stroke = System.Windows.Media.Brushes.Black;
                myLine2.StrokeThickness = 2;
                myLine2.X1 = x;
                myLine2.Y1 = y;
                myLine2.X2 = x + 1;
                myLine2.Y2 = y + 1;
                myGrid.Children.Add(myLine2);
                points.Add(new Point(x, y));
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (RB3.IsChecked == true)
            {
                RB3_Checked(sender, e);
                pointsTrue = true;
            }
            else if (RB4.IsChecked == true)
            {
                RB4_Checked(sender, e);
                pointsTrue = true;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (pointsTrue)
            {
                if (RB1.IsChecked == true)
                {
                    RB1_Checked(sender, e);
                }
                if (RB2.IsChecked == true)
                {
                    RB2_Checked(sender, e);
                }
            }
            else
            {
                string msg = "Naključnih točk še niste zgenerirali! Poskusite znova!";
                MessageBox.Show(msg);
            }
        }
    }
}
