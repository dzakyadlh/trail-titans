using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;
    void Start()
    {
        Debug.Log("Finish script started!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Finish line crossed!");
        finishEffect.Play();
        GetComponent<AudioSource>().Play(); // Play the finish sound
        Invoke("ReloadScene", 1f); // Reload the scene after 1 second
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
