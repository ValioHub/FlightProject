using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Theory]
        [InlineData("email@email.com",2)]
        [InlineData("emailTwo@email.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats )
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights").Options);

            var flight = new Flight(3);

            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf( 
                new BookingRm(passengerEmail, numberOfSeats));
        }
    }
    public class BookingService
    {
        public Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            this.Entities = entities;
        }
        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            Entities.SaveChanges();
        }
        public IEnumerable<BookingRm> FindBooking(Guid flightId)
        {
            return Entities.Flights.Find(flightId).BookingList.Select(booking =>
            new BookingRm(booking.Email, booking.NumberOfSeats));
        }
    }
    public class BookDto
    {
        public Guid FlightId { get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            this.FlightId = flightId;
            this.PassengerEmail = passengerEmail;
            this.NumberOfSeats = numberOfSeats;
        }
    }
    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            this.PassengerEmail = passengerEmail;
            this.NumberOfSeats = numberOfSeats;
        }
    }
}
