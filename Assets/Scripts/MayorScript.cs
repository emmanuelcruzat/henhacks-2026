using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // ✅ NEW

public class MayorDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float typingSpeed = 0.03f;

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        // ✅ New Input System way
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[currentLine];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in lines[currentLine])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= lines.Length)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TypeLine());
        }
    }
}