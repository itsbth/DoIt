using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SearchableCatalog
{
    public class CatalogItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public uint Count { get; set; }
        [XmlIgnore]
        public uint Score { get; set; }
        public SerializableDictionary<string, uint> CountPerWord { get; set; }

        public CatalogItem()
        {
            CountPerWord = new SerializableDictionary<string, uint>();
        }

        public void IncreaseCount(string term)
        {
            Count += 1;
            if (!CountPerWord.ContainsKey(term))
            {
                CountPerWord[term] = 0;
            }
            CountPerWord[term] += 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
