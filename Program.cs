namespace ClElectroCalc
{
    internal class Program
    {
        const double GasConstant = 0.0821;          //л·атм/(моль·К)
        const double FaradeyConstant = 96485.0;     //Кл/моль
        const double Pressure = 1.0;                //атм
        const double ClMass = 71.0;                 //г/моль
        const double MPC = 1.0;                     //мг/м³

        static double SynthCalculation(double I, double time, double Ktemp)
        {
            double molarVolume = (GasConstant * Ktemp) / Pressure;
            return (I * time * molarVolume) / FaradeyConstant;
        }

        static double MPCCalculation(double volumePerTime, double roomVolume)
        {
            return (volumePerTime * 1000 * ClMass) / (roomVolume * 1000);
        }

        public static double OverdoseCalculation(double concentration)
        {
            return ((concentration - MPC) / MPC) * 100;
        }

        static void Main(string[] args)
        {
            double I;
            double time;
            double Ktemp;
            double roomVolume;

            Console.WriteLine("Калькулятор синтезированного объема хлора в ходе процесса электролиза.\n");

            Console.Write("\nВведите ток электролиза, А:\t");
            I = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("\nВведите время электролиза, сек:\t");
            time = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("\nВведите температуру окружающей среды, °C:\t");
            Ktemp = Convert.ToDouble(Console.ReadLine()) + 273.15;

            Console.Write("\nВведите объем помещения, м³: \t");
            roomVolume = Convert.ToDouble(Console.ReadLine());

            double volumePertime = SynthCalculation(I, time, Ktemp);
            Console.Write($"\nБудет синтезировано {volumePertime:F2} литров хлора.");

            double concentration = MPCCalculation(volumePertime, roomVolume);
            Console.Write($"\nКонцентрация хлора составит {concentration:F2} мг/м³.");

            if (concentration > MPC)    Console.WriteLine($"\nВнимание, концентрация хлора превышает ПДК на " +
                $"{OverdoseCalculation(concentration):F2}%!");
        }
    }
}
