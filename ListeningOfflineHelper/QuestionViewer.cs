using System;
using System.Linq;
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
            var PromptSound = LongConversationAsset.Element("prompt").Elements("sound");
            var QuestionSound = LongConversationAsset.Descendants("question").Descendants("sound");
            
            foreach (var Option in Choice.Elements("option"))
            {
                Console.WriteLine(Option.Attribute("id").Value + ". " + Option.Value);
            }
            foreach (var i in PromptSound)
            {
                AudioController.PlayListeningAudio(i.Attribute("src").Value, i.Attribute("duration").Value);
            }
            
            foreach (var p in QuestionSound)
            {
                AudioController.PlayListeningAudio(p.Attribute("src").Value, p.Attribute("duration").Value);
            }                
        }
        public void LongConversation(XElement LongConversationAsset,bool IsSpotDictation)
        {
           
            var Sounds = LongConversationAsset.Descendants("sound").ToList();
            var CSound = LongConversationAsset.Parent.Descendants("sound");
            var Article = LongConversationAsset.Element("prompt").Element("text").ToString().Replace("<tag type=\"text\" />","______").Replace("<text>","").Replace("</text>","");
            Console.WriteLine(Article);
            foreach (var i in CSound)
            {
                AudioController.PlayListeningAudio(i.Attribute("src").Value, i.Attribute("duration").Value);
            } 
        }
        public void SectionA()
        {
            var ShortConversationNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "shortConversations").ToList();
            var LongConversationNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "longConversations").ToList();
            //Prepare resources
            //var ChoicesList = XmlParser.SectionsEnumerator.Current.Descendants("choice").ToList();
            for (int i = 0; i < 3; i++)
            {
                AudioController.PlayListeningAudio(i);
                if (i == 1)
                {
                    Console.WriteLine(XmlParser.CurrentSectionDirection);
                }
            }
            //开始答题 Short Conversations
            foreach (var ShortConversationNode in ShortConversationNodes)
            {
                ShortConversation(ShortConversationNode);

            }
            //Long Conversations
            foreach (var LongConversationNode in LongConversationNodes)
            {
                LongConversation(LongConversationNode);
            }
            XmlParser.MoveToNextSection(XmlParser.SectionsEnumerator);
            Console.WriteLine("------------------!!Next Section!!------------------------");
            SectionB();
        }
        public void SectionB()
        {
            var LongPassageNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "listeningPassages").ToList();
            foreach (var LongPassageNode in LongPassageNodes)
            {
                LongConversation(LongPassageNode);
            }
            XmlParser.MoveToNextSection(XmlParser.SectionsEnumerator);
            Console.WriteLine("------------------!!Next Section!!------------------------");
            SectionC();
        }
        public void SectionC()
        {
            var SpotDictatinNodes = XmlParser.Sections.Descendants("assessmentItem").Where(x => x.Attribute("type").Value == "spotDictation").ToList();
            LongConversation(SpotDictatinNodes[0], true);
        }
    }
}
