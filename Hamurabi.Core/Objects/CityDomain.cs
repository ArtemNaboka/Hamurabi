using System;
using System.Diagnostics.CodeAnalysis;

namespace Hamurabi.Core.Objects
{
    // Владения города на текущий год
    public class CityDomain : IComparable<CityDomain>
    {
        public int BushelsCount { get; set; }           // Кол-во бушелей
        public int HarvestedBushelsPerAcr { get; set; }     // Кол-во собранных бушеле на один акр
        public int AcresCount { get; set; }                 // Кол-во акров во владении города
        public int AlivePeople { get; set; }                // Население города
        public int ComingInCurrentYearPeople { get; set; }  // Кол-во пришедших в этом году людей
        public int StarvedPeople { get; set; }              // Кол-во умерших от голода людей
        public int AcrCost { get; set; }                    // Стоимость одного акра
        public int EatenByRats { get; set; }                // Кол-во бушелей, съеденных крысами
        public int CurrentYear { get; set; }                // Текущий год


        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public CityDomain Clone()
        {
            return new CityDomain
            {
                BushelsCount = this.BushelsCount,
                CurrentYear = this.CurrentYear,
                AlivePeople = this.AlivePeople,
                StarvedPeople = this.StarvedPeople,
                EatenByRats = this.EatenByRats,
                AcrCost = this.AcrCost,
                AcresCount = this.AcresCount,
                ComingInCurrentYearPeople = this.ComingInCurrentYearPeople,
                HarvestedBushelsPerAcr = this.HarvestedBushelsPerAcr
            };
        }

        public int CompareTo(CityDomain other)
        {
            int thisDomainSum = GetDomainSum();
            int otherDomainSum = other.GetDomainSum();

            return Math.Abs(thisDomainSum - otherDomainSum) <= 0.5
                    ? 0
                    : thisDomainSum.CompareTo(otherDomainSum);
        }


        private int GetDomainSum()
        {
            int result = AcresCount / AlivePeople;

            return result;
        }
    }
}
