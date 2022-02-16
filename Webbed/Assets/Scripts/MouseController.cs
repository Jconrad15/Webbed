using UnityEngine;

public class MouseController : MonoBehaviour
{
    private readonly float minOrthogrpahicSize = 2f;
    private readonly float maxOrthographicSize = 10f;

    private Vector3 lastFramePosition;
    private Vector3 currFramePosition;

    // Update is called once per frame
    void Update()
    {
        SetCurrentFramePosition();
        UpdateCameraMovement();
        SetLastFramePosition();
    }

    /// <summary>
    /// Pans and zooms the camera.
    /// </summary>
    private void UpdateCameraMovement()
    {
        // Screen dragging Pan with left mouse button
        if (Input.GetMouseButton(0))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        // Camera Zoom
        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,
                                                    minOrthogrpahicSize,
                                                    maxOrthographicSize);
    }

    /// <summary>
    /// Private. Set the current frame position.
    /// </summary>
    private void SetCurrentFramePosition()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;
    }

    /// <summary>
    /// Private. Set the last frame's position.
    /// </summary>
    private void SetLastFramePosition()
    {
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }
}
