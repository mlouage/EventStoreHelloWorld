using System;

namespace EventStoreHelloWorld.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string UsaState { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"My name is {Firstname} {Lastname}. I have been assigned number {Number} and I live on {Address}";
        }
    }
}