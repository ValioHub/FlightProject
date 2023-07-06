using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Data;
using Domain;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Fact]
        public void Books_flights()
        {
            var entities = new Entities();

            var flight = new Flight(3);

            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passangerEmail: "email@email.com", numberOfSeats: 2
                ));

            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf( 
                new BookingRm(passangerEmail: "email@email.com", numberOfSeats: 2)
                );
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
