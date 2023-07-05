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
        public void Book(string passangerEmail, int numberOfSeats)
        {
            RemainningNumberOfSeats -= numberOfSeats;
        }
    }
}
