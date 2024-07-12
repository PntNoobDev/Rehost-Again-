using System.ComponentModel;

public class ControlProperties : INotifyPropertyChanged
{
    private string _controlName;
    private double _width;
    private double _height;
    private string _imagePath;

    public string ControlName
    {
        get { return _controlName; }
        set
        {
            _controlName = value;
            OnPropertyChanged(nameof(ControlName));
        }
    }

    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            OnPropertyChanged(nameof(Width));
        }
    }

    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            OnPropertyChanged(nameof(Height));
        }
    }

    public string ImagePath
    {
        get { return _imagePath; }
        set
        {
            _imagePath = value;
            OnPropertyChanged(nameof(ImagePath));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
