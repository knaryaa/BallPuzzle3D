using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Levels currentLevel;

    [Button]
    public void SaveLevel()
    {
        EditorUtility.SetDirty(currentLevel);
        
        currentLevel.obstacle.Clear();
        currentLevel.obstacleLocation.Clear();
        currentLevel.obstacleRotation.Clear();
        currentLevel.diamondLocation.Clear();
        currentLevel.rotateButtonLocation.Clear();
        currentLevel.buttonLocation.Clear();
        currentLevel.rotateButton.Clear();
        currentLevel.portalLocation.Clear();
        currentLevel.portal1Location.Clear();
        currentLevel.portal2Location.Clear();
        currentLevel.portal.Clear();
        
        
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            
            var child = transform.GetChild(i);
            if (child.CompareTag("Obstacle"))
            {
                currentLevel.obstacleLocation.Add(child.transform.position);
                currentLevel.obstacleRotation.Add(child.transform.localEulerAngles);
                currentLevel.obstacle.Add(PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject));
            }
            if (child.CompareTag("Diamond"))
            {
                currentLevel.diamondLocation.Add(child.transform.position);
            }
            if (child.CompareTag("Button"))
            {
                currentLevel.rotateButtonLocation.Add(child.transform.position);
                currentLevel.buttonLocation.Add(child.transform.GetChild(0).transform.position);
                currentLevel.rotateButton.Add(PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject));
            }
            if (child.CompareTag("Portal"))
            {
                currentLevel.portalLocation.Add(child.transform.position);
                currentLevel.portal1Location.Add(child.transform.GetChild(0).transform.position);
                currentLevel.portal2Location.Add(child.transform.GetChild(1).transform.position);
                currentLevel.portal.Add(PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject));
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
        DeleteLevel();
        
        int y = 0;
        for (int i = 0; i < currentLevel.obstacle.Count; i++)
        {
            if (currentLevel.obstacle[i])
            {
                PrefabUtility.InstantiatePrefab(currentLevel.obstacle[i], transform);
                currentLevel.obstacle[i].transform.position = currentLevel.obstacleLocation[y];
                currentLevel.obstacle[i].transform.localEulerAngles = currentLevel.obstacleRotation[y];
                y++;
            }
        }
        if (currentLevel.diamond)
        {
            for (int i = 0; i < currentLevel.diamondLocation.Count; i++)
            {
                PrefabUtility.InstantiatePrefab(currentLevel.diamond, transform);
                currentLevel.diamond.transform.position = currentLevel.diamondLocation[i];
            }
        }

        y = 0;
        for (int i = 0; i < currentLevel.rotateButton.Count; i++)
        {
            if (currentLevel.rotateButton[i])
            {
                PrefabUtility.InstantiatePrefab(currentLevel.rotateButton[i], transform);
                currentLevel.rotateButton[i].transform.position = currentLevel.rotateButtonLocation[y];
                currentLevel.rotateButton[i].transform.GetChild(0).transform.position = currentLevel.buttonLocation[i];
                y++;
            }
        }

        y = 0;
        for (int i = 0; i < currentLevel.portalLocation.Count; i++)
        {
            if (currentLevel.portal[i])
            {
                PrefabUtility.InstantiatePrefab(currentLevel.portal[i], transform);
                currentLevel.portal[i].transform.position = currentLevel.portalLocation[y];
                currentLevel.portal[i].transform.GetChild(0).transform.position = currentLevel.portal1Location[i];
                currentLevel.portal[i].transform.GetChild(1).transform.position = currentLevel.portal2Location[i];
                y++;
            }
        }
    
        if (currentLevel.start)
        {
            PrefabUtility.InstantiatePrefab(currentLevel.start, transform);
            currentLevel.start.transform.position = currentLevel.startLocation;
        }

        if (currentLevel.finish)
        {
            PrefabUtility.InstantiatePrefab(currentLevel.finish, transform);
            currentLevel.finish.transform.position = currentLevel.finishLocation;
        }

        if (currentLevel.ball)
        {
            PrefabUtility.InstantiatePrefab(currentLevel.ball, transform);
            currentLevel.ball.transform.position = currentLevel.ballLocation;
        }
        
    }

    [Button]
    public void DeleteLevel()
    {
        int childs = transform.childCount;

        for (int i = childs - 1; i > -1; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    
}
