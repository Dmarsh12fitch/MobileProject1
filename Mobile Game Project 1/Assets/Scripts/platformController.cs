using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    public bool canCrumble;
    public bool isCrumbling;

    public void startCrumbling(float timeLeft)
    {
        isCrumbling = true;
        Destroy(gameObject, timeLeft);
    }

}
