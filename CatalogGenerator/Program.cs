using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SearchableCatalog;
using DoItLib;
using System.Xml.Serialization;

namespace CatalogGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            DoItManager dm = new DoItManager(false);
            dm.Index();
            dm.Save();
        }
    }
}
