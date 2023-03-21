using System.Collections;
using System.Collections.Generic;
using Map;

namespace History
{
    public class Country
    {
        public string Id { get; }
        public List<State> States { get; set; }
        public string Name { get; set; }

        public Country(string id, List<State> states, string name)
        {
            this.Id = id;
            this.States = states;
            this.Name = name;
        }
    }
}
