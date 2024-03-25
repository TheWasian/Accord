using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for GetIP.xaml
    /// </summary>
    public partial class GetIP : Window
    {
        public GetIP()
        {
            InitializeComponent();
        }


        //Adds logic for okay button
        public void button1_Click(object sender, RoutedEventArgs e)
        {
            //This turns the text that is entered into the textbox into an IP
            string TEMPIP = TextBox1.Text.ToString();

            //This saves the IP onto a text file so the other moduel can grab the IP and input it into the sever connect
            StreamWriter coolFile = File.CreateText("ConnectToIP.txt");
            coolFile.WriteLine(TEMPIP);
            coolFile.Close();
            this.Close();
        }
        //adds logic for Cancel button
        public void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
