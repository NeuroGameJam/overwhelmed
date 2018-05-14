using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour {

    private AudioSource _audio;
    private Material _onBlue;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        _audio.Play();
    }
}
