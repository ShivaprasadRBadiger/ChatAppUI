using System;

namespace ChatApp.ChatHistory
{
    public enum ChatType
    {
        Self,
        Other
    }
    public class ChatData
    {
        public readonly ChatType ChatType;
        public readonly string Message;
        public readonly DateTime ReceivedTime;

        public ChatData(DateTime receivedTime, string message,ChatType chatType)
        {
            ReceivedTime = receivedTime;
            Message = message;
            ChatType = chatType;
        }
    }
}