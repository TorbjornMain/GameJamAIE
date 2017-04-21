using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour {

    public string nextLevel = "";

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerInventory>().curWeight > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
        }
    }
}
