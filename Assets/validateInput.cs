using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class validateInput : MonoBehaviour
{
    public int maxValue;
    public int minValue;
    public TMP_InputField textBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkInput()
    {
        if (textBox.text.Equals("-")) textBox.text = "";
        if (textBox.text.Equals(""))
        {
            return;
        }
        
        int num = int.Parse(textBox.text);
        if (num > maxValue) textBox.text = maxValue.ToString();
        if (num < minValue) textBox.text = minValue.ToString();
    }
    public void increaseNum()
    {
        int num = int.Parse(textBox.text);
        num++;
        textBox.text = num.ToString();
        checkInput();

    }
    public void decreaseNum()
    {

        int num = int.Parse(textBox.text);
        num--;
        textBox.text = num.ToString();
        checkInput();

    }
    public void endEdit()
    {
        if (textBox.text.Equals(""))
        {
            textBox.text = minValue.ToString();
            return;
        }
    }

}
