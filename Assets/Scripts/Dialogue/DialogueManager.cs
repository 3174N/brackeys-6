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
    public Queue<string> Sentences;

    // Start is called before the first frame update
    void Start()
    {
        Sentences = new Queue<string>();
    }

    /// <summary>
    /// Starts a dialogue.
    /// </summary>
    /// <param name="dialogue">Dialogue to start.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        // Open dialogue box
        DialogueAnimator.SetBool("IsOpen", true);

        // Set dialogue name and image
        DialogueImage = dialogue.Image;
        NameText.text = dialogue.Name;

        Sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            // Add sentences to queue
            Sentences.Enqueue(sentence);
        }

        // Display first sentence
        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in queue.
    /// </summary>
    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            // There are no more sentences in queue, end dialogue
            EndDialogue();
            return;
        }

        // Set next sentence
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
