namespace Api_S3_AWS.Domain
{
    public class Archive(string title, string path)
    {
        public string Title { get; set; } = title;
        public string Path { get; set; } = path;

    }
}
