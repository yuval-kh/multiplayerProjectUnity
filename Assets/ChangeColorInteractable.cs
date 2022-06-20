using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeColorInteractable : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button myButton;
    private Color activatedColor;
    public Color NotActivatedColor;

    // Start is called before the first frame update
    void Start()
    {
        activatedColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (myButton.IsInteractable()) text.color = activatedColor;
        else text.color = NotActivatedColor;
    }
}
