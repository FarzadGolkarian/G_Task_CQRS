namespace G_Task.Common.Responses
{
    public record BaseCommandResponse
    {
        public long ID { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
