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
        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights").Options);
        readonly BookingService bookingService;
        public FlightApplicationSpecification()
        {
            bookingService = new BookingService(entities: entities);
        }
        [Theory]
        [InlineData("email@email.com", 2)]
        [InlineData("emailTwo@email.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
            var flight = new Flight(3);

            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats));
        }
        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void Cancels_booking(int initialCapacity)
        {
            // Given
            var flight = new Flight(initialCapacity);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flightId: flight.Id, passengerEmail: "email@email.com",
                numberOfSeats: 2));
            //When
            bookingService.CancelBooking(
                new CancelBookingDto(flightId: flight.Id, passengerEmail: "email@email.com",
                numberOfSeats:2));

            // Then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(initialCapacity);
        }
    }
}