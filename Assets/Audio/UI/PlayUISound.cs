using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISound : MonoBehaviour
{
    [SerializeField] private AudioSource uiSource;
    public void playButtonPress()
    {
        uiSource.Play();
    }
}
