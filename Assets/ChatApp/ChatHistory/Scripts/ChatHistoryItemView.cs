using TMPro;
using UnityEngine;

namespace ChatApp.ChatHistory
{
    public class ChatHistoryItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI chatMessage;
        [SerializeField] private TextMeshProUGUI chatReceivedTime;
        
        public void Initialize(ChatData data)
        {
            chatMessage.text = data.Message;
            chatReceivedTime.text = data.ReceivedTime.ToShortTimeString();
        }
    }
}
