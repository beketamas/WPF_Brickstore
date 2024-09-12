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
        List<string> kategoriak = [];
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
                        list.Add(new Bricks($"{x.Element("ItemID")?.Value};{x.Element("ItemName")?.Value};{x.Element("CategoryName")?.Value};{x.Element("ColorName")?.Value};" +
                            $"{x.Element("Qty")?.Value}")));
                }

                if (list.Count == 0)
                    MessageBox.Show("Nincs fájl kiválasztva, vagy nem létezik!");
                else
                {
                    dgBricks.ItemsSource = list;
                    kategoriak.Add("-Alapértelmezett-");
                    list.Select(x => x.CategoryName).Distinct().OrderBy(x => x).ToList().ForEach(x =>
                    {
                        kategoriak.Add(x);
                    });
                }

                cbKategoriak.ItemsSource = kategoriak;
            };

            tbKeresNev.TextChanged += (s, e) =>
            {

                
                if (tbKeresId.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}") &&
                    x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}"));


                kategoriak.Clear();
                kategoriak.Add("-Alapértelmezett-");
                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                    {
                        kategoriak.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().OrderBy(x => x).ToList().First());
                    }

                cbKategoriak.ItemsSource = kategoriak;

            };

            tbKeresId.TextChanged += (s, e) =>
            {

                if (tbKeresNev.Text != "")
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}")
                    && x.ItemID.StartsWith($"{tbKeresId.Text}"));

                else
                    dgBricks.ItemsSource = list.Where(x => x.ItemID.StartsWith($"{tbKeresId.Text}"));


                kategoriak.Clear();
                kategoriak.Add("-Alapértelmezett-");
                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                    {
                        kategoriak.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().OrderBy(x => x).ToList().First());
                    }

                cbKategoriak.ItemsSource = kategoriak;

            };

            cbKategoriak.SelectionChanged += (s, e) =>
            {
                
                if (kategoriak[cbKategoriak.SelectedIndex] == "-Alapértelmezett-")
                {
                    dgBricks.ItemsSource = list;
                }
                else
                {
                    dgBricks.ItemsSource = list.Where(x => x.ItemName.ToLower().StartsWith($"{tbKeresNev.Text.ToLower()}")
                        && x.ItemID.StartsWith($"{tbKeresId.Text}") && x.CategoryName == cbKategoriak.SelectedItem);
                }

                kategoriak.Clear();
                kategoriak.Add("-Alapértelmezett-");
                foreach (Bricks item in dgBricks.Items)
                    if (!cbKategoriak.Items.Contains(item.CategoryName))
                    {
                        kategoriak.Add(list.Select(x => x.CategoryName).Where(x => x == item.CategoryName).Distinct().OrderBy(x => x).ToList().First());
                    }

                cbKategoriak.ItemsSource = kategoriak;
                
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