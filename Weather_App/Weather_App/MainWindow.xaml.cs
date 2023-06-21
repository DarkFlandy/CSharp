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
using Newtonsoft.Json;
using System.Net;

namespace Weather_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Window;
        public MainWindow()
        {
            InitializeComponent();
            Window = this;
            getWeather();
            Time.Text = DateTime.Now.ToShortTimeString();
            Date.Text = DateTime.Now.ToShortDateString();
        }

        string APIKey = "862758417927968c7e981174fc95cf3e";

        private void Drag(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MainWindow.Window.DragMove();
            }
        }

        private void search_click(object sender, RoutedEventArgs e)
        {
            getWeather();
            City.Text = Convert.ToString(city.Text);
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minus_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                oblach.Opacity = 0;
                sun.Opacity = 0;
                rain.Opacity = 0;
                snow.Opacity = 0;
                groza.Opacity = 0;
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q="+Convert.ToString(city.Text)+"&appid="+APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                //picIcon.ImageLocation = "https://openweathermap.org/img/w" + Info.weather[0].icon + ".png";
                Condition.Text = Info.weather[0].main;
                //Detalis.Text = Info.weather[0].discription;
                Wwind.Text = Info.wind.speed.ToString() + " m/s";
                Ppressure.Text = Info.main.pressure.ToString() + " hPa";
                Temp.Text = Convert.ToString(Math.Truncate(Info.main.temp - 273.15)) + "°";
                ccloud.Opacity = 0;
                def.Opacity = 1;
                ssun.Opacity = 0;
                rrain.Opacity = 0;
                ggroz.Opacity = 0;

                string a = Convert.ToString(Condition.Text);
                string Cl = "Clear";
                string Clo = "Clouds";
                string Ra = "Rain";
                string Sn = "Snow";
                string Dr = "Drizzle"; //morosi
                string Gr = "Thunderstorm";
                if (string.Compare(a, Cl) == 0)
                {
                    sun.Opacity = 1;
                    ssun.Opacity = 1;
                }
                else
                if (string.Compare(a, Clo) == 0)
                {
                    oblach.Opacity = 1;
                    ccloud.Opacity = 1;
                }
                else
                if (string.Compare(a, Ra) == 0)
                {
                    rain.Opacity = 1;
                    rrain.Opacity = 1;
                }
                else
                if (string.Compare(a, Sn) == 0)
                {
                    snow.Opacity = 1;
                }
                else
                if (string.Compare(a, Dr) == 0)
                {
                    rain.Opacity = 1;
                    rrain.Opacity = 1;
                }
                else
                if (string.Compare(a, Gr) == 0)
                {
                    groza.Opacity = 1;
                    ggroz.Opacity = 1;
                }
                else
                {
                    def.Opacity = 1;
                    oblach.Opacity = 1;
                }


                //if (string.Compare(a, Clo) == 0) oblach.Opacity = 1;



            }
        }
    }
}
