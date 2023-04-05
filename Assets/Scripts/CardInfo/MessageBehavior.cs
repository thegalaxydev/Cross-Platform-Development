using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehavior : MonoBehaviour
{
    [SerializeField]
    private Text _message;

    /// <summary>
    /// make the message box visible
    /// </summary>
    public void ShowMessage()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// make the message box invisible
    /// </summary>
    public void HideMessage()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// set the text in the message box
    /// </summary>
    /// <param name="message">a string to set the message to</param>
    public void SetMessage(string message)
    {
        _message.text = message;
    }
}
