using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Net;
using ChatClient;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string EU = "Enter Username Here";

        public MainWindow()
        {
            InitializeComponent();
        }
        //logic for Create server button
        protected void Server_Click(object sender, EventArgs e)
        {
            //runs Chatserver.exe
            System.Diagnostics.Process.Start("ChatServer.exe");
        }
        //logic for IP Address button
        private void IP_Click(object sender, EventArgs e)
        {
            GetIP getIP = new GetIP();

            getIP.Show();
        }
        //logic for border
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        //logic for Minimise
        private void ButtonMinimise_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        //logic for Maximise
        private void ButtonMaximised_Click(object sender, RoutedEventArgs e)
        {
           if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
           else
            {
                Application.Current.MainWindow.WindowState= WindowState.Normal;
            }
        }
        //logic for Exit button
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //This ensures that the ip that is on the text file is wiped when the application is closed to ensure security
            StreamWriter coolFile = File.CreateText("ConnectToIP.txt");
            coolFile.WriteLine("      ");
            coolFile.Close();
            this.Close();
            Application.Current.Shutdown();
        }
    }



}
