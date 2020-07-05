using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    //animations
    PlayerAnimationManager aniMan;
    controller2D playerController;

    public ParticleSystem scoreEffect;

    float health;
    bool isAlive;

    public float Health { get => health; private set => health = value; }
    public bool IsAlive { get => isAlive; private set => isAlive = value; }

    private void Start()
    {
        aniMan = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimationManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<controller2D>();
        Health = 1f;
        IsAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("player colliding with " + collision.tag + ", animan was hit: " + aniMan.WasHit);

        if (collision.CompareTag("bottle") && !collision.GetComponent<bottle>().IsBroken)//gain health
        {
            AudioManager.instance.PlaySound("CollectBottle");
            playParticleEffect();
            IncreaseHealth();
        }

        if (collision.CompareTag("bottle") && collision.GetComponent<bottle>().IsBroken)//decrease health when stepping on  a bottle
        {
            AudioManager.instance.PlaySound("IsHurt");
            DecreaseHealth();
        }
    }

    void playParticleEffect()
    {
        scoreEffect.Play();
    }

    public void IncreaseHealth(float amount = 0.05f)
    {
        AudioManager.instance.PlaySound("GetHealth");
        Health += amount;
        if (Health > 1f)
        {
            Health = 1f;
        }
        AudioManager.instance.PlaySound("Grow");
        playerController.Grow(amount);
    }

    public void DecreaseHealth(float amount = 0.12f)
    {
        AudioManager.instance.PlaySound("LoseHealth");
        Health -= amount;
        Debug.Log("health:" + Health);
        if (Health < 0f)
        {
            Health = 0f;
            IsAlive = false;
            playerController.CanMove = IsAlive;
        }
        AudioManager.instance.PlaySound("Shrink");
        playerController.Grow(-amount);
    }
}
