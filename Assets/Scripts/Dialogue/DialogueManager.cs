using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Animator DialogueAnimator;
    public Image DialogueImage;
    public TMP_Text NameText;
    public TMP_Text DialogueText;
    public Queue<Dialogue> Dialogues;

    // Start is called before the first frame update
    void Start()
    {
        Dialogues = new Queue<Dialogue>();
    }

    /// <summary>
    /// Starts a dialogue.
    /// </summary>
    /// <param name="dialogues">Dialogue to start.</param>
    public void StartDialogue(Dialogue[] dialogues)
    {
        // Open dialogue box
        DialogueAnimator.SetBool("IsOpen", true);


        Dialogues.Clear();

        foreach (Dialogue dialogue in dialogues)
        {
            Dialogues.Enqueue(dialogue);
        }

        // Display first sentence
        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in queue.
    /// </summary>
    public void DisplayNextSentence()
    {
        if (Dialogues.Count == 0)
        {
            // There are no more sentences in queue, end dialogue
            EndDialogue();
            return;
        }

        // Set next sentence
        Dialogue dialogue = Dialogues.Dequeue();
        StopAllCoroutines();

        // Set dialogue name and image
        DialogueImage.sprite = dialogue.Image;
        NameText.text = dialogue.Name;
        // Set dialogue text
        StartCoroutine(TypeSentence(dialogue.Sentence));
    }

    /// <summary>
    /// Types dialogue text letter by letter.
    /// </summary>
    /// <param name="sentence">Sentence to type.</param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter; // Add letter to dialogue box
            yield return null; // Wait one frame
        }
    }

    /// <summary>
    /// Ends the current dialogue.
    /// </summary>
    private void EndDialogue()
    {
        // Close dialogue box
        DialogueAnimator.SetBool("IsOpen", false);
    }
}
