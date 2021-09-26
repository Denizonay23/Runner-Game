using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next : MonoBehaviour
{
   

    public void Next()
    {

        Application.LoadLevel("SampleScene");

    }

    public void QuitGame()
    {

        Application.Quit();

    }
    
}
