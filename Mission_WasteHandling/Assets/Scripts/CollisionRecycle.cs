using UnityEngine;

public class CollisionRecycle : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Recyclable"))
        {
            Debug.Log("CORRECT");
            Game.instance.AddScore(1);
        }
        else
        {
            Debug.Log("WRONG");
            Game.instance.AddScore(-1);
        }
        Game.instance.IncrementBottlesCollected();
		collision.gameObject.SetActive(false);
	}
}
