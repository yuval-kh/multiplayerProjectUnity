using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuController : MonoBehaviour
{
    bool isActive;
    public GameObject tutorialMenu;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = false;
            tutorialMenu.SetActive(false);
            return;
        }
        if (!isActive && Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = true;
            tutorialMenu.SetActive(true);
            return;
        }
    }
}
