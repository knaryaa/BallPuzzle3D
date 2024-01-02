using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool isGameStopped = false;
    
        public GameObject pausePanel;
        public GameObject optionsPanel;
        public GameObject mainMenu;
        public GameObject startLevel;
        public GameObject GameUI;
    
    
        private void Start()
        {
            if (pausePanel)
            {
                pausePanel.SetActive(false);
            }
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PausePanelOnOff();
            }
        }
    
        public void PausePanelOnOff()
        {
            isGameStopped = !isGameStopped;
    
            if (pausePanel)
            {
                pausePanel.SetActive(isGameStopped);
    
                if (SoundManager.instance)
                {
                    Time.timeScale = (isGameStopped) ? 0 : 1;
                }
            }
        }
        public void OptionsPanelOff()
        {
            if (optionsPanel)
            {
                optionsPanel.SetActive(false);
                mainMenu.SetActive(true);
            }
        }
        public void OptionsPanelOn()
        {
            if (optionsPanel)
            {
                optionsPanel.SetActive(true);
                mainMenu.SetActive(false);
            }
        }

        public void PlayButton()
        {
            if (startLevel)
            {
                startLevel.SetActive(true);
                mainMenu.SetActive(false);
                GameUI.SetActive(true);
            }
        }
    
        public void PlayAgainFNC()
        {
            Time.timeScale = 1;
        }
}
