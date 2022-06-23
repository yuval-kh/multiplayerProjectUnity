using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOneOptionToggle : MonoBehaviour
{

    public Toggle[] toggles;
    // Start is called before the first frame update
    void Start()
    {
      //  turnAllOff();
      //  toggles[0].isOn = true;
    }

    public void toggleClick(Toggle t) 
    {
        if (!t.isOn)
        {
            t.isOn = true;
            return;
        }
        turnAllOff();
        t.isOn = true;
    }

    private void turnAllOff() {
        foreach (var t in toggles)
        {
            t.isOn = false;
        }
    }
    private bool isAllOff()
    {
        foreach (var t in toggles)
        {
            if (t.isOn)
                return false;
        }
        return true;
    }
    public void  turnOnIndex(int index)
    {
        //   turnAllOff();
        if (isAllOff())
            toggles[index].isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
