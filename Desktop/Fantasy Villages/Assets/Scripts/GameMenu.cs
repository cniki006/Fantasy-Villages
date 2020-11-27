using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    [SerializeField] Text initializeGame;
    [SerializeField] Text volumeText;
    [SerializeField] Canvas gameMenu;
    bool gameStarted = false;
    string off = "Off";
    string on = "On";
    bool b_music = true;
    AudioSource oSon;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        oSon = GameObject.Find("MusicStation").GetComponent<AudioSource>();
        RayCastCheck();
        if (gameStarted == true)
        { initializeGame.text = System.String.Format("Continue"); }

        if (b_music == true)
        {
            oSon.UnPause();
            volumeText.text = System.String.Format("{0} {1}","Volume /",on);
        }
        if (b_music == false)
        {
            oSon.Pause();
            volumeText.text = System.String.Format("{0} {1}","Volume /",off);
        }
    }

    void RayCastCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject GameManager = GameObject.Find("GameManager");
            GameManager_Script gameManager_Script = GameManager.GetComponent<GameManager_Script>();

            foreach (RaycastResult result in gameManager_Script.RaycastResults(gameMenu))
            {
                if (result.gameObject.name == "Start Game")
                {
                    Debug.Log("GameEnabled");
                    this.gameObject.SetActive(false);
                    gameStarted = true;
                }

                if (result.gameObject.name == "Quit")
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }

                if (result.gameObject.name == "Volume")
                {
                    b_music = !b_music;
                }
            }
        }
    }
}
