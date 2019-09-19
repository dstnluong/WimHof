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

    private Audio postBreathing1;
    private Audio postBreathing2;
    private Audio postBreathing3;
    private bool breathingUnfinished;
    public Text display;

    public Light topLight;
    public Light bottomLight;
    private int initialLightIntensity;
    enum state { OPEN, CLOSE };
    // Start is called before the first frame update
    void Start()
    {
        //Restrict movement. 


        instantiateText();
        //Display introText;
        triggerActive = false;
        breathingUnfinished = true;
        //Wait for trigger to activate
        while (!triggerActive) {
            triggerActive = 
        }
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
        //END OBSTACLE (switch scenes)

    }

    void setText(string str) {
        text = GetComponent<Text>();
        text = str;
    }

    void instantiateText(){
        text = GetComponent<Text>();
        string text = “Please take a seat and click trigger when seated”;
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
            setText("");

    }

    IEnumerator postBreathing()
    {
        setText("Hold your breath until you rly need to breath. You should start feeling breath tremors");
        postBreathing1.play();

        setText("Breath in fully");
        postBreathing2.play();

        setText("Hold your breath for 15 seconds");
        postBreathing3.play();

    }

    IEnumerator breathe(string text, Audio audio, int time, bool display)
    {
        //Change text if displayText
        if(display)
        {
            displayText(text)
        }
        //Play Audio
        Audio.play();
        //Wait for time
        yield return new WaitforSeconds(time);

    }

    void eyes(state state)
    {
        int smoothValue = 3f;
        if (state == CLOSE)
        {
            while (topLight.intensity >= 0 && bottomLight.intensity >= 0) 
            {
                if(topLight.intensity >= 0)
                {
                    topLight.intensity -= Time.delaTime*smoothValue;
                }
                if(bottomLight.intensity >= 0)
                {

                    bottomLight.intensity -= Time.delaTime*smoothValue;
                }
            }
        } 
        if (state == CLOSE) {
            while (topLight.intensity >= 0 && bottomLight.intensity >= 0) 
            {
                if(topLight.intensity >= 0) {
                    topLight.intensity -= Time.delaTime*smoothValue;
                }
                if(bottomLight.intensity >= 0){
                    bottomLight.intensity -= Time.delaTime*smoothValue;
                }
            }
        }


    }

    void instantiateText()
    {
        introText = "Take a seat and trigger when ready";
        inhaleText = "inhale deeply";
        exhaleText = "release breath, don't exhale";
        finishText = "";
    }
}
