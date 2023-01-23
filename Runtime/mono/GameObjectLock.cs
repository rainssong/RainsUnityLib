using System;
using UnityEngine;
using System.Collections;

namespace com.rainssong.mono
{
    public class GameObjectLock : MonoBehaviour
    {
        // holds state weather or not there is a total lock in place
        public bool IsLocked;
        private bool prevIsLocked;

        // holds state weather or not there is a specific lock in place
        public bool IsPositionLocked;
        private bool prevIsPositionLocked;
        public bool IsRotationLocked;
        private bool prevIsRotationLocked;
        public bool IsScaleLocked;
        private bool prevIsScaleLocked;

        // used to store the state of the position/rotation/scale at the time of the lock
        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale;


        // Checks to see if a lock state has changed and if so performs a callback
        private void CheckLock(bool locked, ref bool prevlocked, Action callback)
        {
            // if locked but was not previously locked then perform callback
            if (locked != prevlocked)
            {
                if (locked)
                {
                    // changed to a locked state so do callback
                    callback();
                }
                else
                {
                    // restore values
                    this.SetLockedValues();
                }

                // set previous locked state
                prevlocked = locked;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // if totally locked but was not previously locked then capture state of position/rotation/scale
            this.CheckLock(this.IsLocked, ref this.prevIsLocked, () =>
                {
                    this.position = this.transform.position;
                    this.rotation = this.transform.rotation;
                    this.scale = this.transform.localScale;
                });

            // if position locked but was not previously locked then capture state of position
            this.CheckLock(this.IsPositionLocked, ref this.prevIsPositionLocked, () => this.position = this.transform.position);

            // if rotation locked but was not previously locked then capture state of rotation
            this.CheckLock(this.IsRotationLocked, ref this.prevIsRotationLocked, () => this.rotation = this.transform.rotation);

            // if scale locked but was not previously locked then capture state of scale
            this.CheckLock(this.IsScaleLocked, ref this.prevIsScaleLocked, () => this.scale = this.transform.localScale);

            // if is locked then ensure position/rotation/scale are set to the same values they were at the time it was locked
            this.SetLockedValues();
        }

        // used to set the position/rotation/scale is they are in a locked state
        private void SetLockedValues()
        {
            if (this.IsLocked | this.IsPositionLocked) this.transform.position = this.position;
            if (this.IsLocked | this.IsRotationLocked) this.transform.rotation = this.rotation;
            if (this.IsLocked | this.IsScaleLocked) this.transform.localScale = this.scale;
        }
    }
}