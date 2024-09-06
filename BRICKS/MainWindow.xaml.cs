using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BRICKS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bricks> list = [];
        public MainWindow()
        {
            InitializeComponent();
            btnBetoltes.Click += (s, e) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    XDocument document = XDocument.Load(ofd.FileName);

                    document.Descendants("Item").ToList().ForEach(x =>
                        list.Add(new Bricks($"{x.Element("ItemID").Value};{x.Element("ItemName").Value};{x.Element("CategoryName").Value};{x.Element("ColorName").Value};" +
                            $"{x.Element("Qty").Value}")));
                }
                dgBricks.ItemsSource = list;
            };

            tbKeresNev.TextChanged += (s, e) =>
            {
                if (tbKeresId.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}") &&
                    x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}"));
            };

            tbKeresId.TextChanged += (s, e) => 
            {
                if (tbKeresNev.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}") 
                    && x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresId.Text.ToLower()}"));
            };
        }


    }
}