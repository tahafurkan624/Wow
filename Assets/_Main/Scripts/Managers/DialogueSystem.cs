using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public bool isDialogueOpen;

    public UnityEvent endEvent, endEvent1, endEvent2, endEvent3, endEvent4;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private TMP_Text dialogueTmp;
    [SerializeField] private GameObject panelGo;
    [SerializeField] private List<DialogueData> _dialogueDatas;

    public int chapter = 1;
    public int dialogueIndex, quoteIndex;

    private Coroutine _typeCoroutine;
    [SerializeField] private bool _isTyping;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isDialogueOpen) GetChapterText();
        }
    }

    public void StartDialogue()
    {
        isDialogueOpen = true;
    }

    [ContextMenu("SetText")]
    public void GetChapterText()
    {
        if (_isTyping == false)
        {
            _isTyping = true;
            _typeCoroutine = StartCoroutine(TypeSequence());
        }
        else
        {
            if (_typeCoroutine != null)
            {
                StopCoroutine(_typeCoroutine);
                dialogueTmp.text = _dialogueDatas[dialogueIndex].dialogueText[quoteIndex -1];
                _isTyping = false;
            }
        }
    }

    private IEnumerator TypeSequence()
    {
        if (quoteIndex >= _dialogueDatas[dialogueIndex].dialogueText.Count)
        {
            dialogueTmp.enabled = false;
            panelGo.SetActive(false);

            switch (dialogueIndex)
            { 
                case 0:
                    endEvent.Invoke();
                    break;
                case 1:
                    endEvent1.Invoke();
                    break;
                case 2:
                    endEvent2.Invoke();
                    break;
                case 3:
                    endEvent3.Invoke();
                    break;
                case 4:
                    endEvent4.Invoke();
                    break;
            }

            dialogueIndex++;
            quoteIndex = 0;
            _isTyping = false;
            isDialogueOpen = false;
            yield break;
        }

        quoteIndex++;
        if (!dialogueTmp.enabled)
        {
            dialogueTmp.enabled = true;
            panelGo.SetActive(true);
        }

        dialogueTmp.text = null;
        char[] quoteCharArray = _dialogueDatas[dialogueIndex].dialogueText[quoteIndex - 1].ToCharArray();

        for (int i = 0; i < quoteCharArray.Length; i++)
        {
            dialogueTmp.text += quoteCharArray[i].ToString();
            yield return new WaitForSeconds(.05f);
        }

        // check if its in index
        dialogueTmp.text = _dialogueDatas[dialogueIndex].dialogueText[quoteIndex -1];
        _isTyping = false;
    }
}
