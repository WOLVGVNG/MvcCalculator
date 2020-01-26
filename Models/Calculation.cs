using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcCalculator.Models
{
    [Table("Calculations")]

    public class Calculation
    {
        public uint Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public float Number1 { get; set; }
        [Required]
        public float Number2 { get; set; }
        [Required]
        public string Operator { get; set; }

        public float Result
        {
            get
            {
                switch (Operator)
                {
                    case "+": 
                        return Number1 + Number2;
                    case "-": 
                        return Number1 - Number2;
                    case "*": 
                        return Number1 * Number2;
                    case "/": 
                        return Number1 / Number2;
                    default:
                        return 0;
                }
            }
        }
    }
}