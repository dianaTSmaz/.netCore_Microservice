namespace CouponsWeb.Utility
{
    public class StartingDetails 
    {
        //Variable to inject the URL in the main program
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";

        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType

        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
  
}
