using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListeningOfflineHelper
{
    class XMLPaser
    {
        public string SourceFolderPath { get; set; }
        public string XMLFilePath { get; set; }

        public XDocument XMLFile { get; set; }

        public IEnumerable<XElement> Sections { get; set; }

        public IEnumerator<XElement> SectionsEnumerator { get; set; }


        public string CurrentSectionDirection { get; set; }

        public IEnumerable<XElement> CurrentSectionSounds { get; set; }

        //public IEnumerable<XElement> CurrentSectionassessmentItem { get; set; }

        public XMLPaser()
        {
        SourceFolderPath = @"D:\xln\paper\";
        XMLFilePath = SourceFolderPath + "paper.xml";
        XMLFile = XDocument.Load(XMLFilePath);
        Sections = XMLFile.Descendants("section");
        SectionsEnumerator = Sections.GetEnumerator();
        MoveToNextSection(SectionsEnumerator);
        }

        public void MoveToNextSection(IEnumerator<XElement> enumerator)
        {
            enumerator.MoveNext();
            CurrentSectionDirection = enumerator.Current.Descendants("text").First().Value.Replace("<b>", "").Replace("</b>", "");
            CurrentSectionSounds = enumerator.Current.Descendants("sound");
            //CurrentSectionassessmentItem = enumerator.Current.Descendants("choice");
        }
    }
    
    
    
}


