using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    //animator
    private Animator anim;

    public float Speed { get; set; }
   // public bool IsInAir { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //animator
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //update animations
        anim.SetFloat("speed", Speed);
        //anim.SetBool("isJumping", IsInAir);
        
    }

    public void WasHit()
    {
        AudioManager.instance.PlaySound("UkScream");
        anim.SetTrigger("hit");
    }

}
