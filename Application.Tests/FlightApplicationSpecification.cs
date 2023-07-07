using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Theory]
        [InlineData("email@email.com",2)]
        [InlineData("emailTwo@email.com", 2)]
        public void Books_flights(string passangerEmail, int numberOfSeats )
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights").Options);

            var flight = new Flight(3);

            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passangerEmail, numberOfSeats));

            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf( 
                new BookingRm(passangerEmail, numberOfSeats));
        }
    }
    public class BookingService
    {
        public BookingService(Entities entities)
        {

        }
        public void Book(BookDto bookDto)
        {

        }
        public IEnumerable<BookingRm> FindBooking(Guid flightId)
        {
            return new[]
            {
                new BookingRm(passangerEmail: "email@email.com", numberOfSeats: 2)
            };
        }
    }
    public class BookDto
    {
        public BookDto(Guid flightId, string passangerEmail, int numberOfSeats)
        {

        }
    }
    public class BookingRm
    {
        public string PassangerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRm(string passangerEmail, int numberOfSeats)
        {
            this.PassangerEmail = passangerEmail;
            this.NumberOfSeats = numberOfSeats;
        }
    }
}
