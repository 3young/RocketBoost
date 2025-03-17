using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        isControllable = true;
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }

        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!isControllable || !isCollidable) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("We are friend!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    private void StartCrashSequence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
