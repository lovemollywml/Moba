using UnityEngine;


   public class TestProto:MonoBehaviour
    {
        void Start()
        {
            login.msgcharinfo msg = new login.msgcharinfo();
            msg.name = "Send msg";
            msg.uaid = 778899;
        }
    }

