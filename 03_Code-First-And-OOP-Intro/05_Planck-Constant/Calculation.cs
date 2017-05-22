namespace _05_Planck_Constant
{
    public class Calculation
    {
        public const decimal planckConstant = 6.62606896e-34M;
        public const decimal pi = 3.14159M;

        public static decimal GetReducedPlanckConstant()
        {
            return planckConstant / (2 * pi);
        }
    }
}