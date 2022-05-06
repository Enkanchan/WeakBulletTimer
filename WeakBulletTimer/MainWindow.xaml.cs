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
using System.Runtime.InteropServices;

namespace WeakBulletTimer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private DispatcherTimer MyTimer;
        DispatcherTimer dispTimer; 
        double time;
        bool TimerStartFlag = false;
        [DllImport("user32.dll")]
        private static extern short GetKeyState(int vKey);
        public MainWindow()
        {
            
            InitializeComponent();
            var vKey1 = KeyInterop.VirtualKeyFromKey(Key.A);
            MyTimer = new DispatcherTimer();
            MyTimer.Tick += MyTimer_Tick;
            //時間間隔、8ミリ秒にしてみた
            MyTimer.Interval = new TimeSpan(0, 0, 0, 0, 8);
            MyTimer.Start();
            MyCombo1Key.Items.Add(Key.D1);
            MyCombo1Key.Items.Add(Key.D2);
            MyCombo1Key.Items.Add(Key.D3);
            MyCombo1Key.Items.Add(Key.D4);
            MyCombo1Key.Items.Add(Key.D5);
            MyCombo1Key.Items.Add(Key.D6);
            MyCombo1Key.Items.Add(Key.D7);
            MyCombo1Key.Items.Add(Key.D8);
            MyCombo1Key.Items.Add(Key.D9);
            MyCombo1Key.Items.Add(Key.D0);
            MyCombo1Key.Items.Add(Key.F1);
            MyCombo1Key.Items.Add(Key.F2);
            MyCombo1Key.Items.Add(Key.F3);
            MyCombo1Key.Items.Add(Key.F4);
            MyCombo1Key.Items.Add(Key.F5);
            MyCombo1Key.Items.Add(Key.F6);
            MyCombo1Key.SelectedIndex = 1;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispTimer = new DispatcherTimer();
            dispTimer.Tick += DispTimer_Tick;
            dispTimer.Interval = new TimeSpan(0, 0, 1);   
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            var vKey1 = KeyInterop.VirtualKeyFromKey((Key)MyCombo1Key.SelectedItem);
            short key1State = GetKeyState(vKey1);
            if (key1State <= -127 && !TimerStartFlag)
            {
                dispTimer.Stop();
                TimerStartFlag = true;
                TimeText.Text = "15";
                time = 0;
                dispTimer.Start();
            }
            else if (key1State >= 0 && TimerStartFlag)
            {
                TimerStartFlag = false;
            }

            if (time <= 9)
            {
                TimeText.Text = (15 - time).ToString();
                TimeText.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (time >= 10 && time <= 15)
            {
                TimeText.Text = (15 - time).ToString();
                TimeText.Foreground = new SolidColorBrush(Colors.Red);
            }

        }
        private void DispTimer_Tick(object sender, object e)
        {
            time=time+1;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
