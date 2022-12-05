using UnityEngine;
using CodeMonkey.Utils;

public class TextButton : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    [SerializeField] private TextWriter TextWriterInstance;
    [SerializeField] private string[] messageArray;
    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshProUGUI>();
        transform.Find("message").GetComponent<Button_UI>().ClickFunc = () =>
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
        };
    }
}
