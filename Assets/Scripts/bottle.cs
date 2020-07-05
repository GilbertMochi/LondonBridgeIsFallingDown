using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bottle : MonoBehaviour
{
    LevelManager levelMan;
    Rigidbody2D rb;

    bool quirky = false;
    bool isBroken = false;

    int bottleValue = 1;

    Animator bottleAnim;

    public bool IsBroken { get => isBroken; private set => isBroken = value; }
    public int BottleValue { get => bottleValue; private set => bottleValue = value; }

    private void Start()
    {
        bottleAnim = GetComponent<Animator>();
        maybeThisBottleIQuirky();
        rb = GetComponent<Rigidbody2D>();
        levelMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (quirky)
        {
            rb.AddForce(new Vector2(Random.Range(0, 10), Random.Range(0, 10)));
            rb.AddTorque(Random.Range(0, 10));
        }
        IsBroken = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground") || other.CompareTag("bottle"))
        {
            bottleBroke();
            IsBroken = true;
        }
        if (other.CompareTag("Player") && !IsBroken)
        {
            givePoints();
        }
    }

    void bottleBroke()
    {
        levelMan.addBrokenBottle(1);
        bottleBreakAnimation();
    }

    void givePoints()
    {
        levelMan.addToScore(BottleValue);
        Destroy(gameObject);
    }

    void maybeThisBottleIQuirky()
    {
        int n = Random.Range(1, 3);

        if (n == 1)
        {
            quirky = true;
        }
        else if (n == 2)
        {
            quirky = false;
        }
        else
        {
            quirky = false;
        }
    }

    void bottleBreakAnimation()
    {
        AudioManager.instance.PlaySound("BottleBreak");
        bottleAnim.SetTrigger("break");
    }

    public void destroyThisBottle()
    {
        Destroy(gameObject);
    }
}

