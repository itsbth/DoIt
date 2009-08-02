using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SearchableCatalog
{
    [XmlRoot]
    public class Catalog : IEnumerable<CatalogItem>, ICollection<CatalogItem>
    {
        private List<CatalogItem> _items;

        /// <summary>
        /// Do not use...
        /// </summary>
        /// <remarks>This is only here for serialization...</remarks>
        [XmlArray]
        public List<CatalogItem> Items { get; set; }

        [XmlElement]
        public object Meta { get; set; }

        public Catalog()
        {
            _items = new List<CatalogItem>();
        }

        #region IEnumerable<CatalogItem> Members

        public IEnumerator<CatalogItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region ICollection<CatalogItem> Members

        public void Add(CatalogItem item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(CatalogItem item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(CatalogItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CatalogItem item)
        {
            return _items.Remove(item);
        }

        #endregion
    }
}
