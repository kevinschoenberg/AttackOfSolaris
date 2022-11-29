using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAuto : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    [SerializeField] private TextWriter TextWriterInstance;
    [SerializeField] private string[] messageArray;
    private long timer;
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
                    SceneManager.LoadScene(2);
                }
                else if (TextWriterInstance != null && TextWriterInstance.Index < TextWriterInstance.Text.Length)
                {
                    //timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    TextWriterInstance.DisplayRemove();
                }
                else if (msgindex == 8)
                {
                    //timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    // Allow User to name Character.
                    string message = messageArray[msgindex];
                    TextWriterInstance.AddTextor(messageText, message, 0.04f, true);
                    msgindex++;
                }
                else if (msgindex < messageArray.Length)
                {
                    //timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    string message = messageArray[msgindex];
                    TextWriterInstance.AddTextor(messageText, message, 0.04f, true);
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
