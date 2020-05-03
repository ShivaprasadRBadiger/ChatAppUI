using System;
using System.Collections.Generic;
using ChatApp.ChatHistory;
using EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatApp.ChatInputPanel
{
  public class ChatInputBehaviour : MonoBehaviour
  {

    [SerializeField] private Settings settings;
   
    public void OnEnable()
    {
      settings.SubmitButton.onClick.AddListener(OnChatSubmit);
    }
    public void OnDisable()
    {
      settings.SubmitButton.onClick.RemoveListener(OnChatSubmit);
    }
    private void OnChatSubmit()
    {
        var chatData= new ChatData(DateTime.Now,settings.InputField.text,ChatType.Self);
        EventManager.Raise(CustomEventType.OnChatSubmit,chatData);
    }

    [Serializable]
    public class Settings
    {
      public TMP_InputField InputField;
      public Button SubmitButton;
    }
  }
}
