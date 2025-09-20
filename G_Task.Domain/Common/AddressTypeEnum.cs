using System.ComponentModel;

namespace G_Task.Domain.Common
{
    /// <summary>
    /// نوع آدرس
    /// </summary>
    public enum AddressTypeEnum
    {
        [Description("خانه")]
        Home = 2,

        [Description("محل کار")]
        Office = 4,
    }
}
