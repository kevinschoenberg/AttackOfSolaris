using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextWriter : MonoBehaviour
{
    private TMPro.TextMeshProUGUI message;
    public string Text;
    public int Index;
    private float Interval;
    private float timer;
    private bool invisibleCharacters;

    public void AddTextor(TMPro.TextMeshProUGUI message, string Text, float Interval, bool invisibleCharacters)
    {
        this.message = message;
        this.Text = Text;
        this.Interval = Interval;
        Index = 0;
        this.invisibleCharacters = invisibleCharacters;
    }

    public void DeleteAddTextor()
    {
        if (!(Index < Text.Length))
        {
            Text = ""; 
            Index = 0;
        }
    }

    private void Update()
    {
        if (message != null && Text != "")
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += Interval;
                Index++;
                string Temptext = Text.Substring(0, Index);
                if (invisibleCharacters)
                {
                    Temptext += "<color=#00000000>" + Text.Substring(Index) + "</color>";
                }
                message.text = Temptext;
                if (Index >= Text.Length)
                {
                    message = null;
                    return;
                }
            }
        }
    }

    public void DisplayRemove()
    {
        message.text = Text;
        Index = Text.Length;
        DeleteAddTextor();
    }
}
