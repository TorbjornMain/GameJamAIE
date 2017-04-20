using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTest : MonoBehaviour
{
    private int val = 0;

    public GameObject tutorialPopUp;

    void Start()
    {
        PopUp();
    }

    private void PopUp()
    {
        if (val == 0)
        {
            val++;
           // tutorialPopUp.SetActive(true);
            StartCoroutine(DoPopUp());
        }
    }

    IEnumerator DoPopUp()
    {
        yield return new WaitForSeconds(5);
        tutorialPopUp.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialPopUp.SetActive(false);
    }
}
