using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB2022_ZZFashion.DAL;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.Controllers
{
    public class SalesPersonnelController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();



        // GET: SalesPersonnel
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult ViewCustomer()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }


            return ViewCustomer("");
        }

        public ActionResult CreateCustomer()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["CountryList"] = GetCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Customer customer)
        {
            ViewData["CountryList"] = GetCountries();
            if (ModelState.IsValid)
            {
                //Add staff record to database
                string memberid = customerContext.Add(customer);
                return RedirectToAction("PendingVoucher", new { id = memberid });
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(customer);
            }
        }

        
        

        

        [HttpPost]
        public ActionResult ViewCustomer(string search)
        {
            if (search == null || search.Trim() == "")
            {
                search = "";
            }
            List<SPCustomerViewModel> customerPreviewList = customerContext.Search(search.Trim());
            return View(customerPreviewList);
        }

        private List<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Add(new SelectListItem { Value = "", Text = "--Select--" });
            countries.Add(new SelectListItem { Value = "Afghanistan", Text = "Afghanistan" });
            countries.Add(new SelectListItem { Value = "Aland Islands", Text = "Aland Islands" });
            countries.Add(new SelectListItem { Value = "Albania", Text = "Albania" });
            countries.Add(new SelectListItem { Value = "Algeria", Text = "Algeria" });
            countries.Add(new SelectListItem { Value = "American Samoa", Text = "American Samoa" });
            countries.Add(new SelectListItem { Value = "Andorra", Text = "Andorra" });
            countries.Add(new SelectListItem { Value = "Angola", Text = "Angola" });
            countries.Add(new SelectListItem { Value = "Anguilla", Text = "Anguilla" });
            countries.Add(new SelectListItem { Value = "Antarctica", Text = "Antarctica" });
            countries.Add(new SelectListItem { Value = "Antigua and Barbuda", Text = "Antigua and Barbuda" });
            countries.Add(new SelectListItem { Value = "Argentina", Text = "Argentina" });
            countries.Add(new SelectListItem { Value = "Armenia", Text = "Armenia" });
            countries.Add(new SelectListItem { Value = "Aruba", Text = "Aruba" });
            countries.Add(new SelectListItem { Value = "Australia", Text = "Australia" });
            countries.Add(new SelectListItem { Value = "Austria", Text = "Austria" });
            countries.Add(new SelectListItem { Value = "Azerbaijan", Text = "Azerbaijan" });
            countries.Add(new SelectListItem { Value = "Bahamas", Text = "Bahamas" });
            countries.Add(new SelectListItem { Value = "Bahrain", Text = "Bahrain" });
            countries.Add(new SelectListItem { Value = "Bangladesh", Text = "Bangladesh" });
            countries.Add(new SelectListItem { Value = "Barbados", Text = "Barbados" });
            countries.Add(new SelectListItem { Value = "Belarus", Text = "Belarus" });
            countries.Add(new SelectListItem { Value = "Belgium", Text = "Belgium" });
            countries.Add(new SelectListItem { Value = "Belize", Text = "Belize" });
            countries.Add(new SelectListItem { Value = "Benin", Text = "Benin" });
            countries.Add(new SelectListItem { Value = "Bermuda", Text = "Bermuda" });
            countries.Add(new SelectListItem { Value = "Bhutan", Text = "Bhutan" });
            countries.Add(new SelectListItem { Value = "Bolivia", Text = "Bolivia" });
            countries.Add(new SelectListItem { Value = "Bonaire, Sint Eustatius and Saba", Text = "Bonaire, Sint Eustatius and Saba" });
            countries.Add(new SelectListItem { Value = "Bosnia and Herzegovina", Text = "Bosnia and Herzegovina" });
            countries.Add(new SelectListItem { Value = "Botswana", Text = "Botswana" });
            countries.Add(new SelectListItem { Value = "Bouvet Island", Text = "Bouvet Island" });
            countries.Add(new SelectListItem { Value = "Brazil", Text = "Brazil" });
            countries.Add(new SelectListItem { Value = "British Indian Ocean Territory", Text = "British Indian Ocean Territory" });
            countries.Add(new SelectListItem { Value = "Brunei Darussalam", Text = "Brunei Darussalam" });
            countries.Add(new SelectListItem { Value = "Bulgaria", Text = "Bulgaria" });
            countries.Add(new SelectListItem { Value = "Burkina Faso", Text = "Burkina Faso" });
            countries.Add(new SelectListItem { Value = "Burundi", Text = "Burundi" });
            countries.Add(new SelectListItem { Value = "Cambodia", Text = "Cambodia" });
            countries.Add(new SelectListItem { Value = "Cameroon", Text = "Cameroon" });
            countries.Add(new SelectListItem { Value = "Canada", Text = "Canada" });
            countries.Add(new SelectListItem { Value = "Cape Verde", Text = "Cape Verde" });
            countries.Add(new SelectListItem { Value = "Cayman Islands", Text = "Cayman Islands" });
            countries.Add(new SelectListItem { Value = "Central African Republic", Text = "Central African Republic" });
            countries.Add(new SelectListItem { Value = "Chad", Text = "Chad" });
            countries.Add(new SelectListItem { Value = "Chile", Text = "Chile" });
            countries.Add(new SelectListItem { Value = "China", Text = "China" });
            countries.Add(new SelectListItem { Value = "Christmas Island", Text = "Christmas Island" });
            countries.Add(new SelectListItem { Value = "Cocos (Keeling) Islands", Text = "Cocos (Keeling) Islands" });
            countries.Add(new SelectListItem { Value = "Colombia", Text = "Colombia" });
            countries.Add(new SelectListItem { Value = "Comoros", Text = "Comoros" });
            countries.Add(new SelectListItem { Value = "Congo", Text = "Congo" });
            countries.Add(new SelectListItem { Value = "Congo, Democratic Republic of the Congo", Text = "Congo, Democratic Republic of the Congo" });
            countries.Add(new SelectListItem { Value = "Cook Islands", Text = "Cook Islands" });
            countries.Add(new SelectListItem { Value = "Costa Rica", Text = "Costa Rica" });
            countries.Add(new SelectListItem { Value = "Cote D'Ivoire", Text = "Cote D'Ivoire" });
            countries.Add(new SelectListItem { Value = "Croatia", Text = "Croatia" });
            countries.Add(new SelectListItem { Value = "Cuba", Text = "Cuba" });
            countries.Add(new SelectListItem { Value = "Curacao", Text = "Curacao" });
            countries.Add(new SelectListItem { Value = "Cyprus", Text = "Cyprus" });
            countries.Add(new SelectListItem { Value = "Czech Republic", Text = "Czech Republic" });
            countries.Add(new SelectListItem { Value = "Denmark", Text = "Denmark" });
            countries.Add(new SelectListItem { Value = "Djibouti", Text = "Djibouti" });
            countries.Add(new SelectListItem { Value = "Dominica", Text = "Dominica" });
            countries.Add(new SelectListItem { Value = "Dominican Republic", Text = "Dominican Republic" });
            countries.Add(new SelectListItem { Value = "Ecuador", Text = "Ecuador" });
            countries.Add(new SelectListItem { Value = "Egypt", Text = "Egypt" });
            countries.Add(new SelectListItem { Value = "El Salvador", Text = "El Salvador" });
            countries.Add(new SelectListItem { Value = "Equatorial Guinea", Text = "Equatorial Guinea" });
            countries.Add(new SelectListItem { Value = "Eritrea", Text = "Eritrea" });
            countries.Add(new SelectListItem { Value = "Estonia", Text = "Estonia" });
            countries.Add(new SelectListItem { Value = "Ethiopia", Text = "Ethiopia" });
            countries.Add(new SelectListItem { Value = "Falkland Islands (Malvinas)", Text = "Falkland Islands (Malvinas)" });
            countries.Add(new SelectListItem { Value = "Faroe Islands", Text = "Faroe Islands" });
            countries.Add(new SelectListItem { Value = "Fiji", Text = "Fiji" });
            countries.Add(new SelectListItem { Value = "Finland", Text = "Finland" });
            countries.Add(new SelectListItem { Value = "France", Text = "France" });
            countries.Add(new SelectListItem { Value = "French Guiana", Text = "French Guiana" });
            countries.Add(new SelectListItem { Value = "French Polynesia", Text = "French Polynesia" });
            countries.Add(new SelectListItem { Value = "French Southern Territories", Text = "French Southern Territories" });
            countries.Add(new SelectListItem { Value = "Gabon", Text = "Gabon" });
            countries.Add(new SelectListItem { Value = "Gambia", Text = "Gambia" });
            countries.Add(new SelectListItem { Value = "Georgia", Text = "Georgia" });
            countries.Add(new SelectListItem { Value = "Germany", Text = "Germany" });
            countries.Add(new SelectListItem { Value = "Ghana", Text = "Ghana" });
            countries.Add(new SelectListItem { Value = "Gibraltar", Text = "Gibraltar" });
            countries.Add(new SelectListItem { Value = "Greece", Text = "Greece" });
            countries.Add(new SelectListItem { Value = "Greenland", Text = "Greenland" });
            countries.Add(new SelectListItem { Value = "Grenada", Text = "Grenada" });
            countries.Add(new SelectListItem { Value = "Guadeloupe", Text = "Guadeloupe" });
            countries.Add(new SelectListItem { Value = "Guam", Text = "Guam" });
            countries.Add(new SelectListItem { Value = "Guatemala", Text = "Guatemala" });
            countries.Add(new SelectListItem { Value = "Guernsey", Text = "Guernsey" });
            countries.Add(new SelectListItem { Value = "Guinea", Text = "Guinea" });
            countries.Add(new SelectListItem { Value = "Guinea-Bissau", Text = "Guinea-Bissau" });
            countries.Add(new SelectListItem { Value = "Guyana", Text = "Guyana" });
            countries.Add(new SelectListItem { Value = "Haiti", Text = "Haiti" });
            countries.Add(new SelectListItem { Value = "Heard Island and Mcdonald Islands", Text = "Heard Island and Mcdonald Islands" });
            countries.Add(new SelectListItem { Value = "Holy See (Vatican City State)", Text = "Holy See (Vatican City State)" });
            countries.Add(new SelectListItem { Value = "Honduras", Text = "Honduras" });
            countries.Add(new SelectListItem { Value = "Hong Kong", Text = "Hong Kong" });
            countries.Add(new SelectListItem { Value = "Hungary", Text = "Hungary" });
            countries.Add(new SelectListItem { Value = "Iceland", Text = "Iceland" });
            countries.Add(new SelectListItem { Value = "India", Text = "India" });
            countries.Add(new SelectListItem { Value = "Indonesia", Text = "Indonesia" });
            countries.Add(new SelectListItem { Value = "Iran, Islamic Republic of", Text = "Iran, Islamic Republic of" });
            countries.Add(new SelectListItem { Value = "Iraq", Text = "Iraq" });
            countries.Add(new SelectListItem { Value = "Ireland", Text = "Ireland" });
            countries.Add(new SelectListItem { Value = "Isle of Man", Text = "Isle of Man" });
            countries.Add(new SelectListItem { Value = "Israel", Text = "Israel" });
            countries.Add(new SelectListItem { Value = "Italy", Text = "Italy" });
            countries.Add(new SelectListItem { Value = "Jamaica", Text = "Jamaica" });
            countries.Add(new SelectListItem { Value = "Japan", Text = "Japan" });
            countries.Add(new SelectListItem { Value = "Jersey", Text = "Jersey" });
            countries.Add(new SelectListItem { Value = "Jordan", Text = "Jordan" });
            countries.Add(new SelectListItem { Value = "Kazakhstan", Text = "Kazakhstan" });
            countries.Add(new SelectListItem { Value = "Kenya", Text = "Kenya" });
            countries.Add(new SelectListItem { Value = "Kiribati", Text = "Kiribati" });
            countries.Add(new SelectListItem { Value = "Korea, Democratic People's Republic of", Text = "Korea, Democratic People's Republic of" });
            countries.Add(new SelectListItem { Value = "Korea, Republic of", Text = "Korea, Republic of" });
            countries.Add(new SelectListItem { Value = "Kosovo", Text = "Kosovo" });
            countries.Add(new SelectListItem { Value = "Kuwait", Text = "Kuwait" });
            countries.Add(new SelectListItem { Value = "Kyrgyzstan", Text = "Kyrgyzstan" });
            countries.Add(new SelectListItem { Value = "Lao People's Democratic Republic", Text = "Lao People's Democratic Republic" });
            countries.Add(new SelectListItem { Value = "Latvia", Text = "Latvia" });
            countries.Add(new SelectListItem { Value = "Lebanon", Text = "Lebanon" });
            countries.Add(new SelectListItem { Value = "Lesotho", Text = "Lesotho" });
            countries.Add(new SelectListItem { Value = "Liberia", Text = "Liberia" });
            countries.Add(new SelectListItem { Value = "Libyan Arab Jamahiriya", Text = "Libyan Arab Jamahiriya" });
            countries.Add(new SelectListItem { Value = "Liechtenstein", Text = "Liechtenstein" });
            countries.Add(new SelectListItem { Value = "Lithuania", Text = "Lithuania" });
            countries.Add(new SelectListItem { Value = "Luxembourg", Text = "Luxembourg" });
            countries.Add(new SelectListItem { Value = "Macao", Text = "Macao" });
            countries.Add(new SelectListItem { Value = "Macedonia, the Former Yugoslav Republic of", Text = "Macedonia, the Former Yugoslav Republic of" });
            countries.Add(new SelectListItem { Value = "Madagascar", Text = "Madagascar" });
            countries.Add(new SelectListItem { Value = "Malawi", Text = "Malawi" });
            countries.Add(new SelectListItem { Value = "Malaysia", Text = "Malaysia" });
            countries.Add(new SelectListItem { Value = "Maldives", Text = "Maldives" });
            countries.Add(new SelectListItem { Value = "Mali", Text = "Mali" });
            countries.Add(new SelectListItem { Value = "Malta", Text = "Malta" });
            countries.Add(new SelectListItem { Value = "Marshall Islands", Text = "Marshall Islands" });
            countries.Add(new SelectListItem { Value = "Martinique", Text = "Martinique" });
            countries.Add(new SelectListItem { Value = "Mauritania", Text = "Mauritania" });
            countries.Add(new SelectListItem { Value = "Mauritius", Text = "Mauritius" });
            countries.Add(new SelectListItem { Value = "Mayotte", Text = "Mayotte" });
            countries.Add(new SelectListItem { Value = "Mexico", Text = "Mexico" });
            countries.Add(new SelectListItem { Value = "Micronesia, Federated States of", Text = "Micronesia, Federated States of" });
            countries.Add(new SelectListItem { Value = "Moldova, Republic of", Text = "Moldova, Republic of" });
            countries.Add(new SelectListItem { Value = "Monaco", Text = "Monaco" });
            countries.Add(new SelectListItem { Value = "Mongolia", Text = "Mongolia" });
            countries.Add(new SelectListItem { Value = "Montenegro", Text = "Montenegro" });
            countries.Add(new SelectListItem { Value = "Montserrat", Text = "Montserrat" });
            countries.Add(new SelectListItem { Value = "Morocco", Text = "Morocco" });
            countries.Add(new SelectListItem { Value = "Mozambique", Text = "Mozambique" });
            countries.Add(new SelectListItem { Value = "Myanmar", Text = "Myanmar" });
            countries.Add(new SelectListItem { Value = "Namibia", Text = "Namibia" });
            countries.Add(new SelectListItem { Value = "Nauru", Text = "Nauru" });
            countries.Add(new SelectListItem { Value = "Nepal", Text = "Nepal" });
            countries.Add(new SelectListItem { Value = "Netherlands", Text = "Netherlands" });
            countries.Add(new SelectListItem { Value = "Netherlands Antilles", Text = "Netherlands Antilles" });
            countries.Add(new SelectListItem { Value = "New Caledonia", Text = "New Caledonia" });
            countries.Add(new SelectListItem { Value = "New Zealand", Text = "New Zealand" });
            countries.Add(new SelectListItem { Value = "Nicaragua", Text = "Nicaragua" });
            countries.Add(new SelectListItem { Value = "Niger", Text = "Niger" });
            countries.Add(new SelectListItem { Value = "Nigeria", Text = "Nigeria" });
            countries.Add(new SelectListItem { Value = "Niue", Text = "Niue" });
            countries.Add(new SelectListItem { Value = "Norfolk Island", Text = "Norfolk Island" });
            countries.Add(new SelectListItem { Value = "Northern Mariana Islands", Text = "Northern Mariana Islands" });
            countries.Add(new SelectListItem { Value = "Norway", Text = "Norway" });
            countries.Add(new SelectListItem { Value = "Oman", Text = "Oman" });
            countries.Add(new SelectListItem { Value = "Pakistan", Text = "Pakistan" });
            countries.Add(new SelectListItem { Value = "Palau", Text = "Palau" });
            countries.Add(new SelectListItem { Value = "Palestinian Territory, Occupied", Text = "Palestinian Territory, Occupied" });
            countries.Add(new SelectListItem { Value = "Panama", Text = "Panama" });
            countries.Add(new SelectListItem { Value = "Papua New Guinea", Text = "Papua New Guinea" });
            countries.Add(new SelectListItem { Value = "Paraguay", Text = "Paraguay" });
            countries.Add(new SelectListItem { Value = "Peru", Text = "Peru" });
            countries.Add(new SelectListItem { Value = "Philippines", Text = "Philippines" });
            countries.Add(new SelectListItem { Value = "Pitcairn", Text = "Pitcairn" });
            countries.Add(new SelectListItem { Value = "Poland", Text = "Poland" });
            countries.Add(new SelectListItem { Value = "Portugal", Text = "Portugal" });
            countries.Add(new SelectListItem { Value = "Puerto Rico", Text = "Puerto Rico" });
            countries.Add(new SelectListItem { Value = "Qatar", Text = "Qatar" });
            countries.Add(new SelectListItem { Value = "Reunion", Text = "Reunion" });
            countries.Add(new SelectListItem { Value = "Romania", Text = "Romania" });
            countries.Add(new SelectListItem { Value = "Russian Federation", Text = "Russian Federation" });
            countries.Add(new SelectListItem { Value = "Rwanda", Text = "Rwanda" });
            countries.Add(new SelectListItem { Value = "Saint Barthelemy", Text = "Saint Barthelemy" });
            countries.Add(new SelectListItem { Value = "Saint Helena", Text = "Saint Helena" });
            countries.Add(new SelectListItem { Value = "Saint Kitts and Nevis", Text = "Saint Kitts and Nevis" });
            countries.Add(new SelectListItem { Value = "Saint Lucia", Text = "Saint Lucia" });
            countries.Add(new SelectListItem { Value = "Saint Martin", Text = "Saint Martin" });
            countries.Add(new SelectListItem { Value = "Saint Pierre and Miquelon", Text = "Saint Pierre and Miquelon" });
            countries.Add(new SelectListItem { Value = "Saint Vincent and the Grenadines", Text = "Saint Vincent and the Grenadines" });
            countries.Add(new SelectListItem { Value = "Samoa", Text = "Samoa" });
            countries.Add(new SelectListItem { Value = "San Marino", Text = "San Marino" });
            countries.Add(new SelectListItem { Value = "Sao Tome and Principe", Text = "Sao Tome and Principe" });
            countries.Add(new SelectListItem { Value = "Saudi Arabia", Text = "Saudi Arabia" });
            countries.Add(new SelectListItem { Value = "Senegal", Text = "Senegal" });
            countries.Add(new SelectListItem { Value = "Serbia", Text = "Serbia" });
            countries.Add(new SelectListItem { Value = "Serbia and Montenegro", Text = "Serbia and Montenegro" });
            countries.Add(new SelectListItem { Value = "Seychelles", Text = "Seychelles" });
            countries.Add(new SelectListItem { Value = "Sierra Leone", Text = "Sierra Leone" });
            countries.Add(new SelectListItem { Value = "Singapore", Text = "Singapore" });
            countries.Add(new SelectListItem { Value = "Sint Maarten", Text = "Sint Maarten" });
            countries.Add(new SelectListItem { Value = "Slovakia", Text = "Slovakia" });
            countries.Add(new SelectListItem { Value = "Slovenia", Text = "Slovenia" });
            countries.Add(new SelectListItem { Value = "Solomon Islands", Text = "Solomon Islands" });
            countries.Add(new SelectListItem { Value = "Somalia", Text = "Somalia" });
            countries.Add(new SelectListItem { Value = "South Africa", Text = "South Africa" });
            countries.Add(new SelectListItem { Value = "South Georgia and the South Sandwich Islands", Text = "South Georgia and the South Sandwich Islands" });
            countries.Add(new SelectListItem { Value = "South Sudan", Text = "South Sudan" });
            countries.Add(new SelectListItem { Value = "Spain", Text = "Spain" });
            countries.Add(new SelectListItem { Value = "Sri Lanka", Text = "Sri Lanka" });
            countries.Add(new SelectListItem { Value = "Sudan", Text = "Sudan" });
            countries.Add(new SelectListItem { Value = "Suriname", Text = "Suriname" });
            countries.Add(new SelectListItem { Value = "Svalbard and Jan Mayen", Text = "Svalbard and Jan Mayen" });
            countries.Add(new SelectListItem { Value = "Swaziland", Text = "Swaziland" });
            countries.Add(new SelectListItem { Value = "Sweden", Text = "Sweden" });
            countries.Add(new SelectListItem { Value = "Switzerland", Text = "Switzerland" });
            countries.Add(new SelectListItem { Value = "Syrian Arab Republic", Text = "Syrian Arab Republic" });
            countries.Add(new SelectListItem { Value = "Taiwan, Province of China", Text = "Taiwan, Province of China" });
            countries.Add(new SelectListItem { Value = "Tajikistan", Text = "Tajikistan" });
            countries.Add(new SelectListItem { Value = "Tanzania, United Republic of", Text = "Tanzania, United Republic of" });
            countries.Add(new SelectListItem { Value = "Thailand", Text = "Thailand" });
            countries.Add(new SelectListItem { Value = "Timor-Leste", Text = "Timor-Leste" });
            countries.Add(new SelectListItem { Value = "Togo", Text = "Togo" });
            countries.Add(new SelectListItem { Value = "Tokelau", Text = "Tokelau" });
            countries.Add(new SelectListItem { Value = "Tonga", Text = "Tonga" });
            countries.Add(new SelectListItem { Value = "Trinidad and Tobago", Text = "Trinidad and Tobago" });
            countries.Add(new SelectListItem { Value = "Tunisia", Text = "Tunisia" });
            countries.Add(new SelectListItem { Value = "Turkey", Text = "Turkey" });
            countries.Add(new SelectListItem { Value = "Turkmenistan", Text = "Turkmenistan" });
            countries.Add(new SelectListItem { Value = "Turks and Caicos Islands", Text = "Turks and Caicos Islands" });
            countries.Add(new SelectListItem { Value = "Tuvalu", Text = "Tuvalu" });
            countries.Add(new SelectListItem { Value = "Uganda", Text = "Uganda" });
            countries.Add(new SelectListItem { Value = "Ukraine", Text = "Ukraine" });
            countries.Add(new SelectListItem { Value = "United Arab Emirates", Text = "United Arab Emirates" });
            countries.Add(new SelectListItem { Value = "United Kingdom", Text = "United Kingdom" });
            countries.Add(new SelectListItem { Value = "United States", Text = "United States" });
            countries.Add(new SelectListItem { Value = "United States Minor Outlying Islands", Text = "United States Minor Outlying Islands" });
            countries.Add(new SelectListItem { Value = "Uruguay", Text = "Uruguay" });
            countries.Add(new SelectListItem { Value = "Uzbekistan", Text = "Uzbekistan" });
            countries.Add(new SelectListItem { Value = "Vanuatu", Text = "Vanuatu" });
            countries.Add(new SelectListItem { Value = "Venezuela", Text = "Venezuela" });
            countries.Add(new SelectListItem { Value = "Viet Nam", Text = "Viet Nam" });
            countries.Add(new SelectListItem { Value = "Virgin Islands, British", Text = "Virgin Islands, British" });
            countries.Add(new SelectListItem { Value = "Virgin Islands, U.s.", Text = "Virgin Islands, U.s." });
            countries.Add(new SelectListItem { Value = "Wallis and Futuna", Text = "Wallis and Futuna" });
            countries.Add(new SelectListItem { Value = "Western Sahara", Text = "Western Sahara" });
            countries.Add(new SelectListItem { Value = "Yemen", Text = "Yemen" });
            countries.Add(new SelectListItem { Value = "Zambia", Text = "Zambia" });
            countries.Add(new SelectListItem { Value = "Zimbabwe", Text = "Zimbabwe" });
            return countries;
        }

    }
}
