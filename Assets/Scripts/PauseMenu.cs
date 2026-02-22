using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject pauseMenuCanvas;

    void Start()
    {
        Time.timeScale = 1f; //makes sure that time is running at the start of the game

    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //if the player presses the escape key, then the game will pause or unpause depending on the current state
        {
            Cursor.visible = true; //makes the cursor visible
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void Pause()
    {
        AudioSource found = GameObject.Find("Player").GetComponentInChildren<AudioSource>();
        Debug.Log("Found audio source: " + found.gameObject.name);
        found.Stop();
        pauseMenuCanvas.SetActive(true); //activates the pause menu canvas
        Time.timeScale = 0f; //pauses the game by setting time scale to 0
        Cursor.lockState = CursorLockMode.None; //unlocks the cursor so the player can click on the pause menu buttons
        Cursor.visible = true; //makes the cursor visible
        Paused = true;
        

    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false); //deactivates the pause menu canvas
        Time.timeScale = 1f; //resumes the game by setting time scale back to 1
        Cursor.lockState = CursorLockMode.Locked; // locks cursor again
        Cursor.visible = false;
        Paused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
