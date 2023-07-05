using System;

namespace Domain
{
    public class Flight
    {
        public int RemainningNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainningNumberOfSeats = seatCapacity;
        }
        public object? Book(string passangerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainningNumberOfSeats)
            {
                return new OverbookingError();
            }

            RemainningNumberOfSeats -= numberOfSeats;
            return null;
        }
    }
}
