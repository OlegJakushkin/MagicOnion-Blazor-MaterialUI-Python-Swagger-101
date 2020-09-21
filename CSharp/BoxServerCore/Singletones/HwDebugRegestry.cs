using System.Collections.Generic;
using System.Linq;

namespace BoxServerCore
{
    public static class HwDebugRegestry
    {
        public static List<string> HwIDs = new List<string>();
        public static Dictionary<string, int> purchases = new Dictionary<string, int>();
        public static Dictionary<string, KeyValuePair<string, int>> executingRequests = new Dictionary<string, KeyValuePair<string, int>>();
    }
}