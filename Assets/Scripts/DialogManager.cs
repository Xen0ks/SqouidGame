using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public List<GameObject> uisToDisable = new List<GameObject>();
    public List<GameObject> uisToReenable = new List<GameObject>();

    public MovementBehaviour playerMovement;
    public MouseLook mouseLook;

    public GameObject dialogBox;
    public Text dialogText;
    public Text pnjNameText;
    public Text nextButtonText;

    private List<DialogContent> sentences = new List<DialogContent>();
    private int actualDiplayedSentence = 0;

    public static DialogManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialog(List<DialogContent> dialogSentences, string pnjName)
    {
        pnjNameText.text = pnjName;

        sentences.Clear();
        foreach (var dialogContent in dialogSentences)
        {
            sentences.Add(dialogContent);
        }

        actualDiplayedSentence = 0;


        dialogBox.SetActive(true);
        //Désactiver les UI a désactiver
        foreach (var ui in uisToDisable)
        {
            ui.SetActive(false);
        }


        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        playerMovement.enabled = false;
        mouseLook.enabled = false;

        DisplayDialog();
    }
    public void DisplayNextDialog()
    {
        if (!sentences[actualDiplayedSentence].isEnd)
        {
            actualDiplayedSentence++;
            DisplayDialog();
            return;
        }
        EndDialog();
    }

    public void DisplayDialog()
    {
        dialogText.text = sentences[actualDiplayedSentence].dialog;
        nextButtonText.transform.GetComponent<Button>().enabled = true;
        nextButtonText.text = "Suite";
        if (sentences[actualDiplayedSentence].isEnd)
        {
            nextButtonText.text = "Fin";
        }
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        //Désactiver les UI a désactiver
        foreach (var ui in uisToReenable)
        {
            ui.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.Locked;

        actualDiplayedSentence = 0;

        Time.timeScale = 1;

        playerMovement.enabled = true;
        mouseLook.enabled = true;
    }
}
