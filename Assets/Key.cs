using UnityEngine;

public class Key : MonoBehaviour
{
    public Squiduu squiduu;

    Vector3 velocity;

    public bool trigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            squiduu.Free();
            Destroy(gameObject);
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
