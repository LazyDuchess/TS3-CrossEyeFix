using Sims3.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LazyDuchess.CrossEyeFix
{
    public static class Debug
    {
        public static void Notify(string text)
        {
            if (!Main.kDebug)
                return;
            StyledNotification.Show(new StyledNotification.Format(text, StyledNotification.NotificationStyle.kGameMessageNegative));
        }
    }
}
