﻿using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    [SerializeField] private TextWriter TextWriterInstance;
    [SerializeField] private string[] messageArray;
    private float _counter = 0;
    private bool _showText = false;
    public Image ChatBubble;

    void Update()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshProUGUI>();
        if (_showText)
        {
            if (msgindex == 0)
            {
                messageText.enabled = true;
                string message = messageArray[msgindex];
                TextWriterInstance.AddTextor(messageText, message, 0.04f, true);
                msgindex++;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (TextWriterInstance != null && TextWriterInstance.Index < TextWriterInstance.Text.Length)
                {
                    TextWriterInstance.DisplayRemove();
                }
                else if (msgindex < messageArray.Length)
                {
                    string message = messageArray[msgindex];
                    TextWriterInstance.AddTextor(messageText, message, 0.04f, true);
                    msgindex++;
                }
                else if (msgindex == messageArray.Length)
                {
                    messageText.enabled = false;
                    ChatBubble.enabled = false;
                }
            }
        }
        //counter is increasing by deltaTime till to reach the trigger time
        _counter += Time.deltaTime;
        //after or equals 5 sec. show a simple GUIText
        if (_counter >= 5 && !_showText)
        {
            _showText = true;
            ChatBubble.enabled = true;
        }

    }
}