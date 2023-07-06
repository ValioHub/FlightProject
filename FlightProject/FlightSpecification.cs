using System;
using Xunit;
using Domain;
using FluentAssertions;

namespace FlightProject
{
    public class FlightSpecification
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6, 3, 3)]
        [InlineData(10, 6, 4)]
        [InlineData(12, 8, 4)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats,
            int remainningNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);

            flight.Book("email@email.com", numberOfSeats);

            flight.RemainingNumberOfSeats.Should().Be(remainningNumberOfSeats);
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
        [Fact]
        public void Remembers_bookings()
        {
            var flight = new Flight(seatCapacity: 150);

            flight.Book(passangerEmail: "email@email.com", numberOfSeats: 4);
            flight.BookingList.Should().ContainEquivalentOf(new Booking("email@email.com", 4));
        }
        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(4,2,2,4)]
        [InlineData(7, 5, 4, 6)]
        public void Canceling_booking_frees_up_the_seats(int initialCapacity, 
            int numberofSeatsTOBook, int numberOfSeatsToCancel, int remainingNumberOfSeats)
        {
            // Given
            var flight = new Flight(initialCapacity);
            flight.Book(passangerEmail: "email@email.com", numberOfSeats: numberofSeatsTOBook);
            
            // When
            flight.CancelBooking(passangerEmail: "email@email.com", numberOfSeats: numberOfSeatsToCancel);

            // Then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }
        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
        {
            var flight = new Flight(3);
            var error = flight.CancelBooking(passangerEmail: "email@email.com", numberOfSeats: 2);
            error.Should().BeOfType<BookingNotFoundError>();
        }
        [Fact]
        public void Returns_null_when_successfully_cancels_a_booking()
        {
            var flight = new Flight(3);
            flight.Book(passangerEmail: "email@email.com", numberOfSeats: 1);
            var error = flight.CancelBooking(passangerEmail: "email@email.com", numberOfSeats: 1);
            error.Should().BeNull();
        }
    }
}
