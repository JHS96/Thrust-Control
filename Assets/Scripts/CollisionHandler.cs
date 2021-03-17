using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float delayTime = 2f;
  [SerializeField] AudioClip death;
  [SerializeField] AudioClip success;
  [SerializeField] ParticleSystem deathParticles;
  [SerializeField] ParticleSystem successParticles;

  Movement movementScript;
  AudioSource audioSource;

  bool isTransitioning = false;

  void Start()
  {
    movementScript = GetComponent<Movement>();
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning) return;

    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Collided with Friendly Object.");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  void StartSuccessSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success, 0.35f);
    successParticles.Play();
    movementScript.enabled = false;
    Invoke("LoadNextLevel", delayTime);
  }

  void StartCrashSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(death, 0.35f);
    deathParticles.Play();
    movementScript.enabled = false;
    Invoke("ReloadLevel", delayTime);
  }

  void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneindex = currentSceneIndex + 1;
    if (nextSceneindex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneindex = 0;
    }
    SceneManager.LoadScene(nextSceneindex);
  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
}
