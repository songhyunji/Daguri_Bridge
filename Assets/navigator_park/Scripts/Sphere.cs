using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Sphere : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Transform Cylinder;

    public float ATTACK_DISTANCE = 1;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Cylinder = GameObject.Find("Cylinder").transform;
        agent.destination = Cylinder.position;
    }


    void Update()
    {
    }
}

