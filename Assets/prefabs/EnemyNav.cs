using UnityEngine;
//using UnityEngine.Collections;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform Nav1;
    public Transform Nav2;
    public Transform Nav3;
    public Transform Nav4;
    int RandomValue;


    // Use EnemyNav for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RandomValue = Random.Range(1, 5);
        Debug.Log(RandomValue);
        if (RandomValue == 1)
        {
            
            //Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            Ray Position1 = new Ray(Nav1.position, Input.mousePosition);
            RaycastHit Player, hit;
            if (Physics.Raycast(Position1, 1000))
            {
                agent.SetDestination(EnemyNav.point);
            }
        }
        if (RandomValue == 2)
        {
            Ray Position2 = new Ray(Nav2.position, Input.mousePosition);
            RaycastHit EnemyNav2;
            if (Physics.Raycast(Position2, out EnemyNav2, 1000))
            {
                agent.SetDestination(EnemyNav2.point);
            }
        }
    }
}