
namespace StampinUpPlaywrightDemo.Tests.Regression.Addresses
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/GbnMqQ
    /// test validate the workflow of adding addresses as a first time user on a new account
    /// </summary>
    public class FirstTimeAddressToNewAccount : BaseTest
    {

        // from home screen after accoutn creation select account menu options button "Hello {userFirstName}"
        // and select addresses from the menu options

        // Add new address page is displayed with black fields

        // First / Last name

        // address

        // address line 2

        // city

        // state

        // zipcode

        // phone number

        // enter first and last name for user or someone else

        // Observe name fields retain the input data

        // ﻿enter address into the address field


        // ﻿upon user entering address numbers and street names auto address options are suggested


        // select a valid address option from the address suggestion picker


        // City state and Zipcode are auto populated with the correct data from the picker object


        // enter a phone number (10 digits) into the phone number field


        // phone field successfully populated with data input

        // leaving both default address check box's unchecked 
        // select save address button

        // form is submitted and is successful Default Shipping address section is populated

        // repeat steps for default mailing address using a different address

        // default mailing address is populated with the data that matchies the form that was submitted

        // repeat steps for updating mailing to match shipping

        // user is able to successfully update fields to match

        // create a new account and repeat steps this time checking both box's

        // upon form submittal user can see both addresses objects are displayed with the same data

        // create a new account and repeat steps this time only selecting default mailing check box

        // only mailing address or maybe shipping address is also populated

    }
}