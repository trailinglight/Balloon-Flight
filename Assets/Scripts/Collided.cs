using UnityEngine;
using UnityEngine.SceneManagement;

public class Collided : MonoBehaviour
{

    private AudioSource playerAudioSource;

    [SerializeField] private float secondsToWait = 1.5f;

    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip successSound;

    private bool isTransitioning = false;


    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!isTransitioning)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
               Debug.Log("Crashed");
               LoadingManager(nameof(ReloadLevel), crashSound);
               break;
            case "Friendly":
               Debug.Log("Safe");
               break;
            case "Finish":
               Debug.Log("Complete");
               LoadingManager(nameof(LoadNextLevel), successSound);
               break;
            default:
               Debug.Log("What is this?");
               break;

        }
  
    }

    void LoadingManager(string loadFunction, AudioClip oneShotSound)
    {

        GetComponent<Movement>().enabled = false;
        playerAudioSource.Stop();
        playerAudioSource.PlayOneShot(oneShotSound);
        isTransitioning = true; //because we are reloading the level, the state will be reset automatically
        Invoke(loadFunction, secondsToWait);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
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
