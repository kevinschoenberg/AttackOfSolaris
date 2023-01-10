using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAuto : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    [SerializeField] private TextWriter TextWriterInstance;
    [SerializeField] private string[] messageArray;
    [SerializeField] public Smooth_Transition Smooth_Trans;
    private long timer;
    public float WritingInterval = 0.08f;

    private void Awake()
    {
        timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
    private void Update()
    {
        if (TextWriterInstance.Index >= TextWriterInstance.Text.Length || msgindex == 0)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer > 1500)
            {
                messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshProUGUI>();
                
                if (msgindex == messageArray.Length)
                {
                    messageText.enabled = false;
                }
                if (TextWriterInstance != null && TextWriterInstance.Index < TextWriterInstance.Text.Length)
                {
                    TextWriterInstance.DisplayRemove();
                }/*
                else if (msgindex == 7)
                {
                    Smooth_Trans.SwapSound();
                    string message = messageArray[msgindex];
                    TextWriterInstance.AddTextor(messageText, message, 0.04f, true);
                    msgindex++;
                }*/
                else if (msgindex < messageArray.Length)
                {
                    string message = messageArray[msgindex];
                    string Playername = PlayerPrefs.GetString("playername");
                    string newMessage = message.Replace("BLANK", Playername);
                    TextWriterInstance.AddTextor(messageText, newMessage, WritingInterval, true);
                    msgindex++;
                }
                
            }
        }

        else
        {
            timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }


    }
}
