using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(controller2D))]
public class randomizeMovement : MonoBehaviour
{
    public bool drunk = true;

    controller2D cc;

    float waitTime;
    int effect;
    bool spedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<controller2D>();
        randomWaitTime();
        StartCoroutine(randomMovementEffect());
    }


    IEnumerator randomMovementEffect()
    {
        while (drunk)
        {
            //Debug.Log("effect done");
            randomEffect();
            yield return new WaitForSeconds(waitTime);

            randomWaitTime();
        }
    }

    void randomEffect()
    {
        effect = Random.Range(1, 4);

        switch (effect)
        {
            case 1:
                speedUp();
                break;
            case 2:
                speedDown();
                break;
            case 3:
                randomForce();
                break;
            default:
                randomForce();
                break;
        }
    }

    void randomWaitTime()
    {
        waitTime = Random.Range(5, 20);
    }

    void randomForce()
    {
        float x = Random.Range(1, 20);
        cc.Rb.AddForce(new Vector2(x, 0));
        //Debug.Log("force");
    }

    void speedUp()
    {
        if (!spedUp)
        {
            cc.IncreaseSpeed(5);
            //Debug.Log("speed up");
        }
        spedUp = true;
    }
    void speedDown()
    {
        if (spedUp)
        {
            cc.DecreaseSpeed(5);
            //Debug.Log("speed down");
        }
        spedUp = false;
    }
}
