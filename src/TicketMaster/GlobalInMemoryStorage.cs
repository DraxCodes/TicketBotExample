using System.Collections.Generic;

namespace TicketMaster
{
    public static class GlobalInMemoryStorage
    {
        public static List<ulong> TocketIDs { get; set; } = new List<ulong>();
    }
}
