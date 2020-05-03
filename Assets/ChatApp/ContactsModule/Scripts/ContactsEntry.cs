using System;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class ContactsEntry : MonoBehaviour
{
    [SerializeField] private Settings settings;

    void OnEnable()
    {
        settings.contactEntryButton.onClick.AddListener(OnContactButtonPressed);
    }
    void OnDisable()
    {
        settings.contactEntryButton.onClick.RemoveListener(OnContactButtonPressed);
    }

    private void OnContactButtonPressed()
    {
        EventManager.Raise(CustomEventType.OnChatOpened);
    }

    [Serializable]
    public class Settings
    {
        public Button contactEntryButton;
    }
}
