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
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                XDocument document = XDocument.Load(ofd.FileName);
                foreach (var elem in document.Descendants("Item"))
                {
                    list.Add(new Bricks($"{elem.Element("ItemID").Value};{elem.Element("ItemName").Value};{elem.Element("CategoryName").Value};{elem.Element("ColorName").Value};" +
                        $"{elem.Element("Qty").Value}"));
                }
            }
            MessageBox.Show($"{list.First().ItemName}");
            dgBricks.ItemsSource = list;
        }
    }
}