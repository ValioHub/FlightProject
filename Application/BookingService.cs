﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            Entities = entities;
        }
        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            Entities.SaveChanges();
        }
        public IEnumerable<BookingRm> FindBooking(Guid flightId)
        {
            return Entities.Flights.Find(flightId).BookingList.Select(booking =>
            new BookingRm(booking.Email, booking.NumberOfSeats));
        }

        public void CancelBooking(CancelBookingDto cancelBookingDto)
        {
            throw new NotImplementedException();
        }

        public object GetRemainingNumberOfSeatsFor(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
