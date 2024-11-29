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
        private Line myLine8;
        private Line myLine9;

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

        public class Daljice
        {
            public Point x;
            public Point y;
            public double dolzina;

            public Daljice(Point x, Point y, double dolzina)
            {
                this.x = x; this.y = y; this.dolzina = dolzina;
            }


        }

        List<Point> points = new List<Point>();
        List<Daljice> daljice = new List<Daljice>();   

        public static int orientation(Point p, Point q, Point r)
        {
            int val = (q.y - p.y) * (r.x - q.x) -
                    (q.x - p.x) * (r.y - q.y);

            if (val == 0) return 0; // collinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
            
        private bool Intersect(Point P1, Point P2, Point P3, Point P4)
        {
            double D = Vector.CrossProduct(Vector.Subtract(new Vector(P2.x, P2.y), new Vector(P1.x, P1.y)), Vector.Subtract(new Vector(P4.x, P4.y), new Vector(P3.x, P3.y)));
            double A = Vector.CrossProduct(Vector.Subtract(new Vector(P4.x, P4.y), new Vector(P3.x, P3.y)), Vector.Subtract(new Vector(P1.x, P1.y), new Vector(P3.x, P3.y)));
            double B = Vector.CrossProduct(Vector.Subtract(new Vector(P2.x, P2.y), new Vector(P1.x, P1.y)), Vector.Subtract(new Vector(P1.x, P1.y), new Vector(P3.x, P3.y)));
            double Ua = A / D;
            double Ub = B / D;

            if (A == 0 && B == 0 && D == 0)
            {
                return true;
            }

            return 0 < Ua && Ua < 1 && 0 < Ub && Ub < 1;
        }
        List<Daljice> triangulacija = new List<Daljice>();
        List<Daljice> Seznam = new List<Daljice>();
        private void RB1_Checked(object sender, RoutedEventArgs e)
        {

            triangulacija.Clear();
            Seznam.Clear();

            if (pointsTrue)
            {

                Point[] tocka = new Point[st_tock];


                for (int i = 0; i < st_tock; i++)
                {
                    for (int j = i + 1; j < st_tock; j++)
                    {
                        double dolzina = Math.Max(Math.Abs(points[i].x - points[j].x), Math.Abs(points[i].y - points[j].y));
                        Seznam.Add(new Daljice(points[i], points[j], dolzina));
                    }
                }


                        Seznam.Sort((x, y) => x.dolzina.CompareTo(y.dolzina));
                        triangulacija.Add(Seznam[0]);

                
                        Line myLine8 = new Line();
                        myLine8.Stroke = System.Windows.Media.Brushes.Black;
                        myLine8.StrokeThickness = 2;
                        myLine8.X1 = triangulacija[0].x.x;
                        myLine8.Y1 = triangulacija[0].x.y;
                        myLine8.X2 = triangulacija[0].y.x;
                        myLine8.Y2 = triangulacija[0].y.y;

                        //myGrid.Children.Add(myLine8);
                        
                        for (int k = 0; k < st_tock; k++)
                        {
                            tocka[k] = new Point(points[k].x, points[k].y);
                        }
                        List<Point> shell = new List<Point>();

                        int skrajno_levo = 0;
                        for (int f = 1; f < st_tock; f++)
                        {
                            if (tocka[f].x < tocka[skrajno_levo].x)
                            {
                                skrajno_levo = f;
                            }
                        }
                        int position = skrajno_levo, next;

                        do
                        {
                            shell.Add(tocka[position]);
                            next = (position + 1) % st_tock;
                            for (int a = 0; a < st_tock; a++)
                            {
                                if (orientation(tocka[position], tocka[a], tocka[next]) == 2)
                                {

                                    next = a;
                                }


                            }


                            position = next;
                        } while (position != skrajno_levo);
                        
                        for (int g = 0; g < Seznam.Count; g++)
                        {
                            bool seseka = false;
                            for (int h = 0; h < triangulacija.Count; h++)
                            {
                             Point P1 = Seznam[g].x;
                             Point P2 = Seznam[g].y;
                             Point P3 = triangulacija[h].x;
                             Point P4 = triangulacija[h].y;

                                if (Intersect(P1, P2, P3, P4))
                                {
                                    seseka = true;
                                    break;
                                }
                            }
                        if (seseka == false)
                        {
                            triangulacija.Add(Seznam[g]);
                       
                        }

                            if (triangulacija.Count == 3 * points.Count - 3 - shell.Count)
                            {
                                break;
                            }
                               
                        }    
                        
                        for (int i = 1; i < triangulacija.Count; i++)
                        {
                             Line myLine9 = new Line();
                             myLine9.Stroke = System.Windows.Media.Brushes.Black;
                             myLine9.StrokeThickness = 2;
                             myLine9.X1 = triangulacija[i].x.x;
                             myLine9.Y1 = triangulacija[i].x.y;
                             myLine9.X2 = triangulacija[i].y.x;
                             myLine9.Y2 = triangulacija[i].y.y;

                             myGrid.Children.Add(myLine9);
                        }

                points.Clear();
            }
            else
            {
                string msg = "Znova generirajte naključne točke";
                MessageBox.Show(msg);
            }
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
            }
            else
            {
                string msg = "Naključnih točk še niste zgenerirali! Poskusite znova!";
                MessageBox.Show(msg);
            }
        }
    }
}
