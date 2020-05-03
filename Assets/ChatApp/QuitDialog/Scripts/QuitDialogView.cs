using System;
using ChatApp.ChatInputPanel;
using UnityEngine;
using UnityEngine.UI;

namespace ChatApp.QuitDialog
{
    public class QuitDialogView : MonoBehaviour
    {
        [SerializeField] private Settings settings;

        private void OnEnable()
        {
            settings.YesButton.onClick.AddListener(OnYesPressedHandler);
            settings.NoButton.onClick.AddListener(OnNoPressedHandler);
        }

        private void OnNoPressedHandler()
        {
            gameObject.SetActive(false);
        }

        private void OnYesPressedHandler()
        {
            Application.Quit();
        }

        [Serializable]
        public class Settings
        {
            public Button YesButton;
            public Button NoButton;
        }
    }
}
