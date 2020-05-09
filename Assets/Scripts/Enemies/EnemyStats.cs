using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public float baseSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float baseHealth = 100f;
    private float health;
    private bool dead;
    public int reward = 50;

    public GameObject deathEffect;

    [Header("UI Reference")]
    public Image healthBar;

    private void Start()
    {
        speed = baseSpeed;
        health = baseHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / baseHealth;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public void Slow(float percent)
    {
        speed = baseSpeed * (1 - percent);
    }

    private void Die()
    {
        dead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        WaveSpawner.UpdateWaveStatus();

        Destroy(gameObject);
    }
}
