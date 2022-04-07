using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    public class AgentsHolder
    {
        public List<AgentInfo> Values { get; set; } = new List<AgentInfo>();
        public AgentsHolder()
        {
        }

        internal void Add(AgentInfo input)
        {
            Values.Add(input);
        }

        internal object Get()
        {
            return Values;
        }
    }
}