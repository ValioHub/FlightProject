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

            flight.Book("email@email.com",1);

            flight.RemainningNumberOfSeats.Should().Be(2);
        }
    }
}
