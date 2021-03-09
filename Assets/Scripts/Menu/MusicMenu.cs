using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMenu : MonoBehaviour
{
    public AudioSource BeSound;
    public AudioClip Sound;
    public AudioClip Und;

    public void HoverSound ()
    { 
            BeSound.PlayOneShot(Sound);
     }
    public void ClickSound()
    {
        BeSound.PlayOneShot(Und);
    }

}
