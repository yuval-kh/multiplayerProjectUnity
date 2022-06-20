using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour
{
    //public InputField nameField;
    //public InputField passwordField;

    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public TMP_InputField AcceptpasswordField;
    public TextMeshProUGUI SuccessText;
    public TextMeshProUGUI ErrorText;

    public Button submitButtom;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register() 
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User Created Successfuly.");
            menuManager.Instance.OpenMenu("login");
            SuccessText.gameObject.SetActive(true);
            ErrorText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
            ErrorText.gameObject.SetActive(true);
            SuccessText.gameObject.SetActive(false);
        }
    }

    public void VerifyInputs()
    {
        submitButtom.interactable = (nameField.text.Length >= 8 
            && passwordField.text.Length >= 8 && passwordField.text.Equals(AcceptpasswordField.text));
         
    }
}
