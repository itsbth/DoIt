using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchableCatalog
{
    public class CatalogSearcher
    {
        private Catalog _catalog;

        public CatalogSearcher(Catalog catalog)
        {
            _catalog = catalog;
        }

        public IOrderedEnumerable<CatalogItem> Search(string term)
        {
            return _catalog.OrderBy(item => -Match(item, term));
            List<CatalogItem> retVal = _catalog.ToList();
            // And then some magic occurs...
            // Or...
            // TODO: Add magic
            return (IOrderedEnumerable<CatalogItem>)retVal;
        }

        private uint Match(CatalogItem item, string term)
        {
            uint retVal = 0;

            string lname = item.Name.ToLower();
            string lterm = term.ToLower();

            if (lname == lterm)
                retVal += 1000;

            if (lname.IndexOf(lterm) != -1)
                retVal += 100;

            if(item.CountPerWord.ContainsKey(lterm)) retVal += item.CountPerWord[term] * 10;

            int last = 0;
            for (int i = 0; i < lterm.Length; i++)
            {
                int idx = lname.IndexOf(lterm[i], last);
                if (idx != -1)
                {
                    if (idx == 0 || lname[idx - 1] == ' ')
                    {
                        retVal += 40;
                    }
                    else
                    {
                        retVal += 20;
                    }
                    last = idx;
                }
                else if (retVal >= 5)
                {
                    retVal -= 5;
                }
            }
            if (retVal > lname.Length / 2) retVal -= (uint)lname.Length / 2;

            item.Score = retVal;
            return retVal;
        }
    }
}
