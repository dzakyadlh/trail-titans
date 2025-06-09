using UnityEngine;
using UnityEngine.SceneManagement;

public class Chara : MonoBehaviour
{

    [SerializeField] AudioClip crashSFX;
    bool hasCrashed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && !hasCrashed)
        {
            hasCrashed = true; // Prevent multiple triggers
            Debug.Log("You crashed!");
            GetComponent<AudioSource>().PlayOneShot(crashSFX); // Play the crash sound
            FindAnyObjectByType<Player>().DisableControls();
            Invoke("ReloadScene", 2f); // Reload the scene after 1 second
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
