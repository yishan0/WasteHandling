using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        CreateWall("Left",  new Vector2(-width / 2f - 0.5f, 0), new Vector2(1, height));
        CreateWall("Right", new Vector2( width / 2f + 0.5f, 0), new Vector2(1, height));
        CreateWall("Top",   new Vector2(0,  height / 2f + 0.5f), new Vector2(width, 1));
        CreateWall("Bottom",new Vector2(0, -height / 2f - 0.5f), new Vector2(width, 1));
    }

    void CreateWall(string name, Vector2 position, Vector2 size)
    {
        GameObject wall = new GameObject(name);
        wall.transform.parent = transform;
        wall.transform.position = position;

        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
    }
}
