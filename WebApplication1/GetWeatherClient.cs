using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class GetWeatherClient
    {

        public string run()
        {


            //    GetWeather.GlobalWeatherSoapClient getWeather = new GetWeather.GlobalWeatherSoapClient("GlobalWeatherSoap");

            //    GetWeatherHttpGet.GlobalWeatherSoapClient getWeather = new GetWeatherHttpGet.GlobalWeatherSoapClient("GlobalWeatherSoap");

            com.dneonline.www.Calculator calculator = new com.dneonline.www.Calculator();
         //   calculator.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials; 
            string rs = calculator.Add(2, 5).ToString();
            
                

             net.webservicex.www.GlobalWeather globalWeather = new net.webservicex.www.GlobalWeather();
           
            
        //    globalWeather.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            // globalWeather.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
            rs += " ###### " +  globalWeather.GetCitiesByCountry("France"); 

            ////  string rs =  getWeather.Endpoint.ToString(); 

            //// string rs = getWeather.GetCitiesByCountry("France");
            return rs; 
        }


    }
}