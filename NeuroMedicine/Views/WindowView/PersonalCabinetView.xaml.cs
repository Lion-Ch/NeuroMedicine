﻿using NeuroMedicine.BusinessLayer.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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

namespace NeuroMedicine.Views.WindowView
{
    public partial class PersonalCabinet
    {
        private PersonalCabinetVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (PersonalCabinetVM)value;
                this.DataContext = _viewModel;
            }
        }

        public PersonalCabinet()
        {
            InitializeComponent();
        }
    }
}
