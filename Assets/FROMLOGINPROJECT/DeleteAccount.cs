using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteAccount : MonoBehaviour
{

    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI SuccessText;
    public TextMeshProUGUI ErrorText;


    public Button submitButtom;

    public void CallDelete()
    {
        StartCoroutine(DeletePlayer());
    }


    IEnumerator DeletePlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/deletePlayer.php", form);
        yield return www;
        Debug.Log(www.text);
        if(www.text[0] == '0')
        {

            menuManager.Instance.OpenMenu("login");
            SuccessText.gameObject.SetActive(true);
            ErrorText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
            ErrorText.gameObject.SetActive(true);
            SuccessText.gameObject.SetActive(false);
        }
    }

    public void VerifyInputs()
    {
        submitButtom.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);

    }
}