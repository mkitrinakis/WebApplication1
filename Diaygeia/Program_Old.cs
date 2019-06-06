using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.IO;
using Newtonsoft.Json;
using Diaygeia;


namespace Diaygeia
{
    class Program_Old
    {
        static void Main(string[] args)
        {
            bool productionMode = false;
            productionMode = args[0].Equals("PRODUCTION");
            string result = "";
            if (productionMode)
            {
                Console.WriteLine("ATTENTION IS PRODUCTION MODE. Press  Ctrl+C to abort , any other key to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(" -- TEST -- ");
            }

            foreach (string oper in args)
            {
                if (oper.ToUpper().Contains("GETRELEASES"))
                {
                    Console.WriteLine("--- GET RELEASES --- ");
                    result = GetReleases(productionMode);
                }

                if (oper.ToUpper().Contains("REMOVEREQUEST"))
                {
                    Console.WriteLine("--- REMOVE REQUEST --- ");
                    string ada = "";
                    foreach (string adaOper in args)
                    {
                        if (adaOper.ToUpper().Contains("ADA=")) { ada = adaOper.Split('=')[1]; break; }
                    }
                    if (ada.Trim().Equals("")) { Console.WriteLine("Cannot find ADA, there is no argument ADA=XXX"); }
                    else
                    {
                        Console.WriteLine("ADA:" + ada);
                        RemoveRequest(productionMode, ada);
                    }
                }
            }

            Console.WriteLine(result);
        }

        static string GetReleases(bool productionMode)
        {

            var request = productionMode ? (HttpWebRequest)WebRequest.Create(HelpUtils.return___PRODUCTION___DiaygeiaAPIUrl() + "types") : (HttpWebRequest)WebRequest.Create(HelpUtils.returnTESTDiaygeiaAPIUrl() + "types"); ;
            string encoded = productionMode ? HelpUtils.return___PRODUCTION___Authorization() : HelpUtils.returnTESTAuthorization();
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Method = "GET";
            // request.UserAgent = RequestConstants.UserAgentValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }




        struct StructRemove
        {
            public string ada;
            public string comment;
        }

        // "6ΨΑ04653ΠΣ-ΥΘΕ";

        static string RemoveRequest(bool productionMode, string ada)
        {
            var request = productionMode ? (HttpWebRequest)WebRequest.Create(HelpUtils.return___PRODUCTION___DiaygeiaAPIUrl() + "decisions/requests/revocations") : (HttpWebRequest)WebRequest.Create(HelpUtils.returnTESTDiaygeiaAPIUrl() + "decisions/requests/revocations");
            string encoded = productionMode ? HelpUtils.return___PRODUCTION___Authorization() : HelpUtils.returnTESTAuthorization();
            request.Headers.Add("Authorization", "Basic " + encoded);
            StructRemove structRemove = new StructRemove();
            structRemove.ada = ada;
            structRemove.comment = "Διπλοκαταχώρηση";
            //var postData = "ada=hello";
            //postData += "&thing2=world";
            string postData = JsonConvert.SerializeObject(structRemove);
            var data = Encoding.ASCII.GetBytes(postData.ToCharArray());
            request.Method = "POST";
            // request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }




        class toUpload
        {

            public string protocolNumber;
            public string subject;
            public string decisionTypeID;
            public string extraFieldValues;
            public string issueDate;
            public string organizationId;
            public string[] signerIds;
            public string[] unitIds;
            public string[] thematicCategoryIds;
            public string decisionDocumentBase64 = "[BASE-64 ENCODED FILE]";
            public attachmentStructure attachments;
        }

        struct attachmentStructure
        {
            public string description;
            public string filename;
            // public string mimeType = "application/pdf";
            public string mimeType;
            public string contentBase64;
        }




        /* 
         * 
         * 
         * ada 	Αριθμός Διαδικτυακής Ανάρτησης
protocolNumber 	Αριθμός Πρωτοκόλλου
subject 	Θέμα πράξης
issueDate 	Ημερομηνία έκδοσης σε μορφή Unix Timestamp (Milliseconds from epoch)
decisionTypeId 	Κωδικός του τύπου πράξης (Για λεπτομέρειες σχετικά με τις πληροφορίες των τύπων πράξεων δείτε τη σχετική ενότητα)
organizationId 	Κωδικός φορέα. (Για λεπτομέρειες σχετικά με τις πληροφορίες των φορέων δείτε τη σχετική ενότητα)
unitIds 	Λίστα κωδικών που αντιστοιχούν στις μονάδες οι οποίες εμπλέκονται στην έκδοσης της συγκεκριμένης πράξης (Για λεπτομέρειες σχετικά με τις πληροφορίες των οργανωτικών μονάδων δείτε τη σχετική ενότητα)
signerIds 	Λίστα κωδικών που αντιστοιχούν στους τελικούς υπογράφοντες της πράξης (Για λεπτομέρειες σχετικά με τις πληροφορίες των υπογραφόντων δείτε τη σχετική ενότητα)
thematicCategoryIds 	Λίστα κωδικών που αντιστοιχούν στις θεματικές κατηγορίες της πράξης (Για λεπτομέρειες σχετικά με τις πληροφορίες των θεματικών κατηγοριών δείτε τη σχετική ενότητα)
privateData 	Ένδειξη για το αν η πράξη περιλαμβάνει προσωπικά δεδομένα
submissionTimestamp 	Ημερομηνία και ώρα τελευταίας τροποποίησης πράξης σε μορφή Unix Timestamp (Milliseconds from epoch)
status 	Ένδειξη για την κατάσταση της πράξης. Η τιμή αυτού του πεδίου για τις δημόσια προσβάσιμες πράξεις μπορεί μπορεί να έχει τις τιμές PUBLISHED (Αναρτημένη και σε ισχύ), PENDING_REVOCATION (Αναρτημένη και σε ισχύ, έχει υποβληθεί αίτημα ανάκλησής της), REVOKED (Ανακληθείσα). Επίσης, σε πράξεις που έχουν υποβληθεί αλλά δεν τους έχει αποδοθεί ΑΔΑ, η τιμή αυτού του πεδίου θα είναι SUBMITTED
versionId 	Αριθμός έκδοσης. Ο αριθμός αυτός αλλάζει σε περίπτωση που γίνει διόρθωση των μεταδεδομένων μιας αναρτημένης πράξης, ή όταν αλλάξει η κατάστασή της.
documentChecksum 	Τιμή SHA-1 του εγγράφου της πράξης
attachments 	Λίστα περιγραφών των συνημμένων εγγράφων της πράξης. Κάθε τιμή της λίστας περιλαμβάνει τις εξής πληροφορίες:

    id: Μοναδικό αναγνωριστικό του εγγράφου
    description: Περιγραφή του συνημμένου
    filename: Όνομα αρχείου
    mimeType: Τύπος αρχείου
    checksum: Τιμή SHA-1 του εγγράφου

extraFieldValues 	Ενότητα ειδικών πεδίων. Το περιεχόμενο αυτής της ενότητας είναι ανάλογο του τύπου της πράξης. Για παράδειγμα, για πράξεις του τύπου "Γ.3.5 ΥΠΗΡΕΣΙΑΚΗ ΜΕΤΑΒΟΛΗ" ορίζονται τα εξής πεδία:

    eidosYpMetavolis: Είδος υπηρεσιακής μεταβολής
    relatedDecisions: Λίστα σχετικών πράξεων

correctedVersionId 	
        static void upload()
        {
            {
                "protocolNumber": "Α.100",
    "subject": "Περίληψη διακήρυξης",
    "decisionTypeId": "Δ.2.1",
    "extraFieldValues": {
                    "cpv": [
                        "42661000-7"
        ],
        "contestProgressType": "Πρόχειρος",
        "manifestSelectionCriterion": "Χαμηλότερη Τιμή",
        "manifestContractType": "Έργα",
        "orgBudgetCode": "Τακτικός Προϋπολογισμός",
        "estimatedAmount": {
            "amount": 500,
            "currency": "EUR"
        }
    },
    "issueDate": "2014-06-07T00:00:00.000Z",
    "organizationId": "10599",
    "signerIds": [
        "10911"
    ],
    "unitIds": [
        "10602"
    ],
    "thematicCategoryIds": [
        "20"
    ],
    "decisionDocumentBase64": "[BASE-64 ENCODED FILE]",
    "attachments": {
        "attach": [
            {
                "description": "Συνοδευτικό έγγραφο",
                "filename": "attachment1.pdf",
                "mimeType": "application/pdf",
                "contentBase64": "[BASE-64 ENCODED FILE]"
            }
        ]
    }
}
        }
        */
    }
}
