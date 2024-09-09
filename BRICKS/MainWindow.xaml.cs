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
                    try
                    {
                        XDocument document = XDocument.Load(ofd.FileName);

                        document.Descendants("Item").ToList().ForEach(x =>
                            list.Add(new Bricks($"{x.Element("ItemID")?.Value};{x.Element("ItemName")?.Value};{x.Element("CategoryName")?.Value};{x.Element("ColorName")?.Value};" +
                                $"{x.Element("Qty")?.Value}")));


                    }
                    catch (Exception)
                    {

                        MessageBox.Show("A fájl nem létezik vagy nincs kiválasztva!");
                    }

                }
                list.Select(x => x.CategoryName).Distinct().ToList().ForEach(x => cbKategoriak.Items.Add(x));
                //cbKategoriak.ItemsSource = list.Select(x => x.CategoryName).Distinct().ToList();
            };

            tbKeresNev.TextChanged += (s, e) =>
            {
                if (tbKeresId.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}") &&
                    x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}"));


                cbKategoriak.Items.Clear();
                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                        cbKategoriak.Items.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().ToList().First());

            };

            tbKeresId.TextChanged += (s, e) =>
            {
                if (tbKeresNev.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}")
                    && x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemID.StartsWith($"{tbKeresId.Text}"));


                cbKategoriak.Items.Clear();
                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                        cbKategoriak.Items.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().ToList().First());

            };

            cbKategoriak.SelectionChanged += (s, e) =>
            {
                dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}")
                        && x.ItemID.StartsWith($"{tbKeresId.Text}") && x.CategoryName == cbKategoriak.SelectedItem.ToString());


                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                        cbKategoriak.Items.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().ToList().First());
            };

            btnTorles.Click += (s, e) =>
            {
                if (dgBricks.SelectedItems.Count == 1)
                {
                    if (dgBricks.SelectedIndex != -1 && dgBricks.SelectedItem is Bricks)
                    {
                        list.Remove(dgBricks.SelectedItem as Bricks);
                        dgBricks.Items.Refresh();
                    }
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