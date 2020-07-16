using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuteStringScript : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string stringName;
    private AudioSource stringSound;

    void Start()
    {
        stringSound = GetComponent<AudioSource>();
    }

    public int GetId()
    {
        return id;
    }

    public string GetStringName()
    {
        return stringName;
    }

    public void PlaySound()
    {
        stringSound.Play();
    }

}
