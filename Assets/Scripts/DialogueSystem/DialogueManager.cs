using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Image speakerImage;

    [SerializeField] public float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2f;

    private List<dialogueString> dialogueList;

    [Header("Player")]
    [SerializeField] public PlayerController playerController;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public GameObject playerAudio;
    [SerializeField] private Sprite playerSprite;
    private Transform playerCamera;

    private int currentDialogIndex = 0;
    private bool optionSelected = false;
    private Sprite currentNpcSprite;  // Add this field


    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }

    public void DialogueStart(List<dialogueString> textToPrint, Transform NPC, Sprite npcSprite)  // Accept NPC sprite
    {
        dialogueParent.SetActive(true);
        playerController.enabled = false;
        playerAudio.SetActive(false);
        playerAnimator.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(TurnCameraTowardsNPC(NPC));
        dialogueList = textToPrint;
        currentDialogIndex = 0;
        currentNpcSprite = npcSprite;  // Set the current NPC sprite

        DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
        option1Button.interactable = false;
        option2Button.interactable = false;

        option1Button.GetComponentInChildren<Text>().text = "---------";
        option2Button.GetComponentInChildren<Text>().text = "---------";

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
    }

    private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        Quaternion startRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }
        playerCamera.rotation = targetRotation;
    }

    private IEnumerator PrintDialogue()
    {
        while (currentDialogIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogIndex];

            line.startDialogueEvent?.Invoke();

            // Update speaker image based on who is speaking
            speakerImage.sprite = line.isPlayerSpeaking ? playerSprite : currentNpcSprite;  // Use currentNpcSprite

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));
                option1Button.interactable = true;
                option2Button.interactable = true;

                option1Button.GetComponentInChildren<Text>().text = line.AnswerOption1;
                option2Button.GetComponentInChildren<Text>().text = line.AnswerOption2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.Option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.Option2IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }

        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();
        currentDialogIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogIndex].isEnd)
        {
            DialogueStop();
        }
        currentDialogIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
        playerController.enabled = true;
        playerAnimator.enabled = true;
        playerAudio.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Dialogue ended.");
    }
}