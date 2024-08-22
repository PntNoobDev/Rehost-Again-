using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rehost_Again_
{
    public class HttpRequestViewModel : INotifyPropertyChanged
    {
        private string _endpoint;
        private string _method;
        private string _body;
        private string _clientCertificate;
        private string _clientCertificatePassword;
        private bool _enableSSLVerification;
        private string _timeOut;
        private AccpectMode _acceptResponseAs;

        // Endpoint
        public string Endpoint
        {
            get => _endpoint;
            set
            {
                if (_endpoint != value)
                {
                    _endpoint = value;
                    OnPropertyChanged();
                }
            }
        }

        // Method
        public string Method
        {
            get => _method;
            set
            {
                if (_method != value)
                {
                    _method = value;
                    OnPropertyChanged();
                }
            }
        }

        // Body
        public string Body
        {
            get => _body;
            set
            {
                if (_body != value)
                {
                    _body = value;
                    OnPropertyChanged();
                }
            }
        }

        // Client Certificate
        public string ClientCertificate
        {
            get => _clientCertificate;
            set
            {
                if (_clientCertificate != value)
                {
                    _clientCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        // Client Certificate Password
        public string ClientCertificatePassword
        {
            get => _clientCertificatePassword;
            set
            {
                if (_clientCertificatePassword != value)
                {
                    _clientCertificatePassword = value;
                    OnPropertyChanged();
                }
            }
        }

        // Enable SSL Verification
        public bool EnableSSLVerification
        {
            get => _enableSSLVerification;
            set
            {
                if (_enableSSLVerification != value)
                {
                    _enableSSLVerification = value;
                    OnPropertyChanged();
                }
            }
        }

        // Timeout
        public string TimeOut
        {
            get => _timeOut;
            set
            {
                if (_timeOut != value)
                {
                    _timeOut = value;
                    OnPropertyChanged();
                }
            }
        }

        // Accept Response As
        public AccpectMode AcceptResponseAs
        {
            get => _acceptResponseAs;
            set
            {
                if (_acceptResponseAs != value)
                {
                    _acceptResponseAs = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
