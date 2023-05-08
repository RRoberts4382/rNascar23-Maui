namespace rNascar23Multi.Models
{
    public class DriverValueModel : NotifyModel, IEquatable<DriverValueModel>
    {
        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private string _driver;
        public string Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
                OnPropertyChanged(nameof(Driver));
            }
        }

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        #region IEquatable

        public bool Equals(DriverValueModel other)
        {
            if (other is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return other.Position == Position && other.Driver == Driver && other.Value == Value;
        }

        public override int GetHashCode() => (Position, Driver, Value).GetHashCode();

        public static bool operator ==(DriverValueModel lhs, DriverValueModel rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(DriverValueModel lhs, DriverValueModel rhs) => !(lhs == rhs);

        public override bool Equals(object obj)
        {
            return Equals(obj as DriverValueModel);
        }
        #endregion
    }
}
