using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject background;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public float textSpeed;

    //public Animator anim;
    public Image actorImage;

    Sentence[] currentSentences;
    Actor[] currentActors;

    public static bool isActive = false;
    int activeSentence = 0;

    void Start()
    {
        dialogueText.text = string.Empty;
        //currentIdleSentences = CheckpointRespawn.currentTriggerObj.GetComponent<DialogueTrigger>().idleSentences;
    }

    private void Update()
    {
        //Triggering next sentence
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dialogueText.text == currentSentences[activeSentence].sentence)
                {
                    NextSentence();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = currentSentences[activeSentence].sentence;
                }

            }
        }
    }

    public void OpenDialogue(Sentence[] sentences, Actor[] actors)
    {
        //anim.SetBool("IsOpen", true);
        isActive = true;
        background.SetActive(true);
        activeSentence = 0;
        Time.timeScale = 0;

        currentSentences = sentences;
        currentActors = actors;

        DisplaySentence();
    }

    void DisplaySentence()
    {
        Sentence sentenceToDisplay = currentSentences[activeSentence];
        StartCoroutine(TypeSentence(sentenceToDisplay.sentence));

        Actor actorToDisplay = currentActors[sentenceToDisplay.actorId];
        nameText.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextSentence()
    {
        activeSentence++;
        if (activeSentence < currentSentences.Length)
        {
            //dialogueText.text = string.Empty;
            DisplaySentence();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentenceToDisplay)
    {
        dialogueText.text = "";
        foreach (char letter in sentenceToDisplay.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        //anim.SetBool("IsOpen", false);

        background.SetActive(false);
        Time.timeScale = 1;

        isActive = false;
    }

    public void SkipDialogue()
    {
        EndDialogue();
    }
}
