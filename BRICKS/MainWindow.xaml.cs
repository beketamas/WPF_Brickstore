using Microsoft.Win32;
using System.Collections.ObjectModel;
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
        ObservableCollection<Bricks> list = [];
        public MainWindow()
        {
            InitializeComponent();
            dgBricks.ItemsSource = list;
            btnBetoltes.Click += (s, e) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    XDocument document = XDocument.Load(ofd.FileName);

                    document.Descendants("Item").ToList().ForEach(x =>
                        list.Add(new Bricks($"{x.Element("ItemID")?.Value};{x.Element("ItemName")?.Value};{x.Element("CategoryName")?.Value};{x.Element("ColorName")?.Value};" +
                            $"{x.Element("Qty")?.Value}")));
                }
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
                    dgBricks.ItemsSource = list.Where(x => x.ItemID.StartsWith($"{tbKeresId.Text}"));
            };

            btnTorles.Click += (s, e) =>
            {
                if (dgBricks.SelectedItems.Count == 1)
                {
                    if (dgBricks.SelectedIndex != -1 && dgBricks.SelectedItem is Bricks)
                        list.Remove(dgBricks.SelectedItem as Bricks);
                    else
                        MessageBox.Show("Nincs sor kijelölve!");
                }
                else
                    foreach (var item in dgBricks.SelectedItems.Cast<Bricks>().ToList())
                        list.Remove(item);
            };
        }
    }
}