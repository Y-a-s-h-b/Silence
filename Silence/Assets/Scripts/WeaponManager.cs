using MoreMountains.CorgiEngine;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [System.Serializable]
    private class WeaponItem
    {
        public Weapon weapon;
        public RuntimeAnimatorController animController;

        public WeaponItem(Weapon weapon, RuntimeAnimatorController animController)
        {
            this.weapon = weapon;
            this.animController = animController;
        }
    }

    public static WeaponManager Instance;
    private List<WeaponItem> weapons = new();
    private int currentWeapon = 0;

    private CharacterHandleWeapon weaponHandler;
    private Character character;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapWeapon();
        }
    }

    public void StoreWeapon(Character owner, Weapon weapon, RuntimeAnimatorController animController)
    {
        foreach (var weaponItem in weapons)
        {
            if (weaponItem.weapon == weapon)
                return;
        }

        if (weaponHandler == null)
        {
            character = owner;
            weaponHandler = owner.GetComponent<CharacterHandleWeapon>();
        }

        WeaponItem newWeapon = new WeaponItem(weapon, animController);
        weapons.Add(newWeapon);
    }

    public void SwapWeapon()
    {
        if (weapons.Count <= 1) return;

        currentWeapon = (currentWeapon + 1) % weapons.Count;

        if (weaponHandler != null)
        {
            weaponHandler.ChangeWeapon(weapons[currentWeapon].weapon, null);

            if (weapons[currentWeapon].animController != null)
            {
                character.CharacterModel.GetComponent<Animator>().runtimeAnimatorController = weapons[currentWeapon].animController;
            }
        }
    }
}