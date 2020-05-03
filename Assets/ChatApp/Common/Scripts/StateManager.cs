using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    [SerializeField] private Settings settings;
    private GameObject _currentState;
    void Awake()
    {
        _currentState = settings.states[0];
        EventManager.RegisterHandler(CustomEventType.OnChatOpened,ChatStateOpenHandler);
    }

    private void ChatStateOpenHandler(object obj)
    {
        SwitchState(1);
    }

    private void SwitchState(int stateID)
    {
        _currentState.SetActive(false);
        _currentState = settings.states[stateID];
        _currentState.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState == settings.states[0])
            {
                settings.quitState.SetActive(true);
            }
            else
            {
                SwitchState(0);
            }
        }
    }

    [Serializable]
    public class Settings
    {
        public List<GameObject> states;
        public GameObject quitState;
    }
}
