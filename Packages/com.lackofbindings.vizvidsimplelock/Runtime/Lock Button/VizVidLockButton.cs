
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using JLChnToZ.VRC.Foundation;
using System;

namespace JLChnToZ.VRC.VVMW
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    [DisallowMultipleComponent]
    public class VizVidLockButton : UdonSharpBehaviour
    {
        [Locatable] public VizVidLockManager lockManager;
        public bool isLocked = false;
        public int currentOwnerId = 0;
        public Text label;
        public GameObject lockedUI;
        public GameObject unlockedUI;

        void Start()
        {
            UpdateUI();
            Register();
        }

        public override void Interact()
        {
            lockManager.Interact();
        }

        public void UpdateUI()
        {
            // Update lock UI state
            lockedUI.SetActive(isLocked);
            unlockedUI.SetActive(!isLocked);
            // Check current owner
            VRCPlayerApi currentOwner = VRCPlayerApi.GetPlayerById(currentOwnerId);
            // Display who the current owner is (if locked)
            label.text = (isLocked && Utilities.IsValid(currentOwner)) ? currentOwner.displayName : "";

        }

        // Automatically add this button to list of all buttons in lock manager
        private void Register()
        {
            // If the manager's array is missing, then just make a new array with us in it
            if (!Utilities.IsValid(lockManager.lockButtons)) {
                lockManager.lockButtons = new VizVidLockButton[] { this };
                return;
            }
            // If we are already in the array then just exit
            if (Array.IndexOf(lockManager.lockButtons, this) >= 0) return;

            // Otherwise extend the array by one and add ourselves into it
            var temp = new VizVidLockButton[lockManager.lockButtons.Length + 1];
            Array.Copy(lockManager.lockButtons, temp, lockManager.lockButtons.Length);
            temp[lockManager.lockButtons.Length] = this;
            lockManager.lockButtons = temp;
            UnityEngine.Debug.Log("[VizVid Lock] Button auto-registered with lock manager at index: " + (lockManager.lockButtons.Length - 1));
        }
    }
}
