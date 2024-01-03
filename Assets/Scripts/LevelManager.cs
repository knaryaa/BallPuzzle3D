using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public Levels[] levels;
    public int levelNumber;
    public int actNumber;

    [SerializeField] TextMeshProUGUI levelTxt;
    //public int i;
    
    public void Start()
    {
        actNumber = 1;
        LoadLevel();
        SetLevelText();
    }

    private void SetLevelText()
    {
        levelTxt.text = "LEVEL     "+actNumber+" - " + levelNumber;
        
    }

    public void LoadLevel()
    {
        int x = levelNumber - 1;
        int y = 0;
        
        for (int i = 0; i < levels[x].obstacle.Length; i++)
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
}
