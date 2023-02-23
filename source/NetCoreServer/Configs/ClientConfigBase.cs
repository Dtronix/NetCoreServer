namespace NetCoreServer.Configs;
internal abstract class ClientConfigBase : ConfigBase
{
    internal ClientEvents Events;


    protected ClientConfigBase(int sendReceiveBufferSize) 
        : base(sendReceiveBufferSize, 3)
    {
    }
}
