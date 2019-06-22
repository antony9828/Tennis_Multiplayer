using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour
{
    [SerializeField] private float speed = 0.2f;
    private Vector2 direction;
   
   private void RespawnBall()
    {
        if (isServer)
        {
            if (transform.position.x > GameCamera.Size.x)
                CustomNetworkManager.Instance.Players[1].Points++;
            else if (transform.position.x < GameCamera.Size.x)
                CustomNetworkManager.Instance.Players[0].Points++;
        }

        transform.position = Vector3.zero;
        var x = Random.Range(0.3f, 0.7f);
        var y = Mathf.Sqrt(1 - x * x);
        direction = new Vector2(x, y);
    }

    private void Start()
    {
        RespawnBall();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed);
        if (Mathf.Abs(transform.position.x) > GameCamera.Size.x + transform.localScale.x / 2)
            RespawnBall();
        if (Mathf.Abs(transform.position.y) + transform.localScale.y / 2 > GameCamera.Size.y)
            direction = new Vector2(direction.x, -direction.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        direction = new Vector2(-direction.x, direction.y);
    }
}