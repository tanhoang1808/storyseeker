using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerRedFlask : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI text;


    private void Awake()
    {
        Image = GetComponent<Image>();


    }

  

    public void UpdateFlask(float currentHealth, float maxHealth)
    {
        Image.fillAmount = currentHealth / maxHealth;
        text.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
