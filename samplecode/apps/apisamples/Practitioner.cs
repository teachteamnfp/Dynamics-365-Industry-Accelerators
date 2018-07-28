﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Practitioner
    {
        #region Class Level Members

        /// <summary>
        /// Stores the organization service proxy.
        /// </summary>
        private OrganizationServiceProxy _serviceProxy;

        #endregion Class Level Members

        #region How To Sample Code
        /// <summary>
        /// Create and configure the organization service proxy.
        /// Initiate the method to create any data that this sample requires.
        /// Create a practitioner.
        /// </summary>
        /// <param name="organizationUrl">Contains organization service url</param>
        /// <param name="homeRealmUri">Contains home real Uri</param>
        /// <param name="clientCredentials">Contains client credentials</param>
        /// <param name="deviceCredentials">Contains device credentials</param>

        public void Run(string organizationUrl, string homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
        {
            try
            {
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(new Uri(organizationUrl), new Uri(homeRealmUri), clientCredentials, deviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    Entity practitioner = new Entity("contact");

                    //Setting contact type as practioner
                    practitioner["msemr_contacttype"] = new OptionSetValue(935000001);
                    practitioner["firstname"] = "John";
                    practitioner["lastname"] = "Smith";

                    Guid GeneralPractioner = Contact.GetContactId(_serviceProxy, "Emily Williams", 935000001);
                    if (GeneralPractioner != Guid.Empty)
                    {
                        practitioner["msemr_generalpractioner"] = new EntityReference("contact", GeneralPractioner);
                    }

                    practitioner["emailaddress1"] = "john.smith@hotmail.com";
                    practitioner["telephone2"] = "1-888-751-4083";
                    practitioner["mobilephone"] = "555-555-1234";
                    practitioner["telephone1"] = "653-123-1234";
                    practitioner["preferredcontactmethodcode"] = new OptionSetValue(3); //Phone
                    practitioner["gendercode"] = new OptionSetValue(1); //Male
                    practitioner["familystatuscode"] = new OptionSetValue(2); //Married
                    practitioner["anniversary"] = DateTime.Now.AddYears(-20);
                    practitioner["spousesname"] = "Crista Smith";
                    practitioner["address1_line1"] = "3386";
                    practitioner["address1_line2"] = "Gateway Avenue";
                    practitioner["address1_city"] = "Lancaster";
                    practitioner["address1_stateorprovince"] = "CA";
                    practitioner["address1_postalcode"] = "93534";
                    practitioner["address1_country"] = "US";
                    
                    practitioner["birthdate"] = DateTime.Now.AddYears(-50);

                    Guid PractionerId = _serviceProxy.Create(practitioner);

                    // Verify that the record has been created.
                    if (PractionerId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", PractionerId);
                    }
                }
            }
            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        #endregion How To Sample Code

    }
}
