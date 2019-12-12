using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigAI : MonoBehaviour
{
    public int StartingHealth = 100;
    public int currentHealth;
    public enum CurrentState { idle, trace, attack, dead }
    public CurrentState curState = CurrentState.idle;
    public Rigidbody rigidbody;
    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 500.0f;
    public float attackDist = 30.0f;
    public float speed = 20.0f;


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
                    nvAgent.speed = 50.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    break;
                case CurrentState.trace:
                    nvAgent.speed = 50.0f;
                    nvAgent.acceleration = 100.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    _animator.SetBool("Run", true);
                    break;
                case CurrentState.attack:
                    nvAgent.speed = 70.0f;
                    nvAgent.acceleration = 70.0f;
                    _animator.SetBool("Attack", true);

                    break;

            }
            yield return null;
        }
    }
}
