using UnityEngine;

public class CollisionRecycle : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		AudioManager.Instance.PlaySFX("Trash");
		Debug.Log(Game.instance.getMode());
		if (Game.instance.getMode() == 1)
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
		else if (Game.instance.getMode() == 2)
		{
			Debug.Log("Collision detected with: " + collision.gameObject.name);
			Cleaner cleaner = collision.gameObject.GetComponent<Cleaner>();
			if (cleaner != null)
			{
				if (cleaner.isCorrectlyRecycled())
				{
					Debug.Log("CORRECT (2)");
					Game.instance.AddScore(1);
				}
				else
				{
					Debug.Log("WRONG (2)");
					Game.instance.AddScore(-1);

				}
				Game.instance.IncrementBottlesCollected();
				collision.gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("No Cleaner component found on the collided object.");
			}
		}
	}
}
