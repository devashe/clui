using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeVashe.CluiLib
{
    public partial class Clui : IDisposable
    {
        private readonly Action<string, object[]> _printer;
        public event EventHandler<string> OnMessage;
        private readonly Dictionary<string, object> _context = new Dictionary<string, object>();

        public Clui(Action<string, object[]> printer)
        {
            _printer = printer;
        }

        /// <summary>
        /// Raises OnEvent event with specified message text
        /// </summary>
        /// <param name="message"></param>
        public virtual void Notify(string message)
        {
            if (OnMessage != null)
            {
                OnMessage(this, message);
            }
        }

        /// <summary>
        /// Prints a message to a printer
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Clui Print(string message, params object[] args)
        {
            _printer(message, args);
            return this;
        }

        /// <summary>
        /// Executes a delegate
        /// </summary>
        /// <param name="whatToDo"></param>
        /// <returns></returns>
        public virtual Clui Exec(Action<Clui> whatToDo)
        {
            whatToDo(this);
            return this;
        }

        /// <summary>
        /// Prints message and waiting for user input, then calls handler passing it received user input
        /// </summary>
        /// <param name="message"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public virtual Clui Ask(string message, Action<string, Clui> handler)
        {
            Console.Write($"{message}: ");
            var input = Console.ReadLine();
            handler(input, this);
            return this;
        }

        /// <summary>
        /// Loops body until cycleBreaker delegate returns false
        /// </summary>
        /// <param name="loopBody"></param>
        /// <param name="cycleBreaker"></param>
        /// <returns></returns>
        public virtual Clui LoopUntil(Action<Clui> loopBody, Func<Clui, bool> cycleBreaker)
        {
            while (!cycleBreaker(this))
            {
                loopBody(this);
            }

            return this;
        }
        /// <summary>
        /// Executes delegate in a new instanse of Clui
        /// </summary>
        /// <param name="scopeBody"></param>
        /// <returns></returns>
        public Clui Scope(Action<Clui> scopeBody)
        {
            scopeBody(new Clui(_printer));
            return this;
        }

        /// <summary>
        /// Sets a value for a context key
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public virtual Clui Set(string name, object value)
        {
            if (_context.ContainsKey(name))
            {
                _context[name] = value;
            }
            else
            {
                _context.Add(name, value);
            }
            return this;
        }

        /// <summary>
        /// Gets a value from context by key and calls handler with that value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public virtual Clui Get(string name, Action<object> handler)
        {
            var val = Get(name);
            handler(val);
            return this;
        }

        /// <summary>
        /// Returns a value from context by key
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual object Get(string name)
        {
            _context.TryGetValue(name, out var val);
            return val;
        }

        /// <summary>
        /// Awaits async delegate
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Clui AsyncAwait(Func<Task> task)
        {
            task().GetAwaiter().GetResult();
            return this;
        }
        public Clui Menu<T>(string title, CluiMenuItem<T>[] menuItems, string chooseMenuItemMessage)
        {

            Print(title);

            bool shouldExit = false;

            while (!shouldExit)
            {
                int counter = 0;
                foreach (var menuItem in menuItems)
                {
                    Print("{0}. {1}", counter, menuItem.Label);
                    counter++;
                }

                Print("ENTER - exit");

                Ask(chooseMenuItemMessage, (input, cli) =>
                {

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        shouldExit = true;
                    }
                    else
                    {
                        if (!int.TryParse(input, out var numberFromInput)) return;
                        if (menuItems.Length <= numberFromInput) return;

                        var selectedMenuItem = menuItems[numberFromInput];
                        selectedMenuItem.Handler(selectedMenuItem.Data, this);
                    }

                });

            }

            return this;
        }
        public Clui Menu(string title, SimpleCliMenuItem[] menuItems, string chooseMenuItemMessage)
        {

            Print(title);

            bool shouldExit = false;

            while (!shouldExit)
            {
                int counter = 0;
                foreach (var menuItem in menuItems)
                {
                    Print("{0}. {1}", counter, menuItem.Label);
                    counter++;
                }

                Print("ENTER - exit");

                Ask(chooseMenuItemMessage, (input, cli) =>
                {

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        shouldExit = true;
                    }
                    else
                    {
                        if (!int.TryParse(input, out var numberFromInput)) return;
                        if (menuItems.Length <= numberFromInput) return;

                        var selectedMenuItem = menuItems[numberFromInput];
                        selectedMenuItem.Handler(selectedMenuItem, this);
                    }

                });

            }

            return this;
        }


        public void Dispose()
        {
        }
    }
}