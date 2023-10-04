using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBehaviour : MonoBehaviour
{

    public BoxCollider2D box;
    public Transform player;
    public Transform boss;
    public Transform entrance;
    public GameObject bossBar;
    public GameObject DeadCanvas;
    public GameObject VictoryCanvas;
    private void Awake()
    {
        box = GameObject.Find("Entrance").GetComponent<BoxCollider2D>();
        box.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        
    }

    private void Start()
    {
        boss.transform.gameObject.SetActive(false);
        bossBar.SetActive(false);
        DeadCanvas.SetActive(false);
        VictoryCanvas.SetActive(false);
    }


    private void Update()
    {
        CheckCondition();
    }


    private void StartGame()
    {
        boss.transform.gameObject.SetActive(true);
        box.enabled = true;
        bossBar.SetActive(true);
        if (bossBar == null) return;
    }

    private void CheckCondition()
    {
        if(player.position.x > entrance.position.x)
        {
            StartCoroutine(Ready());
        }
    }

    protected virtual IEnumerator Ready()
    {
        if (player.position.x > entrance.position.x)
        {
            yield return new WaitForSeconds(1f);
            StartGame();
        }
    }
}
