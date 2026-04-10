using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        GameObject core = GameObject.FindGameObjectWithTag("Finish");
        if (core != null) agent.SetDestination(core.transform.position);
    }
}