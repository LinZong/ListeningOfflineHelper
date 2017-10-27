using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Xml.Linq;

namespace ListeningOfflineHelper
{
    class AudioPlayer
    {
        public string SoundFolderPath { get; set; }
        private List<XElement> SoundList { get; set; }
        public AudioPlayer(List<XElement> ParseSoundList,string soundPath)
        {
            SoundList = ParseSoundList;
            SoundFolderPath = soundPath+ @"sound\";
        }

        public void PlayListeningAudio(int Indexer)
        {
            var t = SoundFolderPath + SoundList[Indexer].Attribute("src").Value;
            Audio audio = new Audio(t);
            var Duration = int.Parse(SoundList[Indexer].Attribute("duration").Value);
            audio.Play();
            Thread.Sleep(Duration * 1000);
        }

        public void PlayListeningAudio(string FileName, string Duration)
        {
            Audio audio = new Audio(SoundFolderPath + FileName);
            var dw = int.Parse(Duration);
            audio.Play();
            //Console.WriteLine(SoundFolderPath + FileName);
            Thread.Sleep(dw * 1000);
            //Thread.Sleep(1000);
        }
    }
}
