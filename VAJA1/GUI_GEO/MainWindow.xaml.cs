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


namespace GUI_GEO
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RB1_Checked(object sender, RoutedEventArgs e)
        {
            if (RB1 != null)
            {
                T1x.IsEnabled = true;
                T1y.IsEnabled = true;
                T2x.IsEnabled = true;
                T2y.IsEnabled = true;
                T3x.IsEnabled = false;
                T3y.IsEnabled = false;
                T4x.IsEnabled = false;
                T4y.IsEnabled = false;

                myGrid.Children.Clear();
                if (String.IsNullOrEmpty(T1x.Text) || String.IsNullOrEmpty(T1y.Text) || String.IsNullOrEmpty(T2x.Text) || String.IsNullOrEmpty(T2y.Text))
                {
                    string msg = "Vnesite koordinate v odprta polja";
                    MessageBox.Show(msg);
                }
                else
                {
                    double T1x_text = double.Parse(T1x.Text);
                    double T1y_text = double.Parse(T1y.Text);
                    double T2x_text = double.Parse(T2x.Text);
                    double T2y_text = double.Parse(T2y.Text);


                    myLine = new Line();
                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                    myLine.X1 = T1x_text;
                    myLine.Y1 = T1y_text;
                    myLine.X2 = T1x_text + 1;
                    myLine.Y2 = T1y_text + 1;
                    myLine.StrokeThickness = 2;
                    myGrid.Children.Add(myLine);

                    myLine2 = new Line();
                    myLine2.Stroke = System.Windows.Media.Brushes.Black;
                    myLine2.X1 = T2x_text;
                    myLine2.Y1 = T2y_text;
                    myLine2.X2 = T2x_text + 1;
                    myLine2.Y2 = T2y_text + 1;
                    myLine2.StrokeThickness = 2;
                    myGrid.Children.Add(myLine2);

                    double manhattan =  Math.Abs(T1x_text - T2x_text) + Math.Abs(T1y_text - T2y_text);

                    double evklid = Math.Sqrt(Math.Pow((T1x_text - T2x_text), 2) + (Math.Pow((T1y_text - T2y_text), 2)));

                    double chebyshev = Math.Max(Math.Abs(T1x_text - T2x_text), Math.Abs(T1y_text - T2y_text));

                    Razdalje.Text = "Manhattanova razdalja: " + manhattan + "\n" +
                        "Evklidska razdalja: " + evklid + "\n" +
                        "Chebysheva razdalja: " + chebyshev;
                }
            }
        }
        private void RB2_Checked(object sender, RoutedEventArgs e)
        {

            if (RB2 != null)
            {
                T1x.IsEnabled = true;
                T1y.IsEnabled = true;
                T2x.IsEnabled = true;
                T2y.IsEnabled = true;
                T3x.IsEnabled = true;
                T3y.IsEnabled = true;
                T4x.IsEnabled = false;
                T4y.IsEnabled = false;

                myGrid.Children.Clear();

                if (String.IsNullOrEmpty(T1x.Text) || String.IsNullOrEmpty(T1y.Text) || String.IsNullOrEmpty(T2x.Text) || String.IsNullOrEmpty(T2y.Text) || String.IsNullOrEmpty(T3x.Text) || String.IsNullOrEmpty(T3y.Text))
                {
                    string msg = "Vnesite koordinate v odprta polja";
                    MessageBox.Show(msg);
                }
                else
                {                    
                    double T1x_text = double.Parse(T1x.Text);
                    double T1y_text = double.Parse(T1y.Text);
                    double T2x_text = double.Parse(T2x.Text);
                    double T2y_text = double.Parse(T2y.Text);
                    double T3x_text = double.Parse(T3x.Text);
                    double T3y_text = double.Parse(T3y.Text);

                    myLine = new Line();
                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                    myLine.X1 = T1x_text;
                    myLine.Y1 = T1y_text;
                    myLine.X2 = T1x_text + 1;
                    myLine.Y2 = T1y_text + 1;
                    myLine.StrokeThickness = 3;
                    myGrid.Children.Add(myLine);

                    myLine2 = new Line();
                    myLine2.Stroke = System.Windows.Media.Brushes.Black;
                    myLine2.X1 = T2x_text;
                    myLine2.Y1 = T2y_text;
                    myLine2.X2 = T3x_text;
                    myLine2.Y2 = T3y_text;
                    myLine2.StrokeThickness = 2;
                    myGrid.Children.Add(myLine2);
      

                        double V1x = (T3x_text - T2x_text);
                        double V1y = (T3y_text - T2y_text);

                        double maxValue = Math.Sqrt(Math.Pow(V1x + V1y, 2));

                        double Vnx = V1x / maxValue;
                        double Vny = V1y / maxValue;

                        double V2x = (T1x_text - T2x_text);
                        double V2y = (T1y_text - T2y_text);

                        double sp = Vnx * V2x + Vny * V2y;

                        double Tpx = T2x_text + (Vnx * sp);
                        double Tpy = T2y_text + (Vny * sp);

                        double V1_razdalja = Math.Sqrt(V1x * V1x + V1y * V1y);

                    if (sp >= 0 && sp <= V1_razdalja)
                        { 
                            myLine3 = new Line();
                            myLine3.Stroke = System.Windows.Media.Brushes.Red;
                            myLine3.X1 = T1x_text;
                            myLine3.Y1 = T1y_text;
                            myLine3.X2 = Tpx;
                            myLine3.Y2 = Tpy;
                            myLine3.StrokeThickness = 1;
                            myGrid.Children.Add(myLine3);

                        double T1_razdalja = Math.Sqrt(Math.Pow(T1x_text - Tpx, 2) + Math.Pow(T1y_text - Tpy, 2));

                        Razdalje.Text = "Točka leži na daljici." + "\n" +
                        "Koordinate: " + Tpx + " | " + Tpy + "\n" +
                        "Razdalja: " + T1_razdalja;
                        }
                        else
                        {
                        double T2_razdalja = Math.Sqrt(Math.Pow(T1x_text - T2x_text, 2) + Math.Pow(T1y_text - T2y_text, 2));
                        double T3_razdalja = Math.Sqrt(Math.Pow(T1x_text - T3x_text, 2) + Math.Pow(T1y_text - T3y_text, 2));

                        if (T2_razdalja > T3_razdalja)
                        {
                            myLine3 = new Line();
                            myLine3.Stroke = System.Windows.Media.Brushes.Red;
                            myLine3.X1 = T1x_text;
                            myLine3.Y1 = T1y_text;
                            myLine3.X2 = T3x_text;
                            myLine3.Y2 = T3y_text;
                            myLine3.StrokeThickness = 1;
                            myGrid.Children.Add(myLine3);

                            if (T2x_text < T3x_text)
                            {          
                                Razdalje.Text = "Točka leži izven daljice." + "\n" +
                                "Koordinate: " + Tpx + " | " + Tpy + "\n" +
                                "Razdalja med T1 in T3: " + T3_razdalja + "\n" +
                                "Razdalja med T1 in T2: " + T2_razdalja + "\n" +
                                "Točka leži na desni strani daljice";
                            }
                            else
                            {
                                Razdalje.Text = "Točka leži izven daljice." + "\n" +
                                "Koordinate: " + Tpx + " | " + Tpy + "\n" +
                                "Razdalja med T1 in T3: " + T3_razdalja + "\n" +
                                "Razdalja med T1 in T2: " + T2_razdalja + "\n" +
                                "Točka leži na levi strani daljice";
                            }
                        }
                        else
                        {
                            myLine3 = new Line();
                            myLine3.Stroke = System.Windows.Media.Brushes.Red;
                            myLine3.X1 = T1x_text;
                            myLine3.Y1 = T1y_text;
                            myLine3.X2 = T2x_text;
                            myLine3.Y2 = T2y_text;
                            myLine3.StrokeThickness = 1;
                            myGrid.Children.Add(myLine3);

                            if (T2x_text < T3x_text)
                            {

                                Razdalje.Text = "Točka leži izven daljice." + "\n" +
                                "Koordinate: " + Tpx + " | " + Tpy + "\n" +
                                "Razdalja med T1 in T2: " + T2_razdalja + "\n" +
                                "Razdalja med T1 in T3: " + T3_razdalja + "\n" +
                                "Točka leži na levi strani daljice";
                            }
                            else
                            {
                                Razdalje.Text = "Točka leži izven daljice." + "\n" +
                                "Koordinate: " + Tpx + " | " + Tpy + "\n" +
                                "Razdalja med T1 in T2: " + T2_razdalja + "\n" +
                                "Razdalja med T1 in T3: " + T3_razdalja + "\n" +
                                "Točka leži na levi strani daljice";
                            }
                        }
                    }


                        
                }
            }
        }

        private void RB3_Checked(object sender, RoutedEventArgs e)
        {
            if (RB3 != null)
            {
                T1x.IsEnabled = true;
                T1y.IsEnabled = true;
                T2x.IsEnabled = true;
                T2y.IsEnabled = true;
                T3x.IsEnabled = true;
                T3y.IsEnabled = true;
                T4x.IsEnabled = true;
                T4y.IsEnabled = true;

                myGrid.Children.Clear();

                if (String.IsNullOrEmpty(T1x.Text) || String.IsNullOrEmpty(T1y.Text) || String.IsNullOrEmpty(T2x.Text) || String.IsNullOrEmpty(T2y.Text) || String.IsNullOrEmpty(T3x.Text) || String.IsNullOrEmpty(T3y.Text) || String.IsNullOrEmpty(T4x.Text) || String.IsNullOrEmpty(T4y.Text))
                {
                    string msg = "Vnesite koordinate v odprta polja";
                    MessageBox.Show(msg);
                }
                else
                {
                    double T1x_text = double.Parse(T1x.Text);
                    double T1y_text = double.Parse(T1y.Text);
                    double T2x_text = double.Parse(T2x.Text);
                    double T2y_text = double.Parse(T2y.Text);
                    double T3x_text = double.Parse(T3x.Text);
                    double T3y_text = double.Parse(T3y.Text);
                    double T4x_text = double.Parse(T4x.Text);
                    double T4y_text = double.Parse(T4y.Text);

                    myLine = new Line();
                    myLine.Stroke = System.Windows.Media.Brushes.Blue;
                    myLine.X1 = T1x_text;
                    myLine.Y1 = T1y_text;
                    myLine.X2 = T2x_text;
                    myLine.Y2 = T2y_text;
                    myLine.StrokeThickness = 2;
                    myGrid.Children.Add(myLine);

                    myLine2 = new Line();
                    myLine2.Stroke = System.Windows.Media.Brushes.Green;
                    myLine2.X1 = T3x_text;
                    myLine2.Y1 = T3y_text;
                    myLine2.X2 = T4x_text;
                    myLine2.Y2 = T4y_text;
                    myLine2.StrokeThickness = 2;
                    myGrid.Children.Add(myLine2);

                    double D = (T2x_text - T1x_text) * (T4y_text - T3y_text) - (T4x_text - T3x_text) * (T2y_text - T1y_text);
                    double A = (T4x_text - T3x_text) * (T1y_text - T3y_text) - (T1x_text - T3x_text) * (T4y_text - T3y_text);
                    double B = (T2x_text - T1x_text) * (T1y_text - T3y_text) - (T1x_text - T3x_text) * (T2y_text - T1y_text);

                    double Ua = A / D;
                    double Ub = B / D;

                    if (D == A && A == B && B == 0)
                    {
                        Razdalje.Text = "Daljici sovpadata" + "\n" +
                            "Z rdečo barvo je prikazan sovpadan del";

                        if ((T1x_text > T3x_text && T2x_text < T4x_text) || (T1y_text > T3y_text && T2y_text < T4y_text))
                        {
                            myLine5 = new Line();
                            myLine5.Stroke = System.Windows.Media.Brushes.Red;
                            myLine5.X1 = T1x_text;
                            myLine5.Y1 = T1y_text;
                            myLine5.X2 = T2x_text;
                            myLine5.Y2 = T2y_text;
                            myLine5.StrokeThickness = 3;
                            myGrid.Children.Add(myLine5);
                        }
                        else if ((T1x_text > T3x_text && T4x_text < T2x_text) || (T1y_text > T3y_text && T4y_text < T2y_text))
                        {
                            myLine5 = new Line();
                            myLine5.Stroke = System.Windows.Media.Brushes.Red;
                            myLine5.X1 = T1x_text;
                            myLine5.Y1 = T1y_text;
                            myLine5.X2 = T4x_text;
                            myLine5.Y2 = T4y_text;
                            myLine5.StrokeThickness = 3;
                            myGrid.Children.Add(myLine5);
                        }
                        else if ((T1x_text < T3x_text && T2x_text < T4x_text) || (T1y_text < T3y_text && T2y_text < T4y_text))
                        {
                            myLine5 = new Line();
                            myLine5.Stroke = System.Windows.Media.Brushes.Red;
                            myLine5.X1 = T3x_text;
                            myLine5.Y1 = T3y_text;
                            myLine5.X2 = T2x_text;
                            myLine5.Y2 = T2y_text;
                            myLine5.StrokeThickness = 3;
                            myGrid.Children.Add(myLine5);
                        }
                        else if ((T1x_text < T3x_text && T4x_text < T2x_text) || (T1y_text < T3y_text && T4y_text < T2y_text))
                        {
                            myLine5 = new Line();
                            myLine5.Stroke = System.Windows.Media.Brushes.Red;
                            myLine5.X1 = T3x_text;
                            myLine5.Y1 = T3y_text;
                            myLine5.X2 = T4x_text;
                            myLine5.Y2 = T4y_text;
                            myLine5.StrokeThickness = 3;
                            myGrid.Children.Add(myLine5);
                        }
                    }
                    else if (D == 0)
                    {
                        Razdalje.Text = "Daljici sta vzporedni";
                    }
                    else if (0 <= Ua && Ua <= 1 && 0 <= Ub && Ub <= 1)
                    {
                        double PresecisceX = T1x_text + Ua * (T2x_text - T1x_text);
                        double PresecisceY = T1y_text + Ua * (T2y_text - T1y_text);

                        if (T1y_text == T3y_text || T2y_text == T4y_text || T1x_text == T3x_text || T2x_text == T4x_text)
                        {
                            Razdalje.Text = "Daljici se sekata v eni točki" + "\n" +
                            "Koordinate presecisca: " + PresecisceX + " | " + PresecisceY;
                        }
                        else
                        {


                            myLine4 = new Line();
                            myLine4.Stroke = System.Windows.Media.Brushes.Red;
                            myLine4.X1 = PresecisceX;
                            myLine4.Y1 = PresecisceY;
                            myLine4.X2 = PresecisceX + 1;
                            myLine4.Y2 = PresecisceY + 1;
                            myLine4.StrokeThickness = 5;
                            myGrid.Children.Add(myLine4);


                            Razdalje.Text = "Daljici se sekata" + "\n" +
                                "Koordinate presecisca: " + PresecisceX + " | " + PresecisceY;
                        }
                    }
                    else
                    {
                        Razdalje.Text = "Daljici se ne sekata / sovpadata / sta vzporedni";
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RB1.IsChecked == true)
            {
                RB1_Checked(sender, e);
            }
            else if (RB2.IsChecked == true)
            {
                    RB2_Checked(sender, e);
            }
            else if (RB3.IsChecked == true)
            {
                RB3_Checked(sender, e);
            }
        }
    }
}
