using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPointSystem : MonoBehaviour
{
    public GameObject partSystem;
    public GameObject pointDisplay;
    public bool hasBeenCalled;


    public void destroyMe()
    {
        if (!hasBeenCalled)
        {
            hasBeenCalled = true;
            GameObject.Find("Player").GetComponent<PlayerController>().updateScoreDisplay(5f);
            partSystem.gameObject.SetActive(true);
            pointDisplay.gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }

}
