using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace WimHof
{
    public class WimHof : MonoBehaviour
    {
        /* Canvas Text */
        private Text display;
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
            display = GetComponent<Canvas>().GetComponent<Text>();
            sound = GetComponentInChildren<Sound>();
            maxTime = 10f;
            close = false;
            open = false;
            InstantiateText();

            // Display initial text to users;
            SetText(initialText);
            breathingUnfinished = true;
        }
        /*
         * Begin rounds of power breathing followed by deep breaths.
         * cycles - how many times to repeat entire process
         * rounds - how many times to inhale and exhale
         */
        IEnumerator BeginWinHof(int cycles, int rounds)
        {
            sound.PlaySound(0);
            for (int cycle = 1; cycles <= 3; cycle++)
            {
                // Close eyes after the first cycle
                if (cycle == 2)
                {
                    SetText("Now slowly close your eyes.");
                    ActivateEyes(true);
                }
                for (int round = 1; round <= rounds; round++)
                {
                    bool isFirstCycle = cycle == 1;

                    // Inhale
                    yield return new WaitForSeconds(5);
                    Inhale(isFirstCycle);

                    // Exhale
                    yield return new WaitForSeconds(5);
                    bool isNotLastCycle = round != rounds;
                    Exhale(isFirstCycle, isNotLastCycle);
                }


                // Final breathing
                for (int j = 1; j <= 3; j++)
                {
                    yield return new WaitForSeconds(10);
                    SetText("Hold your breath until you really need to breathe. You should start feeling breath tremors.");
                    sound.PlaySound(4);

                    yield return new WaitForSeconds(5);
                    SetText("Breathe in fully.");
                    sound.PlaySound(5);

                    yield return new WaitForSeconds(15);
                    SetText("Hold your breath for 15 seconds.");
                    sound.PlaySound(6);
                }
            }
            // All done
            sound.PlaySound(7);
            SetText(finishText);
            // Open eyes again
            ActivateEyes(false);

        }
        /* Returns when to start the obstable */
        bool ActivateTrigger()
        {
            return true;
        }

        void Update()
        {
            triggerActive = ActivateTrigger();

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
                IEnumerator wimhof = BeginWinHof(3, 30);
                StartCoroutine(wimhof);
            }
        }

        /* Set Canvas to display text
         * str - text to disaply on canvas
         */
        void SetText(string str)
        {
            display.text = str;
            Debug.Log(display);
        }

        /*
         * Initiate exhale sequence
         * display - should text be displayed or not
         * isMainExhale - alternate text and audio should be played on last breath
         */
        void Exhale(bool isFirstCycle, bool isMainExhale)
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

            if (isFirstCycle) {
                SetText(text);
            }
            sound.PlaySound(clip);
        }

        /*
         * Initiate inhale sequence
         * display - should text be displayed or not
         */
        void Inhale(bool isFirstCycle)
        {
            if (isFirstCycle)
            {
                SetText(inhaleText);
            }
            sound.PlaySound(1);
        }

        /*
         * Close or open eyes.
         * eyes - True if eyes should close, False if eyes should open
         */
        void ActivateEyes(bool eyesState)
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
        void InstantiateText()
        {
            display.text = "";
            initialText = "Please take a seat and click trigger when seated";
            inhaleText = "Inhale deeply.";
            exhaleText = "Exhale.";
            alternateExhaleText = "Release your breath, but don't exhale";
            finishText = "Open your eyes. Congrats you have finished 🙂";
        }
    }
}
