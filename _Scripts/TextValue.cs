using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextValue : MonoBehaviour
{
    public TextMesh text;
    public Animator anim;
    private void Awake()
    {
        text = GetComponent<TextMesh>();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public void UpdateText(int damage)
    {
       
        text.text = damage.ToString();
        gameObject.SetActive(true);
        if(BossCtrl.Instance.transform.localScale.x != this.transform.localScale.x)
        {
            this.transform.localScale = new Vector2(BossCtrl.Instance.transform.localScale.x, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(this.transform.localScale.x, 1);
        }

        StartCoroutine(Wait());
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

}
