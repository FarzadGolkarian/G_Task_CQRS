namespace G_Task.Domain.Common
{
    public interface IAuditEntity
    {
        ///<summary>
        ///تاریخ آیجاد
        /// </summary>
         DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// ایجاد کننده
        /// </summary>
        string CreateBy { get; set; }
    }
}
