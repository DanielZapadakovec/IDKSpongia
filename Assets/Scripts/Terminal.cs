using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public InputField inputField;
    public Text outputText;
    public UnityEvent eventhap;
    public bool generatorChecked;
    public PlayerController playerController;
    public Camera mainCam;

    public AudioClip typingSound;       // Zvuk pÌsania na kl·vesnici
    public float typingSpeed = 0.05f;   // R˝chlosù pÌsania po pÌsmenk·ch

    public AudioSource audioSource;    // Zdroj zvuku
    private bool isTyping;              // Kontrola, Ëi sa aktu·lne nieËo vypisuje
    public ObjectiveManager objectiveManager;
    bool hasCompletedObjective;
    bool terminalActive;

    private void Start()
    {
        audioSource.clip = typingSound;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && terminalActive && inputField.text =="")
        {
            ActivateInput(); 
        }
        else if (Input.GetKeyDown(KeyCode.Return) && terminalActive && inputField.text !="")
        {
            ProcessCommand(inputField.text);
        }
    }

    public void StartTerminal()
    {
        terminalActive = true;
        playerController.enabled = false;
        mainCam.transform.LookAt(this.transform);
        generatorChecked = false;
        StartCoroutine(BootSequence());
        
    }
    public void ActivateInput()

    {
        inputField.Select();
        inputField.ActivateInputField();
    }
    IEnumerator BootSequence()
    {
        outputText.text = "";
        yield return TypeText("Initializing Terminal......");
        yield return new WaitForSeconds(0.5f);
        yield return TypeText("Loading system files......");
        yield return new WaitForSeconds(0.5f);
        yield return TypeText("Welcome to the terminal! Type /help for commands.\n");
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        foreach (char letter in message)
        {
            outputText.text += letter;
            if (audioSource && typingSound)
            {
                audioSource.PlayOneShot(typingSound, 0.5f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void ProcessCommand(string command)
    {
        if (isTyping || string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        switch (command.ToLower())
        {
            case "/help":
                ShowHelp();
                break;

            case "/checkgenerator":
                CheckGenerator();
                break;

            case "/end":
                End();
                break;

            case "/spongia":
                ShowSpongia();
                break;

            default:
                ShowError();
                break;
        }

        inputField.text = "";
    }

    void ShowHelp()
    {
        outputText.text = "";
        StartCoroutine(TypeText("Available commands:\n/help - Show available commands\n/checkgenerator - Check the generator status\n/spongia - Display special message\n/end - Close terminal\n"));
    }

    void CheckGenerator()
    {
        outputText.text = "";
        StartCoroutine(TypeText("Generator status: All systems functional!\n"));
        generatorChecked = true;
    }

    void ShowSpongia()
    {
        outputText.text = "";
        StartCoroutine(TypeText(
            "         ~~~~ äPONGIA 2024 ~~~~\n" +
            "       _________________________\n" +
            "      |                         |\n" +
            "      |   .    o    o    .      |\n" +
            "      |       _________         |\n" +
            "      |   o  |         |   o    |\n" +
            "      |      |  o   o  |        |\n" +
            "      |   .  |_________|   .    |\n" +
            "      |                         |\n" +
            "      | .   o    .      o    .  |\n" +
            "       ~~~~~~~~~~~~~~~~~~~~~~~~~\n"));
    }

    void ShowError()
    {
        outputText.text = "";
        StartCoroutine(TypeText("Error: Command not recognized. Please enter a valid command.\n"));
    }

    void End()
    {
        if(!hasCompletedObjective)
        {
            objectiveManager.CompleteObjective();
            hasCompletedObjective = true;
        }
        outputText.text = "";
        if (generatorChecked)
        {
            eventhap.Invoke();
            generatorChecked = false;
            playerController.enabled = true;
            terminalActive = false;
            this.enabled = false;
        }
        else
        {
            StartCoroutine(TypeText("Error: Check generator, then end terminal.\n"));
        }
        
    }
}