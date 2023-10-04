using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCtrl : MonoBehaviour
{
    private static SceneCtrl instance;
    public PlayerMove playermove;
    public static SceneCtrl Instance { get => instance;}
    //public PlayerHealth playerHealth;
    public GameObject DeadCanvas;
    public GameObject VictoryCanvas;
    public bool buttonHit = false;
    private void Awake()
    {

        if(instance != null)
        {
            Debug.LogError("Exceed SceneManager");
        }
        instance = this;

       
    }

    private void Update()
    {
        AfterPlayerDead();
        AfterBossDead();
    }
    private void AfterPlayerDead()
    {
        if (PlayerCtrl.Instance.playerHealth.isDead())
        {
            StartCoroutine(WaitPlayerDeadAnimation());
        }
    }

    protected virtual IEnumerator WaitPlayerDeadAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerCtrl.Instance.gameObject.SetActive(false);
        DeadCanvas.SetActive(true);
    }

    public void StartGameButton()
    {
        LoadNewScene("Boss");
        
    }

    public void MenuButton()
    {
        LoadNewScene("Menu");
    }

    public void LoadNewScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }


    public void AfterBossDead()
    {
        if(BossCtrl.Instance.bossHealth.isDead())
        {
            StartCoroutine(WaitBossDeadAnimation());
        }
    }

    protected virtual IEnumerator WaitBossDeadAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        BossCtrl.Instance.gameObject.SetActive(false);
        VictoryCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    
}
