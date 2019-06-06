using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WebApplication1
{
    [ServiceContract(Namespace = "")]


    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WCFHello
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        //  [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public string DoWork()
        {
            // Add your operation implementation here
        
            return "Hello my F*** World"; 
        }

        public struct Employee
        {
            public string Name;
            public string Surname;
            public int Age; 
        }

        [OperationContract]
     //   [WebInvoke(Method = "GET",
     //ResponseFormat = WebMessageFormat.Json)]

        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        public List<Employee> AllEmployees()
        {
            Employee empl1 = new Employee() { Name = "Nansy", Surname = "Karkoyli", Age = 45 };

            Employee empl2 = new Employee() { Name = "Tasoula", Surname = "Oikonomou\"s ", Age = 43 };

            Employee empl3 = new Employee() { Name = "Markos", Surname = "Kitrinakis", Age = 45 };

            List<Employee> empls = new List<Employee>();
            empls.Add(empl1); empls.Add(empl2);
            return empls; 
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
