using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseView, firstSelectedButton;

    private static bool isGamePaused = false, first = false;

    public float t = 1f, initialTime, finalTime, speedTime;

    [Space] [SerializeField] private PlayerControl inputControl;

    void Awake()
    {

        inputControl = new PlayerControl();

        inputControl.Control_Global.Pause.performed += ctx => OnPause();
        inputControl.Control_Global.Back.performed += ctx => Resume();


    }

    private void Start()
    {
        pauseView.GetComponent<Canvas>().worldCamera = Camera.main;

    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    public void OnPause()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (t >= 1)
            {
                if (!isGamePaused)
                {
                    t = 0f;

                    //isGamePaused = true;
                    initialTime = 1f;
                    finalTime = 0f;

                }
                else
                {
                    t = 0f;

                    //isGamePaused = false;
                    initialTime = 0f;
                    finalTime = 1f;

                    pauseView.SetActive(false);
                }


                isGamePaused = !isGamePaused;
            }

        }
    }

    private void Update()
    {
        if (t < 1)
        {

           Time.timeScale = Mathf.MoveTowards(initialTime, finalTime, t);
           Time.fixedDeltaTime = Time.timeScale * 0.02f;


            t += speedTime * Time.unscaledDeltaTime;
        }
        else if(isGamePaused)
        {
            pauseView.SetActive(true);
            Time.timeScale = 0f;

            if (!first)
            {
                firstSelectedButton.GetComponent<Button>().Select();
                first = true;
            }
        }
        else
        {
            Time.timeScale = 1f;

            first = false;
        }
    }

    public void Resume()
    {
        if (pauseView.activeSelf)
        {
            isGamePaused = false;
            pauseView.SetActive(false);

            t = 0f;
            initialTime = 0f;
            finalTime = 1f;
        }

    }

    public void MainMenu()
    {
        isGamePaused = false;
        pauseView.SetActive(false);

        t = 0f;
        initialTime = 0f;
        finalTime = 1f;

        Destroy(GameObject.FindWithTag("GameManager"));

        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
                         Application.Quit();
#endif
    }


}
