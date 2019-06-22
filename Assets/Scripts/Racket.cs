using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Racket : NetworkBehaviour
{
    private GameCamera gameCamera;
    [SyncVar] private int playerNumber;

    [SerializeField] private float speed = 0.1f;
    [SyncVar(hook = "OnPointsChanged")] private int points;
    public int Points
    { get { return points; }
        set
        {
            if(value > 0 && points != value)
            {
                points = value;
            }
        }
    }


    private void OnPointsChanged(int points)
    {
        gameCamera.UpdateUI(playerNumber, points);
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < GameCamera.Size.y - (transform.localScale.y / 2))
            transform.Translate(0, speed, 0);
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -GameCamera.Size.y + (transform.localScale.y / 2))
            transform.Translate(0, -speed, 0);

    }
     
    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        base.OnStartLocalPlayer();

        if (isServer)
            playerNumber = 1;
    }

    private void Awake()
    {
        gameCamera = Camera.main.GetComponent<GameCamera>();
        points = 0;
    }
}
