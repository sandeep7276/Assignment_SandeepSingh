
using UnityEngine;
using SnakeGame;

public class SnakeCollision : MonoBehaviour
{
    public delegate void CollisionTriggered(CollisionType cType);
    public  CollisionTriggered collision;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DataConst.FoodTag))
        {
            collision?.Invoke(CollisionType.Food);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(DataConst.WallTag))
        {
            collision?.Invoke(CollisionType.Wall);
        }
    }
}
