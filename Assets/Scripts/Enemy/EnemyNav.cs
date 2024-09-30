using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Nav1;
    public Transform Nav2;
    public Transform Nav3;
    public Transform Nav4;
    int randomValue;
    public float changeDestinationInterval = 5f; // Time interval to change destination
    private float timeSinceLastChange;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
        timeSinceLastChange = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeDestinationInterval)
        {
            SetRandomDestination();
            timeSinceLastChange = 0f;
        }
    }

    void SetRandomDestination()
    {
        randomValue = Random.Range(1, 5);

        switch (randomValue)
        {
            case 1:
                agent.SetDestination(Nav1.position);
                break;
            case 2:
                agent.SetDestination(Nav2.position);
                break;
            case 3:
                agent.SetDestination(Nav3.position);
                break;
            case 4:
                agent.SetDestination(Nav4.position);
                break;
            default:
                break;
        }
    }
}
