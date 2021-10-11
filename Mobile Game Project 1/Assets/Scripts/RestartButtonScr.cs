using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScr : MonoBehaviour
{
   public void restartGame()
    {
        SceneManager.LoadScene("DavidGameScene");
    }
}
