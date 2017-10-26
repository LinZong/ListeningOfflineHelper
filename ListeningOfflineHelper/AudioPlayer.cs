using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DirectX.AudioVideoPlayback;

namespace ListeningOfflineHelper
{
    class AudioPlayer
    {
        public static void PlayListeningAudio(string AudioFilePath,int SoundDuration)
        {
                Audio audio = new Audio(AudioFilePath);
                audio.Play();
                Thread.Sleep(SoundDuration * 1000);
        }
    }
}
