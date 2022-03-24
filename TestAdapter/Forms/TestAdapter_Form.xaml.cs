using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TestAdapter.Common;
using TestAdapter.Models;

namespace TestAdapter.Forms
{
    /// <summary>
    /// Interaction logic for TestAdapter_Form.xaml
    /// </summary>
    public partial class TestAdapter_Form : Window, INotifyPropertyChanged
    {
        private Order order;

        #region -formatedXml- property
        private String _formatedXml;
        public String formatedXml
        {
            get { return _formatedXml; }
            set
            {
                if (_formatedXml != value)
                {
                    _formatedXml = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -formatedJson- property
        private String _formatedJson;
        public String formatedJson
        {
            get { return _formatedJson; }
            set
            {
                if (_formatedJson != value)
                {
                    _formatedJson = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -formatedFilteredJson- property
        private String _formatedFilteredJson;
        public String formatedFilteredJson
        {
            get { return _formatedFilteredJson; }
            set
            {
                if (_formatedFilteredJson != value)
                {
                    _formatedFilteredJson = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //helper props
        private readonly string defaultXmlPath = @"C:\zadatak\zadatak.xml"; //default path

        public TestAdapter_Form()
        {
            this.DataContext = this;
            InitializeComponent();
        }


        //Funkcije za zadatak
        private void DoAction()
        {
            //T5 
            var doc = LoadXmlDocument(defaultXmlPath);
            if (doc != null)
            {
                formatedXml = FormatXml(doc.OuterXml);

                //T6
                order = XMLSerializer.Deserialize<Order>(doc.OuterXml);

                if (order != null)
                {
                    //T7
                    order.Total = order.GetTotalValue();
                    formatedJson = JsonConvert.SerializeObject(order, Newtonsoft.Json.Formatting.Indented); //T4

                    //T8
                    order.FilterItemsByName("burger");
                    order.Total = order.GetTotalValue();
                    formatedFilteredJson = JsonConvert.SerializeObject(order, Newtonsoft.Json.Formatting.Indented);
                }
            }
        }

        public XmlDocument LoadXmlDocument(string inPath)
        {
            try
            {
                bool fileFound = false;
                if (!File.Exists(inPath))
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "XML Files| *.xml";
                    if (fileDialog.ShowDialog() == true)
                    {
                        inPath = fileDialog.FileName;
                        fileFound = true;
                    }
                }
                else
                    fileFound = true;

                if (fileFound)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(inPath);

                    return doc;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message}");
            }
            return null;
        }

        //Helper functions
        string FormatXml(string inXmlString)
        {
            try
            {
                XDocument formatedDoc = XDocument.Parse(inXmlString);
                return formatedDoc.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message}");
            }
            return inXmlString;
        }

        //Events
        private void button_action_Click(object sender, RoutedEventArgs e)
        {
            DoAction();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region INotifyPropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion   
    }//[Window]
}//[Namespace]