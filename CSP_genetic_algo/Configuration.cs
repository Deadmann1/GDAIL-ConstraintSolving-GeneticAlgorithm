using System;
using System.Security.Cryptography;

namespace CSP_genetic_algo
{
    public class Configuration
    {
        private TypeEnum type;
        private int fuel;
        private bool skibag;
        private bool fourWheel;
        private bool pdc;
        private int fitness;

        private int[] _fuelDomainValues = {4, 6, 10};

        public TypeEnum Type
        {
            get => type;
            set => type = value;
        }

        public int Fuel
        {
            get => fuel;
            set => fuel = value;
        }

        public bool Skibag
        {
            get => skibag;
            set => skibag = value;
        }

        public bool FourWheel
        {
            get => fourWheel;
            set => fourWheel = value;
        }

        public bool Pdc
        {
            get => pdc;
            set => pdc = value;
        }

        public enum TypeEnum
        {
            City,
            Limo,
            Combi,
            Xdrive
        }

        public int Fitness
        {
            get => fitness;
            set => fitness = value;
        }

        public Configuration()
        {
        }

        public Configuration(TypeEnum type, int fuel, bool skibag, bool fourWheel, bool pdc)
        {
            this.type = type;
            this.fuel = fuel;
            this.skibag = skibag;
            this.fourWheel = fourWheel;
            this.pdc = pdc;
        }

        public Configuration(Configuration parentA, Configuration parentB, int crossoverPoint)
        {
            int currentTrait = 0;
            this.type = currentTrait <= crossoverPoint ? parentA.Type : parentB.Type;
            currentTrait++;
            this.fuel = currentTrait <= crossoverPoint ? parentA.Fuel : parentB.Fuel;
            currentTrait++;
            this.skibag = currentTrait <= crossoverPoint ? parentA.Skibag : parentB.Skibag;
            currentTrait++;
            this.fourWheel = currentTrait <= crossoverPoint ? parentA.FourWheel : parentB.FourWheel;
            currentTrait++;
            this.pdc = currentTrait <= crossoverPoint ? parentA.Pdc : parentB.Pdc;
        }

        public void SetRandomValuesFromDomain()
        {
            Random rng = new Random();

            Type = (TypeEnum) rng.Next(0, 3);
            Fuel = _fuelDomainValues[rng.Next(0, 2)];
            Skibag = rng.Next(0, 1) != 0;
            FourWheel = rng.Next(0, 1) != 0;
            Pdc = rng.Next(0, 1) != 0;
        }

        public void MutateRandomTrait()
        {
            Random rng = new Random();

            switch (rng.Next(0, 4))
            {
                case 0:
                    Type = (TypeEnum) rng.Next(0, 3);
                    break;
                case 1:
                    Fuel = _fuelDomainValues[rng.Next(0, 2)];
                    break;
                case 2:
                    Skibag = rng.Next(0, 1) != 0;
                    break;
                case 3:
                    FourWheel = rng.Next(0, 1) != 0;
                    break;
                case 4:
                    Pdc = rng.Next(0, 1) != 0;
                    break;
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(type)}: {type}, {nameof(fuel)}: {fuel}, {nameof(skibag)}: {skibag}, {nameof(fourWheel)}: {fourWheel}, {nameof(pdc)}: {pdc}, {nameof(fitness)}: {fitness}";
        }
    }
}