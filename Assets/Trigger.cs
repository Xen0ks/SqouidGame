using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool end;
    public GameObject radeau;
    public Mob mob;
    public GameObject endUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (end)
            {
                StartCoroutine(End());
            }
            else
            {
                Destroy(radeau);
            }
        }
    }


    IEnumerator End()
    {
        endUI.SetActive(true);
        yield return new WaitForSeconds(8f);
        endUI.SetActive(false);
        mob.Screamer();
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
