using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WimHof
{
    public class WimHof : MonoBehaviour
    {
        /* Canvas Text */
        Text display;
        /* Text to display to user */
        private bool triggerActive;
        private string initialText;
        private string inhaleText;
        private string exhaleText;
        private string finishText;
        private string alternateExhaleText;

        /* Is obstacle finished yet */
        private bool breathingUnfinished;

        /* Light in scene to mimic closing and opening eyes */
        public Light topLight;
        public Light bottomLight;

        /* Sound script to play various clips */
        private Sound sound;

        /* Eyes should be closing */
        private bool close;

        /* Eyes should be opening */
        private bool open;

        /* Determine time spent closing eyes */
        private float mStartTime;
        private float mEndTime;
        private float maxTime;

        void Start()
        {
            /* Instantiate everything */
            display = GetComponent<Text>();
            sound = GetComponentInChildren<Sound>();
            maxTime = 10f;
            close = false;
            open = false;
            instantiateText();

            // Display initial text to users;
            setText(initialText);
            breathingUnfinished = true;
        }
        /*
         * Begin rounds of power breathing followed by deep breaths.
         * cycles - how many times to repeat entire process
         * rounds - how many times to inhale and exhale
         */
        IEnumerator beginWinHof(int cycles, int rounds)
        {
            sound.playSound(0);
            for (int cycle = 1; cycles <= 3; cycle++)
            {
                // Close eyes after the first cycle
                if (cycle == 2)
                {
                    setText("Now slowly close your eyes.");
                    eyes(true);
                }
                for (int round = 1; round <= rounds; round++)
                {
                    bool isFirstCycle = cycle == 1;

                    // Inhale
                    yield return new WaitForSeconds(5);
                    inhale(isFirstCycle);

                    // Exhale
                    yield return new WaitForSeconds(5);
                    bool isNotLastCycle = round != rounds;
                    exhale(isFirstCycle, isNotLastCycle);
                }


                // Final breathing
                for (int j = 1; j <= 3; j++)
                {
                    yield return new WaitForSeconds(10);
                    setText("Hold your breath until you really need to breathe. You should start feeling breath tremors.");
                    sound.playSound(4);

                    yield return new WaitForSeconds(5);
                    setText("Breathe in fully.");
                    sound.playSound(5);

                    yield return new WaitForSeconds(15);
                    setText("Hold your breath for 15 seconds.");
                    sound.playSound(6);
                }
            }
            // All done
            sound.playSound(7);
            setText(finishText);
            // Open eyes again
            eyes(false);

        }
        /* Returns when to start the obstable */
        bool activateTrigger()
        {
            return true;
        }

        void Update()
        {
            triggerActive = activateTrigger();

            /* Dim lights */
            if (close && topLight && bottomLight)
            {
                topLight.intensity = Mathf.InverseLerp(mEndTime, mStartTime, Time.time);
                bottomLight.intensity = Mathf.InverseLerp(mEndTime, mStartTime, Time.time);
            }
            /* Brighten lights */
             else if (open && topLight && bottomLight)
            {
                topLight.intensity = Mathf.InverseLerp(mStartTime, mEndTime, Time.time);
                bottomLight.intensity = Mathf.InverseLerp(mStartTime, mEndTime, Time.time);
            }

            /* Prevents multiple CoRoutines at once */
            if (triggerActive && breathingUnfinished)
            {
                breathingUnfinished = false;
                StartCoroutine(beginWinHof(3, 30));
            }
        }

        /* Set Canvas to display text 
         * str - text to disaply on canvas
         */
        void setText(string str)
        {
            display.text = str;
            Debug.Log(display);
        }

        /*
         * Initiate exhale sequence
         * display - should text be displayed or not
         * isMainExhale - alternate text and audio should be played on last breath
         */
        void exhale(bool display, bool isMainExhale)
        {
            string text;
            int clip;

            if (isMainExhale) {
                text = exhaleText;
                clip = 2;
            } else {
                text = alternateExhaleText;
                clip = 3;
            }

            if (display) {
                setText(clip);
            }
            sound.playSound(sound);
        }

        /*
         * Initiate inhale sequence
         * display - should text be displayed or not
         */
        void inhale(bool display)
        {
            if (display)
            {
                setText(inhaleText);
            }
            sound.playSound(1);
        }

        /*
         * Close or open eyes.
         * eyes - True if eyes should close, False if eyes should open
         */
        void eyes(bool eyesState)
        {
            if (eyesState)
            {
                Debug.Log("CLOSE");
            }
            else
            {
                Debug.Log("OPEN");
            }
            // Time for for eyes to close/open
            mStartTime = Time.time;
            mEndTime = mStartTime + maxTime;

            // Only one should be True at a time. 
            open = !eyesState;
            close = eyesState;

        }

        /* Instantiate text lines */
        void instantiateText()
        {
            display = "";
            initialText = "Please take a seat and click trigger when seated";
            inhaleText = "Inhale deeply.";
            exhaleText = "Exhale.";
            alternateExhaleText = "Release your breath, but don't exhale";
            finishText = "Open your eyes. Congrats you have finished 🙂";
        }
    }
}