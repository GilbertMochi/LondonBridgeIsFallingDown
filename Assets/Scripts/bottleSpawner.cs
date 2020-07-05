using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleSpawner : MonoBehaviour

{
    public GameObject bottle;

    LevelManager lm;
    float interval=5;

    // Start is called before the first frame update
    void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        StartCoroutine(spawnBottle());
    }

    IEnumerator spawnBottle()
    {
        while (!lm.gameOver)
        {
            yield return new WaitForSeconds(interval);
            instantiateNewBottle();

            //get a random new interval as last thing
            interval = Random.Range(1, 5);

        }
    }

    void instantiateNewBottle()
    {
        //-8 and 8 are just taken visually from editor they're kind of the edges on the left and right side of the play area
        float x = Random.Range(-8, 8);
        Instantiate(bottle, new Vector3(x, 5, 0), Quaternion.identity, transform);
    }
}
