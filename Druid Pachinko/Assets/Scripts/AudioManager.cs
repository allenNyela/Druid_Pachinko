using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<AudioManager>();
                    Debug.Log("Generating new game manager");
                }
            }
            return _instance;
        }
    }

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource fxSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip musicLoop;
    [SerializeField] AudioClip buttonFx;
    [SerializeField] AudioClip plantFx;
    [SerializeField] AudioClip defeatFx;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicLoop;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void MusicPause()
    {
        musicSource.Pause();
    }

    public void MusicUnpause()
    {
        musicSource.UnPause();
    }

    public void FXButtonClick() 
    {
        fxSource.PlayOneShot(buttonFx);
    }

    public void FXPlant()
    {
        fxSource.PlayOneShot(plantFx);
    }

    public void FXDefeat()
    {
        fxSource.PlayOneShot(defeatFx);
        musicSource.Stop();
    }
}
