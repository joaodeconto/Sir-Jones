using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DistanceTraveled : MonoBehaviour
{
    public NavMeshAgent agent;
    public TMP_Text distanceText;
    public bool countingDistance;

    private Vector3 startingPosition;
    public float distanceTraveled = 0;

    void Start()
    {
        startingPosition = agent.transform.position;
    }

    void Update()
    {
        if (!countingDistance) return;
        distanceTraveled += agent.velocity.magnitude * Time.deltaTime;
        //float distance = Vector3.Distance(startingPosition, agent.transform.position);
        distanceText.text = "Distance Traveled: " + distanceTraveled.ToString("F2") + " units";
    }
}