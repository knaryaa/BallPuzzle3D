using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        public GameObject levelComplete;
        public GameObject levelSelectMenu;
        
        //public RectTransform[] actMenu;
        
        private LevelManager levelManager;
        //public GameManager gameManager;

        public Button[] buttons;
        public GameObject[] levelButtons;

        private void Awake()
        {
            ButtonsToArray();
            UnlockLevel();
        }

        private void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            //gameManager = FindObjectOfType<GameManager>();
            if (pausePanel)
            {
                pausePanel.SetActive(false);
            }
        }
    
        private void FixedUpdate()
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
                //startLevel.SetActive(true);
                //GameUI.SetActive(true);
                mainMenu.SetActive(false);
                levelSelectMenu.SetActive(true);
            }
        }
    
        public void PlayAgain()
        {
            Time.timeScale = 1;
            levelManager.LoadLevel();
            pausePanel.SetActive(false);
            levelComplete.SetActive(false);
        }

        public void HomeButton()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            GameUI.SetActive(false);
            levelComplete.SetActive(false);
            mainMenu.SetActive(true);
            
            levelManager.DeleteLevel();
        }

        public void LevelFinish()
        {
            if (levelComplete)
            {
                levelComplete.SetActive(true);
            }
        }

        public void NextLevelButton()
        {
            levelComplete.SetActive(false);
            levelManager.levelNumber++;
            levelManager.LoadLevel();
        }
        
        private void UnlockLevel()
        {
            int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            unlockedlevel = Mathf.Clamp(unlockedlevel, 0, buttons.Length);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
                buttons[i].transform.GetChild(0).localScale = new Vector3(0,0,0);
                buttons[i].transform.GetChild(1).localScale = new Vector3(1,1,1);
            }
            for (int i = 0; i < unlockedlevel; i++)
            {
                buttons[i].interactable = true;
                buttons[i].transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                buttons[i].transform.GetChild(1).localScale = new Vector3(0, 0, 0);
            }
        }
        public void LevelSelect(int level)
        {
            levelManager.levelNumber = level;
            levelManager.LoadLevel();
            GameUI.SetActive(true);
        }

        void ButtonsToArray()
        {
            buttons = new Button[levelButtons.Length*levelButtons[0].transform.childCount];
            
            int y =0;
            for (int i = 0; i < levelButtons.Length; i++)
            {
                for (int x = 0; x < levelButtons[i].transform.childCount; x++)
                {
                    buttons[y] = levelButtons[i].transform.GetChild(x).gameObject.GetComponent<Button>();
                    y++;
                }
            }
        }
}
