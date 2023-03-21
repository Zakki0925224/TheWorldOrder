using System.Collections;
using System.Collections.Generic;

namespace Map
{
    public class State
    {
        public string Id { get; }
        public List<Province> Provinces { get; }
        public string Name { get; set; }

        public State(string id, List<Province> provinces, string name)
        {
            this.Id = id;
            this.Provinces = provinces;
            this.Name = name;
        }
    }
}
