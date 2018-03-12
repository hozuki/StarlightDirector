using System.ComponentModel;
using System.Reflection;
using Caliburn.Micro;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Common {
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChangedEx {

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void NotifyOfPropertyChange([NotNull] string propertyName) {
            if (!IsNotifying) {
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh() {
            if (!IsNotifying) {
                return;
            }

            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties) {
                NotifyOfPropertyChange(property.Name);
            }
        }

        public bool IsNotifying { get; set; } = true;

    }
}
