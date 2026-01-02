using UnityEngine;
using UnityEngine.InputSystem;
public class Clean : MonoBehaviour
{
    [SerializeField] private DragTarget drag;

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Cleaner cleaner = drag.GetComponentFromDragged<Cleaner>();

            if (cleaner != null)
            {
                cleaner.rinse();
                Debug.Log("CLEANED");
            }
        }

        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            Cleaner cleaner = drag.GetComponentFromDragged<Cleaner>();

            if (cleaner != null)
            {
                cleaner.empty();
                Debug.Log("EMPTIED");
            }
        }
    }
}