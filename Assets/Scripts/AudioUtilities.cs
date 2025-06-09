using System.Collections;
using UnityEngine;

public class AudioUtilities : MonoBehaviour
{

    public IEnumerator FadeOutAudio(AudioSource audioSource, float fadeDuration = 0.5f)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset for next play
    }
}
