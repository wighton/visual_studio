using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();

        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();


        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            //itemsBinding.DataSource = store.Items;

            itemsBinding.DataSource = store.Items.Where(not => not.Sold == false).ToList();

            itemsListBox.DataSource = itemsBinding;
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";
        }

        private void SetupData()
        {
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Smith" });
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Joens" }); ;
            store.Vendors.Add(new Vendor { FirstName = "John", LastName = "Phillips" });

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.25M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "BFG",
                Description = "A book about a Big fucking guy!",
                Price = 10.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Sexy and the City",
                Description = "A book about old womoen",
                Price = 10.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Harry Potter",
                Description = "A book about a wizard",
                Price = 9.99M,
                Owner = store.Vendors[2]
            });

            store.Name = "A book about something.";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);

            //store.Items.Remove(selectedItem);
            //itemsBinding.ResetBindings(false);
        }

        private void makePurchase_Clck(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                if (item != null)
                {
                    item.Sold = true;
                }

            }
            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(not => not.Sold == false).ToList();

            cartBinding.ResetBindings(false);
            //store.Items.Clear();
            //SetupData();
            itemsBinding.ResetBindings(false);
        }

    }
}
