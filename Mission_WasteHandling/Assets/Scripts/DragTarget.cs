using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.
/// </summary>
public class DragTarget : MonoBehaviour
{
    public LayerMask m_DragLayers;

    [Range(0.0f, 100.0f)]
    public float m_Damping = 1.0f;

    [Range(0.0f, 100.0f)]
    public float m_Frequency = 5.0f;

    public bool m_DrawDragLine = true;
    public Color m_Color = Color.cyan;

    private TargetJoint2D m_TargetJoint;

    // Public property to access the currently dragged GameObject
    public GameObject DraggedObject { get; private set; }

    private bool hasPlayedPickupSFX = false; // ensures pickup SFX only plays once per drag

    public T GetComponentFromDragged<T>() where T : Component
    {
        if (DraggedObject != null)
        {
            return DraggedObject.GetComponent<T>();
        }
        return null;
    }

    void Update()
    {
        var currentMouse = Mouse.current;

        // Calculate the world position for the mouse.
        var worldPos = Camera.main.ScreenToWorldPoint(currentMouse.position.value);

        if (currentMouse.leftButton.wasPressedThisFrame)
        {
            // Fetch the first collider.
            var collider = Physics2D.OverlapPoint(worldPos, m_DragLayers);
            if (!collider)
                return;

            // Fetch the collider body.
            var body = collider.attachedRigidbody;
            if (!body)
                return;

            // Store the dragged object
            DraggedObject = body.gameObject;

            // Add a target joint to the Rigidbody2D GameObject.
            m_TargetJoint = DraggedObject.AddComponent<TargetJoint2D>();
            m_TargetJoint.dampingRatio = m_Damping;
            m_TargetJoint.frequency = m_Frequency;

            // Attach the anchor to the local-point where we clicked.
            m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(worldPos);

            // ---------- Play Pickup SFX ----------
            if (!hasPlayedPickupSFX)
            {
                string objName = DraggedObject.name;

				if (objName.Contains("Water"))
					AudioManager.Instance.PlaySFX("Plastic");
				else if (objName.Contains("Juice") || objName.Contains("Coke"))
					AudioManager.Instance.PlaySFX("Glass");
				else if (objName.Contains("Milk") || objName.Contains("Carton"))
					AudioManager.Instance.PlaySFX("Paper");
                hasPlayedPickupSFX = true;
            }
        }
        else if (currentMouse.leftButton.wasReleasedThisFrame)
        {
            // Optionally play drop sound
            // AudioManager.Instance.PlaySFX("Drop");

            Destroy(m_TargetJoint);
            m_TargetJoint = null;
            DraggedObject = null; // Clear the reference
            hasPlayedPickupSFX = false; // reset for next drag
            return;
        }

        // Update the joint target.
        if (m_TargetJoint)
        {
            m_TargetJoint.target = worldPos;

            // Draw the line between the target and the joint anchor.
            if (m_DrawDragLine)
                Debug.DrawLine(m_TargetJoint.transform.TransformPoint(m_TargetJoint.anchor), worldPos, m_Color);
        }
    }
}