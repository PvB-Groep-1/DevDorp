using UnityEngine;

/// <summary>
/// Class that changes the value of the AudioSource component on the same gameobject.
/// </summary>
public class VolumeValueChange : MonoBehaviour
{
    // Reference to Audio Source component.
    private AudioSource _audioSrc;

    // Music volume veraible that will be modified by dragging slider knob.
    private float _musicVolume = 1f;

    private void Start()
    {
        // Assign Audio Source component to control it.
        _audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame.
    private void Update()
    {
        // Setting volume option of Audio Source to be equal to musicVolume.
        _audioSrc.volume = _musicVolume;
    }

    /// <summary>
    /// Method that is called by slider game object.
    /// Sets the _musicVolume to the value of the slider.
    /// </summary>
    /// <param name="vol"> The vol value passed by the slider. </param>
    public void SetVolume(float vol)
    {
        _musicVolume = vol;
    }

    /// <summary>
    /// Method that toggles the volume between mute and un-mute.
    /// </summary>
    public void ToggleVolume()
    {
        _audioSrc.mute = !_audioSrc.mute;
    }
}
