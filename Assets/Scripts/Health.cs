using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake = false;
    [SerializeField] Slider healthSlider;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    int maxHealth;

    public int GetHealth() => health;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        maxHealth = health;
        healthSlider.gameObject.SetActive(false);
        healthSlider.value = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null)
        {
            return;
        }

        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        ShakeCamera();
        audioPlayer.PlayDamageClip();
        damageDealer.Hit();

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = health / (float)maxHealth;
            healthSlider.gameObject.SetActive(true);
        }

        if (health > 0)
        {
            audioPlayer.PlayDamageClip();
        }
        else
        {
            audioPlayer.PlayDestroyClip();

            Die();
        }
    }

    private void Die()
    {
        if (isPlayer)
        {
            levelManager.LoadGameOverMenu();
        }
        else
        {
            scoreKeeper.ModifyScore(score);
        }

        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    private void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }
    }
}
