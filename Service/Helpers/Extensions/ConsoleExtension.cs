using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Extensions
{
    public static class ConsoleExtension
    {
        public async static Task ConsoleMessage(this ConsoleColor color, string msj)
        {
            Console.ForegroundColor = color;
            await Console.Out.WriteLineAsync(msj);
            Console.ResetColor();
        }

    }
}
