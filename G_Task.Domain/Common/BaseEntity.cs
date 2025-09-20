namespace G_Task.Domain.Common
{
    public abstract class BaseEntity
    {
        public long ID { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
