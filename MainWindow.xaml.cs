using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Book book = new Book();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowMenu_Click(object sender, RoutedEventArgs e)
        {
            containerMenu.Visibility = containerMenu.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        }

        private void btnImportXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            };

           if(openFileDialog.ShowDialog() == true)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Book));
                    using(Stream reader = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        book = (Book)serializer.Deserialize(reader);
                        txtTitle.Text = book.Title;
                        txtAuthor.Text = book.Author;
                        txtYear.Text = book.Year.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnExportXml_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    //book.Title = txtTitle.Text;
                    //book.Author = txtAuthor.Text;
                    //book.Year = ushort.Parse(txtYear.Text);
                    UpdateBookFromUI();

                    XmlSerializer serializer = new XmlSerializer(typeof(Book));
                    using (Stream writer = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        serializer.Serialize(writer, book);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateBookFromUI()
        {
            book.Title = titlePanel.Visibility == Visibility.Visible ? txtTitle.Text : null;
            book.Author = authorPanel.Visibility == Visibility.Visible ? txtAuthor.Text : null;
            book.Year = yearPanel.Visibility == Visibility.Visible && ushort.TryParse(txtYear.Text, out ushort year) ? year : (ushort)0;
        }

        private void btnAddField_Click(object sender, RoutedEventArgs e)
        {
            AddFieldWindow popupWindow = new AddFieldWindow();

            if(popupWindow.ShowDialog() == true)
            {
                var fieldName = popupWindow.FieldName;
                var fieldValue = popupWindow.FieldValue;
                if (!string.IsNullOrWhiteSpace(fieldName) && !string.IsNullOrWhiteSpace(fieldValue))
                {
                    var fieldContainer = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
                    fieldContainer.Children.Add(new Label { Content = $"{fieldName}:", Width = 100 });
                    fieldContainer.Children.Add(new TextBox { Text = fieldValue, Width = 200, Margin = new Thickness(10, 0, 0, 0)});

                    dynamicFieldsPanel.Children.Add(fieldContainer);

                    book.DynamicFields[fieldName] = fieldValue;
                    book.DynamicFieldsList.Select(kvp => new KeyValuePairXml { Key = kvp.Key, Value = kvp.Value }).ToList();

                }
            }
        }

        private void btnRemoveField_Click(object sender, RoutedEventArgs e)
        {
            var fieldNameToRemove = txtFieldNameToRemove.Text;
            bool fieldRemoved = false;

            if (fieldNameToRemove.Equals("Title"))
            {
                titlePanel.Visibility = Visibility.Collapsed;
                fieldRemoved = true;
            }
            else if (fieldNameToRemove.Equals("Author"))
            {
                authorPanel.Visibility = Visibility.Collapsed;
                fieldRemoved = true;
            }
            else if (fieldNameToRemove.Equals("Year"))
            {
                yearPanel.Visibility = Visibility.Collapsed;
                fieldRemoved = true;
            }
            else if (book.DynamicFields.Remove(fieldNameToRemove))
            {
                var fieldContainer = dynamicFieldsPanel.Children.OfType<StackPanel>()
                    .FirstOrDefault(sp => sp.Children.OfType<Label>().Any(lbl => lbl.Content.ToString().TrimEnd(':').Equals(fieldNameToRemove)));

                if (fieldContainer != null)
                {
                    dynamicFieldsPanel.Children.Remove(fieldContainer);
                    fieldRemoved = true;
                }
            }

            if (!fieldRemoved)
            {
                MessageBox.Show("Field not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
