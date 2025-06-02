using UnityEngine;

public class KnifeController : WeaponController
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position;  // Set the position of the knife to the weapon's position
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.moveDir); // Set the direction of the knife based on player movement
        
    }


}
