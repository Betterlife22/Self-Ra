using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Core.Utils
{
    public static class MentorConnection
    {
        private static readonly Dictionary<string, string> _connections = new();

        public static void Add(string mentorId, string connectionId)
        {
            _connections[mentorId] = connectionId;
        }

        public static void RemoveByConnectionId(string connectionId)
        {
            var match = _connections.FirstOrDefault(x => x.Value == connectionId);
            if (!string.IsNullOrEmpty(match.Key))
            {
                _connections.Remove(match.Key);
            }
        }

        public static string? GetConnectionId(string mentorId)
        {
            return _connections.TryGetValue(mentorId, out var connId) ? connId : null;
        }
    }
}
