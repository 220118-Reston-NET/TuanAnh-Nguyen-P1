namespace APIPortal.Consts
{
  public static class RouteConfigs
  {
    //AUTHENTICATION
    public const string Register = "Register";
    public const string Login = "Login";

    //ADMIN
    public const string Customers = "Customers";
    public const string Stores = "Stores";
    public const string GetProduct = "Products/{p_prodID}";
    public const string AddProduct = "Products";
    public const string UpdateProduct = "Products/{p_prodID}";
    public const string Products = "Products";
    public const string AddRoleToUser = "AddRoleToUser";

    //CUSTOMER
    public const string CustomerProfile = "Profile";
    public const string CustomerOrders = "Orders";
    public const string CustomerOrder = "Orders/{p_orderID}";

    //HOME
    public const string SearchProduct = "Products";
    public const string SearchStore = "Stores";

    //STORE
    public const string StoreProfile = "Profile";
    public const string Inventories = "Inventories";
    public const string Inventory = "Inventories/{p_prodID}";
    public const string StoreOrders = "Orders";
    public const string StoreOrder = "Orders/{p_orderID}";
    public const string AcceptOrder = "Orders/{p_orderID}/accept";
    public const string RejectOrder = "Orders/{p_orderID}/reject";
    public const string CompleteOrder = "Orders/{p_orderID}/complete";
    public const string Trackings = "Orders/{p_orderID}/Trackings";
    public const string Tracking = "Orders/{p_orderID}/Trackings/{p_trackingID}";
  }
}