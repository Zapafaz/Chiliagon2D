using UnityEngine;
using System.Collections;

public class WallControl : MonoBehaviour
{
    private Transform playerWall;
    private Vector3 wallSize;
    private Vector2 wallAngle;
    private float wallLength;

    void Start()
    {       
        playerWall = GetComponent<Transform>();
        playerWall.position = new Vector3(1000, 0, 0);
    }

    void Update()
    {
        // If the game isn't paused and the player has released their last drag, create a wall based on that drag
        if ((LineControl.clickRelease) && (!PauseMenuControl.isPaused))
        {
            wallLength = Vector3.Distance(LineControl.endPoint, LineControl.startPoint);
            wallSize = new Vector3(x: wallLength, y: 1, z: 0);
            wallAngle = LineControl.endPoint - LineControl.startPoint;

            playerWall.position = Vector2.Lerp(LineControl.startPoint, LineControl.endPoint, 0.5f);
            playerWall.localScale = wallSize;
            playerWall.rotation = Quaternion.FromToRotation(Vector3.right, wallAngle);
        }
    }
}