using UnityEngine;

public class LineControl : MonoBehaviour
{    
    internal static Vector2 endPoint;
    internal static Vector2 startPoint;
    internal static bool clickRelease;
    internal static bool startClick;

    private static LineRenderer linePlacing;

    void Start()
    {
        linePlacing = GetComponent<LineRenderer>();
        clickRelease = false;
        startClick = false;
    }

    void Update()
    {
        // If the game isn't paused and the player hasn't started a drag, set the start position for the drag.
        if ((Input.GetMouseButtonDown(0)) && (!startClick) && (!PauseMenuControl.isPaused))
        {
            linePlacing.enabled = true;
            startPoint = GetCurrentMousePosition();
            linePlacing.SetPosition(0, startPoint);
            startClick = true;
            clickRelease = false;
        }

        // If the game isn't paused and the player is holding down the mouse button, update endPoint to the current mouse position...
        // ...and set the end location of the line renderer. This way the player can see where the wall will be placed.
        if (Input.GetMouseButton(0) && (!PauseMenuControl.isPaused))
        {
            endPoint = GetCurrentMousePosition();
            linePlacing.SetPosition(1, endPoint);
        }

        // If the game isn't paused and the player releases their drag, disable the linePlacing object so the line stops rendering.
        // WallControl will then move the wall and adjust its size as necessary based on the startPoint and endPoint of the drag.
        if ((Input.GetMouseButtonUp(0)) && (startClick) && (!PauseMenuControl.isPaused))
        {
            linePlacing.enabled = false;
            startClick = false;
            clickRelease = true;
        }
    }

    // Returns a vector2 of the current mouse position on the screen
    private Vector2 GetCurrentMousePosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return pos;
    }
}