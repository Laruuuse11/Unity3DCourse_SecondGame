
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeCrashDelay = 2f;
    [SerializeField] float invokeFinishDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource myAudioSource;
    

    bool isTransitioning = false;
    

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        
            switch (other.gameObject.tag)
            {
                case "Finish":
                    StartSuccessSequence();
                    break;

                case "Friendly":
                    Debug.Log("it's friendly");
                    break;

                default:
                    StartCrashSequence();
                    break;
            }
        
    }

    void StartCrashSequence()
    {
        //todo add SFX upon crash
        //todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSFX);
        isTransitioning = true;
        Invoke("ReloadLevel", invokeCrashDelay);
        
    }

    void StartSuccessSequence()
    {
        //todo add SFX upon finish
        //todo add particle effect upon finish
       myAudioSource.Stop();
       myAudioSource.PlayOneShot(successSFX);
       successParticle.Play();
       GetComponent<Movement>().enabled = false;
       isTransitioning = true;
       Invoke("LoadNextScene", invokeFinishDelay);
            
        
    }

    private void LoadNextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);        
    }

    private void ReloadLevel()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
