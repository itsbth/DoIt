using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SearchableCatalog;
using DoItLib;
using System.Xml.Serialization;

namespace SearchTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 100;

            Indexer indexer = new Indexer(new string[]{ Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), @"C:\ProgramData\Microsoft\Windows\Start Menu" });
            Catalog catalog = (Catalog)(new XmlSerializer(typeof(Catalog)).Deserialize(new FileStream("Catalog.xml", FileMode.Open)));
            string search;
            do
            {
                search = Console.ReadLine();
                IOrderedEnumerable<CatalogItem> list = new CatalogSearcher(catalog).Search(search);
                foreach (CatalogItem item in list.Take(5))
                {
                    Console.WriteLine(item.Name);
                }
                //list.First().IncreaseCount(search);
            } while (search != "quit");
	{
	         
	}
        }
    }
}
