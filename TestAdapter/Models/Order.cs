using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestAdapter.Models.Interfaces;

namespace TestAdapter.Models
{
    [XmlRoot(ElementName = "Order")]
    public class Order : IValue
    {
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "Total")]
        public double Total { get; set; }

        [XmlElement(ElementName = "Item")]
        public List<Item> Items { get; set; }



        public double GetTotalValue()
        {
            double toRet = 0;
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                    toRet += item.GetTotalValue();
            }
            return toRet;
        }

        public void FilterItemsByName(string inName = "burger")
        {
            if (Items != null && Items.Count > 0)
                Items = Items.Where(o => o.Name.Equals(inName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        //[XmlIgnore]
        //[JsonIgnore]
        //private string itemsFilterString { get; set; }
        //public List<Item> FilteredItems { get; set; }

        //[OnSerializing]
        //internal void OnSerializingMethod(StreamingContext context)
        //{
        //    if (!string.IsNullOrEmpty(itemsFilterString))
        //    {
        //        FilteredItems = Items.Where(o => o.Name.ToLower() == itemsFilterString.ToLower());
        //    }
        //}

    }//[Class]
}//[Namespace]
