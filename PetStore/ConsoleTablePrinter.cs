using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore
{
    public class ConsoleTablePrinter
    {
        public static void PrintWelcomeScreen()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================");
            Console.WriteLine("=   Hi! Welcome to Our Pet Store!              =");
            Console.WriteLine("=   Press any key to see available pets.   =");
            Console.WriteLine("=   Or type (all) see all available pets.   =");
            Console.WriteLine("================================================");            
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning! Swagger pet store api may be compromised. So please go to https://github.com/swagger-api/swagger-petstore/blob/master/README.md " +
                "to pull and run the docker Image locally using V3 API and input the local base url in the prompt. Otherwise input https://petstore.swagger.io/v2/pet/ at your own risk");
            Console.ResetColor();
            Console.Write("Enter BaseUrl (Example:http://localhost:8080/api/v3/pet/): ") ;
        }
        public static void WriteTableHeaders()
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-10}", "Category", "Name", "Status", "Tags");
            Console.WriteLine(new string('-', 80)); // Table separator
        }
   
    }
}
