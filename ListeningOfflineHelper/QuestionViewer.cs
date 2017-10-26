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
        public XMLPaser parser = new XMLPaser();
        public async void SectionA()
        {
            
       

            var SoundList = parser.CurrentSectionSounds.ToList();
            var ChoicesList = parser.SectionsEnumerator.Current.Descendants("choice").ToList();
            //for (int i = 0; i < 4; i++)
            //{
            //    var SoundPath = parser.SourceFolderPath + @"\sound\" + SoundList[i].Attribute("src").Value;
            //    var SoundDura = int.Parse(SoundList[i].Attribute("duration").Value);
            //    AudioPlayer.PlayListeningAudio(SoundPath, SoundDura);
            //    if (i == 1)
            //    {
            //        Console.WriteLine(parser.CurrentSectionDirection);
            //    }
                
            //}
            //开始答题
            for (int j = 4,k=0;(j < 9 && k<5); j++,k++)
            {
                var SoundPath = parser.SourceFolderPath + @"\sound\" + SoundList[j].Attribute("src").Value;
                var SoundDura = int.Parse(SoundList[j].Attribute("duration").Value);
                AudioPlayer.PlayListeningAudio(SoundPath, SoundDura);
                foreach (var Option in ChoicesList[k].Elements("option"))
                {
                    Console.WriteLine(Option.Attribute("id").Value + ". " + Option.Value);
                }
                //Thread.Sleep(500);
            }
        }

        public void SectionB()
        {

        }

        public void SectionC()
        {

        }
    }
}
