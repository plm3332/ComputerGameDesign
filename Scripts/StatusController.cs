using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    //체력
    [SerializeField]
    private int hp;
    private int currentHp;
    private bool damaged;

    [SerializeField]
    private Image[] images_Gauge;

    private GameObject player;
    private PlayerHealth playerHealth;

    private const int HP = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = playerHealth.currentHealth;
        GaugeUpdate();
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
    }

}
