using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float delayTime = 2f;
  Movement movementScript;

  void Start()
  {
    movementScript = GetComponent<Movement>();
  }

  void OnCollisionEnter(Collision other)
  {
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
    // TODO add sfx on success
    // TODO add particle effect on success
    movementScript.enabled = false;
    Invoke("LoadNextLevel", delayTime);
  }

  void StartCrashSequence()
  {
    // TODO add sfx on crash
    // TODO add particle effect on crash
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
