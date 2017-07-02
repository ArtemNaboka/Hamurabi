using System;
using Hamurabi.Core.Objects.TurnHandlers;

namespace Hamurabi.ConsoleUI
{
    public class Program
    {
        private static void Main()
        {
            DefaultTurnHandler handler = new DefaultTurnHandler();
            handler.Initialize();
        }
    }
}