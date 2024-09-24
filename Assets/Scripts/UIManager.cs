using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
        public bool isGameStopped = false;
    
        public GameObject pausePanel;
        public GameObject optionsPanel;
        public GameObject mainMenu;
        public GameObject startLevel;
        public GameObject gameUI;
        public GameObject levelComplete;
        public GameObject levelSelectMenu;
        public GameObject screenFade;

        public float nextLevelTimer = 0;
        
        //public RectTransform[] actMenu;
        
        private LevelManager levelManager;
        public GameManager gameManager;

        public Button[] buttons;
        public GameObject[] levelButtons;

        public TextMeshProUGUI praiseTxt;
        public TextMeshProUGUI totalDiamondTxt;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            levelManager = FindObjectOfType<LevelManager>();
            ButtonsToArray();
            UnlockLevel();
        }

        private void Start()
        {
            if (pausePanel)
            {
                pausePanel.SetActive(false);
            }
            MenuPanelAnimation(mainMenu);
        }

        private void Update()
        {
            if (nextLevelTimer <= 3f && (levelComplete.gameObject.activeSelf || pausePanel.gameObject.activeSelf))
            {
                nextLevelTimer += Time.fixedDeltaTime;
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
                //PanelAnimation(pausePanel);
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
                nextLevelTimer = 0f;
            }
        }
        public void OptionsPanelOn()
        {
            if (optionsPanel)
            {
                mainMenu.SetActive(false);
                PanelAnimation(optionsPanel);
            }
        }

        public void PlayButton()
        {
            if (startLevel)
            {
                mainMenu.SetActive(false);
                PanelAnimation(levelSelectMenu);
            }
        }
    
        public void PlayAgain()
        {
            if (nextLevelTimer >= 3f)
            {
                Time.timeScale = 1;
                levelManager.LoadLevel();
                pausePanel.SetActive(false);
                levelComplete.SetActive(false);
                
                nextLevelTimer = 0f;
            }
        }

        public void HomeButton()
        {
            if (nextLevelTimer >= 3f)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                gameUI.SetActive(false);
                levelComplete.SetActive(false);
                
                //mainMenu.SetActive(true);
                
                
                levelManager.DeleteLevel();
                SceneManager.LoadScene(0);
                nextLevelTimer = 0f;
            }
            
        }

        public void CloseButton()
        {
            levelSelectMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void LevelFinish()
        {
            if (levelComplete)
            {
                PanelAnimation(levelComplete);

                totalDiamondTxt.text = "You collected "+gameManager.diamondCount+" diamonds!";
                
                if (gameManager.diamondCount == 3)
                {
                    praiseTxt.text = "Perfect!";
                }
                else if (gameManager.diamondCount == 2)
                {
                    praiseTxt.text = "Well Done!";
                }
                else if (gameManager.diamondCount == 1)
                {
                    praiseTxt.text = "Great!";
                }
                else if (gameManager.diamondCount == 0)
                {
                    praiseTxt.text = "Good!";
                    totalDiamondTxt.text = "But you didn't collect any diamonds!";
                }
            }
        }

        public void NextLevelButton()
        {
            if (nextLevelTimer >= 3f)
            {
                levelComplete.SetActive(false);
                levelManager.levelNumber++;
                levelManager.LoadLevel();
                nextLevelTimer = 0f;
            }
            
        }
        
        private void UnlockLevel()
        {
            int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            unlockedlevel = Mathf.Clamp(unlockedlevel, 0, buttons.Length);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
                buttons[i].transform.GetChild(0).localScale = new Vector3(0,0,0);
                buttons[i].transform.GetChild(1).localScale = new Vector3(0.6f, 0.6f, 0.6f);
                for (int y = 0; y < 3; y++)
                {
                    buttons[i].transform.GetChild(y+2).localScale = new Vector3(0, 0, 0);
                }
            }
            for (int i = 0; i < unlockedlevel; i++)
            {
                buttons[i].interactable = true;
                buttons[i].transform.GetChild(0).localScale = new Vector3(0.9f,0.9f,0.9f);
                buttons[i].transform.GetChild(1).localScale = new Vector3(0, 0, 0);
                int diamonds = PlayerPrefs.GetInt("Diamonds"+(i+1).ToString(), 0);
                for (int y = 0; y < diamonds; y++)
                {
                    buttons[i].transform.GetChild(y+2).localScale = new Vector3(1, 1, 1);
                }
            }
        }

        private void ShowCollectedDiamonds()
        {
            int collectedDiamond = PlayerPrefs.GetInt("CollectedDiamonds", 0);
            for (int i = 0; i < 3; i++)
            {
                buttons[i].transform.GetChild(i).localScale = new Vector3(1, 1, 1);
            }
        }
        public void LevelSelect(int level)
        {
            levelManager.levelNumber = level;
            levelManager.LoadLevel();
            //gameUI.SetActive(true);
            levelSelectMenu.SetActive(false);
            
            screenFade.SetActive(true);
            PanelAnimation(gameUI);
            
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

        public void PanelAnimation(GameObject panel)
        {
            var childCount = transform.childCount;
            Transform child = panel.transform.GetChild(0);
            child.transform.localScale = new Vector3(0, 0, 0);
            panel.SetActive(true);
            child.transform.DOScale(new Vector3(1,1,1), 0.5f);
        }

        public void MenuPanelAnimation(GameObject panel)
        {
            var childCount = transform.childCount;
            Transform child1 = panel.transform.GetChild(0);
            Transform child2 = panel.transform.GetChild(1);
            child1.transform.localScale = new Vector3(0, 0, 0);
            child2.transform.localScale = new Vector3(0, 0, 0);
            panel.SetActive(true);
            child1.transform.DOScale(new Vector3(1,1,1), 0.5f);
            child2.transform.DOScale(new Vector3(1,1,1), 0.5f);
        }
        
        
}
