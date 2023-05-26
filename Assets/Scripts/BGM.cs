using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    static AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void StartBGM()
    {
        audioSource.Play();
    }
    public static void StopBGM()
    {
        audioSource.Stop();
    }
}