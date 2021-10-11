using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject rockPlatPrefab;
    public GameObject unstableRockPlatPrefab;
    public GameObject backgroundPrefab;
    public GameObject starPointPrefab;

    public Transform thePlayer;
    private float nextSpawningTrigger = 0;
    private float previousPlatX = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(thePlayer.position.y >= nextSpawningTrigger)
        {
            nextSpawningTrigger += 20;
            makeNewArea();
        }
        GameObject[] allPlat = GameObject.FindGameObjectsWithTag("platform");
        //do the same for points!!!
        foreach(GameObject plat in allPlat)
        {
            if(plat.transform.position.y < transform.position.y - 25)
            {
                Destroy(plat);
            }
        }
        GameObject[] allPoint = GameObject.FindGameObjectsWithTag("StarPoint");
        foreach (GameObject star in allPoint)
        {
            if (star.transform.position.y < transform.position.y - 25)
            {
                Destroy(star);
            }
        }
    }

    void makeNewArea()
    {
        makeNewBackground();
        int rand;

        for(int i = -8; i <= 8; i += 4)
        {
            if(previousPlatX == 0)
            {
                rand = Random.Range(0, 4);
                if (rand == 0)
                {
                    makeAPlatform(i, 0);
                    previousPlatX = 0;
                    maybeMakeAStarPoint(i, 0);
                }
                else if (rand == 1)
                {
                    makeAPlatform(i, 2.1f);
                    previousPlatX = 2.1f;
                    maybeMakeAStarPoint(i, 2.1f);

                }
                else if(rand == 2)
                {
                    makeAPlatform(i, -2.1f);
                    previousPlatX = -2.1f;
                    maybeMakeAStarPoint(i, -2.1f);
                } else
                {
                    makeAPlatform(i, -2.1f);
                    makeAPlatform(i, 2.1f);
                    rand = Random.Range(0, 2);
                    if(rand == 0)
                    {
                        previousPlatX = -2.1f;
                        maybeMakeAStarPoint(i, 2.1f);
                    } else
                    {
                        previousPlatX = 2.1f;
                        maybeMakeAStarPoint(i, -2.1f);
                    }
                }
            } else if(previousPlatX == -2.1f)
            {
                rand = Random.Range(0, 2);
                if(rand == 0)
                {
                    makeAPlatform(i, -2.1f);
                    previousPlatX = -2.1f;
                    maybeMakeAStarPoint(i, -2.1f);
                } else
                {
                    makeAPlatform(i, 0);
                    previousPlatX = 0;
                    maybeMakeAStarPoint(i, 0);
                }
            } else
            {
                //previousPlatX must = 2.1f then
                rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    makeAPlatform(i, 2.1f);
                    previousPlatX = 2.1f;
                    maybeMakeAStarPoint(i, 2.1f);
                }
                else
                {
                    makeAPlatform(i, 0);
                    previousPlatX = 0;
                    maybeMakeAStarPoint(i, 0);
                }
            }
        }
       

    }

    void makeNewBackground()
    {
        Vector3 spawnPos = new Vector3(backgroundPrefab.transform.position.x, backgroundPrefab.transform.position.y + nextSpawningTrigger, backgroundPrefab.transform.position.z);
        Quaternion spawnRot = new Quaternion(180, 0, 0, -180);
        Instantiate(backgroundPrefab, spawnPos, spawnRot);
    }

    void makeAPlatform(float posY, float posX)
    {
        Vector3 spawnPos = new Vector3(unstableRockPlatPrefab.transform.position.x + posX, unstableRockPlatPrefab.transform.position.y + nextSpawningTrigger + posY, unstableRockPlatPrefab.transform.position.z);
        Quaternion spawnRot = new Quaternion(0, 0, 0, 0);
        Instantiate(unstableRockPlatPrefab, spawnPos, spawnRot);

    }

    void maybeMakeAStarPoint(float posY, float posX)
    {
        float rand = Random.Range(0, 20);
        if (rand > 18)
        {
            Vector3 spawnPos = new Vector3(unstableRockPlatPrefab.transform.position.x + posX, unstableRockPlatPrefab.transform.position.y + nextSpawningTrigger + posY + 1.5f, unstableRockPlatPrefab.transform.position.z);
            Quaternion spawnRot = new Quaternion(0, 0, 0, 0);
            Instantiate(starPointPrefab, spawnPos, spawnRot);
        }
    }

}
