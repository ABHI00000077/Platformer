using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audiosource-----")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("-------AudioClip-------")]
    public AudioClip background;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip openbox;
    public AudioClip die;
    public AudioClip finish;
    public AudioClip time;
    private void Start()
    {
        MusicSource.clip = background;
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}   
