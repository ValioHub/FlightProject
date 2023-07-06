using System;
using Xunit;
using Domain;
using FluentAssertions;

namespace FlightProject
{
    public class FlightSpecification
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("email@email.com", 1);

            flight.RemainningNumberOfSeats.Should().Be(2);
        }
        [Fact]
        public void Avoids_overbooking()
        {
            // Given
            var flight = new Flight(seatCapacity: 3);

            // When
            var error = flight.Book("email@email.com", 4);

            // Then
            error.Should().BeOfType<OverbookingError>();
        }
        [Fact]
        public void Books_flights_successfully()
        {
            var flight = new Flight(seatCapacity: 3);
            var error = flight.Book("email@email.com", 1);
            error.Should().BeNull();
        }
    }
}
