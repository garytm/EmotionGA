using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Color color = new Color(1.0f, 1.0f, 1.0f);
    public Material material;
    int timer;
    //an enum to hold the various emotional states available to agents
    public enum EmotionalStates
    {
        Happy,
        Sad,
        Angry,
        Scared
    }
    //creating a public instance of the EmotionalStates enum to allow manipulation
    public EmotionalStates emotionalState;
    //accessing the very beginning of runtime
    public void Awake()
    {
        timer = Random.Range(0, 20);
        //setting the emotional state for agents to be random on start
        emotionalState = (EmotionalStates)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EmotionalStates)).Length);
        material = gameObject.GetComponent<Renderer>().material;
    }
    public void Update()
    {
        AssignColour(this.gameObject);
        if (timer < 10)
        {
            emotionalState = (EmotionalStates)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EmotionalStates)).Length);
        }
    }
    public void AssignColour(GameObject ground)
    {
        if (emotionalState == EmotionalStates.Happy)
        {
            ground.GetComponent<Agent>().SetColor(Color.yellow);
            transform.tag = "Happy";

        }
        else if (emotionalState == EmotionalStates.Sad)
        {
            ground.GetComponent<Agent>().SetColor(Color.blue);
            transform.tag = "Sad";
        }
        else if (emotionalState == EmotionalStates.Angry)
        {
            ground.GetComponent<Agent>().SetColor(Color.red);
            transform.tag = "Angry";
        }
        else if (emotionalState == EmotionalStates.Scared)
        {
            ground.GetComponent<Agent>().SetColor(Color.black);
            transform.tag = "Scared";
        }
    }
    public void SetColor(Color colour)
    {
        //update the instances colour with the passed parameter
        this.color = colour;
        material.color = color;
    }
}

