using System;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;

namespace LazyDuchess.CrossEyeFix
{
	public class Main
	{
		[Tunable] static bool init;
		[Tunable] public static float kMinimumDistanceForEyeLookAt = 0.5f;
		[Tunable] public static bool kDebug = false;
		
		static Main()
		{
			World.sOnStartupAppEventHandler += OnStartup;
		}

		static void OnStartup(object sender, EventArgs e)
        {
			CustomLookAtMgr.Initialize();
        }
	}
}