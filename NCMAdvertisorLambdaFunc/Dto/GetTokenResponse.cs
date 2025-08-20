namespace NCMAdvertisorLambdaFunc.Dto
{
    public class GetTokenResponse
    {
        public string apiKey { get; set; }
        public ServiceDetails serviceDetails { get; set; }
        public string[] services { get; set; }
        public string token { get; set; }
    }

    public class ServiceDetails
    {
        public string DATAINGESTOR { get; set; }
        public string TARGET { get; set; }
        public string CREATIVES { get; set; }
        public string UNIFIEDDATALOADER { get; set; }
        public string EXPORT { get; set; }
        public string ANALYTICS { get; set; }
        public string UPC { get; set; }
        public string MAYISERVICE { get; set; }
        public string UNIFIEDPLANNER { get; set; }
        public string DELIVERY { get; set; }
        public string ORGANIZATION { get; set; }
        public string NOTES { get; set; }
        public string PSENTITY { get; set; }
        public string MDM_2 { get; set; }
        public string FINANCE { get; set; }
        public string DASHBOARDIM { get; set; }
        public string DASHBOARD { get; set; }
        public string MDM { get; set; }
        public string ATTACHMENTS { get; set; }
        public string ORDERS { get; set; }
        public string UM { get; set; }
        public string INGRESS { get; set; }
        public string IMCONTROLLER { get; set; }
        public string PLANNERANALYTICS { get; set; }
        public string UNISON { get; set; }
        public string IMEGRESS { get; set; }
        public string NOTIFICATIONS { get; set; }
        public string IMSCHEDULER { get; set; }
        public string APIDOCS { get; set; }
        public string RATECARD { get; set; }
        public string TEMPLATESERVICE { get; set; }
        public string WORKFLOWSERVER { get; set; }
        public string CUSTOMFIELDS { get; set; }
        public string USERSETTINGS { get; set; }
        public string PRODUCTS { get; set; }
        public string CRM { get; set; }
    }
}

