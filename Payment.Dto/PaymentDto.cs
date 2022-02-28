// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;

namespace Payment.Dto
{   
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public DateTime CreateDate { get; set; }
        
        public string Message { get; set; }
    }
}
