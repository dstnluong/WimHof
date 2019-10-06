using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WimHof

{
    public class Sound : MonoBehaviour
    {
        public AudioSource source;

        /* Various sound clips */
        public AudioClip initialAudio;
        public AudioClip inhaleAudio;
        public AudioClip exhaleAudio;
        public AudioClip lastExhaleAudio;
        public AudioClip postBreathing1;
        public AudioClip postBreathing2;
        public AudioClip postBreathing3;
        public AudioClip finishAudio;
        public AudioClip[] clips;
        void Start()
        {
            // Stop playing sound just in case
            source.Stop();

            // Load clips
            clips = new AudioClip[8];
            clips[0] = initialAudio;
            clips[1] = inhaleAudio;
            clips[2] = exhaleAudio;
            clips[3] = lastExhaleAudio;
            clips[4] = postBreathing1;
            clips[5] = postBreathing2;
            clips[6] = postBreathing3;
            clips[7] = finishAudio;
        }

        public void playSound(int choice)
        {
            source.Stop();
            source.clip = clips[choice];
            source.Play();
        }
    }
}