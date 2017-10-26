using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListeningOfflineHelper
{
    class QuestionViewer
    {
        public XMLPaser XmlParser { get; set; }

        public AudioPlayer AudioController { get; set; }

        public QuestionViewer(XMLPaser parserInstance)
        {
            XmlParser = parserInstance;
            AudioController = new AudioPlayer(XmlParser.CurrentSectionSounds.ToList(),XmlParser.SourceFolderPath);
        }
        public void ShortConversation(XElement ShortConversationAsset)
        {
            var Choice = ShortConversationAsset.Descendants("choice").First();
            var Sounds = ShortConversationAsset.Descendants("sound").ToList();
            AudioController.PlayListeningAudio(Sounds[0].Attribute("src").Value, Sounds[0].Attribute("duration").Value);
            
            if (Sounds[0].Attribute("src").Value.StartsWith("now"))
            {
                AudioController.PlayListeningAudio(Sounds[1].Attribute("src").Value, Sounds[1].Attribute("duration").Value);
                foreach (var Option in Choice.Elements("option"))
                {
                    Console.WriteLine(Option.Attribute("id").Value + ". " + Option.Value);
                }
                
                AudioController.PlayListeningAudio(Sounds[2].Attribute("src").Value, Sounds[2].Attribute("duration").Value);
            }
            else
            {
                foreach (var Option in Choice.Elements("option"))
                {
                    Console.WriteLine(Option.Attribute("id").Value + ". " + Option.Value);
                }
                AudioController.PlayListeningAudio(Sounds[1].Attribute("src").Value, Sounds[1].Attribute("duration").Value);
            }
        }
        public void LongConversation(XElement LongConversationAsset)
        {
            var Choice = LongConversationAsset.Descendants("choice");
            var Sounds = LongConversationAsset.Descendants("sound").ToList();
            var PromptSound = LongConversationAsset.Element("prompt").Elements("sound");
            var QuestionSound = LongConversationAsset.Descendants("question").Descendants("sound");
            foreach (var i in PromptSound)
            {
                AudioController.PlayListeningAudio(i.Attribute("src").Value, i.Attribute("duration").Value);
            }
            foreach (var Option in Choice.Elements("option"))
            {
                Console.WriteLine(Option.Attribute("id").Value + ". " + Option.Value);
            }
            foreach (var p in QuestionSound)
            {
                AudioController.PlayListeningAudio(p.Attribute("src").Value, p.Attribute("duration").Value);
            }                
        }

        public void SectionA()
        {
            var ShortConversationNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "shortConversations").ToList();
            var LongConversationNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "longConversations").ToList();
            //Prepare resources
            //var ChoicesList = XmlParser.SectionsEnumerator.Current.Descendants("choice").ToList();
            //for (int i = 0; i < 3; i++)
            //{
            //    AudioController.PlayListeningAudio(i);
            //    if (i == 1)
            //    {
            //        Console.WriteLine(XmlParser.CurrentSectionDirection);
            //    }
            //}
            //开始答题 Short Conversations
            for (var j = 0; j < 5; j++)
            {
                ShortConversation(ShortConversationNodes[j]);
            }
            //Long Conversations
            for (int k = 0; k < 2; k++)
            {
                LongConversation(LongConversationNodes[k]);
            }
            XmlParser.MoveToNextSection(XmlParser.SectionsEnumerator);
            SectionB();
        }
        public void SectionB()
        {
            var LongPassageNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "listeningPassages").ToList();
            for (int i = 0; i < 3; i++)
            {
                LongConversation(LongPassageNodes[i]);
            }

            XmlParser.MoveToNextSection(XmlParser.SectionsEnumerator);
            SectionC();
        }
        public void SectionC()
        {

        }
    }
}
