using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace SuperPhotoShop.View.Windows
{
    public partial class InputDialog : Window
    {
        public List<string> InputValues { get; private set; } = new List<string>();

        public InputDialog(List<string> fieldLabels)
        {
            InitializeComponent();
            GenerateInputFields(fieldLabels);
        }

        private void GenerateInputFields(List<string> fieldLabels)
        {
            foreach (var field in fieldLabels)
            {
                var textblock = new TextBlock()
                {
                    Text = field,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                InputFieldsPanel.Children.Add(textblock);

                var textBox = new TextBox()
                {
                    Margin = new Thickness(0,5,0,10)
                };
                InputFieldsPanel.Children.Add(textBox);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs args)
        {
            InputValues.Clear();
            foreach (var child in InputFieldsPanel.Children)
            {
                if (child is TextBox textBox)
                {
                    if(textBox.Text != null)
                        InputValues.Add(textBox.Text);
                }
            }

            DialogResult = true;
            Close();
        }
    }
}
