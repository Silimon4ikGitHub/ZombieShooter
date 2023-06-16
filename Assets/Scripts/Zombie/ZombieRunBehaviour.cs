using Opsive.UltimateCharacterController.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRunBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _attackRange = 3;
    [SerializeField] private float _chaseRange = 50;
    [SerializeField] private float _runSpeed = 4;
    [SerializeField] AttributeManager zombie;

    private NavMeshAgent _agent;
    private Transform _player;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 4;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);
        float distance = Vector3.Distance(animator.transform.position, _player.position);

        if (distance < _attackRange)
        {
            animator.SetBool("IsAttack", true);
        }

        if (distance > _chaseRange)
        {
            animator.SetBool("IsRun", false);
        }

        if (zombie.Ge > 100)
        {

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
        _agent.speed = _runSpeed * 0.1f;
    }
}
