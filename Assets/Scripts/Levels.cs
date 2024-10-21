using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Levels : SerializedScriptableObject
{
    public List<GameObject> obstacle;
    public List<Vector3> obstacleLocation;
    public List<Vector3> obstacleRotation;

    public List<GameObject> rotateButton;
    public List<Vector3> buttonLocation;
    public List<Vector3> rotateButtonLocation;

    public List<GameObject> portal;
    public List<Vector3> portalLocation;
    public List<Vector3> portal1Location;
    public List<Vector3> portal2Location;
    
    public GameObject diamond;
    public List<Vector3> diamondLocation;

    public GameObject start;
    public Vector3 startLocation;
    
    public GameObject finish;
    public Vector3 finishLocation;

    public GameObject ball;
    public Vector3 ballLocation;

    
}
