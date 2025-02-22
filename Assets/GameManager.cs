using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform screamerCamera;

    public void Awake()
    {
        instance = this;
    }


    public IEnumerator Screamer()
    {
        screamerCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
