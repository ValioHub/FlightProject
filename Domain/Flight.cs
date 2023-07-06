using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Flight
    {
        List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;
        public int RemainingNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public object? Book(string passangerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainingNumberOfSeats)
            {
                return new OverbookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;

            bookingList.Add(new Booking(passangerEmail, numberOfSeats));

            return null;
        }

        public object? CancelBooking(string passangerEmail, int numberOfSeats)
        {
            if (!bookingList.Any(booking => booking.Email == passangerEmail))
            {
                return new BookingNotFoundError();
            }

            RemainingNumberOfSeats += numberOfSeats;

            return null;
        }
    }
}
