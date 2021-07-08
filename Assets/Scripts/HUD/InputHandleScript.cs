using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandleScript : MonoBehaviour
{

    public bool autoAim;
    public string deviceCurrent;
    public UnityEngine.EventSystems.EventSystem EventSystem;

    public void CheckAutoAim()
    {
        if(deviceCurrent == "Gamepad")
        {
            //avisar que não pode
            Debug.Log("NoUCant");
        }
        else
        {
            autoAim = !autoAim;

            if (GameObject.Find("MenuSpawnPoint"))
            {
                GameObject.Find("MenuSpawnPoint").GetComponent<MainMenu>().AutoAim(autoAim, deviceCurrent);
            }
        }
    }

    private void Awake()
    {

        if (GameObject.Find("MenuSpawnPoint"))
        {
            GameObject.Find("MenuSpawnPoint").GetComponent<MainMenu>().AutoAim(autoAim, deviceCurrent);
        }
    }

    public void ControlChanged()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log(transform.Find("InputHandle").GetComponent<PlayerInput>().currentControlScheme);
            
            deviceCurrent = transform.Find("InputHandle").GetComponent<PlayerInput>().currentControlScheme;

            if (deviceCurrent == "Gamepad")
            {
                if (EventSystem.currentSelectedGameObject == null)
                    FindObjectOfType<Button>().FindSelectableOnUp().Select();

                autoAim = true;

                if (GameObject.Find("MenuSpawnPoint"))
                {
                    GameObject.Find("MenuSpawnPoint").GetComponent<MainMenu>().AutoAim(autoAim,deviceCurrent);
                }
            }
            else
            {
                autoAim = false;

                if (GameObject.Find("MenuSpawnPoint"))
                {
                    GameObject.Find("MenuSpawnPoint").GetComponent<MainMenu>().AutoAim(autoAim, deviceCurrent);
                }

            }
        }
        else
        {
            //Instanciar Popup dizendo que um joystick foi reconhecido
            //Perguntar se o jogador ira usa-lo e assim avisar que a mira automatica sera ativada
            //Se quiser desativa-la terá q desconectar o controle e então ir no  menu
        }
    }

}
