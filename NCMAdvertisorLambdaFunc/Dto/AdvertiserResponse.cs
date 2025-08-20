namespace NCMAdvertisorLambdaFunc.Dto
{
    public class AdvertiserResponse
    {
        public string id { get; set; }
        public string mdmId { get; set; }
        public bool active { get; set; }
        public string createdBy { get; set; }
        public string lastModifiedBy { get; set; }
        public string name { get; set; }
        public double discountRate { get; set; }
        public string[] platform { get; set; }
        public string[] orderType { get; set; }
        public string template { get; set; }
        public int aosAccountId { get; set; }
        public string parentAdvertiserId { get; set; }
        public bool incomplete { get; set; }
        public bool directBuy { get; set; }
        public string createdDate { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
