using ScriptCore;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.SimIFace;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LazyDuchess.CrossEyeFix
{
    public class CustomLookAtTask : ILookAtTask, IDisposable
    {
        private LookAtTask mOriginalLookAtTask;
        private int mPriority;
        private IScriptProxy mSimObj;
        private LookAtJointFilter mJointFilter;

        void RecalculateJointFilter()
        {
            var sim = mSimObj.ObjectId.ObjectFromId<Sim>();

            if (sim == null)
            {
                Cancel();
                return;
            }

            var targetObjectGuid = new ObjectGuid(TargetObjectId);

            if (!targetObjectGuid.IsValid)
            {
                Cancel();
                return;
            }

            if (targetObjectGuid == sim.ObjectId)
            {
                Cancel();
                return;
            }

            var target = targetObjectGuid.ObjectFromId<GameObject>();

            if (target == null)
            {
                Cancel();
                return;
            }

            var distance = (target.Position - sim.Position).Length();

            if (distance < Main.kMinimumDistanceForEyeLookAt)
            {
                    Notify($"Cancelled eye look at. (Dist: {distance})", sim);
                mJointFilter &= ~LookAtJointFilter.EyeBones;
                Recreate();
            }
            else
            {
                    Notify($"Allowed eye look at. (Dist: {distance})", sim);
            }

            void Cancel()
            {
                Notify("Cancelled lookat fully.", sim);
                mJointFilter = 0;
                Recreate();
            }

            void Recreate()
            {
                mOriginalLookAtTask.Dispose();
                mOriginalLookAtTask = new LookAtTask(mSimObj, mPriority, mJointFilter);
            }

            void Notify(string text, Sim actor)
            {
                if (actor.IsSelectable)
                    Debug.Notify($"({actor.FullName}) {text}");
            }
        }

        public CustomLookAtTask(IScriptProxy simObj, int priority, LookAtJointFilter jointFilter)
        {
            mOriginalLookAtTask = new LookAtTask(simObj, priority, jointFilter);
            mSimObj = simObj;
            mPriority = priority;
            mJointFilter = jointFilter;
        }

        public ulong TargetObjectId
        {
            get
            {
                return mOriginalLookAtTask.TargetObjectId;
            }
            set
            {
                mOriginalLookAtTask.TargetObjectId = value;
            }
        }
        public uint TargetSlotNameHash
        {
            get
            {
                return mOriginalLookAtTask.TargetSlotNameHash;
            }
            set
            {
                mOriginalLookAtTask.TargetSlotNameHash = value;
            }
        }
        public Vector3 TargetOffsetVector
        {
            get
            {
                return mOriginalLookAtTask.TargetOffsetVector;
            }
            set
            {
                mOriginalLookAtTask.TargetOffsetVector = value;
            }
        }
        public float LookTowardsSpeed
        {
            get
            {
                return mOriginalLookAtTask.LookTowardsSpeed;
            }
            set
            {
                mOriginalLookAtTask.LookTowardsSpeed = value;
            }
        }
        public float LookAwaySpeed
        {
            get
            {
                return mOriginalLookAtTask.LookAwaySpeed;
            }
            set
            {
                mOriginalLookAtTask.LookAwaySpeed = value;
            }
        }
        public bool IgnoreFrustum
        {
            get
            {
                return mOriginalLookAtTask.IgnoreFrustum;
            }
            set
            {
                mOriginalLookAtTask.IgnoreFrustum = value;
            }
        }
        public bool HoldAtJointLimits
        {
            get
            {
                return mOriginalLookAtTask.HoldAtJointLimits;
            }
            set
            {
                mOriginalLookAtTask.HoldAtJointLimits = value;
            }
        }
        public uint BlendInFrames
        {
            set
            {
                mOriginalLookAtTask.BlendInFrames = value;
            }
        }
        public Delegate EventHandler 
        { set
            {
                mOriginalLookAtTask.EventHandler = value;
            }
        }
        public float RightTiltness
        {
            get
            {
                return mOriginalLookAtTask.RightTiltness;
            }
            set
            {
                mOriginalLookAtTask.RightTiltness = value;
            }
        }
        public float TorsoThreshold
        {
            get
            {
                return mOriginalLookAtTask.TorsoThreshold;
            }
            set
            {
                mOriginalLookAtTask.TorsoThreshold = value;
            }
        }
        public float TorsoPercent
        {
            get
            {
                return mOriginalLookAtTask.TorsoPercent;
            }
            set
            {
                mOriginalLookAtTask.TorsoPercent = value;
            }
        }
        public float AttenuatorPitch
        {
            get
            {
                return mOriginalLookAtTask.AttenuatorPitch;
            }
            set
            {
                mOriginalLookAtTask.AttenuatorPitch = value;
            }
        }
        public float AttenuatorYaw
        {
            get
            {
                return mOriginalLookAtTask.AttenuatorYaw;
            }
            set
            {
                mOriginalLookAtTask.AttenuatorYaw = value;
            }
        }
        public float LookTowardsCurveRate
        {
            get
            {
                return mOriginalLookAtTask.LookTowardsCurveRate;
            }
            set
            {
                mOriginalLookAtTask.LookTowardsCurveRate = value;
            }
        }

        public void Dispose()
        {
            mOriginalLookAtTask.Dispose();
        }

        public bool EnableIt()
        {
            RecalculateJointFilter();
            return mOriginalLookAtTask.EnableIt();
        }

        public void StopIt(bool hardStop)
        {
            mOriginalLookAtTask.StopIt(hardStop);
        }
    }
}
