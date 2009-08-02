using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using SearchableCatalog;
using System.Diagnostics;

namespace DoItLib
{
    public class DoItManager
    {
        private Catalog _catalog;
        private CatalogSearcher _searcher;

        public DoItManager() : this(true) {}

        public DoItManager(bool loadCatalog)
        {
            if (loadCatalog)
            {
                try
                {
                    LoadCatalog();
                }
                catch (Exception)
                {
                    RebuildCatalog();
                }
                _searcher = new CatalogSearcher(_catalog);
            }
        }

        public IOrderedEnumerable<CatalogItem> Search(string term)
        {
            if (_searcher == null) throw new InvalidOperationException("Catalog is not loaded");
            return _searcher.Search(term);
        }

        public void LoadCatalog()
        {
            using (FileStream fs = new FileStream("Catalog.xml", FileMode.Open))
            {
                _catalog = (Catalog)(new XmlSerializer(typeof(Catalog)).Deserialize(fs));
            }
        }

        public void RebuildCatalog()
        {
            if (File.Exists("Catalog.xml"))
            {
                File.Delete("Catalog.xml");
            }
            Process.Start("CatalogGenerator.exe").WaitForExit();
            LoadCatalog();
        }

        public void Index()
        {
            Indexer indexer = new Indexer(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) });
            Catalog catalog = new Catalog();
            List<string> items = indexer.Index();
            foreach (string item in items)
            {
                string name = new FileInfo(item).Name;
                catalog.Add(new CatalogItem() { Path = item, Name = name.Substring(0, name.Length - 4) });
            }
            _catalog = catalog;
            _catalog.Meta = new CatalogMeta { LastGenerated = DateTime.Now };
        }

        public void Save()
        {
            using(FileStream fs = new FileStream("Catalog.xml", FileMode.Open))
	        {
                ((CatalogMeta)_catalog.Meta).LastSaved = DateTime.Now;
		         new XmlSerializer(typeof(Catalog)).Serialize(fs, _catalog);
	        }
        }
    }

    internal class CatalogMeta
    {
        public DateTime LastGenerated { get; set; }
        public DateTime LastSaved { get; set; }
    }
}
