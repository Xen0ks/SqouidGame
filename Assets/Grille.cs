using UnityEngine;

public class Grille : MonoBehaviour
{
    public Transform newPos;
    public Transform grille;

    Vector3 velocity;

    public bool trigger;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && trigger)
        {
            grille.position = Vector3.SmoothDamp(grille.position, newPos.position, ref velocity, 1f);
            grille.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
