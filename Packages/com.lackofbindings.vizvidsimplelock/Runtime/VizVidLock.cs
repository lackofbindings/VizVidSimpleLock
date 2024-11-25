
using System.Diagnostics;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace JLChnToZ.VRC.VVMW
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    [DisallowMultipleComponent]
    public class VizVidLock : UdonSharpBehaviour
    {
        public FrontendHandler PlaylistQueueHandler;
        [UdonSynced] public bool isLocked = false;
        private bool wantToLock = false;
        public Text label;
        public GameObject lockedUI;
        public GameObject unlockedUI;

        void Start()
        {
            UpdateUI();
            UpdateVizVid();
        }

        public override void Interact()
        {
            // UnityEngine.Debug.Log("[VizVid Lock] Interact, Owner: " + Networking.GetOwner(this.gameObject).displayName);
            if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                // If we are already the owner then we can toggle the lock normally
                // UnityEngine.Debug.Log("[VizVid Lock] is owner, Locked: " + isLocked);
                isLocked = !isLocked;
                RequestSerialization();
                UpdateUI();
                UpdateVizVid();
            }
            else if (!isLocked || Networking.LocalPlayer.isInstanceOwner)
            {
                // Remember that we are trying to lock and then request ownership
                wantToLock = true;
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            }
        }

        public override bool OnOwnershipRequest(VRCPlayerApi requester, VRCPlayerApi newOwner)
        {
            // Only let someone become the new owner if it's currently unlocked
            // or if they are the instance owner
            return !isLocked || requester.isInstanceOwner;
        }

        public override void OnOwnershipTransferred(VRCPlayerApi player)
        {
            // If we are the new owner and we want to lock, then engage the lock
            if (player.isLocal && wantToLock)
            {
                isLocked = true;
                wantToLock = false;
                RequestSerialization();
            }
            UpdateUI();
            UpdateVizVid();
        }

        public override void OnDeserialization()
        {
            UpdateUI();
            UpdateVizVid();
        }

        private void UpdateUI()
        {
            // Update lock UI state
            lockedUI.SetActive(isLocked);
            unlockedUI.SetActive(!isLocked);
            // Check current owner
            VRCPlayerApi currentOwner = Networking.GetOwner(this.gameObject);
            // Display who the current owner is (if locked)
            label.text = (isLocked && Utilities.IsValid(currentOwner)) ? currentOwner.displayName : "";

        }

        private void UpdateVizVid()
        {
            if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                PlaylistQueueHandler._OnUnlock();
            }
            else
            {
                if (isLocked)
                {
                    PlaylistQueueHandler._Lock();
                }
                else
                {
                    PlaylistQueueHandler._OnUnlock();
                }
            }

        }
    }
}