using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GasSimulation
{
    public abstract class Variable
    {
        #region Attributes

        #endregion
        #region Constructors
        protected Variable() { }
        #endregion
        #region Methods
        // when variable affects, it changes the thickness of the layer + possible creation of the new layer
        public abstract Layer AffectZ(Ozone ozone);
        public abstract Layer AffectX(Oxygen oxygen);
        public abstract Layer AffectC(CarbonDioxide carbonDioxide);

        #endregion
    }
    public class ThunderStorm : Variable
    {
        #region Attributes
        private static ThunderStorm instance;
        #endregion
        #region Constructors
        private ThunderStorm() : base() { }

        public static ThunderStorm Instance()
        {
            if (instance == null)
            {
                instance = new ThunderStorm();
            }
            return instance;
        }
        #endregion
        #region Methods
        public override Layer AffectZ(Ozone ozone)
        {
            // thunderstorm doesn't affect ozone
            Layer? result = null;
            return result;
        }
        public override Layer AffectX(Oxygen oxygen)
        {
            // 50% turns into ozone
            Layer? result = new Ozone('Z', oxygen.GetThickness()*0.5);
            oxygen.DecreaseThickness(0.5);
            return result;
        }
        public override Layer AffectC(CarbonDioxide carbonDioxide)
        {
            // thunderstorm doesn't affect carbondioxide
            Layer? result = null;
            return result;
        }
        #endregion
    }
    public class Sunshine : Variable
    {
        #region Attributes
        private static Sunshine instance;
        #endregion
        #region Constructors
        private Sunshine() : base() { }
        public static Sunshine Instance()
        {
            if (instance == null)
            {
                instance = new Sunshine();
            }
            return instance;
        }
        #endregion
        #region Methods
        public override Layer AffectZ(Ozone ozone)
        {
            // sunshine doesn't affect ozone
            Layer? result = null;
            return result;
        }
        public override Layer AffectX(Oxygen oxygen)
        {
            // 5% turns into ozone
            Layer? result = new Ozone('Z', oxygen.GetThickness()*0.05);
            oxygen.DecreaseThickness(0.05);
            return result;
        }
        public override Layer AffectC(CarbonDioxide carbonDioxide)
        {
            // 5% turns into oxygen
            Layer? result = new Oxygen('X', carbonDioxide.GetThickness()*0.05);
            carbonDioxide.DecreaseThickness(0.05);
            return result;
        }
        #endregion
    }
    public class Other : Variable
    {
        #region Attributes
        private static Other instance;
        #endregion
        #region Constructors
        private Other() : base() { }
        public static Other Instance()
        {
            if (instance == null)
            {
                instance = new Other();
            }
            return instance;
        }
        #endregion
        #region Methods
        public override Layer AffectZ(Ozone ozone)
        {
            // 5% turns into oxygen
            Layer? result = new Oxygen('X', ozone.GetThickness()*0.05);
            ozone.DecreaseThickness(0.05);
            return result;
        }
        public override Layer AffectX(Oxygen oxygen)
        {
            // 10% turns into carbondioxide
            Layer? result = new CarbonDioxide('C', oxygen.GetThickness()*0.1);
            oxygen.DecreaseThickness(0.1);
            return result;
        }
        public override Layer AffectC(CarbonDioxide carbonDioxide)
        {
            // other doesn't affect carbondioxide
            Layer? result = null;
            return result;
        }
        #endregion
    }

}
