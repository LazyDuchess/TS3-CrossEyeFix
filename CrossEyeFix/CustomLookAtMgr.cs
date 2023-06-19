using ScriptCore;
using Sims3.SimIFace;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LazyDuchess.CrossEyeFix
{
    public class CustomLookAtMgr : ILookAt
    {
        ILookAt mOriginalLookAtMgr;
        public static void Initialize()
        {
            var customLookAtMgr = new CustomLookAtMgr();
            customLookAtMgr.mOriginalLookAtMgr = LookAt.gLookAtMgr;
            LookAt.gLookAtMgr = customLookAtMgr;
        }

        public ILookAtTask CreateLookAtTask(IScriptProxy simObj, int priority, LookAtJointFilter jointFilter)
        {
            return new CustomLookAtTask(simObj, priority, jointFilter);
        }

        public int FlushLookAtQueue(IScriptProxy simObj, int priority)
        {
            return mOriginalLookAtMgr.FlushLookAtQueue(simObj, priority);
        }
    }
}
