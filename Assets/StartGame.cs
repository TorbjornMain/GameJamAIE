using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public string levelToGoTo;

    public void Clicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToGoTo);
    }
}
