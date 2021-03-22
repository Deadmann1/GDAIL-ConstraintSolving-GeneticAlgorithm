using System.Collections.Generic;

namespace CSP_genetic_algo
{
    public class Population
    {
        private List<Configuration> members;
        private int totalFitness;

        public List<Configuration> Members
        {
            get => members;
            set => members = value;
        }

        public int TotalFitness
        {
            get => totalFitness;
            set => totalFitness = value;
        }

        public Population()
        {
            members = new List<Configuration>();
        }
    }
}