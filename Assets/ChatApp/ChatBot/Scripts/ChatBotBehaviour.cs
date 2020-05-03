using System;
using System.Collections;
using System.Collections.Generic;
using ChatApp.ChatHistory;
using EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ChatApp.ChatBot
{
    public class ChatBotBehaviour:MonoBehaviour
    {
        [SerializeField] private TextAsset randomChatDataFile;
        private StringList _randomChatList;

        void Awake()
        {
            _randomChatList = JsonUtility.FromJson<StringList>(randomChatDataFile.text);
        }

        void OnEnable()
        {
            EventManager.RegisterHandler(CustomEventType.OnChatSubmit,OnChatSubmitHandler);
        }
        void OnDisable()
        {
            EventManager.UnregisterHandler(CustomEventType.OnChatSubmit,OnChatSubmitHandler);
        }
        private void OnChatSubmitHandler(object obj)
        {
            if (!(obj is ChatData))
            {
                throw new ArgumentException("Invalid arguments for the event!");
            }
            var chatData= (ChatData) obj;
            if (chatData.ChatType == ChatType.Other)
            {
                return;
            }
            StartCoroutine(ReactToChat(chatData));
        }

        private IEnumerator ReactToChat(ChatData chatData)
        {
            yield return  new WaitForSeconds(Random.Range(0.5f,2.5f));
            var responseData= new ChatData(DateTime.Now,_randomChatList.Data[Random.Range(0,_randomChatList.Data.Count)],ChatType.Other);
            EventManager.Raise(CustomEventType.OnChatSubmit,responseData);
        }
    }
    [Serializable]
    public class StringList
    {
        public List<string> Data;
    }
}