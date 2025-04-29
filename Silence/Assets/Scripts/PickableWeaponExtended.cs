using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class PickableWeaponExtended : PickableWeapon
{
    protected override void Pick(GameObject picker)
    {
        Character character = _collidingObject.gameObject.MMGetComponentNoAlloc<Character>();

        if (character == null)
        {
            return;
        }

        foreach (var bar in GUIManager.Instance.weaponCooldownBars)
        {
            bar.gameObject.SetActive(true);
        }

        if (_characterHandleWeapon != null)
        {
            _characterHandleWeapon.ChangeWeapon(WeaponToGive, null);

            if (weaponAnimatorController != null)
            {
                character.CharacterModel.GetComponent<Animator>().runtimeAnimatorController = weaponAnimatorController;
            }

            WeaponManager.Instance.StoreWeapon(character, WeaponToGive, weaponAnimatorController);
        }
    }
}
