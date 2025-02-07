// ask for input
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
string? resp;
do{
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
resp = Console.ReadLine();
if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");
    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");

        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    if(File.Exists("data.txt")) {
        StreamReader sr = new StreamReader("data.txt");
        while (!sr.EndOfStream) {
            string? line = sr.ReadLine();
            //create array to store each part of first split
            string[] parts = line.Split(',');
            //Parse and store week as DateTime
            DateTime weekStart = DateTime.Parse(parts[0]);
            //Store data as string. I could not figure out how to convert to double then store in a double array in the same line
            string[] hoursSlept = parts[1].Split('|');
            //create total count (gets reset to 0 each line)
            double total = 0;
                for(int j =0; j < 7; j++) { total += Convert.ToDouble(hoursSlept[j]); }
            //Write "week of ___" message
            Console.WriteLine($"Week of {weekStart:MMM}, {weekStart:dd}, {weekStart:yyyy}");
            //Formatted days of week
            Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}{7,4}{8,5}","Su","Mo","Tu","We","Th","Fr","Sa","Tot","Avg");
            //Formatted --
            Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}{7,4}{8,5}","--","--","--","--","--","--","--","---","---");
            //Formatted data
            //Technically different than example since I added 5 characters to avg formatting instead of 4 because the average can be 4 characters including the decimal
            //With 4 characters it caused there to be no space between tot and avg numbers making it look like one big number
            Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}{7,4}{8,5:F1}\n", hoursSlept[0], hoursSlept[1], hoursSlept[2], hoursSlept[3], hoursSlept[4], hoursSlept[5], hoursSlept[6], total, total/7);
        }
    } else Console.WriteLine("File not found. Please create data file before parsing data.");
}
} while (resp == "1"||resp == "2");
Console.WriteLine("Goodbye");