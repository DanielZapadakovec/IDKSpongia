using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NPCTransform;
    [SerializeField] private Sprite npcSprite;  // Add this field for NPC sprite
    private bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpoken)
        {
            var dialogueManager = other.gameObject.GetComponent<DialogueManager>();
            dialogueManager.DialogueStart(dialogueStrings, NPCTransform, npcSprite);  // Pass the NPC sprite
            hasSpoken = true;
        }
    }
}

[System.Serializable]
public class dialogueString
{
    public string text;
    public bool isEnd;

    [Header("Branch")]
    public bool isQuestion;
    public string AnswerOption1;
    public string AnswerOption2;
    public int Option1IndexJump;
    public int Option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;

    [Header("Speaker")]
    public bool isPlayerSpeaking;  // Add this field
}
