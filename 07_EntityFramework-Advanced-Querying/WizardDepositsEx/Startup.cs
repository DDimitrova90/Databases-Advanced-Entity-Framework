namespace WizardDepositsEx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            WizardDepositsContext context = new WizardDepositsContext();

            // Problem 19.Deposits Sum for Ollivander Family
            //DepositsSumForOllivanderFamily(context);    

            // Problem 20.Deposits Filter
            //DepositsFilter(context);
        }

        public static void DepositsSumForOllivanderFamily(WizardDepositsContext context)
        {
            var depositGroups = context.WizzardDepositsOlds
                .Where(w => w.MagicWandCreator == "Ollivander family")
                .GroupBy(w => w.DepositGroup)
                .Select(w => new { DepositGroup = w.Key, Sum = w.Sum(d => d.DepositAmount) })
                .ToList();

            foreach (var dep in depositGroups)
            {
                Console.WriteLine($"{dep.DepositGroup} - {dep.Sum:F2}");
            }
        }

        public static void DepositsFilter(WizardDepositsContext context)
        {
            var depositGroups = context.WizzardDepositsOlds
                            .Where(w => w.MagicWandCreator == "Ollivander family")
                            .GroupBy(w => w.DepositGroup)
                            .Select(w => new { DepositGroup = w.Key, Sum = w.Sum(d => d.DepositAmount) })
                            .ToList()
                            .Where(w => w.Sum < 150000)
                            .OrderByDescending(w => w.Sum);

            foreach (var dep in depositGroups)
            {
                Console.WriteLine($"{dep.DepositGroup} - {dep.Sum:F2}");
            }
        }
    }
}
