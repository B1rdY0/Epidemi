using System;

namespace Epidemi;

public class Epidemi
{
    public static PandeminForm form = new PandeminForm();

    [STAThread]
    public static void Main(string[] args)
    {
        //RunConsoleVersion();
        Application.Run(form);
    }

    private static void RunConsoleVersion()
    {
        int N = ReadIntValueFromConsole("Skriv in populationen");
        int S0 = ReadIntValueFromConsole("Skriv in hur många som är sjuka på den första veckan");
        int d = ReadIntValueFromConsole("Sjukdomens varaktighet i veckor");
        float k = ReadFloatValueFromConsole("Hur lätt man blir sjuk");
        //TODO - Fel hantering

        float M = N, I = 0, S = S0; //M = N eftersom antalet som kan bli sjuka och populationen i start är samma 
        Console.WriteLine("M\tS\tI");
        Console.WriteLine(M + "\t" + S + "\t" + I); //skriver ut värderna för dem värden 
        int weekNumber = 0; //start värdet för veckorna 

        while (true)
        {
            float mOld = M; // Där den äldre värdet på M = nya M
            float sOld = S; //Samma här
            weekNumber++; //Lägger en ny vecka

            M = mOld - (k * sOld * mOld);  //Moellen för att räkna ut nya M
            S = S + (k * sOld * mOld) - (sOld / d); //Modellen för att räkna ut nya antalet sjuka
            I = I + (sOld / d); // Modellen för antalet immuna efter varje vecka 

            Console.WriteLine(M + "\t" + S + "\t" + I); //Skriver ut vad varje värde är på varje rad
            if (S < 1) //Om antalet smittade är mindre än 1 så är pandemin(epidemin) över
                break;
        }
        Console.WriteLine("Pandemin är slut efter " + weekNumber + "weeks");
    }

    private static int ReadIntValueFromConsole(string msg) //Den läser värderna i programmet så att man inte skriver fel
    {
        do
        {
            try
            {
                Console.WriteLine(msg);
                string? temp = Console.ReadLine();
                if (temp != null)
                {
                    int returnValue = 0;
                    bool success = Int32.TryParse(temp, out returnValue); 
                    if (success && (returnValue > 0))
                        return returnValue;
                }
            }
            catch (Exception) { }
            Console.WriteLine("Fel värde. Försöka igen");
        } while (true);
    }

    private static float ReadFloatValueFromConsole(string msg) //Gör samma sak som förra förutom att det är floats och inte ints
    {
        do
        {
            try
            {
                Console.WriteLine(msg);
                string? temp = Console.ReadLine();
                if (temp != null)
                {
                    float returnValue = 0;
                    bool success = float.TryParse(temp, out returnValue); //Om floaten funkar så kommer den att skicka ut en value
                    if (success && (returnValue > 0.0F))
                        return returnValue;
                }
            }
            catch (Exception) { }
            Console.WriteLine("Fel värde. Försöka igen");
        } while (true);
    }
}