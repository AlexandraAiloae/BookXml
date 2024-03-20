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
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for AddFieldWindow.xaml
    /// </summary>
    public partial class AddFieldWindow : Window
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public AddFieldWindow()
        {
            InitializeComponent();
        }

        private void addNewField_Click(object sender, RoutedEventArgs e)
        {
            FieldName = txtFieldName.Text;
            FieldValue = txtFieldValue.Text;
            DialogResult = true;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
