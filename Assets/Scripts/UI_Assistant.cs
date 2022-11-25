using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_Assistant : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    private TextWriter.TextWriterSingle textWriterSingle;
    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshProUGUI>();
        transform.Find("message").GetComponent<Button_UI>().ClickFunc = () =>
        {
            string[] messageArray = new string[]
            {
            "The news struck like lightning.",
            "Aliens are attacking.",
            "The solar system has already been invaded, only earth remains.",
            "A drastic measure must be taken.",
            "So on a dark and stormy night. Every human on earth participated in a lottery.",
            "It turns out YOU are the winner.",
            "You have been chosen as the savior of humankind.",
            "All you have to do is eradicate the invading aliens in our solar system!",
            "So savior, what is your name?",
            "Well then no time like the present, first stop the moon!",
            };
        
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                textWriterSingle.WriteAllAndDestroy();
            }
            else if (msgindex == 8)
            {
                // Allow User to name Character.
                string message = messageArray[msgindex];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
                msgindex++;
            }
            else if (msgindex < messageArray.Length)
            {
                string message = messageArray[msgindex];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
                msgindex++;
            }
        };
    }
}
