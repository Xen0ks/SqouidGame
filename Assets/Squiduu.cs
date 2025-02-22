using UnityEngine;

public class Squiduu : MonoBehaviour
{
    public Transform newSquiduu;
    public Transform oldSquiduu;

    public void Free()
    {
        newSquiduu.gameObject.SetActive(true);
        oldSquiduu.gameObject.SetActive(false);
    }
}
