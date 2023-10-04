﻿namespace PeopleManager.Model
{
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Description { get; set; }
        public IList<Vehicle> ResponsibleForVehicles { get; set; } = new List<Vehicle>();
    }
}
