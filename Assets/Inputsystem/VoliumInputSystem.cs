using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Volume : MonoBehaviour
{
    public AudioSource audioSource;

    public bool mute = false;
    public float volume = 0.5f;
    public float minVolume = 0f;
    public float maxVolume = 1f;

    private void Update()
    {
        audioSource.volume = volume;
    }

    public void VoleumUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (volume < maxVolume)
            {
                volume = volume + 0.1f;
            }
            else
            {
                volume = maxVolume;
            }
        }
    }

    public void VolumeDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (volume > minVolume)
            {
                volume = volume - 0.1f;
            }
            else
            {
                volume = minVolume;
            }
        }
    }

    public void Mute(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mute = !mute;
            audioSource.mute = mute;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(700, 0, 100, 30), "Volume");
        volume = GUI.HorizontalSlider(new Rect(700, 30, 100, 30), volume, minVolume, maxVolume);
    }


}
