namespace TestFramework_NET.Common
{
    internal static class QLogger
    {
        // -> https://en.wikipedia.org/wiki/List_of_Unicode_characters
        // -> https://unicode-table.com/en/
        // -> https://www.coolsymbol.top/cool-triangle.html

        internal static void PrintStart()
            => TestContext.Out.WriteLine("╔═════════════════════════════════════════════════════════════════════════");

        internal static void PrintStartWithTcName()
        {
            TestContext.Out.WriteLine( "╔═════════════════════════════════════════════════════════════════════════");
            TestContext.Out.WriteLine($"╠═▶▷ {TestContext.CurrentContext.Test.Name} 🖉");
            TestContext.Out.WriteLine( "║");
        }

        internal static void PrintEnd()
            => TestContext.Out.WriteLine("╚═════════════════════════════════════════════════════════════════════════");

        internal static void Print(string msg)
            => TestContext.Out.WriteLine($"║ {msg}");

        internal static void PrintWithTimeStamp(string msg)
            => TestContext.Out.WriteLine($"║ {DateTime.Now}: {msg}");

        // For test results
        internal static void PrintAttachmentInfo(string path, string description)
            => TestContext.AddTestAttachment(path, description);
        internal static void PrintError(string msg)
            => TestContext.Error.WriteLine($"=> {msg}");

        // to check and play :)
        internal static void PrintLinesToCheck()
        {
            Console.WriteLine("Deafult colors");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Green / Magenta");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Red / Cyan");
            Console.ResetColor();
            Console.WriteLine("After reset colors");
            TestContext.Progress.WriteLine("║ Progress");
            TestContext.Error.WriteLine("║ Error");
            TestContext.Error.WriteLine("║ Error");
        }
    }
}
