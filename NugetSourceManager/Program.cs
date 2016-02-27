using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NugetSourceManager.SourceFile;

namespace NugetSourceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SourceFileBase sourceFile = DefaultSourceFile.GetInstace();

            foreach (var source in sourceFile.List())
            {
                Console.WriteLine(source);
            }

            Console.ReadLine();
        }
    }
}
