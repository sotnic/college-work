using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

// Oleksandr Sotnykov - #300986475
namespace WpfBill
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ObservableCollection<Dish> bill = new ObservableCollection<Dish>();
        public event PropertyChangedEventHandler PropertyChanged;
        List<String> beverages = new List<string>();
        List<String> apperitizers = new List<string>();
        List<String> mains = new List<string>();
        List<String> desserts = new List<string>();
        List<Dish> dishes = new List<Dish>()
        {
            new Dish {Name = "Soda", Price = 1.95, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "Tea", Price = 1.50, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "Coffee", Price = 1.25, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "Mineral Water", Price = 2.95, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "Milk", Price = 1.5, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "Juice", Price = 2.5, Quantity = 1, Type = "Beverage"},
            new Dish {Name = "BUffallo Wings", Price = 5.95, Quantity = 1, Type = "Apperitizer"},
            new Dish {Name = "Lasagna", Price = 6.95, Quantity = 1, Type = "Apperitizer"},
            new Dish {Name = "Potato Skins", Price = 5.60, Quantity = 1, Type = "Apperitizer"},
            new Dish {Name = "Nachos", Price = 8.95, Quantity = 1, Type = "Apperitizer"},
            new Dish {Name = "Shrimp Coctail", Price = 8.60, Quantity = 1, Type = "Apperitizer"},
            new Dish {Name = "Seafood Alfredo", Price = 17.50, Quantity = 1, Type = "Main Course"},
            new Dish {Name = "Chicken Alfredo", Price = 12.70, Quantity = 1, Type = "Main Course"},
            new Dish {Name = "Turkey Club", Price = 13.95, Quantity = 1, Type = "Main Course"},
            new Dish {Name = "Lobster Pie", Price = 19.95, Quantity = 1, Type = "Main Course"},
            new Dish {Name = "Rib ", Price = 20.95, Quantity = 1, Type = "Main Course"},
            new Dish {Name = "Apple Pie", Price = 5.50, Quantity = 1, Type = "Dessert"},
            new Dish {Name = "Sundae", Price = 6.95, Quantity = 1, Type = "Dessert"},
            new Dish {Name = "Carrot Cake", Price = 5.95, Quantity = 1, Type = "Dessert"},
            new Dish {Name = "Mud Pie", Price = 4.95, Quantity = 1, Type = "Dessert"},
            new Dish {Name = "Apple Crisps", Price = 4.50, Quantity = 1, Type = "Dessert"}
        };
        private double subtotal;
        private double tax;
        private double total;
        public double Subtotal { get { return subtotal; } set { subtotal = value; } }
        public double Tax { get { return tax; } set { tax = value; } }
        public double Total { get { return total; } set { total = value; } }
        public MainWindow()
        {
            InitializeComponent();
            AddToBill();
        }
        public void AddToBill()
        {
            DataGridTable.ItemsSource = bill;

            foreach (var dish in dishes)
            {
                if (dish.Type == "Beverage")
                {
                    beverages.Add(dish.Name);
                }
                else if (dish.Type == "Main Course")
                {
                    mains.Add(dish.Name);
                }
                else if (dish.Type == "Apperitizer")
                {
                    apperitizers.Add(dish.Name);
                }
                else
                {
                    desserts.Add(dish.Name);
                }
            }

            cmbBeverage.ItemsSource = beverages;
            cmbAppetizer.ItemsSource = apperitizers;
            cmbDessert.ItemsSource = desserts;
            cmbMain.ItemsSource = mains;

        }
        private void DataGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculation();
        }
        private void cmbBox_SelectionChanged(object sender, EventArgs e)
        {
            var cmb = sender as ComboBox;
            string item = cmb.SelectedItem.ToString();

            if (bill.Contains(bill.Where(x => x.Name == item).FirstOrDefault()))
            {
                bill.Where(x => x.Name == item).FirstOrDefault().Quantity += 1;
                DataGridTable.ItemsSource = null;
                DataGridTable.ItemsSource = bill;
            }
            else
            {
                Dish newDish = new Dish(dishes.Where(x => x.Name == item).FirstOrDefault());
                bill.Add(newDish);
            }
            Calculation();
        }
        public void Calculation()
        {
            subtotal = 0;
            total = 0;
            tax = 0;
            foreach (var dish in bill)
            {
                subtotal += dish.Price * dish.Quantity;
                txtSubtotal.Text = subtotal.ToString();
            }
            tax = subtotal * 0.13;
            txtTax.Text = tax.ToString();

            total = tax + subtotal;
            txtTotal.Text = total.ToString();
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            bill.Clear();
            DataGridTable.ItemsSource = null;
            DataGridTable.ItemsSource = bill;

            txtSubtotal.Clear();
            txtTax.Clear();
            txtTotal.Clear();

            cmbBeverage.Text = "";
            cmbAppetizer.Text = "";
            cmbMain.Text = "";
            cmbDessert.Text = "";
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTable.SelectedIndex != -1)
            {
                bill.RemoveAt(DataGridTable.SelectedIndex);
                DataGridTable.ItemsSource = null;
                DataGridTable.ItemsSource = bill;

                dishes.ToString();
                Calculation();
            }

        }
        private void OnPropertyChanged(string x)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(x));
            }
        }
        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("IExplore.exe", "https://centennialcollege.ca");
        }
    }
}
