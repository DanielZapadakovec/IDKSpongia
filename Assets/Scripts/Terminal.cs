using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public InputField inputField; // Referencia na InputField
    public Text outputText;       // Referencia na Text pre výstup
    public UnityEvent eventhap;
    public bool generatorChecked;

    void Start()
    {
        generatorChecked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputField.onEndEdit.AddListener(ProcessCommand); // Po zadaní príkazu
        outputText.text = "Welcome to the terminal! Type /help for commands.";
    }

    void ProcessCommand(string command)
    {
        // Ignoruj prázdny vstup
        if (string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        switch (command.ToLower()) // Príkaz zmeníme na malé písmená
        {
            case "/help":
                ShowHelp();
                break;

            case "/checkgenerator":
                CheckGenerator(); ;
                break;
            case "/end":
                End();
                break;

            default:
                ShowError();
                break;
        }

        inputField.text = "";
    }

    void ShowHelp()
    {
        outputText.text = "Available commands:\n/help - Show available commands\n/checkgenerator - Check the generator status\n/end - Close terminal";
    }

    void CheckGenerator()
    {
        outputText.text = "Generator status: All systems functional!";
        generatorChecked = true;
    }

    void ShowError()
    {
        outputText.text = "Error: Command not recognized. Please enter a valid command.";
    }

    void End()
    {
        if(generatorChecked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            eventhap.Invoke();
            generatorChecked = false;

        }
        else
        { outputText.text = "Error: Check generator, then end terminal"; }
    }
}