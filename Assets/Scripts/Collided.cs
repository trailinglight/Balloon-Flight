using UnityEngine;
using UnityEngine.SceneManagement;

public class Collided : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag); 
        
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                Debug.Log("Crashed");
                ReloadLevel();
                break;
            case "Friendly":
                Debug.Log("Safe");
                break;
            case "Finish":
                Debug.Log("Complete");
                LoadNextLevel();
                break;
            default:
                Debug.Log("What is this?");
                break;

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("You have beat the game.");
            //Depending on if you want to reload the game from the start when complete
            //nextSceneIndex = 0;
            //SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
}
