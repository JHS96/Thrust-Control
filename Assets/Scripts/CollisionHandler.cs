using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  void OnCollisionEnter(Collision other)
  {
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Collided with Friendly Object.");
        break;
      case "Fuel":
        Debug.Log("Collected Fuel");
        break;
      case "Finish":
        LoadNextLevel();
        break;
      default:
        ReloadLevel();
        break;
    }
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
