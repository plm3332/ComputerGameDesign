using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{

    [SerializeField]
    private fire ff;

    public enum CurrentState { idle, trace, attack, dead }
    public CurrentState curState = CurrentState.idle;
    public Rigidbody rigidbody;
    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    public Animator _animator;

    public float traceDist = 30.0f;
    public float attackDist = 1.7f;
    public float speed = 2.0f;

    private bool isDead = false;

  
    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

    }

    // Update is called once per frame
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;

            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        rigidbody = GetComponent<Rigidbody>();
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.speed = 30.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    _animator.SetBool("Claw Attack", false);
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    nvAgent.speed = 30.0f;
                    _animator.SetBool("Run", true);
                    _animator.SetBool("Claw Attack", false);
                    break;
                case CurrentState.attack:
                    nvAgent.speed = 0.0f;
                    _animator.SetBool("Claw Attack", true);
                    ff.muzzleFlash.Play();
                    _animator.SetBool("AttackFin", true);
                    break;

            }
            yield return null;
        }
    }
}
