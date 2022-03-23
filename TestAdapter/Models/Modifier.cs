using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestAdapter.Models.Interfaces;

namespace TestAdapter.Models
{
    [XmlRoot(ElementName = "Modifiers")]
    public class Modifier : IValue
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Quantity")]
        public int Quantity { get; set; }

        [XmlElement(ElementName = "Price")]
        public double Price { get; set; }

        public double GetTotalValue()
        {
            return Quantity * Price;
        }
    }//[Class]
}//[Namespace]
