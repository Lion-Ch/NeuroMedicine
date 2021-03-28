using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using NeuroMedicine.BusinessLayer.ViewModels;

namespace NeuroMedicine.Views.WindowView
{
    /// <summary>
    /// Логика взаимодействия для DiagnosticsView.xaml
    /// </summary>
    partial class DiagnosticsView
    {
        private DiagnosticVM _viewModel;
        public override BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = (DiagnosticVM)value;
                this.DataContext = _viewModel;
            }
        }


        public DiagnosticsView()
        {
            InitializeComponent();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                //SelectedPatient.PhotoUrl = dlg.FileName;
                OnPropertyChanged("Patients");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
