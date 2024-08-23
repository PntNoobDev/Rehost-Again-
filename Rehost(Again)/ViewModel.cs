using System.ComponentModel;

public class HttpRequestViewModel : INotifyPropertyChanged
{
    private string endpoint;
    private string preview;
    private string timeOut;
    private string clientCertificate;
    private string clientCertificatePassword;
    private string requestMethod;
    private string acceptResponseAs;

    public string Endpoint
    {
        get => endpoint;
        set { endpoint = value; OnPropertyChanged(nameof(Endpoint)); }
    }

    public string Preview
    {
        get => preview;
        set { preview = value; OnPropertyChanged(nameof(Preview)); }
    }

    public string TimeOut
    {
        get => timeOut;
        set { timeOut = value; OnPropertyChanged(nameof(TimeOut)); }
    }

    public string ClientCertificate
    {
        get => clientCertificate;
        set { clientCertificate = value; OnPropertyChanged(nameof(ClientCertificate)); }
    }

    public string ClientCertificatePassword
    {
        get => clientCertificatePassword;
        set { clientCertificatePassword = value; OnPropertyChanged(nameof(ClientCertificatePassword)); }
    }

    public string RequestMethod
    {
        get => requestMethod;
        set { requestMethod = value; OnPropertyChanged(nameof(RequestMethod)); }
    }

    public string AcceptResponseAs
    {
        get => acceptResponseAs;
        set { acceptResponseAs = value; OnPropertyChanged(nameof(AcceptResponseAs)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
