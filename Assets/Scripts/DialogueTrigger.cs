using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Transform playerPos;
    public float distanceFromPlayer = 5f;
    public Dialogue dialogue;

    Transform floatingText;
    bool dialogueStarted = false;

    void Start()
    {
        floatingText = transform.Find("FloatingText");

        if (playerPos == null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Doctor").transform;
        }
    }

    void Update()
    {
        gameObject.transform.LookAt(playerPos);

        if (Vector3.Magnitude(playerPos.position - transform.position) <= distanceFromPlayer)
        {
            floatingText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !dialogueStarted)
            {
                dialogueStarted = true;
                TriggerDialogue();
            }
            else if (Input.GetKeyDown(KeyCode.E) && dialogueStarted)
            {
                ContinueDialogue();
            }
        }
        else
        {
            floatingText.gameObject.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, gameObject);
    }

    public void ContinueDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
