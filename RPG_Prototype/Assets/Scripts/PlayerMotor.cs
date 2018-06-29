using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;
    Transform target;   // target represents the interactable a player has right clicked on

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget (Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .9f;    // stop player from standing on top of interactable when interactable isnt moving
        agent.updateRotation = false;                    
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget ()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true; 
        target = null;

    }

    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
