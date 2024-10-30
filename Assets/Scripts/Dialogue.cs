using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{

    public Text textComponents;
    public string[] lines;
    public float textSpeed;
    public PlayerController playerController;
    public GameObject Panel;

    private int index;

    private bool Started;
    // Start is called before the first frame update
    void Start()
    {
        textComponents.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Started)
        {
            if(textComponents.text == lines[index]) 
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponents.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        playerController.canMove = false;
        Panel.SetActive(true);
        Started = true;

    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponents.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length-1)
        {
            index++;
            textComponents.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerController.canMove =true;
            textComponents.text = "";
            Started = false;
            Panel.SetActive(false);
            enabled = false;

        }
    }
}
