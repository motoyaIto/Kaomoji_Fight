using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour 
{

    //---------------------------
    // Exposed Fields
    //---------------------------

    /// <summary>
    /// Max inclination angle in degress.
    /// </summary>
    public float MaxInclinationAngle = 45;

    /// <summary>
    /// Angular speed used to rotate.
    /// </summary>
    public float AngularSpeed = 20;

    /// <summary>
    /// Camera distance from board.
    /// </summary>
    public float Distance = 15;

    //---------------------------
    // Internal Fields
    //---------------------------


    //---------------------------
    // Methods
    //---------------------------

    #region --- Unity Methods ---

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void FixedUpdate()
    {
        ProcessInput();
    }

    #endregion


    private void ProcessInput()
    {
        float dts = AngularSpeed * Time.deltaTime;
        Vector2 pos = GetNormalizedMousePos();
        Vector3 euler = GetClampedEuler();

        bool upperBoundsX = euler.x >= MaxInclinationAngle;
        bool upperBoundsZ = euler.z >= MaxInclinationAngle;

        bool lowerBoundsX = euler.x <= -MaxInclinationAngle;
        bool lowerBoundsZ = euler.z <= -MaxInclinationAngle;

        float clampX = !(upperBoundsX || lowerBoundsX) || (upperBoundsX && pos.y > 0) || (lowerBoundsX && pos.y < 0) ? 1 : 0;
        float clampZ = !(upperBoundsZ || lowerBoundsZ) || (upperBoundsZ && pos.x < 0) || (lowerBoundsZ && pos.x > 0) ? 1 : 0;

    
        Vector3 rot = dts * ((Vector3.right * -pos.y * clampX) + (Vector3.forward * pos.x * clampZ)) + euler;
        transform.rotation = Quaternion.Euler(rot);
    }

        /// <summary>
    /// Returns the clamped euler angles [-180..180].
    /// </summary>
    private Vector3 GetClampedEuler()
    {
        Vector3 euler = transform.rotation.eulerAngles;

        euler[0] = ClampAngle(euler[0]);
        euler[1] = ClampAngle(euler[1]);
        euler[2] = ClampAngle(euler[2]);

        return euler;
    }

    /// <summary>
    /// Clamps an angle to [-180..180].
    /// </summary>
    private float ClampAngle(float angle)
    {
        return angle > 180.0f ? angle - 360.0f : angle;
    }

    /// <summary>
    /// Returns the normalized mouse position with the origin located in screen center.
    /// </summary>
    private Vector2 GetNormalizedMousePos()
    {
        Vector2 mousPosNorm = (new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height) - Vector2.one * 0.5f) * 2.0f;

        return mousPosNorm;
    }
}
