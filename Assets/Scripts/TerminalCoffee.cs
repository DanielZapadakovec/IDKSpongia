using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TerminalCoffee : MonoBehaviour
{
    public InputField inputField;
    public Text outputText;
    public UnityEvent eventhap;
    public bool generatorChecked;

    public AudioClip typingSound;       // Zvuk p�sania na kl�vesnici
    public float typingSpeed = 0.05f;   // R�chlos� p�sania po p�smenk�ch
    public AudioClip doneSound;
    public AudioClip preparingSound;

    private AudioSource audioSource;
    private bool isTyping;              // Kontrola, �i sa aktu�lne nie�o vypisuje
    public ObjectiveManager objectiveManager;
    bool orderedCoffee;
    bool completedObjective;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = typingSound;
        completedObjective = true;
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
        isTyping = true;  // Nastavenie pr�znaku, �e sa pr�ve p�e
        outputText.text = ""; // Vymazanie predch�dzaj�ceho textu

        foreach (char letter in message)
        {
            outputText.text += letter;
            if (audioSource && typingSound)
            {
                audioSource.PlayOneShot(typingSound, 0.5f);  // Zvuk pre ka�d� p�smenko
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;  // P�sanie skon�ilo
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

            case "/end":
                End();
                break;

            case "/spongia":
                ShowSpongia();
                break;

            case "/ordercoffee":
                OrderCoffee();
                break;

            default:
                ShowError();
                break;
        }

        inputField.text = "";
    }

    void ShowHelp()
    {
        if (!isTyping)
        {
            StartCoroutine(TypeText("Available commands:\n/help - Show available commands\n/ordercoffee - Just order a coffee\n/spongia - Display special message\n/end - Close terminal\n"));
        }
    }

    void OrderCoffee()
    {
        if (!isTyping)
        {
            StartCoroutine(TypeText("Ordering Coffee.....\nPreparing......\nDone, enjoy your coffee :)"));
            orderedCoffee = true;
        }
    }

    void ShowSpongia()
    {
        if (!isTyping)
        {
            StartCoroutine(TypeText(
                "         ~~~~ �PONGIA 2024 ~~~~\n" +
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
    }

    void ShowError()
    {
        if (!isTyping)
        {
            StartCoroutine(TypeText("Error: Command not recognized. Please enter a valid command.\n"));
        }
    }

    void End()
    {
        if (completedObjective)
        {
            objectiveManager.CompleteObjective();
            completedObjective = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        eventhap.Invoke();
        orderedCoffee = false;
        outputText.text = "";
    }
}