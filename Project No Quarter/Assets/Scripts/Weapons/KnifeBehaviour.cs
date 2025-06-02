using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    KnifeController kc;

    protected override void Start()
    {
        base.Start();
        kc = FindAnyObjectByType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * kc.speed * Time.deltaTime; // set the movement of the knife
    }
}
