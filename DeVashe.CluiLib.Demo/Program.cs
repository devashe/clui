using System.Threading.Tasks;

namespace DeVashe.CluiLib.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var script = new CluiLib.Clui(printer: System.Console.WriteLine);

            script
                .Print("Hello, I'm Clui!")
                .Print("You can use me to build command line user interface")
                .Menu("Main menu", new[]
                {
                    new SimpleCliMenuItem("Menu item 1",MenuItem1Handler),
                    new SimpleCliMenuItem("Menu item 2",MenuItem2Handler),
                    new SimpleCliMenuItem("Menu item 3", (input, clui) => { clui.Print(input.Label); })
                }, "Enter menu item number to invoke")
                .Exec(Hello)
                .Exec(f => { System.Console.WriteLine("I'm inline delegate 2"); })
                .Ask("Select option", (ans, cli) =>
                {
                    cli.Set("i", ans);
                })
                .Print("Thanks")
                .Exec(f => f.Print($"i1={script.Get("i")}"))
                .Exec(f =>
                {
                    for (var i = 0; i < 3; i++)
                    {
                        f.Print("i={0}", i);
                    }
                })
                .Print("Async wait 5 secs")
                .AsyncAwait(() => Task.Delay(5000))
                .Print("Async await 2")
                .AsyncAwait(async () => await Task.Delay(5000))
                .Exec(LoopExample)
                .Print("Scope example. I now {0}", script.Get("i"))
                .Scope(newScope =>
                {
                    newScope
                        .Print("Hi, I'm new scope")
                        .Set("i", 555)
                        .Print("i in new scope is {0}", newScope.Get("i"))
                        ;
                })
                .Print("Returned to main scope. i now {0}", script.Get("i"))
                .Print("Bye!")
                ;
        }

        static void MenuItem1Handler(SimpleCliMenuItem item, CluiLib.Clui cli)
        {
            cli.Ask("Enter something", (input, cli) =>
            {
                cli.Print("You entered {0}", input);
            });
        }
        static void MenuItem2Handler(SimpleCliMenuItem item, CluiLib.Clui cli)
        {
            cli.Print("Hello from {0}", nameof(MenuItem2Handler));
        }

        static void LoopExample(CluiLib.Clui cli)
        {
            cli
                .LoopUntil(f =>
                {
                    f
                        .Print("1. Edit profile")
                        .Print("2. Set avatar")
                        .Print("3. Exit")
                        .Ask("Enter action number", (input, cli) =>
                        {
                            switch (input)
                            {
                                case "1":
                                    EditProfile(cli);
                                    break;
                                case "3":
                                    cli.Set("break", "1");
                                    break;
                            }
                        });
                }, ctx => (string)ctx.Get("break") == "1");
            ;
        }

        static CluiLib.Clui EditProfile(CluiLib.Clui cli)
        {
            return cli
                .Ask("Enter first name", (input, cl) => cl.Set("firstName", input))
                .Ask("Enter last name", (input, cl) => cl.Set("lastName", input))
                .Print("We will now call you {0} {1}", cli.Get("firstName"), cli.Get("lastName"))
                ;
        }

        static void Hello(CluiLib.Clui cli)
        {
            cli.Print("Hello!");
        }

    }
}
