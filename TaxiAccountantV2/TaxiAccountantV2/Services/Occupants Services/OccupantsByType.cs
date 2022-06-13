using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TaxiAccountantV2.Services.TripCalculation
{
    internal class OccupantsByType
    {
        public static IReadOnlyList<int> TotalOccupants { get; private set; }
        private readonly int MaxOccupants = 0;
        private readonly int[] Occupants;

        public OccupantsByType(int maxOccupants)
        {
            MaxOccupants = maxOccupants;
            Occupants = new int[MaxOccupants];
        }

        public void SetAmountOfOccupants()
        {
            for (int i = 0; i < Occupants.Length; i++)
                Occupants[i] = i+1;

            TotalOccupants = Occupants;
        }
    }
}
