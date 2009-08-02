using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchableCatalog;

namespace DoItLib
{
    class SearchResult
    {
        public CatalogItem Item { get; set; }
        public int Score { get; set; }
    }
}
