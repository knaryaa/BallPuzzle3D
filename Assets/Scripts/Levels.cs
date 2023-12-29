using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Levels : SerializedScriptableObject
{
    public GameObject[] obstacle;
    public List<Vector3> obstacleLocation;
    public List<Vector3> obstacleRotation;

    public GameObject diamond;
    public List<Vector3> diamondLocation;

    public GameObject start;
    public Vector3 startLocation;
    
    public GameObject finish;
    public Vector3 finishLocation;

    public GameObject ball;
    public Vector3 ballLocation;

    
}
