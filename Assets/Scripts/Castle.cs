using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public float health =100.0f;
    public TextMeshProUGUI healthText;


    void GetDamaged(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            Die();
        }
        print("Damaged, current health: "+health);
    }
    void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }
    void StartGame()
    {
        health = 100.0f;
    }
    void Update()
    {
        healthText.text = "Health: "+health;
    }
}
