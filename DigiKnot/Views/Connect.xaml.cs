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

namespace DigiKnot.Views
{
    /// <summary>
    /// Interaction logic for Connect.xaml
    /// </summary>
    public partial class Connect : Window
    {
        public string dbLocation = null;
        public bool track = false;

        public Connect()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dbLocation = dbString.Text;
            track = true;

            Close();
        }
    }
}
