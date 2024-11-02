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

    public AudioClip typingSound;       // Zvuk pÌsania na kl·vesnici
    public float typingSpeed = 0.05f;   // R˝chlosù pÌsania po pÌsmenk·ch

    private AudioSource audioSource;    // Zdroj zvuku
    private bool isTyping;              // Kontrola, Ëi sa aktu·lne nieËo vypisuje

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = typingSound;
    }

    public void StartTerminal()
    {
        generatorChecked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputField.onEndEdit.AddListener(ProcessCommand);
        StartCoroutine(BootSequence());
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
                audioSource.PlayOneShot(typingSound, 0.5f);  // Zvuk pre kaûdÈ pÌsmenko
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
        outputText.text = "";
        if (generatorChecked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            eventhap.Invoke();
            generatorChecked = false;
        }
        else
        {
            StartCoroutine(TypeText("Error: Check generator, then end terminal.\n"));
        }
    }
}