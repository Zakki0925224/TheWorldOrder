using System.Collections;
using System.Collections.Generic;

namespace Map
{
    public class State
    {
        public string Id { get; }
        public List<Province> Provinces { get; }

        public State(string id, List<Province> provinces)
        {
            this.Id = id;
            this.Provinces = provinces;
        }
    }
}
