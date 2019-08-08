using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Combo_Tool
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Combo Tool | By Vape & LevensLes";

            Console.Write("Drag the folder in containing the combos: ");

            string text = Console.ReadLine();
            text = text.Replace("\"", "");
            if (!Directory.Exists(text))
            {
                Console.WriteLine("Directory could not be found!");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Combining...");
            Console.ForegroundColor = ConsoleColor.Gray;

            int num = 0;
            int num2 = 0;
            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.txt");
            using (StreamWriter streamWriter = new StreamWriter(outputfile))
            {
                foreach (string path in Directory.GetFiles(text, "*.txt"))
                {
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        string value;
                        while ((value = streamReader.ReadLine()) != null)
                        {
                            streamWriter.WriteLine(value);
                            num2++;
                        }
                        num++;
                        Console.WriteLine("Completed \"" + Path.GetFileName(path) + "\".");
                    }
                }
            }
            Console.WriteLine("Completed combining " + num + " Combos");

            string input = outputfile;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Filtering...");
            Console.ForegroundColor = ConsoleColor.Gray;
            List<string> lines = new List<string>();



            foreach (string Line in File.ReadLines(input).AsParallel())
            {

                lines.Add(Line);
            }
            Console.WriteLine("Original amount of lines: " + lines.Count.ToString());
            List<string> list = lines.Distinct<string>().ToList<string>();
            Console.WriteLine("New amount of lines: " + list.Count.ToString());
            int math = lines.Count - list.Count;
            Console.WriteLine("Removed " + math + " lines");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sorting...");
            Console.ForegroundColor = ConsoleColor.Gray;

            List<string> UkList = new List<string>();
            List<string> USLists = new List<string>();
            List<string> other = new List<string>();

            foreach (string value in list)
            {
                // big yucky gay code dont look pls
                if (value.Contains("@talktalk.net") || value.Contains("@tiscali.co.uk") || value.Contains("@screaming.net") || value.Contains("@lineone.net") || value.Contains("@ukgateway.net") || value.Contains("@tinyonline.co.uk") || value.Contains("@tinyworld.co.uk") || value.Contains("@blueyonder.co.uk") || value.Contains("@virginmedia.com") || value.Contains("@ntlworld.com") || value.Contains("@homechoice.co.uk") || value.Contains("@btinternet.com"))
                {

                    UkList.Add(value);
                    other.Add(value);
                }

                if (value.Contains("@aol.com") || value.Contains("@outlook.com") || value.Contains("@mail.com") || value.Contains("@yahoo.com"))
                {

                    USLists.Add(value);
                    other.Add(value);
                }

            }

            TextWriter UkWriter = new StreamWriter(input + " - UK Filtered.txt");
            foreach (string mail in UkList)
            {

                UkWriter.WriteLine(mail);
            }

            TextWriter USWriter = new StreamWriter(input + " - US Filtered.txt");
            foreach (string mail in USLists)
            {

                USWriter.WriteLine(mail);
            }

            TextWriter OtherWriter = new StreamWriter(input + " -Other Filtered.txt");
            foreach (string mail in other)
            {

                OtherWriter.WriteLine(mail);
            }

            int totalothermails = UkList.Count() + USLists.Count();
            int othermails = list.Count() - totalothermails;
            Console.WriteLine("Found " + UkList.Count() + " UK emails");
            Console.WriteLine("Found " + USLists.Count() + " US emails");
            Console.WriteLine("Found " + othermails.ToString() + " other emails");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Done");
            Console.ReadLine();


        }
    }
}
