using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;
    private List<TextWriterSingle> textWriterSingeList;

    private void Awake()
    {
        instance = this;
        textWriterSingeList = new List<TextWriterSingle>();
    }
    public static TextWriterSingle AddWriter_Static(TMPro.TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
    }
    public TextWriterSingle AddWriter(TMPro.TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters);
        textWriterSingeList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TMPro.TextMeshProUGUI uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TMPro.TextMeshProUGUI uiText)
    {
        for (int i = 0; i < textWriterSingeList.Count; i++)
        {
            if (textWriterSingeList[i].GetUIText() == uiText)
            {
                textWriterSingeList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < textWriterSingeList.Count; i++)
        {
            bool destroyInstance = textWriterSingeList[i].Update();
            if (destroyInstance)
            {
                textWriterSingeList.RemoveAt(i);
                i--;
            }
        }
    }
    public class TextWriterSingle
    {
        private TMPro.TextMeshProUGUI uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public TextWriterSingle(TMPro.TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters) 
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            characterIndex = 0;
            this.invisibleCharacters = invisibleCharacters;
        }
        public bool Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color =#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }

                uiText.text = textToWrite.Substring(0, characterIndex);
                if (characterIndex >= textToWrite.Length)
                {
                    // If the entire message has been written
                    uiText = null;
                    return true;
                }
            }
            return false;
        }
        public TMPro.TextMeshProUGUI GetUIText()
        {
            return uiText;
        }
        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }
        public void WriteAllAndDestroy()
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            TextWriter.RemoveWriter_Static(uiText);
        }
    }
}