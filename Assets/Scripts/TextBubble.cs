﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    private TMPro.TextMeshProUGUI messageText;
    private int msgindex = 0;
    [SerializeField] private TextWriter TextWriterInstance;
    [SerializeField] private string[] messageArray;
    private float _counter = 0;
    private bool _showText = false;
    public Image ChatBubble;
    [SerializeField] public Smooth_Transition Smooth_Trans;
    public GameObject HiddenJetPack;
    public GameObject Player;
    public bool hasJetpack = false;
    public bool isIntro = false;
    public float WritingInterval = 0.04f;

    private void Start()
    {
        if (ChatBubble.name == "IntroChatFrame")
        {
            _showText = true;
            ChatBubble.enabled = true;
        }
    }
    void Update()
    {
        if (hasJetpack && _showText == false)
        {
            float Dist_player_jetpack = Vector2.Distance(HiddenJetPack.transform.position, Player.transform.position);
            if (Dist_player_jetpack < 3f && ChatBubble.name == "JetPackIntro")
            {
                _showText = true;
                ChatBubble.enabled = true;
            }
        }
 
        messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshProUGUI>();
        if (_showText)
        {
            if (msgindex == 0)
            {
                messageText.enabled = true;
                string message = messageArray[msgindex];
                string Playername = PlayerPrefs.GetString("playername");
                string newMessage = message.Replace("BLANK", Playername);
                TextWriterInstance.AddTextor(messageText, newMessage, WritingInterval, true);
                msgindex++;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (TextWriterInstance.Index < TextWriterInstance.Text.Length)
                {
                    TextWriterInstance.DisplayRemove();
                }
                else if (msgindex < messageArray.Length)
                {
                    if (msgindex == messageArray.Length - 2 && ChatBubble.name == "IntroChatFrame")
                        Smooth_Trans.SwapSound();
                    string message = messageArray[msgindex];
                    string Playername = PlayerPrefs.GetString("playername");
                    string newMessage = message.Replace("BLANK", Playername);
                    TextWriterInstance.AddTextor(messageText, newMessage, WritingInterval, true);
                    msgindex++;
                }
                else if (msgindex == messageArray.Length)
                {
                    messageText.enabled = false;
                    ChatBubble.enabled = false;
                    if (ChatBubble.name == "IntroChatFrame")
                        NextLevel(); 
                }
            }
        }
        
        _counter += Time.deltaTime;
        
        if (_counter >= 5 && !_showText)
        {
            if (isIntro)
            {
                _showText = true;
                ChatBubble.enabled = true;
            }
        }

    }
    private void NextLevel()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);
    }
}