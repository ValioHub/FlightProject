using System;

namespace Domain.Tests
{
    public class Flight
    {
        public int RemainningNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainningNumberOfSeats = seatCapacity;
        }
        public void Book(string v1,int v2)
        {
            RemainningNumberOfSeats -= v2;
        }
    }
}
