using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.IO;
using Newtonsoft.Json;

namespace Diaygeia
{
    class BatchGet
    {

        string baseUrl;
        
        string organizationUid = "100015990"; // Φορέας
      //  string decisionType = "ΑΝΑΛΗΨΗ ΥΠΟΧΡΕΩΣΗΣ"; 
        // string textEntryNumbers = "36996,36995,36997"; // Αρ. Καταχώρησης 
       
       // string textBudgetType = "Τακτικός Προϋπολογισμός";  // Τύπος ??? (Κατηγορία) Προυπολογισμού
       // string textBudgetKind = "";//Είδος Προϋπολογισμου
       // string ada = "";
      //  string status = "PUBLISHED";
      //  string submissionTimestamp = "";
      //  string protocolNumber = "";
      //  string issueDate = "";
        string from_issue_date = "";
        string to_issue_date = "";
        bool productionMode = true;
        string qry = "search/advanced.json?q=organizationUid:\"100015990\" AND decisionType:\"ΑΝΑΛΗΨΗ ΥΠΟΧΡΕΩΣΗΣ\" AND textBudgetType:\"Τακτικός Προϋπολογισμός\"";
        string inputFileName = "input.txt";
        string outputFileName = "";
        string separator = ";"; 
        // q=organizationUid:"100015990" AND decisionType:"ΑΝΑΛΗΨΗ ΥΠΟΧΡΕΩΣΗΣ" AND textBudgetType:"Τακτικός Προϋπολογισμός" AND textEntryNumber:"36995" AND (submissionTimestamp:[DT(2019-03-01T00:00:00) TO DT(2019-05-01T23:59:59)])
        // amount // Αξία ?? (Συνολικό Ποσό)


//        ada
//status
//issueDate
//protocolNumber
//amountWithVAT":{"amount":5488.77,"currency":"EUR"}
// Hm Ekdosis ????? 
        public void run()

        {
            // https://diavgeia.gov.gr/api/help
            // https://test3.diavgeia.gov.gr/luminapi/opendata/search/terms.json
            // https://diavgeia.gov.gr/opendata
            init();
            pump();
          
        }

        

        struct structResponse
        {
            public structDecision[] decisions;
            public structInfo info; 
        }

        struct structDecision
        {
            public string protocolNumber;
            public string ada;
            public long issueDate;
            public structExtraFieldValues extraFieldValues;
            public long submissionTimestamp;
            public string status;
            public string url;
            public string[] signerIds;
            public string[] unitIds;
            public string[] thematicCategoryIds; 
        }

        struct structInfo {
            public string query;
            public int page; 

            }

        struct structExtraFieldValues
        {
            public string entryNumber;
            public string recalledExpenseDecision; 
            public structAmountWithVAT amountWithVAT;
          
        }

        struct structAmountWithVAT
        {
            public double amount;
            public string currency; 
        }



        private void init()
        {
            inputFileName = "input.txt";
            DateTime now = System.DateTime.Now; 
            outputFileName = "output_" + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".csv"; 
            baseUrl = productionMode ? HelpUtils.return___PRODUCTION___DiaygeiaAPIUrl() : HelpUtils.returnTESTDiaygeiaAPIUrl();
            Console.Write("Διαχωριστικό τιμών στις γραμμές (;) ή (,): ");
            separator = Console.ReadLine();
            while (from_issue_date.Trim().Equals(""))
            {
                Console.Write("Ημερομηνία Ανάρτησης Από: YYYY-MM-DD : ");
                from_issue_date = Console.ReadLine();
                try
                {
                    string[] parts = from_issue_date.Split('-');
                    DateTime temp = new DateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2])); 
                }
                catch (Exception e)
                {
                    Console.WriteLine("Λάθος format Ημερομηνίας");
                    from_issue_date = ""; 
                }
            }

           
            while (to_issue_date.Trim().Equals(""))
            {
                Console.Write("Ημερομηνία Ανάρτησης Έως: YYYY-MM-DD : ");
                to_issue_date = Console.ReadLine();
                try
                {
                    string[] parts = to_issue_date.Split('-');
                    DateTime temp = new DateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Λάθος format Ημερομηνίας");
                    to_issue_date = "";
                }
            }

            string issueterm = "(submissionTimestamp:[DT(" + from_issue_date + "T00:00:00) TO DT(" + to_issue_date + "T23:59:59)])";
            qry += "AND " + issueterm;
        }




        public string pump()
        {
          
            if (from_issue_date.Equals("") && to_issue_date.Equals(""))
            { return "ERROR: Πρέπει να συμπληρώσετε τουλάχιστον μία ημερομηνία"; }
            if (from_issue_date.Equals("")) { from_issue_date = to_issue_date; }
            if (to_issue_date.Equals("")) { to_issue_date = from_issue_date; }

            string fullRequestUrl = baseUrl + qry;
            string encoded = productionMode ? HelpUtils.return___PRODUCTION___Authorization() : HelpUtils.returnTESTAuthorization();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var content = string.Empty;
            var lines = File.ReadLines("input.txt");
            using (StreamWriter sw = new StreamWriter(File.Open(outputFileName, FileMode.Create), Encoding.UTF8))
            {
                string titles = "ΑΡ ΚΑΤΑΧ. (ΠΗΓΑΙΟΣ)" + separator + "ΑΡ ΚΑΤΑΧ. (ΔΙΑΥΓΕΙΑ)" + separator + " ΑΔΑ " + separator + " STATUS " + separator + " ΗΜ. ΑΝΑΡΤΗΣΗΣ " + separator + " ΑΡ. ΠΡΩΤ. " + separator + " ΗΜ. ΕΚΔΟΣΗΣ" + separator + " ΠΟΣΟ " + separator + "ΕΙΝΑΙ ΑΝΑΚΛΗΣΗ" + separator + "ΥΠΟΓΡΑΦΩΝ" + separator + "UNIT" + separator + "ΘΕΜ. ΚΑΤΗΓ." ;
                sw.WriteLine(titles);
                foreach (string textEntryNumber in lines)
                {
                    if (!textEntryNumber.Trim().Equals(""))
                    {
                        Console.WriteLine("-------------------");
                        string q = fullRequestUrl + "AND textEntryNumber:\"" + textEntryNumber + "\"";
                        //   Console.WriteLine(q); 
                      

                        
                        int retries = 0;
                        bool success = false;
                        while (!success)
                        {
                            var request = WebRequest.Create(q);

                            // proxy start 
                            //     request.Credentials = CredentialCache.DefaultCredentials;
                            IWebProxy proxy = request.Proxy;
                            if (proxy != null)
                            {
                                string proxyuri = proxy.GetProxy(request.RequestUri).ToString();
                                request.UseDefaultCredentials = true;
                                request.Proxy = new WebProxy(proxyuri, false);
                                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            }
                            // proxy end 
                            request.Headers.Add("Authorization", "Basic " + encoded);
                            request.Method = "GET";
                            try
                            {
                                System.Threading.Thread.Sleep(100*retries);
                                using (var response = (HttpWebResponse)request.GetResponse())
                                {
                                    using (var stream = response.GetResponseStream())
                                    {


                                        using (var sr = new StreamReader(stream))
                                        {
                                            bool found = false;
                                            try
                                            {
                                                content = sr.ReadToEnd();
                                                structResponse json = JsonConvert.DeserializeObject<structResponse>(content);
                                                
                                                foreach (structDecision decision in json.decisions)
                                                {
                                                    found = true;
                                                    string mytsIssueStr = decision.issueDate.ToString();
                                                    try
                                                    {
                                                        DateTime mytsIssue = (new DateTime(1970, 1, 1, 0, 0, 0)).AddMilliseconds(decision.issueDate);
                                                        mytsIssueStr = mytsIssue.Day + "/" + mytsIssue.Month + "/" + mytsIssue.Year;
                                                    }
                                                    catch { };
                                                    string mytsSubmissionStr = decision.submissionTimestamp.ToString();
                                                    try
                                                    {
                                                        DateTime mytsSubmission = (new DateTime(1970, 1, 1, 0, 0, 0)).AddMilliseconds(decision.submissionTimestamp);
                                                        mytsSubmissionStr = mytsSubmission.Day + "/" + mytsSubmission.Month + "/" + mytsSubmission.Year;
                                                    }
                                                    catch { };


                                                    string rs = mystr(textEntryNumber) + separator + mystr(decision.extraFieldValues.entryNumber) + separator + mystr(decision.ada) + separator;
                                                    rs += mystr(decision.status) + separator + mystr(mytsSubmissionStr) + separator + mystr(decision.protocolNumber) + separator;
                                                    rs += mystr(mytsIssueStr) + separator + mystr(decision.extraFieldValues.amountWithVAT.amount.ToString()) + separator;
                                                    rs += (decision.extraFieldValues.recalledExpenseDecision.Equals("true", StringComparison.InvariantCultureIgnoreCase) ? "ΝΑΙ" : "") + separator;
                                                    rs += String.Join("-", decision.signerIds) + separator;
                                                    rs += String.Join("-", decision.unitIds) + separator;
                                                    rs += String.Join("-", decision.thematicCategoryIds) + separator;
                                                    sw.WriteLine(rs);
                                                    Console.WriteLine(rs);
                                                }
                                            }
                                            catch (Exception e) { Console.WriteLine("wrong output for: " + textEntryNumber + " will be registered as N/A"); }
                                            if (!found)
                                            {
                                                sw.WriteLine(textEntryNumber + " # N/A");
                                                Console.WriteLine(textEntryNumber + " # N/A");
                                            }
                                        }
                                    }
                                }
                                retries = 0;
                                success = true; 
                            }
                            catch (Exception e) { retries++;  Console.WriteLine(e.Message + "error retrying"); }
                        }
                    }
                    //    Console.WriteLine(content);
                    content = string.Empty;
                }
                sw.Close();
            }
            

            return content;
        }


        private string mystr(string src)
        {
            return src.Replace(separator, (separator.Equals(";") ? "," : ";")); 
        }

    }
}