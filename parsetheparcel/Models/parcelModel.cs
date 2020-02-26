using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace parsetheparsel.Models
{
    public class parcelModel
    {
        public parcelModel()
        {

        }
        [Required(ErrorMessage = "Please enter Parcel Length.")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Please enter Parcel Breadth.")]
        public int Breadth { get; set; }

        [Required(ErrorMessage = "Please enter Parcel Height.")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Please enter Parcel Weight.")]
        public int Weight { get; set; }

        public parcelModel(int pLength, int pBreadth, int pHeight)
        {
            Length = pLength;
            Breadth = pBreadth;
            Height = pHeight;
        }

    }
}