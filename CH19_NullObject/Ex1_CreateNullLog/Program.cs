﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_CreateNullLog
{
    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    public class NullLog : ILog
    {
        // todo
        public int RecordLimit { get; } = int.MaxValue;
        public int RecordCount { get; set; } = int.MinValue;
        public void LogInfo(string message)
        {
            ++RecordCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace Ex1_CreateNullLog.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingleCallTest()
        {
            var a = new Account(new NullLog());
            a.SomeOperation();
        }
    }


}


