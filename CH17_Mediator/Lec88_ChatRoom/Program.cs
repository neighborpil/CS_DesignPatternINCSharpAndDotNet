﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Lec88_ChatRoom
{
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

        public Person(string name)
        {
            Name = name;
        }

        public void Say(string message)
        {
            Room.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatLog.Add(s);
            WriteLine($"[{Name}'s chat session] {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new List<Person>();

        public void Join(Person newPerson)
        {
            string joinMsg = $"{newPerson.Name} joins the chat";
            Broadcast("room", joinMsg);
            //people.ForEach(person => Broadcast(person.Name, joinMsg));
            newPerson.Room = this;
            people.Add(newPerson);
        }

        public void Broadcast(string source, string message)
        {
            foreach (var p in people)
                if (p.Name != source)
                    p.Receive(source, message);
        }
        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            john.Say("hey, john");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone");
            jane.PrivateMessage("Simon", "gald you to join us");
        }
    }
}
