using System;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
        [SerializeField] private float scrollWheelBreakPoint;
        [Space]
        [SerializeField] private Weapon[] weapons;
        [SerializeField] private RectTransform worldTextCanvas;
        [SerializeField] private WorldSpaceText worldSpaceText;

        private Weapon currentWeapon;

        private int currentWeaponIndex;
        private float scrollWheelDelta;

        private void Start()
        {
                foreach (var weapon in weapons)
                {
                        weapon.gameObject.SetActive(false);
                }

                SwitchWeapon(weapons[0]);
        }

        private void Update()
        {
                HandleWeaponSwitching();
                
                if (Input.GetMouseButtonDown(0)) currentWeapon?.Fire();
                if (Input.GetKeyDown(KeyCode.F)) CreateWorldText();
        }

        private void CreateWorldText()
        {
                var worldText = Instantiate(worldSpaceText, worldTextCanvas);
        }

        private void HandleWeaponSwitching()
        {
                scrollWheelDelta += Input.mouseScrollDelta.y;

                if (Mathf.Abs(scrollWheelDelta) < scrollWheelBreakPoint) return;
                
                NextWeapon((int)Mathf.Sign(scrollWheelDelta));
                SwitchWeapon(weapons[currentWeaponIndex]);

                scrollWheelDelta = 0;
        }

        private void SwitchWeapon(Weapon newWeapon)
        {
                if (currentWeapon != null) currentWeapon.gameObject.SetActive(false);
                currentWeapon = newWeapon;
                currentWeapon.gameObject.SetActive(true);
        }

        /// <summary></summary>
        /// <param name="next">
        ///     Next has to be 1 or -1.
        /// </param>
        private void NextWeapon(int next)
        {
                if (!(next is 1 or -1))
                {
                        Debug.LogWarning("next has to be 1 or -1! and not: " + next);
                        return;
                }

                var length = weapons.Length;
                var newIndex = currentWeaponIndex + next;

                if (newIndex < 0) newIndex += length;
                else if (newIndex >= length) newIndex -= length;
                
                currentWeaponIndex = newIndex;
        }
}