using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp5
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Book
    {

        private string titleField;

        private string authorField;

        private ushort yearField;

        private Dictionary<string,string> dynamicFields = new Dictionary<string,string>();

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string Author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public ushort Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        [XmlIgnore]
        public Dictionary<string,string> DynamicFields
        {
            get { return this.dynamicFields; }
            set { this.dynamicFields = value; }
        }

        public List<KeyValuePairXml> DynamicFieldsList
        {
            get
            {
                var list = new List<KeyValuePairXml>();
                foreach(var kvp in DynamicFields)
                {
                    list.Add(new KeyValuePairXml
                    {
                        Key = kvp.Key,
                        Value = kvp.Value
                    });
                }
                return list;
            }
            set
            {
                dynamicFields = new Dictionary<string, string>();
                foreach(var kvp in value)
                {
                    dynamicFields[kvp.Key] = kvp.Value;
                }
            }
        }

        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrEmpty(Title);
        }

        public bool ShouldSerializeAuthor()
        {
            return !string.IsNullOrEmpty(Author);
        }

        public bool ShouldSerializeYear()
        {
            return Year != 0;
        }

    }

    //Helper class for XML serialization of Dictionary
    public class KeyValuePairXml
    {
        [XmlAttribute]
        public string Key;

        [XmlElement]
        public string Value;
    }


}
