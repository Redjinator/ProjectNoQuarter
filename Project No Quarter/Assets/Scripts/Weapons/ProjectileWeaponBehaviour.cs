using UnityEngine;

// ProjectileWeaponBehaviour is a base class for projectile weapons in Unity.
// To be placed on a prefab of a projectile weapon.

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0)
        {
            scale.x = scale.x * -4; // Flip the sprite horizontally
            scale.y = scale.y * -4; // Flip the sprite vertically
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(diry, dirx) * Mathf.Rad2Deg - 45f); // Rotate the sprite to face the direction of movement
    }

}
