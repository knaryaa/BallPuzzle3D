using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class LevelCreater : MonoBehaviour
{
    public Levels currentLevel;
    
    [Button]
    public void SaveLevel()
    {
        EditorUtility.SetDirty(currentLevel);
        
        currentLevel.obstacleLocation.Clear();
        currentLevel.diamondLocation.Clear();
        
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            
            var child = transform.GetChild(i);
            if (child.CompareTag("Obstacle"))
            {
                currentLevel.obstacleLocation.Add(child.transform.position);
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
}
