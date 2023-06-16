using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWalkBehaviour : StateMachineBehaviour
{

    [SerializeField] private float _maxTimer = 10;
    [SerializeField] private float _chaseRange = 50;

    private Vector3 currentDestination;
    private GameObject[] destinations;
    private List<Transform> points = new List<Transform>();
    private Transform _player;
    private NavMeshAgent _agent;
    private float _timer;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        destinations = GameObject.FindGameObjectsWithTag("Points");

        _timer = 0;

        for (int i = 0; i < destinations.Length; i++)
        {
            points.Add(destinations[i].transform);
        }

        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.SetDestination(points[0].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            currentDestination = points[Random.Range(0, points.Count)].position;
            _agent.SetDestination(currentDestination);
        }
            
        _timer += Time.deltaTime;

        if (_timer > _maxTimer)
            animator.SetBool("isPatroling", false);

        float distance = Vector3.Distance(animator.transform.position, _player.position);

        if (distance < _chaseRange)
            animator.SetBool("IsRun", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }
}
