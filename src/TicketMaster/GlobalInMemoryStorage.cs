using System.Collections.Generic;

namespace TicketMaster
{
    public static class GlobalInMemoryStorage
    {
        public static List<ulong> TicketIDs { get; set; } = new List<ulong>();
    }
}
