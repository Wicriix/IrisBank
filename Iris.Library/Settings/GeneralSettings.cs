namespace Iris.Commons.Settings
{
    public class GeneralSettings
    {
        public string ApiName { get; set; }
        public short MajorVersion { get; set; }
        public short MinorVersion { get; set; }
        public string Environment { get; set; }
        public string SecretKey { get; set; }
    }
}