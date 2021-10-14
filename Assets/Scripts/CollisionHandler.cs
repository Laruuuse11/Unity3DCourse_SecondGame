
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
    bool collisionDisabled = false;
        

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }

        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        
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
        isTransitioning = true;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSFX);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", invokeCrashDelay);
    }

    void StartSuccessSequence()
    {
        //todo add SFX upon finish
        //todo add particle effect upon finish
       isTransitioning = true;
       myAudioSource.Stop();
       myAudioSource.PlayOneShot(successSFX);
       successParticle.Play();
       GetComponent<Movement>().enabled = false;       
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
