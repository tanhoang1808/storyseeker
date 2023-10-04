using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossRedFlask : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI text;

    private void Awake()
    {
        Image = GetComponent<Image>();
        
        
    }

    private void Update()
    {
      
    }

    public void UpdateFlask(float currentHealth,float maxHealth)
    {
        Image.fillAmount = currentHealth / maxHealth;
        text.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
