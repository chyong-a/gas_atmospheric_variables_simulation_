using GasSimulation;
using static GasSimulation.Simulation;
using static System.Net.Mime.MediaTypeNames;

namespace GasSimulationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAffect()
        {
            ThunderStorm ts = ThunderStorm.Instance();
            Sunshine s = Sunshine.Instance();
            Other o = Other.Instance();

            Oxygen x1 = new Oxygen('X', 5);
            Oxygen x2 = new Oxygen('X', 5);
            Oxygen x3 = new Oxygen('X', 5);
            Ozone z1 = new Ozone('Z', 5);
            Ozone z2 = new Ozone('Z', 5);
            Ozone z3 = new Ozone('Z', 5);
            CarbonDioxide cd1 = new CarbonDioxide('C', 5);
            CarbonDioxide cd2 = new CarbonDioxide('C', 5);
            CarbonDioxide cd3 = new CarbonDioxide('C', 5);

            Layer test1 = ts.AffectZ(z1);
            Assert.AreEqual(test1, null);
            Assert.AreEqual(z1.GetThickness(), 5);

            Layer test2 = s.AffectZ(z2);
            Assert.AreEqual(test2, null);
            Assert.AreEqual(z2.GetThickness(), 5);

            Layer test3 = o.AffectZ(z3);
            Assert.AreEqual(test3.GetName(), 'X');
            Assert.AreEqual(test3.GetThickness(), 0.25);
            Assert.AreEqual(z3.GetThickness(), 4.75);

            Layer test4 = ts.AffectX(x1);
            Assert.AreEqual(test4.GetName(), 'Z');
            Assert.AreEqual(test4.GetThickness(), 2.5);
            Assert.AreEqual(x1.GetThickness(), 2.5);

            Layer test5 = s.AffectX(x2);
            Assert.AreEqual(test5.GetName(), 'Z');
            Assert.AreEqual(test5.GetThickness(), 0.25);
            Assert.AreEqual(x2.GetThickness(), 4.75);

            Layer test6 = o.AffectX(x3);
            Assert.AreEqual(test6.GetName(), 'C');
            Assert.AreEqual(test6.GetThickness(), 0.5);
            Assert.AreEqual(x3.GetThickness(), 4.5);

            Layer test7 = ts.AffectC(cd1);
            Assert.AreEqual(test7, null);
            Assert.AreEqual(cd1.GetThickness(), 5);

            Layer test8 = s.AffectC(cd2);
            Assert.AreEqual(test8.GetName(), 'X');
            Assert.AreEqual(test8.GetThickness(), 0.25);
            Assert.AreEqual(cd2.GetThickness(), 4.75);

            Layer test9 = o.AffectC(cd3);
            Assert.AreEqual(test9, null);
            Assert.AreEqual(cd3.GetThickness(), 5);
        }

        [TestMethod]
        public void TestGetters()
        {
            Oxygen x1 = new Oxygen('X', 5);
            Ozone z1 = new Ozone('Z', 5);
            CarbonDioxide cd1 = new CarbonDioxide('C', 5);
            Assert.AreEqual(x1.GetName(), 'X');
            Assert.AreEqual(z1.GetName(), 'Z');
            Assert.AreEqual(cd1.GetName(), 'C');
            Assert.AreEqual(x1.GetThickness(), 5);
            Assert.AreEqual(z1.GetThickness(), 5);
            Assert.AreEqual(cd1.GetThickness(), 5);
        }

        [TestMethod]
        public void TestDecreaseThickness()
        {
            Oxygen x1 = new Oxygen('X', 5);
            Ozone z1 = new Ozone('Z', 5);
            CarbonDioxide cd1 = new CarbonDioxide('C', 5);
            Assert.AreEqual(x1.GetThickness(), 5);
            Assert.AreEqual(z1.GetThickness(), 5);
            Assert.AreEqual(cd1.GetThickness(), 5);
            x1.DecreaseThickness(0.5);
            z1.DecreaseThickness(0.5);
            cd1.DecreaseThickness(0.5);
            Assert.AreEqual(x1.GetThickness(), 2.5);
            Assert.AreEqual(z1.GetThickness(), 2.5);
            Assert.AreEqual(cd1.GetThickness(), 2.5);
        }

        [TestMethod]
        public void TestIncreaseThickness()
        {
            Oxygen x1 = new Oxygen('X', 5);
            Ozone z1 = new Ozone('Z', 5);
            CarbonDioxide cd1 = new CarbonDioxide('C', 5);
            Assert.AreEqual(x1.GetThickness(), 5);
            Assert.AreEqual(z1.GetThickness(), 5);
            Assert.AreEqual(cd1.GetThickness(), 5);
            x1.IncreaseThickness(0.5);
            z1.IncreaseThickness(0.5);
            cd1.IncreaseThickness(0.5);
            Assert.AreEqual(x1.GetThickness(), 5.5);
            Assert.AreEqual(z1.GetThickness(), 5.5);
            Assert.AreEqual(cd1.GetThickness(), 5.5);
        }

        [TestMethod]
        public void TestNoVariablesException()
        {
            Simulation s = new Simulation("emptyvar.txt");
            try
            {
                s.Simulate();
                Assert.Fail("No exception thrown");
            } catch (Exception ex)
            {
                Assert.IsTrue(ex is NoVariablesException);
            }
        }

        [TestMethod]
        public void TestNoLayersException()
        {
            Simulation s = new Simulation("emptylay.txt");
            try
            {
                s.Simulate();
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NoLayersException);
            }
        }
    }
}