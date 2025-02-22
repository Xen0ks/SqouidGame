using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool trigger;
    public Transform tpPoint;
    public Transform player;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && trigger)
        {
            player.GetComponent<MovementBehaviour>().enabled = false;
            player.position = tpPoint.position;
            player.GetComponent<MovementBehaviour>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            player = other.transform;
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
