using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Application.Tests;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Theory]
        [InlineData("email@email.com", 2)]
        [InlineData("emailTwo@email.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
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
}