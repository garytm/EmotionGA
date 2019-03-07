using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    Material groundMaterial;

    void Start()
    {
        groundMaterial = gameObject.GetComponent<Renderer>().material;
        StartCoroutine("ChangeColours"); 
    }
    IEnumerator ChangeColours()
    {
        Vector3 prevColour = new Vector3(groundMaterial.color.r, groundMaterial.color.g, groundMaterial.color.b);
        Vector3 currColour = prevColour;
        float transTime = 5.0f;

        while (true)
        {
            Vector3 newColour = new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            Vector3 deltaColour = (newColour - prevColour) * (1.0f / transTime);
            while ((newColour - currColour).magnitude > 0.1f)
            {
                currColour = currColour + deltaColour * Time.deltaTime;
                groundMaterial.color = new Color(currColour.x, currColour.y, currColour.z);
                yield return null;
            }
            prevColour = newColour;
        }
    }
}
