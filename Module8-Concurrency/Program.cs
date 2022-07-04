/*  Author: Charles Phyle
 *  Date: 7/3/2022
 *  purpose: This application can chew through your memmory and cpu if you throw large enough numbers at it. Depending on what number the user puts in. As a warrning it gets unstabe around the 2 billion mark however, with enough ram it should be able to push higher.
 */

namespace Module8_Concurrency
{
    public class Module8_Concurrency
    {
        static void Main(string[] ars)
        {
            
            do
            {
                Console.WriteLine("Welcome! This program will demonstrate the difference in the amount of time it takes to calculate the sum of an array by utalizing one thread vs. Multithreaded.\n What size array would you like to calculate?\n or -1 to exit.");
                try
                {
                     
                   //BigInteger
                    var userChoice = Convert.ToDouble(Console.ReadLine());
                    
                    if(userChoice == -1) { Environment.Exit(0); }
                    List<double> array = new List<double>();
                    //double[] array = new double[userChoice];
                    Random rand = new();
                    var watch1 = new System.Diagnostics.Stopwatch();
                    watch1.Start();
                    for(double i = 0; i<userChoice; i++)
                    {
                        array.Add(rand.Next(10)+1) ;
                        //array[i] = rand.Next(10)+1;
                    }
                    var arrayHolder = array.ToArray();
                    watch1.Stop();
                    var arrayTime = watch1.ElapsedMilliseconds;
                    watch1.Restart();
                    double sum = arrayHolder.AsParallel().Sum();   //used to calculate the sum of the array in parrallel threads
                    watch1.Stop();
                    var sum1Time = watch1.ElapsedMilliseconds;
                    double[] singleThread = Sumcomputation(arrayHolder);
                    Console.WriteLine($"It took {sum1Time} milliseconds (or {sum1Time/1000.0} seconds) to calculate the sum of an array length {userChoice}, with a final value of {sum} utilizing multithreading! By contrast, it took {singleThread[0]} milliseconds (or {singleThread[0]/1000.0} seconds) to calculate with a final sum of {singleThread[1]} for single threaded! A fun fact is it took {arrayTime} milliseconds to calculate the random numbers to fill into the array!");
                    Console.WriteLine("Would you like to calculate another?(Y/N)");
                    var userInput = Console.ReadLine();
                    userInput = userInput.ToLower();
                    if (userInput == "n")
                    {
                        Environment.Exit(0);
                    }
                    arrayHolder = null;  //These three are used to empty the arrays and lits to clean things up for the user
                    array = null;
                    singleThread = null;
                    GC.Collect();       //Sends garbage collection
                }
                catch (Exception)
                {
                    
                    Console.WriteLine("Please only enter postitive whole numbers!");

                }


            } while (true);
        }



        static double[] Sumcomputation(double[] arrayToCompute)
        {
            double[] sum = new double[2];
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (long i = 0; i < arrayToCompute.Length; i++)
            {
                sum[1] = sum[1] + arrayToCompute[i];
            }
            watch.Stop();
            sum[0] = watch.ElapsedMilliseconds;
            return sum;
        }
    }
}
