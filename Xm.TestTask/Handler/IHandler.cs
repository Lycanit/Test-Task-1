public interface IHandler
{
    public string DataType { get; }
    public void Handle(byte[] ar);
}