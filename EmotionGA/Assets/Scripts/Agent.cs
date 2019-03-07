using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float fitnessScore = 1.0f;
    public Color color = new Color(1.0f, 1.0f, 1.0f);
    public Material material;
    //accessing the very beginning of runtime
	void Awake ()
    {
        material = gameObject.GetComponent<Renderer>().material;
	}

	public void SetColor(Color colour)
    {
        //update the instances colour with the passed parameter
        this.color = colour;
        material.color = color;
    }
}
