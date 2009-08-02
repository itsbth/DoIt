using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SearchableCatalog;
using System.Xml.Serialization;

namespace TestGUI
{
    public partial class MainForm : Form
    {
        Catalog catalog;
        CatalogSearcher searcher;
        ImageList images;

        public MainForm()
        {
            InitializeComponent();
            catalog = (Catalog)(new XmlSerializer(typeof(Catalog)).Deserialize(new FileStream("Catalog.xml", FileMode.Open)));
            searcher = new CatalogSearcher(catalog);
            images = new ImageList();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            IOrderedEnumerable<CatalogItem> items = searcher.Search(searchBox.Text);
            hitList.Items.Clear();
            foreach (CatalogItem item in items.Take(5))
            {
                ListViewItem li = new ListViewItem(new string[] { item.Score.ToString(), item.Name });
                hitList.Items.Add(li);
            }
        }
    }
}
