using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string pnjName;

    public List<DialogContent> dialogs = new List<DialogContent>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.instance.StartDialog(dialogs, pnjName);
            Destroy(gameObject);
        }
    }
}


[System.Serializable]
public class DialogContent
{
    [TextArea(5, 10)]
    public string dialog;
    public bool isEnd;
}
