using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour {
    public float totalTime;
    float currentTime;
    bool active = false;
    public string gameOverScene;
    public Text timerText;
	// Use this for initialization
	void Start () {
        currentTime = totalTime;
        timerText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(active)
        {

            currentTime -= Time.deltaTime;
            timerText.text = "TIME REMAINING: " + Mathf.RoundToInt(currentTime);
            if(currentTime <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
                active = false;
            }
        }
	}

    void Activate()
    {
        active = true;
        timerText.enabled = true;
    }
}
