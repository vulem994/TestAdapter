using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestAdapter.Models.Interfaces;

namespace TestAdapter.Models
{
    [XmlRoot(ElementName = "Item")]
    public class Item : IValue
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Quantity")]
        public int Quantity { get; set; }

        [XmlElement(ElementName = "Price")]
        public double Price { get; set; }

        [XmlElement(ElementName = "Modifiers")]
        public List<Modifier> Modifiers { get; set; }

        public double GetTotalValue()
        {
            double toRet = Quantity * Price;
            if (Modifiers != null && Modifiers.Count > 0)
            {
                foreach (var modifier in Modifiers)
                {
                    toRet += modifier.GetTotalValue();
                }
            }
            return toRet;
        }
    }//[Class]
}//[Namespace]
