using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public Levels[] levels;
    public int levelNumber;
    public int levelUINumber;
    public int actNumber;

    
    public GameManager gameManager;

    [SerializeField] TextMeshProUGUI levelTxt;
    //public int i;
    
    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }

    // private void Start()
    // {
    //      actNumber = 1;
    //      LoadLevel();
    //      SetLevelText();
    // }

    public void SetLevelText()
    {
        levelUINumber = levelNumber;
        if (levelNumber > 9)
        {
            levelUINumber = levelNumber % 9;
            actNumber = (levelNumber / 9)+1;
        }
        if (levelNumber % 9 == 0)
        {
            levelUINumber = 9;
            actNumber = (levelNumber / 9);
        }
        levelTxt.text = "LEVEL"+System.Environment.NewLine+actNumber+" - " + levelUINumber;
    }

    public void LoadLevel()
    {
        SetLevelText();
        gameManager.DiamondCountReset();
        
        int childs = transform.childCount;

        for (int i = childs - 1; i > -1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        int x = levelNumber - 1;
        int y = 0;
        
        for (int i = 0; i < levels[x].obstacle.Count; i++)
        {
            if (levels[x].obstacle[i])
            {
                Instantiate(levels[x].obstacle[i], levels[x].obstacleLocation[y], Quaternion.Euler(levels[x].obstacleRotation[y]), transform);
                y++;
            }
        }
        

        if (levels[x].diamond)
        {
            for (int i = 0; i < levels[x].diamondLocation.Count; i++)
            {
                Instantiate(levels[x].diamond, levels[x].diamondLocation[i], Quaternion.identity, transform);
            }
        }

        if (levels[x].start)
        {
            Instantiate(levels[x].start, levels[x].startLocation, Quaternion.identity, transform);
        }

        if (levels[x].finish)
        {
            Instantiate(levels[x].finish, levels[x].finishLocation, Quaternion.identity, transform);
        }

        if (levels[x].ball)
        {
            Instantiate(levels[x].ball, levels[x].ballLocation, Quaternion.identity, transform);
        }
        
    }

    public void DeleteLevel()
    {
        int childs = transform.childCount;

        for (int i = childs - 1; i > -1; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
