# CLUI
Simple, Extendable and Customizable DotNet Command Line User Interface

Made just for fun

Usage example:
```
        static void Main(string[] args)
        {
            var script = new Clui(printer: System.Console.WriteLine);

            script
                .Print("Hello, I'm Clui!")
                .Print("You can use me to build command line user interface")
                .Menu("Main menu", new[]
                {
                    new SimpleCliMenuItem("Menu item 1",MenuItem1Handler),
                    new SimpleCliMenuItem("Menu item 2",MenuItem2Handler),
                    new SimpleCliMenuItem("Menu item 3", (menuItem, clui) => { clui.Print(menuItem.Label); })
                }, "Enter menu item number to invoke")
                .Menu(
                    "select i",
                    new[] {
                        new CluiMenuItem<int>("1", 1, (val, cli) => { cli.Print("Selected val is {0}", val); })
                    },
                    "Select val"
                )
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
```

