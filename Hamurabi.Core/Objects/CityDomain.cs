using System.Diagnostics.CodeAnalysis;

namespace Hamurabi.Core.Objects
{
    public class CityDomain
    {
        public int BushelsCount { get; set; }
        public int HarvestedBushelsPerAcr { get; set; }
        public int AcresCount { get; set; }
        public int AlivePeople { get; set; }
        public int ComingInCurrentYearPeople { get; set; }
        public int StarvedPeople { get; set; }
        public int AcrCost { get; set; }
        public int EatenByRats { get; set; }
        public int CurrentYear { get; set; }


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
    }
}
