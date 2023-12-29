using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public Levels[] levels;
    public int levelNumber;
    
    public void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        int x = levelNumber - 1;

        if (levels[x].obstacle)
        {
            for (int i = 0; i < levels[x].obstacleLocation.Count; i++)
            {
                Instantiate(levels[x].obstacle, levels[x].obstacleLocation[i], Quaternion.identity, transform);
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
