using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject panel;
    public TextMeshProUGUI speakerNameText;
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;

    [Header("Typing")]
    public float typingSpeed = 0.03f;

    [Header("Input (New Input System)")]
    public Key advanceKey = Key.Space;

    string[] _lines;
    int _index;
    bool _isTyping;

    void Awake()
    {
        //Hide();
    }

    void Update()
    {
        if (!panel.activeSelf) return;

        if (Keyboard.current != null && Keyboard.current[advanceKey].wasPressedThisFrame)
        {
            if (_isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = _lines[_index];
                _isTyping = false;
            }
            else
            {
                Next();
            }
        }
    }

    public void Show(DialogueData data)
    {
        _lines = data.lines;
        _index = 0;

        speakerNameText.text = data.speakerName;

        portraitImage.sprite = data.portrait;
        portraitImage.enabled = (data.portrait != null);

        panel.SetActive(true);
        StartCoroutine(TypeLine(_lines[_index]));
    }

    public void Hide()
    {
        panel.SetActive(false);
        dialogueText.text = "";
    }

    void Next()
    {
        _index++;
        if (_index >= _lines.Length)
        {
            Hide();
            return;
        }

        StartCoroutine(TypeLine(_lines[_index]));
    }

    IEnumerator TypeLine(string line)
    {
        _isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        _isTyping = false;
    }
}