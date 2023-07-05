using System;
using Xunit;
using Domain.Tests;
using FluentAssertions;

namespace FlightProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("email@email.com",1);

            flight.RemainningNumberOfSeats.Should().Be(2);
        }
    }
}
