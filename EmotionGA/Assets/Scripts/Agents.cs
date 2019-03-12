using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{
    public int popSize = 100;
    public GameObject ground;
    protected List<Agent> agents = new List<Agent>();
    public List<Agent> happyAgents = new List<Agent>();
  
    void Start()
    {
        Bounds boundary = ground.GetComponent<Renderer>().bounds;
        for (int i = 0; i < popSize; i++)
        {
            Agent agent = SpawnAgent(boundary);
            agents.Add(agent);
        }
        StartCoroutine(EvalLoop());
    }
    void Update()
    {
       
    }

    public Agent SpawnAgent(Bounds bounds)
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f) * bounds.size.x, UnityEngine.Random.Range(-0.5f, 0.5f) * bounds.size.y, UnityEngine.Random.Range(-0.5f, 0.5f) * bounds.size.z);
        Vector3 worldPos = ground.transform.position + randomPos;
        GameObject tmpAgent = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Agent agent = tmpAgent.AddComponent<Agent>();
        float agentHeight = tmpAgent.GetComponent<MeshFilter>().mesh.bounds.size.y;
        worldPos.y += agentHeight / 2.0f;
        tmpAgent.transform.position = worldPos;
       
        return agent;
    }
   
    float EvalFitness(Color ground, Color agent)
    {
        float fitness = (new Vector3(ground.r, ground.g, ground.b) - new Vector3(agent.r,agent.g,agent.b)).magnitude;
        return fitness;
    }
    void EvalPopulation()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            float fitness = EvalFitness(ground.GetComponent<MeshRenderer>().material.color, agents[i].GetComponent<MeshRenderer>().material.color);
            agents[i].fitnessScore = fitness;
        }
        agents.Sort(delegate (Agent agent1, Agent agent2)
            {
                if (agent1.fitnessScore > agent2.fitnessScore)
                    return 1;
                else if (agent1.fitnessScore == agent2.fitnessScore)
                    return 0;
                else return -1;
            });
        int fiftyPercent = (int)agents.Count / 2;
        if (fiftyPercent % 2 != 0)
            fiftyPercent++;

        for (int i =fiftyPercent; i <agents.Count; i++)
        {
            Destroy(agents[i].gameObject);
            agents[i] = null;
        }
        agents.RemoveRange(fiftyPercent, agents.Count - fiftyPercent);
        NextGen();
    }
    void NextGen()
    {
        List<Agent> tempAgents = new List<Agent>();
        //start at one to ensure every two sets of parents are selected to make the next gen incrememnting by 2
        for(int i = 1; i<agents.Count; i+=2)
        {
            int parent1Index = i - 1;
            int parent2Index = i;

            float geneSplit = UnityEngine.Random.Range(0.0f, 1.0f);
            Bounds bounds = ground.GetComponent<Renderer>().bounds;
            Agent childAgent1 = SpawnAgent(bounds);
            Agent childAgent2 = SpawnAgent(bounds);
            tempAgents.Add(childAgent1);
            tempAgents.Add(childAgent2);

            if(geneSplit <= 0.16f)
            {
                Color tempColour = new Color(agents[parent1Index].color.r, agents[parent1Index].color.g, agents[parent2Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent2Index].color.r, agents[parent2Index].color.g, agents[parent1Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }

            if (geneSplit <= 0.32f)
            {
                Color tempColour = new Color(agents[parent1Index].color.r, agents[parent2Index].color.g, agents[parent1Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent2Index].color.r, agents[parent1Index].color.g, agents[parent2Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }

           else if (geneSplit <= 0.48f)
            {
                Color tempColour = new Color(agents[parent1Index].color.r, agents[parent2Index].color.g, agents[parent2Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent2Index].color.r, agents[parent1Index].color.g, agents[parent1Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }

            else if (geneSplit <= 0.64f)
            {
                Color tempColour = new Color(agents[parent2Index].color.r, agents[parent1Index].color.g, agents[parent2Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent1Index].color.r, agents[parent2Index].color.g, agents[parent1Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }

            else if (geneSplit <= 0.80f)
            {
                Color tempColour = new Color(agents[parent2Index].color.r, agents[parent2Index].color.g, agents[parent1Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent1Index].color.r, agents[parent1Index].color.g, agents[parent2Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }
            else
            {
                Color tempColour = new Color(agents[parent2Index].color.r, agents[parent1Index].color.g, agents[parent2Index].color.b);
                childAgent1.SetColor(tempColour);

                tempColour = new Color(agents[parent1Index].color.r, agents[parent2Index].color.g, agents[parent1Index].color.b);
                tempColour = EvalMutation(tempColour);
                childAgent2.SetColor(tempColour);
            }
        }
        agents.AddRange(tempAgents);
    }
    public Color EvalMutation(Color agent)
    {
        float mutationRate = 0.1f;
        Vector3 mutationColour = new Vector3(agent.r, agent.g, agent.b);
        for(int i = 0; i < 3; i++)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= mutationRate)
            {
                mutationColour[i] = UnityEngine.Random.Range(0.0f, 1.0f);
            }
        }
        return new Color(mutationColour.x,mutationColour.y,mutationColour.z);
    }
    IEnumerator EvalLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            EvalPopulation();
        }
    }
   
}
