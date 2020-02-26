using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using parsetheparsel.Models;

namespace parsetheparsel.Controllers
{
    public class parcelcheckController : Controller
    {
        decimal price = 0;
        int _maxweight = 25;
        
        // Dictionary for storing the size set of small, medium and large
        private Dictionary<string, parcelModel> _parcelSize = new Dictionary<string, parcelModel>();
       
        //constructor
       public parcelcheckController()
        {

        }
        //adding size dimensions to dictionary
        public void standardparcelsize()
        {
            _parcelSize.Add("Small", new parcelModel(200, 300, 150));
            _parcelSize.Add("Medium", new parcelModel(300, 400, 200));
            _parcelSize.Add("Large", new parcelModel(400, 600, 250));

        }

        //the view for getting L,B,H and weight of parcel from user.
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //main method with parameters
        [HttpPost]
        public ActionResult Index(parcelModel datacheck)
        {
            
            if (ModelState.IsValid)
            {
                int _length = datacheck.Length;
                int _breadth = datacheck.Breadth;
                int _height = datacheck.Height;
                int _weight = datacheck.Weight;
                decimal checkcost = 0;
                
                checkcost = CalculateParcelCost(_length, _breadth, _height, _weight);
                
                if (checkcost > 0)
                {
                    ViewBag.finalmessage = "The price of the parcel is $" + checkcost;
                }
                else
                {
                    ViewBag.finalmessage = "There is some issue with the parcel";
                }
            }
            return View("Index", datacheck);
        }

        // to calculate cost of parcel according to weight and dimensions
        public decimal CalculateParcelCost(int lth, int bth, int hth, int wth)
        {
            try
            {
                if (checkparcelweight(wth))
                {
                    standardparcelsize();
                    if (checkforgreaterthanzero(lth, bth, hth))
                    {
                        if (lth <= _parcelSize["Small"].Length &&
                           bth <= _parcelSize["Small"].Breadth &&
                           hth <= _parcelSize["Small"].Height)
                        {
                            price = 5.00M;
                            ViewBag.parcelcost="The size of parcel is Small";
                        }
                        else if (lth <= _parcelSize["Medium"].Length &&
                                 bth <= _parcelSize["Medium"].Breadth &&
                                hth <= _parcelSize["Medium"].Height)
                        {
                            price = 7.50M;
                            ViewBag.parcelcost="The size of parcel is Medium";
                        }
                        else if (lth <= _parcelSize["Large"].Length &&
                                 bth <= _parcelSize["Large"].Breadth &&
                                 hth <= _parcelSize["Large"].Height)
                        {
                            price = 8.50M;
                            ViewBag.parcelcost="The size of parcel is Large";
                        }
                        else
                        {
                           ViewBag.parcelcost="The size of parcel is not valid.<br> For small parcel it should be less than or equal to (200 X 300 X 150)*LxBxH <br> For Medium parcel it should be less than or equal to (300 X 400 X 200)*LxBxH <br> For Large parcel it should be less than or equal to (400 X 600 X 250)*LxBxH ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured {0}", ex.Message);
                throw;
            }
            return price;
        }

        //for checking parcel weight
        public bool checkparcelweight(int weight)
        {
            if (weight > 0 && weight <= _maxweight)
            {
                return true;
            }
            else
            {
                ViewBag.Weight = "Sorry we are unable to parcel over 25kg";
                return false;
            }
                
        }

        //for checking dimension greater than zero
        private bool checkforgreaterthanzero(int length,int breadth,int height)
        {
            checklength(length);
            checkbreadth(breadth);
            checkheight(height);
            if (checklength(length) && checkbreadth(breadth) && checkheight(height))
            {
                return true;
            }
            else
            {
                
                return false;
            } 
           

        }

        //checking length greater than 0
        private bool checklength(int length)
        {
            if (length > 0 || length.ToString() == null)
            {
                return true;
            }
            else
            {
                ViewBag.Length = "Length cannot be empty and less than or equal to 0";
                return false;
            }
        }

        //checking breadth greater than 0
        private bool checkbreadth(int breadth)
        {
            if (breadth > 0 || breadth.ToString() == null)
            {
                return true;
            }
            else
            {
                ViewBag.Breadth = "Breadth cannot be empty and less than or equal to 0";
                return false;
            }
        }

        //checking height greater than 0
        private bool checkheight(int height)
        {
            if (height > 0 || height.ToString() == null)
            {
                return true;
            }
            else
            {
                ViewBag.Height = "Height cannot be empty and less than or equal to 0";
                return false;
            }
        }

    }
}