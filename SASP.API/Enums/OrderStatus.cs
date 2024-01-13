namespace SASP.API.Enums
{
    public static class OrderStatus
    {
       public static string ConvertStatus(string inStatus)
        {
            switch(inStatus)
            {
                case nameof(OrderStatusEnum.InProcessing): 
                    return "В процессе";
                case nameof(OrderStatusEnum.OnMyWay): 
                    return "В пути";
                case nameof(OrderStatusEnum.Delivered):
                    return "Доставлено";
                default: 
                    return null;
            }
        }

        public static bool IsStatus(string status)
        {
            var statusList = Enum.GetValues(typeof(OrderStatusEnum)).Cast<OrderStatusEnum>();

            foreach (var item in statusList)
            {
                if (status == item.ToString()) return true;
            }

            return false;
        }
    }
    public enum OrderStatusEnum
    {
        InProcessing = 1,
        OnMyWay = 2,
        Delivered = 3,
    }
}
