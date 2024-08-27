using System;
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
    private bool enableSSLVerification = false; 

    public string Endpoint
    {
        get => endpoint;
        set
        {
            if (endpoint != value)
            {
                endpoint = value;
                OnPropertyChanged(nameof(Endpoint));
            }
        }
    }

    public string Preview
    {
        get => preview;
        set
        {
            if (preview != value)
            {
                preview = value;
                OnPropertyChanged(nameof(Preview));
            }
        }
    }

    public string TimeOut
    {
        get => timeOut;
        set
        {
            if (timeOut != value)
            {
                timeOut = value;
                OnPropertyChanged(nameof(TimeOut));
            }
        }
    }

    public string ClientCertificate
    {
        get => clientCertificate;
        set
        {
            if (clientCertificate != value)
            {
                clientCertificate = value;
                OnPropertyChanged(nameof(ClientCertificate));
            }
        }
    }

    public string ClientCertificatePassword
    {
        get => clientCertificatePassword;
        set
        {
            if (clientCertificatePassword != value)
            {
                clientCertificatePassword = value;
                OnPropertyChanged(nameof(ClientCertificatePassword));
            }
        }
    }

    public string RequestMethod
    {
        get => requestMethod;
        set
        {
            if (requestMethod != value)
            {
                requestMethod = value;
                OnPropertyChanged(nameof(RequestMethod));
            }
        }
    }

    public string AcceptResponseAs
    {
        get => acceptResponseAs;
        set
        {
            if (acceptResponseAs != value)
            {
                acceptResponseAs = value;
                OnPropertyChanged(nameof(AcceptResponseAs));
            }
        }
    }

    public bool EnableSSLVerification
    {
        get => enableSSLVerification;
        set
        {
            if (enableSSLVerification != value)
            {
                enableSSLVerification = value;
                OnPropertyChanged(nameof(EnableSSLVerification));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
