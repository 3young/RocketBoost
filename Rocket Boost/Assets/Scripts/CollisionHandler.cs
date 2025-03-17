using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("We are friend!");
                break;
            case "Finish":
                Debug.Log("Finish!");
                break;
            case "Fuel":
                Debug.Log("Charge Now!");
                break;
            default:
                ReloadLevel();
                break;

        }
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
