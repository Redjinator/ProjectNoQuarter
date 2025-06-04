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
    }
}
