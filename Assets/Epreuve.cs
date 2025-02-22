using UnityEngine;

public class Epreuve : MonoBehaviour
{
    public Transform radeau;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(radeau.gameObject);
        }
    }
}
