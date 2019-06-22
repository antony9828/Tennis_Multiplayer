using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private List<Text> pointsText;

    private static Vector2 size;
    public static Vector2 Size {get {return size; } }

    public void UpdateUI(int racketNumber, int points)
    {
        pointsText[racketNumber].text = points.ToString();
    }

    private void Start()
    {
        var cam = Camera.main;
        size = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
    }
}
