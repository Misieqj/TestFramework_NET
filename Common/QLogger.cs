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

        internal static void PrintHeader(string msg)
            => TestContext.Out.WriteLine($"╠═▶ {msg}");

        internal static void Print(string msg = "< ??? >")
        {
            var msgs = msg.Split([Environment.NewLine], StringSplitOptions.None);
            foreach (var m in msgs)
            {
                TestContext.Out.WriteLine($"║ {m}");
            }
        }

        internal static void PrintWithTimestamp(string msg)
            => TestContext.Out.WriteLine($"║ {DateTime.Now}: {msg}");

         //====================================================================
        /// <summary>
        /// Prints error message to the TestContext Error output.
        /// </summary>
        internal static void PrintError(string msg)
            => TestContext.Error.WriteLine($"=> {msg}");

        /// <summary>
        /// Prints the attachment information to the TestContext.
        /// </summary>
        internal static void PrintAttachmentInfo(string path, string description)
            => TestContext.AddTestAttachment(path, description);

         //====================================================================
        // to check and play :)
        internal static void PrintLinesToCheck()
        {
            TestContext.Out.WriteLine($"╠════════════════════════════════");
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
        }
    }
}
