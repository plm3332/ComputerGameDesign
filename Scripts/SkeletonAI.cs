using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkeletonAI : MonoBehaviour
{

    private float fDestroyTime = 2f;
    private float fTickTime;

    public int StartingHealth = 100;
    public int currentHealth;
    public enum CurrentState { idle, trace, attack, dead }
    public CurrentState curState = CurrentState.idle;
    public Rigidbody rigidbody;
    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;
    private AudioSource audioSource;
    public AudioClip fire_Sound;
    public float traceDist = 2000.0f;
    public float attackDist = 1.7f;
    public float speed = 2.0f;


    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
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
        rigidbody = GetComponent<Rigidbody>();
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.speed = 25.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    _animator.SetBool("Run", true);
                    _animator.SetBool("Dead", false);
                    _animator.SetBool("Attack", false);
                    break;
                case CurrentState.trace:
                    nvAgent.speed = 25.0f;
                    nvAgent.destination = playerTransform.position;
                    nvAgent.Resume();
                    _animator.SetBool("Dead", false);
                    
                    _animator.SetBool("Attack", false);
                    _animator.SetBool("Run", true);
                    break;
                case CurrentState.attack:
                    nvAgent.speed = 0.0f;
                    nvAgent.Stop();
                    
                    _animator.SetBool("Run", false);
                    _animator.SetBool("Dead", false);
                    _animator.SetBool("Attack", true);
                    //PlaySE(fire_Sound);
                    yield return new WaitForSeconds(0.2f);

                    break;

            }
            yield return null;
        }
    }

    public void OnDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Attack", false);
            _animator.SetBool("Dead", true);
            isDead = true;
            Destroy(this.gameObject,8);
        }
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }


}
