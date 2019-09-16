using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathing : MonoBehaviour
{
    private bool triggerActive;
    private string introText;
    private string inhaleText;
    private string exhaleText;

    private string alternateExhaleText;
    
    private bool breathingUnfinished;
    public Text display;

    enum state { OPEN, CLOSE };
    // Start is called before the first frame update
    void Start()
    {
        instantiateText();
        //Display introText;
        triggerActive = false;
        breathingUnfinished = true;
        //Wait for trigger to activate
        if(triggerActive)
        {
            for (int i = 1; i <= 3; i++)
            {
                if(i == 2)
                {
                    eyes(CLOSE);
                }
                //Start inhale and exhale loop
                powerBreathing(i);
                //Final thing
                postBreathing();
                
            }
            eyes(OPEN);
        }   

    }
    void powerBreathing(int cycle) { 

            bool isFirstCycle = cycle == 1;
           
            int rounds = 30;
            //Loop
            for (int i = 1; i <= rounds; i++)
            {
                //Inhale
                breathe(inhaleText, inhaleAudio, 2, isFirstCycle);
                //Exhale
                if (i != 30)
                {
                    breathe(exhaleText, exhaleAudio, 1, isFirstCycle);
                }
                else
                {
                    breathe(alternateExhaleText, alternateExhaltAudio, 3, isFirstCycle);
                }
            }
            //remove text
            display = "";
        
    }

    IEnumerator postBreathing()
    {
        display = "Hold your breath until you rly need to breath. You should start feeling breath tremors";
        Audio.play;

        display = "Breath in fully";
        Audio.play;

        display = "Hold your breath for 15 seconds";
        Audio.play;

    }

    IEnumerator breathe(string text, Audio audio, int time, bool displayText)
    {
        //Change text if displayText
        if(displayText)
        {
            display = text;
        }
        //Play inhale Audio
        Audio.play;
        //Wait for 2 seconds
        yield return new WaitforSeconds(time);
        
    }

    void eyes(state state)
    {
        double speed = 5;
        if (state == OPEN)
        {
            speed *= -1;
        }
        //Create box

        //Move box

    }
    void instantiateText()
    {
        introText = "Take a seat and trigger when ready";
        inhaleText = "inhale deeply";
        exhaleText = "release breath, don't exhale";
        finishText = "";
    }
}
