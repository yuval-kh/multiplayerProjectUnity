using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setOptions : MonoBehaviour
{
    public bool isMp;
    public Toggle mazeMapToggle;
    public Toggle Map1Toggle;
    public Toggle Map2Toggle;
    public Toggle SurvivalToggle;
    public Toggle FreeRoamToggle;


    public void click()
    {
        bool isSurvival;
        int mapIndex;
        if (SurvivalToggle.isOn) isSurvival = true;
        else isSurvival = false;
        if (mazeMapToggle.isOn) mapIndex = 0;
        else if (Map1Toggle.isOn) mapIndex = 1;
        else mapIndex = 2;
        LauncherGameOver.Instance.setLevelOptions(isMp, isSurvival,  mapIndex);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
