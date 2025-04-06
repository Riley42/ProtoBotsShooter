using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmt = 100;
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmt);
    }
}
