using System;
using System.Collections.Generic;
using EventSystem;
using JetBrains.Annotations;
using Pooling;
using UnityEngine;

namespace ChatApp.ChatHistory
{
    public class ChatHistoryView : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        [SerializeField] private Transform contentTransform;
        private List<ChatHistoryView> _chatHistoryViews;
        
        private MonoPool<SelfChatHistoryItemView> _selfChatHistoryItemPool;
        private MonoPool<OtherChatHistoryItemView> _otherChatHistoryItemPool;


        private void Awake()
        {
            _selfChatHistoryItemPool = new MonoPool<SelfChatHistoryItemView>(_settings.CommonPoolSettings,_settings.SelfChatItemViewPrefab,contentTransform);
            _otherChatHistoryItemPool = new MonoPool<OtherChatHistoryItemView>(_settings.CommonPoolSettings,_settings.OtherChatHistoryItemView,contentTransform);
        }

        void OnEnable()
        {
            EventManager.RegisterHandler(CustomEventType.OnChatSubmit,OnChatSubmitted);
        }
        private void OnDisable()
        {
            EventManager.UnregisterHandler(CustomEventType.OnChatSubmit,OnChatSubmitted);
            _selfChatHistoryItemPool.ReturnAllObjectsToPool();
            _otherChatHistoryItemPool.ReturnAllObjectsToPool();
        }

        private void OnChatSubmitted(object obj)
        {
            if (!(obj is ChatData))
            {
                throw new ArgumentException("Invalid argument passed while raising the event!");
            }
            var chatData = (ChatData) obj;
            ResolveChat(chatData);
        }

        private void InitializeHistory(List<ChatData> chatItems)
        {
            foreach (var chatItem in chatItems)
            {
                ResolveChat(chatItem);
            }
        }
        
        private void ResolveChat([NotNull] ChatData newChat)
        {
            if (newChat == null) throw new ArgumentNullException(nameof(newChat));

            switch (newChat.ChatType)
            {
                case ChatType.Self:
                    AddSelfChat(newChat);
                    break;
                case ChatType.Other:
                    AddOtherChat(newChat);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddOtherChat(ChatData newChat)
        {
            var chatItemView= _otherChatHistoryItemPool.Spawn();
            chatItemView.transform.SetAsLastSibling();
            chatItemView.Initialize(newChat);
        }

        private void AddSelfChat(ChatData newChat)
        {
            var chatItemView= _selfChatHistoryItemPool.Spawn();
            chatItemView.transform.SetAsLastSibling();
            chatItemView.Initialize(newChat);
        }

     

        [Serializable]
        public class Settings
        {
            public MonoPoolSettings CommonPoolSettings;
            public SelfChatHistoryItemView SelfChatItemViewPrefab;
            public OtherChatHistoryItemView OtherChatHistoryItemView;
        }
    }
}
