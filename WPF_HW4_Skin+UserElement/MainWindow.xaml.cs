using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_HW4_Skin_UserElement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResourceDictionary lightDictionary = new ResourceDictionary
        { Source = new Uri("LightSkin.xaml", UriKind.Relative) };
        ResourceDictionary darkDictionary = new ResourceDictionary
        { Source = new Uri("DarkSkin.xaml", UriKind.Relative) };
        Style labelStyle;
        Style textBoxStyle;
        Setter setterForeground;
        Setter setterFontSize;
        Setter setterFontFamily;

        public MainWindow()
        {
            InitializeComponent();
            this.Resources.MergedDictionaries[0] = lightDictionary;
            ImageSourceConverter converter = new ImageSourceConverter();
            ImageSource imageSource = (ImageSource)converter.ConvertFromString(System.IO.Path.GetFullPath("..\\..\\1.jpg"));
            imagePhoto.Source = imageSource;
            imagePhoto.Width = 300;
            imagePhoto.Height = 300;
            SolidColorBrush brush = new SolidColorBrush(new Color());
            setterForeground = new Setter { Property = ForegroundProperty, Value = brush };
            setterFontSize = new Setter { Property = FontSizeProperty, Value = 12d };
            setterFontFamily = new Setter { Property = FontFamilyProperty, Value = new FontFamily("Arial") };

        }

        private void ButtonDarkSkin_Click(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[0] = darkDictionary;
        }

        private void ButtonLightSkin_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries[0] = lightDictionary;
        }

        private void ButtonAdvice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int height;
                int weight;
                if(int.TryParse(textBoxHeight.Text, out height) && int.TryParse(textBoxWeight.Text, out weight))
                {
                    if (height - 110 - 2 <= weight && height - 110 + 2 >= weight)
                        textBoxRecommendations.Text = "Your have perfect weight!";
                    else if (height - 110 - 2 > weight)
                        textBoxRecommendations.Text = "Your body is skinny. Eat more!";
                    else
                        textBoxRecommendations.Text = "Your body is fat. Eat even more to be fat!";
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void ContextMenuItemColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelStyle = new Style { TargetType = typeof(System.Windows.Controls.Label) };
                textBoxStyle = new Style { TargetType = typeof(System.Windows.Controls.TextBox) };
                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
                setterForeground = new Setter { Property = ForegroundProperty, Value = brush };
                SetStyleSetters(labelStyle, setterForeground, setterFontSize, setterFontFamily);
                SetStyleSetters(textBoxStyle, setterForeground, setterFontSize, setterFontFamily);
                SetStyleTextBox(textBoxStyle, textBoxHeight, textBoxRecommendations, textBoxWeight);
                SetStyleLabel(labelStyle, labelHeight, labelRecommendations, labelWeight);
            }
        }

        private void SetStyleSetters(Style style, params Setter[] setter)
        {
            foreach (Setter item in setter)
            {
                style.Setters.Add(item);
            }
        }

        private void SetStyleLabel(Style style, params System.Windows.Controls.Label[] label)
        {
            foreach (System.Windows.Controls.Label item in label)
            {
                item.Style = style;
            }
        }
        private void SetStyleTextBox(Style style, params System.Windows.Controls.TextBox[] textBoxes)
        {
            foreach (System.Windows.Controls.TextBox item in textBoxes)
            {
                item.Style = style;
            }
        }

        private void ContextMenuItemFont_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelStyle = new Style { TargetType = typeof(System.Windows.Controls.Label) };
                textBoxStyle = new Style { TargetType = typeof(System.Windows.Controls.TextBox) };
                Setter setterFontSize = new Setter { Property = FontSizeProperty, Value = (double)fd.Font.Size };
                Setter setterFontFamily = new Setter { Property = FontFamilyProperty, Value = new FontFamily(fd.Font.FontFamily.GetName(0)) };
                SetStyleSetters(labelStyle, setterForeground, setterFontSize, setterFontFamily);
                SetStyleSetters(textBoxStyle, setterForeground, setterFontSize, setterFontFamily);
                SetStyleTextBox(textBoxStyle, textBoxHeight, textBoxRecommendations, textBoxWeight);
                SetStyleLabel(labelStyle, labelHeight, labelRecommendations, labelWeight);
            }
        }
    }
}
