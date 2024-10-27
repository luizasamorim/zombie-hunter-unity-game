using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class npcScript : MonoBehaviour
{
    public GameObject pc;
    private NavMeshAgent agent;
    public GameObject[] waypoints = new GameObject[4];
    private int index;
    private GameObject destination;
    private bool chase;
    private int lives;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        next();
        chase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase || Vector3.Distance(transform.position, pc.transform.position) < 10)
        {
            chase = true;
            agent.SetDestination(pc.transform.position);
        }

        else if (Vector3.Distance(transform.position, destination.transform.position) < 5)
        {
            next();
        }
    }

    private void OnTriggerEnter(Collider other) {
        lives = pcScript.takeLife();

        if (lives == 0) SceneManager.LoadSceneAsync(2);
    }
    
    private void next() {
        destination = waypoints[index++];
        agent.SetDestination(destination.transform.position);
        if (index == 4) index = 0;
    }
}
