namespace Glasswall.Redact.Api
{
    public interface IJsonToXmlConverter<T>
    {
        string Convert(string json);
    }
}
