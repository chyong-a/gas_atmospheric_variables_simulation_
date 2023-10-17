using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasSimulation
{
    public abstract class Layer
    {
        #region Attributes
        protected char name;
        protected double thickness;
        #endregion

        #region Getters
        public char GetName()
        {
            return this.name;
        }
        public double GetThickness()
        {
            return this.thickness;
        }
        public abstract Layer GetAffected(Variable v);
        #endregion

        #region Constructors
        public Layer(char name, double thickness)
        {
            this.name = name;
            this.thickness = thickness;
        }
        #endregion
        #region Methods
        public void DecreaseThickness(double percentage) // transformation of one layer
        {
            this.thickness -= this.thickness*percentage;
        }
        public void IncreaseThickness(double amount) // into another
        {
            this.thickness += amount;
        }
        public bool StillExists()
        {
            return this.thickness >= 0.5;
        }
        public override string ToString()
        {
            String name = "";
            switch (this.name)
            {
                case 'X':
                    name = "Oxygen";
                    break;
                case 'Z':
                    name = "Ozone";
                    break;
                case 'C':
                    name = "Carbon Dioxide";
                    break;
            }
            return name + ": " + this.thickness;
        }
        #endregion 
    }
    public class Ozone : Layer
    {
        #region Constructors
        public Ozone(char name, double thickness) : base(name, thickness) { }
        #endregion
        #region Methods
        public override Layer GetAffected(Variable v)
        {
            return v.AffectZ(this);
        }
        #endregion
    }
    public class Oxygen : Layer
    {
        #region Constructors
        public Oxygen(char name, double thickness) : base(name, thickness) { }
        #endregion
        #region Methods
        public override Layer GetAffected(Variable v)
        {
            return v.AffectX(this);
        }
        #endregion
    }
    public class CarbonDioxide : Layer
    {
        #region Constructors
        public CarbonDioxide(char name, double thickness) : base(name, thickness) { }
        #endregion
        #region Methods
        public override Layer GetAffected(Variable v)
        {
            return v.AffectC(this);
        }
        #endregion
    }
}
