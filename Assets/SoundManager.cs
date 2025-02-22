using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource source;

    public AudioClip[] repereSfx;
    public AudioClip screamerSfx;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void ScreamerSfx()
    {
        source.PlayOneShot(screamerSfx);
    }

    public void Repere()
    {
        source.PlayOneShot(repereSfx[Random.Range(0, repereSfx.Length-1)]);
    }
}
