using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ZombieIdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _maxTimer = 1;
    [SerializeField] private float _chaseRange = 50;

    private Transform _player;
    private float _timer;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        
        if (_timer > _maxTimer)
            animator.SetBool("isPatroling", true);

        float distance = Vector3.Distance(animator.transform.position, _player.transform.position);

        if (distance > _chaseRange)
            animator.SetBool("IsRun", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
