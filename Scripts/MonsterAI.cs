using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour {

    private int attackDamage = 1;
    private GameObject player;
    private PlayerHealth playerHealth;
    public int StartingHealth = 60;
    public int currentHealth;

    public enum CurrentState { idle, trace, attack, dead}
    public CurrentState curState = CurrentState.idle;
    public Rigidbody rigidbody;
    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 2000.0f;
    public float attackDist = 5.0f;
    public float speed = 1.0f;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHealth = StartingHealth;
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
        //rigidbody = GetComponent<Rigidbody>();
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.speed = 15.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    break;
                case CurrentState.trace:
                    nvAgent.speed = 15.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    break;
                case CurrentState.attack:
                    nvAgent.speed = 5.0f;
                    _animator.SetBool("Attack", true);
                    break;

            }
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void OnDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            attackDamage = 0;
            Destroy(this.gameObject, 8);
            nvAgent.Stop();
            _animator.SetBool("Dead", true);
            isDead = true;
            
        }
    }
}
