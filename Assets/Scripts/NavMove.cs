using UnityEngine;
using UnityEngine.AI;
public class NavMove : MonoBehaviour
{
    public Transform[] points;
    private int currentPoint = 0;

    private NavMeshAgent myNavAgent;

    void Awake()
    {
        myNavAgent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(myNavAgent.destination == null)
        {
            SetNewDestination(points[currentPoint]);
        }
        else if(myNavAgent.remainingDistance < 1.0f)
        {
            currentPoint++;
            SetNewDestination(points[currentPoint]);
        }
        else 
        { 
            return;
        }
    }
    void SetNewDestination(Transform nextPoint)
    {
        myNavAgent.SetDestination(nextPoint.position);
        myNavAgent.CalculatePath(nextPoint.position, myNavAgent.path);
        myNavAgent.SetPath(myNavAgent.path);
        Debug.Log("Set new destination " + nextPoint);
    }
}
