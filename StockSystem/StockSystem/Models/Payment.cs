﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.DAL.Models
{
    //TODO rozdelil jsem payment na payment info a pak jednotlive platby, kdzytak to checkni jestli to tak sedi
    public class Payment
    { 
        public int Id { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public int Amount { get; set; }

        public int PaymentInfoId { get; set; }
        [Required]
        public virtual PaymentInfo PaymentInfo { get; set; }
    }
}
