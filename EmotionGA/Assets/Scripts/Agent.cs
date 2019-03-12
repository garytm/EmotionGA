using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float fitnessScore = 1.0f;
    public Color color = new Color(1.0f, 1.0f, 1.0f);
    public Material material;
    public Agents agentsRef;
    int RNG;
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
    void Awake ()
    {
        //setting the emotional state for agents to be random on start
        emotionalState = (EmotionalStates)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EmotionalStates)).Length);
        material = gameObject.GetComponent<Renderer>().material;
	}
    public void AssignColour(GameObject agent)
    {
        if (emotionalState == EmotionalStates.Happy)
        {
            agent.GetComponent<Agent>().SetColor(Color.yellow);
            transform.tag = "Happy";

        }
        else if (emotionalState == EmotionalStates.Sad)
        {
            agent.GetComponent<Agent>().SetColor(Color.blue);
            transform.tag = "Sad";
        }
        else if (emotionalState == EmotionalStates.Angry)
        {
            agent.GetComponent<Agent>().SetColor(Color.red);
            transform.tag = "Angry";
        }
        else if (emotionalState == EmotionalStates.Scared)
        {
            agent.GetComponent<Agent>().SetColor(Color.black);
            transform.tag = "Scared";
        }
    }
    void OnCollision(Collision collider)
    {
        if (emotionalState == EmotionalStates.Happy)
        {
            if (collider.transform.tag == "Sad" && RNG < 4)
            {
                emotionalState = EmotionalStates.Happy;
            }
            else if (emotionalState == EmotionalStates.Scared && RNG < 4)
            {
                emotionalState = EmotionalStates.Scared;
            }
            else emotionalState = EmotionalStates.Sad;
        }
    }
    public void SetColor(Color colour)
    {
        //update the instances colour with the passed parameter
        this.color = colour;
        material.color = color;
    }
    public void Update()
    {
        AssignColour(this.gameObject);
        RNG = Random.Range(0, 9);
    }
}
