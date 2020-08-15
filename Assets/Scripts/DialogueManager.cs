using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text continueText;
    public Animator animator;
    public EnemyLevelManager enemyLevelManager;
    
    AudioSource audioSource;

    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        continueText.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
    }

    public void StartDialogue(Dialogue dialogue, GameObject source)
    {
        animator.SetBool("IsOpen", true);

        audioSource = source.GetComponent<AudioSource>();

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            enemyLevelManager.spawnNextArea = true;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        audioSource.Play();
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        audioSource.Pause();
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
