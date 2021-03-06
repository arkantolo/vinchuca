﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using Vinchuca.REPL;

namespace Vinchuca
{
    public class ColorConsoleTraceListener : ConsoleTraceListener
    {
        private static readonly Dictionary<TraceEventType, ConsoleColor> EventColor = new Dictionary<TraceEventType, ConsoleColor>();
        private readonly VirtualConsole _console;

        static ColorConsoleTraceListener()
        {
            EventColor.Add(TraceEventType.Verbose, ConsoleColor.DarkGray);
            EventColor.Add(TraceEventType.Information, ConsoleColor.Gray);
            EventColor.Add(TraceEventType.Warning, ConsoleColor.Yellow);
            EventColor.Add(TraceEventType.Error, ConsoleColor.DarkRed);
            EventColor.Add(TraceEventType.Critical, ConsoleColor.Red);
            EventColor.Add(TraceEventType.Start, ConsoleColor.DarkCyan);
            EventColor.Add(TraceEventType.Stop, ConsoleColor.DarkCyan);
        }

        public ColorConsoleTraceListener(int identLevel)
        {
            _console = new VirtualConsole(22, Console.WindowHeight);
            _il = identLevel;
        }
 
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            TraceEvent(eventCache, source, eventType, id, "{0}", message);
        }
 
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            IndentLevel = _il;
            IndentSize = 2;
            //var originalColor = Console.ForegroundColor;
            //Console.ForegroundColor = GetEventColor(eventType, originalColor);
            _console.WriteLine(string.Format("{2} {0} {1}: {3}", source, eventType, DateTime.Now.TimeOfDay, string.Format(format, args)));
            //Console.ForegroundColor = originalColor;
        }
 
        private static ConsoleColor GetEventColor(TraceEventType eventType, ConsoleColor defaultColor)
        {
            return !EventColor.ContainsKey(eventType) ? defaultColor : EventColor[eventType];
        }

        public int _il { get; set; }
    }
}
