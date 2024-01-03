using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public Levels currentLevel;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    [Button]
    public void SaveLevel()
    {
        EditorUtility.SetDirty(currentLevel);
        
        currentLevel.obstacleLocation.Clear();
        currentLevel.obstacleRotation.Clear();
        currentLevel.diamondLocation.Clear();
        
        
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            
            var child = transform.GetChild(i);
            if (child.CompareTag("Obstacle"))
            {
                currentLevel.obstacleLocation.Add(child.transform.position);
                currentLevel.obstacleRotation.Add(child.transform.localEulerAngles);

            }
            if (child.CompareTag("Diamond"))
            {
                currentLevel.diamondLocation.Add(child.transform.position);
            }
            if (child.CompareTag("Start"))
            {
                currentLevel.startLocation = child.transform.position;
            }
            if (child.CompareTag("Finish"))
            {
                currentLevel.finishLocation = child.transform.position;
            }
            if (child.CompareTag("Ball"))
            {
                currentLevel.ballLocation = child.transform.position;
            }
            
        }
        
    }
    [Button]
    public void LoadLevel()
    {
        int childs = transform.childCount;

        for (int i = childs - 1; i > -1; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        int y = 0;
        
        for (int i = 0; i < currentLevel.obstacle.Length; i++)
        {
            if (currentLevel.obstacle[i])
            {
                Instantiate(currentLevel.obstacle[i], currentLevel.obstacleLocation[y], Quaternion.Euler(currentLevel.obstacleRotation[y]), transform);
                y++;
            }
        }
        if (currentLevel.diamond)
        {
            for (int i = 0; i < currentLevel.diamondLocation.Count; i++)
            {
                Instantiate(currentLevel.diamond, currentLevel.diamondLocation[i], Quaternion.identity, transform);
            }
        }

        if (currentLevel.start)
        {
            Instantiate(currentLevel.start, currentLevel.startLocation, Quaternion.identity, transform);
        }

        if (currentLevel.finish)
        {
            Instantiate(currentLevel.finish, currentLevel.finishLocation, Quaternion.identity, transform);
        }

        if (currentLevel.ball)
        {
            Instantiate(currentLevel.ball, currentLevel.ballLocation, Quaternion.identity, transform);
        }
        
    }
    
}
